namespace RPS.Tests
{
    public class GameState
    {
        public GameState When(IEvent @event) => this;

        public GameState When(GameCreated @event)
        {
            this.Status = GameStatus.ReadyToStart;
            return this;
        }

        public GameState When(GameStarted @evemt)
        {
            this.Status = GameStatus.Started;
            return this;
        }

        public GameState When(RoundStarted @event)
        {
            return this;
        }

        public GameState When(GameEnded @event)
        {
            this.Status = GameStatus.Ended;
            return this;
        }

        public GameStatus Status { get; set; }
    }

}
