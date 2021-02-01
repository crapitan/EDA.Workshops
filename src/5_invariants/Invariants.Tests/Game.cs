using System.Collections.Generic;

namespace Invariants.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    namespace Invariants.Tests
    {
        public static class Game
        {
            public static IEnumerable<IEvent> Handle(JoinGameCommand command, GameState state)
            {
                if (state.Players.PlayerOne.Id == command.PlayerId)
                    yield break;

                if (state.Players.PlayerTwo == default)
                {
                    yield return new GameStartedEvent { GameId = command.GameId, PlayerId = command.PlayerId };
                    yield return new RoundStartedEvent { GameId = command.GameId, Round = 1 };
                }
            }

            public static IEnumerable<IEvent> Handle(JoinGameCommand command, IEvent[] events)
            {
                if (events
                    .OfType<GameCreatedEvent>()
                    .Any(e => e.PlayerId == command.PlayerId))
                    yield break;

                if (!events
                    .OfType<GameStartedEvent>()
                    .Any(e => e.PlayerId == command.PlayerId))
                {
                    yield return new GameStartedEvent { GameId = command.GameId, PlayerId = command.PlayerId };
                    yield return new RoundStartedEvent { GameId = command.GameId, Round = 1 };
                }
            }
        }
    }

}
