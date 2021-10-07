using DG.Tweening;
using UnityEngine;

namespace Eatable
{
    public sealed class ActiveGameContoller : ControllerBasic
    {
        #region Fields

        private const string _nameEffectRes = "Star.prefab";
        private GameModel _gameModel;
        private ControllerBasic _card;
        private Vector3 _startTransformCard;
        private GameCfg _cfg;

        #endregion


        #region Init

        public ActiveGameContoller(GameModel gameModel)
        {

            _gameModel = gameModel;
            _gameModel.TypeGuess.Subscribe(TransitionNextCard);
            _cfg = _gameModel.GameCfg;
            AddController(new ScoresController(_gameModel.Scores));
            AddController(new LivesController(_gameModel.Lives, _gameModel.GameState));
            AddController(new TimeController(_gameModel));
            _gameModel.Lives.Value = _gameModel.StartLive;
            _gameModel.Scores.Value = 0;

            CreateCard();
        }

        protected override void OnDispose()
        {
            _gameModel.TypeGuess.UnSubscribe(TransitionNextCard);
        }

        #endregion


        #region Process

        private void TransitionNextCard(TypeGuess typeGuess)
        {
            if (typeGuess == TypeGuess.none) return;
            if (typeGuess == TypeGuess.correct)
            {
                var vector = (_startTransformCard - _card[0].gameObject.transform.position).normalized;
                var endPosition = _card[0].gameObject.transform.position - vector * _cfg.ExitLenghth * Screen.width;

                _card[0].gameObject.transform.DOMove(endPosition, _cfg.SpeedCorrectExitCard).OnComplete(CreateCard);
                AddEffect(_nameEffectRes, _card[0].gameObject.transform.position, 50);
            }
            else
                _card[0].gameObject.transform.DOShakePosition(_cfg.SpeedIncorrectExitCard, _cfg.ShakeStrenghth,
                    _cfg.ShakeVibrato, _cfg.ShakeRandomLess).OnComplete(CreateCard);
        }

        private void AddEffect(string nameRes, Vector3 position, int count)
        {
            for (int i = 0; i < count; i++)
            {
                CreateGameObjectAddressable(nameRes, Reference.Canvas, position, Quaternion.identity, (obj) =>
                 {
                     obj.gameObject.transform.DOMove(position + Random.insideUnitSphere * 0.4f * Screen.width, 0.5f).OnComplete(() =>
                            {
                                DestroyGameObject(obj);
                                obj.gameObject.transform.DOKill();
                            });
                     obj.gameObject.transform.DORotate(new Vector3(0, 0, 180), 0.5f, RotateMode.FastBeyond360).SetLoops(-1);
                 });
            }
        }

        private void CreateCard()
        {
            _gameModel.TypeGuess.Value = TypeGuess.none;
            _gameModel.CurrentTime.Value = _gameModel.TimeReaction;
            if (_card != null) DestroyController(_card);
            AddController(_card = new FabricCard(_gameModel));
            _card.EvtAddressableCompleted += () => _startTransformCard = _card[0].gameObject.transform.position;
        }

        #endregion
    }
}