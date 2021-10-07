using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Eatable
{

    public sealed class FabricCard : ControllerBasic
    {
        private string nameRes = "Card.prefab";
        private GameModel _gameModel;

        public FabricCard(GameModel gameModel)
        {
            _gameModel = gameModel;
            CreateGameObjectAddressable(nameRes, Reference.ActiveElements);
            EvtAddressableCompleted += EndCreate;
        }

        private void EndCreate()
        {
            var viewCard = _gameObjects[0].gameObject.GetComponent<ViewCard>();
            var dataCard = viewCard.CardsCfg.DataCard[Random.Range(0, viewCard.CardsCfg.DataCard.Length)];

            var gameObjectImage = _gameObjects[0].gameObject.GetComponentInChildren<TagImage>();
            if (gameObjectImage == null) Debug.LogWarning($"Dont find TagImage");
            if (gameObjectImage.TryGetComponent<Image>(out Image image))
                image.sprite = dataCard.sprite;
            var gameObjectText = _gameObjects[0].gameObject.GetComponentInChildren<TagText>();
            if (gameObjectText == null) Debug.LogWarning($"Dont find TagText");
            if (gameObjectText.TryGetComponent(out TextMeshProUGUI text))
                text.text = dataCard.name;

            IStateForCard stateForCard = dataCard.isEatable ? (IStateForCard)new StateForEat(_gameModel) :
                (IStateForCard)new StateForNonEat(_gameModel);

            AddController(new MoveCardController(viewCard,_gameModel, stateForCard));
            viewCard.transform.DOScale(new Vector3(1,1,1),_gameModel.GameCfg.SpeedReturnCard);                
        }
    }
}