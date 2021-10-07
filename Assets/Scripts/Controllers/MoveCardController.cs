using UnityEngine;
using DG.Tweening;

namespace Eatable
{

    public sealed class MoveCardController : ControllerBasic, IExecute
    {
        ViewCard _viewCard;
        private Transform _transform;
        private Vector3 _differencePosMouse;
        private GameModel _gameModel;
        private bool _isDrag;
        private Vector3 _startPos;
        private IStateForCard _stateForCard;

        public MoveCardController(ViewCard viewCard, GameModel gameModel, IStateForCard stateForCard)
        {
            _viewCard = viewCard;
            _gameModel = gameModel;
            _stateForCard = stateForCard;
            _transform = _viewCard.gameObject.transform;
            _viewCard.evtOnBeginDrag += BeginDrag;
            _viewCard.evtOnEndDrag += EndDrag;
            _startPos = _transform.position;
            _gameModel.TypeGuess.Subscribe(DoCard);
        }

        private void DoCard(TypeGuess typeGuess)
        {
            if (typeGuess != TypeGuess.none)
                Unsubscribe();
        }

        public void Execute(float deltaTime)
        {
            if (!_isDrag) return;
            _transform.position = Input.mousePosition - _differencePosMouse;
        }

        protected override void OnDispose() => Unsubscribe();

        private void Unsubscribe()
        {
            _viewCard.evtOnBeginDrag -= BeginDrag;
            _viewCard.evtOnEndDrag -= EndDrag;
        }

        private void BeginDrag()
        {
            _transform.DOKill();
            _differencePosMouse = Input.mousePosition - _transform.position;
            _isDrag = true;
        }

        private void EndDrag()
        {
            var difference = _transform.position.x - _startPos.x;
            if (Mathf.Abs(difference) > _gameModel.GameCfg.SizeSwipe * Screen.width)
            {
                if (difference < 0) _stateForCard.DoNonEat();
                else _stateForCard.DoEat();
            }
            else _transform.DOMove(_startPos, _gameModel.GameCfg.SpeedReturnCard);
            _isDrag = false;
        }
    }
}