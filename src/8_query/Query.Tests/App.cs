using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Tests
{
    public class App
    {
        List<IEvent> history = new List<IEvent>();

        public void Given(params IEvent[] events) => history.AddRange(events);

        public Task<T> QueryAsync<T>(IQuery<T> q)
        {
            if (q is GameQuery)
            {
                var collection = new List<IEvent>();

                var found1 = history
                  .OfType<GameCreatedEvent>()
                  .Where(f => f.GameId == ((GameQuery)q).GameId);

                var found2 = history
                    .OfType<GameCreatedEvent>()
                    .Where(f => f.GameId == ((GameQuery)q).GameId);

                var found3 = history
                    .OfType<GameCreatedEvent>()
                    .Where(f => f.GameId == ((GameQuery)q).GameId);

                collection.AddRange(found1);
                collection.AddRange(found2);
                collection.AddRange(found3);

                return from.Result()
            }
        }
    }

    public interface IEvent
    {
        Guid EventId { get; }
    }

    public class GameCreatedEvent : IEvent
    {
        public Guid GameId { get; set; }
        public string PlayerId { get; set; }
        public string Title { get; set; }
        public int Rounds { get; set; }
        public DateTime Created { get; set; }
        public GameStatus Status { get; set; } = GameStatus.Started;
        public string SourceId => GameId.ToString();
        public Guid EventId { get; set; } = Guid.NewGuid();
    }

    public class GameStartedEvent : IEvent
    {
        public Guid GameId { get; set; }
        public string PlayerId { get; set; }
        public string SourceId => GameId.ToString();
        public Guid EventId { get; set; } = Guid.NewGuid();
    }

    public class GameEndedEvent : IEvent
    {
        public Guid GameId { get; set; }
        public string SourceId => GameId.ToString();
        public Guid EventId { get; set; } = Guid.NewGuid();
    }

    public enum GameStatus
    {
        None = 0,
        ReadyToStart = 10,
        Started = 20,
        Ended = 50
    }

    public interface IQuery<T>
    { }

    public class GameQuery : IQuery<GameView>
    {
        public Guid GameId { get; set; }
    }
}
