using System.Collections.Generic;

namespace Invariants.Tests
{
    public static class Game
    {
        public static IEnumerable<IEvent> Handle(JoinGameCommand command, GameState state)
        {
            if (state.CreateBy )
            yield return new GameStartedEvent { GameId = command.GameId, PlayerId = command.PlayerId };
            yield return new RoundStartedEvent { GameId = command.GameId, Round = 1 };
        }

        public static IEnumerable<IEvent> Handle(JoinGameCommand command, IEvent[] events)
        {
            yield return new GameStartedEvent { GameId = command.GameId, PlayerId = command.PlayerId };
            yield return new RoundStartedEvent { GameId = command.GameId, Round = 1 };
        }
    }
}
