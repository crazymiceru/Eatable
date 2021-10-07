namespace Eatable
{
    public sealed class StateForNonEat : IStateForCard
    {
        GameModel _gameModel;

        public StateForNonEat(GameModel gameModel) => _gameModel = gameModel;
        
        public void DoNonEat()
        {            
            _gameModel.Scores.Value++;
            _gameModel.TypeGuess.Value = TypeGuess.correct;
        }

        public void DoEat()
        {
            _gameModel.Lives.Value--;
            _gameModel.TypeGuess.Value = TypeGuess.incorrect;
        }
    }
}