using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Tests
{
    public class GamesView
    {
        public Dictionary<string, GameView> Games { get; set; } = new Dictionary<string, GameView>();

        public GamesView When(IEvent @event) => this;

        public GamesView When(GameCreatedEvent @event)
        {
            Games.Add(@event.GameId.ToString(), new GameView
            {
                Id = @event.GameId,
                Title = @event.Title,
                StartedBy = @event.PlayerId,
                Status = @event.Status.ToString()
            });
            return this;
        }

        public GamesView When(GameStartedEvent @event)
        {
            var gameId = @event.GameId.ToString();
            Games[gameId].Status = GameStatus.Started.ToString();
            return this;
        }

        public GamesView When(GameEndedEvent @event)
        {
            var gameId = @event.GameId.ToString();
            Games[@event.GameId.ToString()].Status = GameStatus.Ended.ToString();
            return this;
        }
    }

    public class GameView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string StartedBy { get; set; }
    }
}
