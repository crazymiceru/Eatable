namespace Eatable
{
    public sealed class GameModel
    {
        public SubscriptionField<GameState> GameState { get; }
        public SubscriptionField<int> Scores { get; }
        public SubscriptionField<int> Lives { get; }
        public SubscriptionField<TypeGuess> TypeGuess { get; }
        public SubscriptionField<float> CurrentTime { get; }

        public readonly int StartLive;
        public readonly float TimeReaction;
        public readonly GameCfg GameCfg;

        public GameModel(GameCfg gameCfg,OutData outData)
        {
            GameCfg = gameCfg;
            GameState = new SubscriptionField<GameState>();
            Scores = new SubscriptionField<int>();
            Lives = new SubscriptionField<int>();
            TypeGuess = new SubscriptionField<TypeGuess>();
            CurrentTime = new SubscriptionField<float>();
            StartLive = outData.StartLive;
            TimeReaction = outData.TimeReaction;
        }
    }
}