using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Eatable
{
    public class TimeController : ControllerBasic, IExecute
    {
        #region Fields

        private const float _constRounding = 0.99f;
        private Image _image;
        private TextMeshProUGUI _text;
        private int _currentIntTime;
        private GameModel _gameModel;

        #endregion


        #region Init

        public TimeController(GameModel gameModel)
        {
            _gameModel = gameModel;
            var tagTimeProgress = Reference.Canvas.GetComponentInChildren<TagTimeProgress>();
            if (tagTimeProgress != null && tagTimeProgress.TryGetComponent(out Image image))
                _image = image;
            else Debug.LogWarning($"Dont find TagTimeProgress or Image");
            var tagTimeText = Reference.Canvas.GetComponentInChildren<TagTimeText>();
            if (tagTimeText != null && tagTimeText.TryGetComponent(out TextMeshProUGUI text))
                _text = text;
            else Debug.LogWarning($"Dont find TagTimeText or TextMeshProUGUI");
        }

        #endregion


        #region Process

        public void Execute(float deltaTime)
        {
            if (_gameModel.GameState.Value != GameState.StartLevel) return;

            _gameModel.CurrentTime.Value -= deltaTime;
            if (_gameModel.CurrentTime.Value <= 0 && _gameModel.TypeGuess.Value == TypeGuess.none)
            {
                _gameModel.Lives.Value--;
                _gameModel.TypeGuess.Value = TypeGuess.incorrect;
                _gameModel.CurrentTime.Value = 0;
            }

            _image.fillAmount = _gameModel.CurrentTime.Value / _gameModel.TimeReaction;

            if (_currentIntTime != (int)(_gameModel.CurrentTime.Value + _constRounding))
            {
                _currentIntTime = (int)(_gameModel.CurrentTime.Value + _constRounding);
                _text.text = _currentIntTime.ToString();

                DOTween.Sequence().Append(_text.gameObject.transform.DOShakeScale(0.1f, 1, 5, 100)).
                    Append(_text.gameObject.transform.DOScale(Vector3.one, 0.1f));
            }
        }

        #endregion
    }
}
