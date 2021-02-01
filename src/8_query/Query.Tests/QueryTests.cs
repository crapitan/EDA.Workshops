using System;
using System.Threading.Tasks;
using Xunit;

namespace Query.Tests
{
    public class QueryTests
    {
        [Fact]
        public async Task GameViewById()
        {
            var app = new App();
            var gameId = Guid.NewGuid();
            var gameId2 = Guid.NewGuid();

            //GIVEN
            app.Given(new IEvent[] {
                new GameCreatedEvent { GameId = gameId, PlayerId = "alex@rps.com", Rounds = 1, Title = "Game #1" },
                new GameStartedEvent { GameId = gameId, PlayerId = "sue@rps.com" },
                new GameEndedEvent { GameId = gameId },
                new GameCreatedEvent { GameId = gameId2, PlayerId = "sue@rps.com", Rounds = 1, Title = "Game #2" },
                new GameStartedEvent { GameId = gameId2, PlayerId = "joe@rps.com" },
                new GameEndedEvent { GameId = gameId2 }
            });

            //WHEN
            GameView gameView = await app.QueryAsync(new GameQuery { GameId = gameId2 });

            //THEN
            Assert.Equal(GameStatus.Ended.ToString(), gameView.Status);
        }
    }
}
