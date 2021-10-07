namespace Eatable
{
    public sealed class StateForEat : IStateForCard
    {
        GameModel _gameModel;

        public StateForEat(GameModel gameModel) => _gameModel = gameModel;

        public void DoNonEat()
        {
            _gameModel.Lives.Value--;
            _gameModel.TypeGuess.Value = TypeGuess.incorrect;
        }

        public void DoEat()
        {
            _gameModel.Scores.Value++;
            _gameModel.TypeGuess.Value = TypeGuess.correct;
        }
    }
}