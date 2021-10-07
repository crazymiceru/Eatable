
namespace Eatable
{
    internal sealed class GameController : ControllerBasic
    {

        #region Fields

        private GameModel _gameModel;
        private const string _resGameCfg = "GameCfg.asset";
        private GameCfg _gameCfg;

        #endregion


        #region Init

        internal GameController()
        {
            
            var outData= new SaveLoad<OutData>().Load("OutData.txt");

            LoadCfg(_resGameCfg, (obj) =>
            {
                _gameCfg = (GameCfg)obj;
                _gameModel = new GameModel(_gameCfg,outData);
                _gameModel.GameState.Subscribe(ChangeStateGame);
                _gameModel.GameState.Value = GameState.StartLevel;
            });
        }

        protected override void OnDispose()
        {
            _gameModel.GameState.UnSubscribe(ChangeStateGame);
        }

        #endregion


        #region Process

        private void ChangeStateGame(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    Menu();
                    break;
                case GameState.StartLevel:
                    StartGame();
                    break;
                case GameState.GameOver:
                    GameOver();
                    break;
                default:
                    break;
            }
        }

        private void GameOver()
        {
            AddController(new GameOverController(_gameModel.GameState,_gameModel.Scores.Value));
        }

        private void Menu()
        {
            Clear();
        }

        private void StartGame()
        {
            Clear();
            AddController(new ActiveGameContoller(_gameModel));
        }

        #endregion
    }
}
