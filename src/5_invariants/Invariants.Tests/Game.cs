using System;
using System.Collections.Generic;
using System.Text;

namespace Invariants.Tests
{
    public static class Game
    {
        public static IEnumerable<IEvent> Handle(JoinGameCommand command, GameState state)
        {
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
