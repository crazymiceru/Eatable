using UnityEngine;

namespace Eatable
{
    public class LivesController : ControllerBasic
    {
        private IReadOnlySubscriptionField<int> _lives;
        private Transform _folderLives;
        private string _nameRes = "Heart.prefab";
        private int _currentLives;
        private SubscriptionField<GameState> _gameState;

        public LivesController(IReadOnlySubscriptionField<int> lives, SubscriptionField<GameState> gameState)
        {
            _lives = lives;
            _gameState = gameState;
            var tagLives = Reference.Canvas.GetComponentInChildren<TagLives>();
            if (tagLives != null) _folderLives = tagLives.transform;
            else Debug.LogWarning($"Dont find TagLives");
            _currentLives = 0;
            _lives.Subscribe(SetLives);
        }

        protected override void OnDispose() => _lives.UnSubscribe(SetLives);

        private void SetLives(int scoresValue)
        {
            if (_currentLives == scoresValue || (scoresValue < 0 && _currentLives == 0)) return;
            if (_currentLives < scoresValue)
            {
                _currentLives++;
                CreateGameObjectAddressable(_nameRes, _folderLives, (obj) => SetLives(scoresValue));
            }
            else
            {
                _currentLives--;
                DestroyGameObject(_gameObjects[0]);
                SetLives(scoresValue);
                if (_currentLives == 0) _gameState.Value = GameState.GameOver;
            }
        }
    }
}