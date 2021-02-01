using System;

namespace Invariants.Tests
{
    public class GameState
    {
        public string CreateBy { get; private set; }
        public Guid GameId { get; private set; }
        public int Rounds { get; private set; }
        public string Title { get; private set; }

        public GameState When(IEvent @event) => this;
        public GameState When(GameCreatedEvent gameCreatedEvent)
        {
            this.CreateBy = gameCreatedEvent.PlayerId;
            this.GameId = gameCreatedEvent.GameId;
            this.Rounds = gameCreatedEvent.Rounds;
            this.Title = gameCreatedEvent.Title;

            return this;
        }
    }

}
