using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Eatable
{
    public class GameOverController : ControllerBasic
    {
        private string _nameRes = "GameOver.prefab";
        private SubscriptionField<GameState> _gameState;
        private Button _button;

        public GameOverController(SubscriptionField<GameState> gameState,int scores)
        {
            _gameState = gameState;
            CreateGameObjectAddressable(_nameRes, Reference.Canvas, (obj) =>
            {
                var tagText = obj.gameObject.GetComponentInChildren<TagText>();
                if (tagText != null && tagText.TryGetComponent(out TextMeshProUGUI text))
                    text.text = $"Счёт:{scores}";
                else Debug.LogWarning($"Dont find TagTimeText or TextMeshProUGUI");

                _button=obj.gameObject.GetComponentInChildren<Button>();
                if (_button != null) _button.onClick.AddListener(Click);
            }
            );
        }

        protected override void OnDispose() => _button.onClick.RemoveListener(Click);

        private void Click()=> _gameState.Value = GameState.StartLevel;        
    }
}