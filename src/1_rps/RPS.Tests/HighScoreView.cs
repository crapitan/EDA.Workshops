using System;

namespace RPS.Tests
{
    public class HighScoreView
    {
        private int rounds, currentRound;
        private string player1, player2;
        private string title;
        private Guid gameId;

        public HighScoreView When(IEvent @event) => this;
        
        public HighScoreView When(GameCreated gameCreated)
        {
            this.rounds = gameCreated.Rounds;
            this.player1 = gameCreated.PlayerId;
            this.title = gameCreated.Title;
            this.GameStatus = GameStatus.ReadyToStart;
            this.gameId = gameCreated.GameId;

            return this;
        }

        public HighScoreView When(GameStarted gameStarted)
        {
            this.player2 = gameStarted.PlayerId;
            this.GameStatus = GameStatus.Started;
                
            return this;
        }

        public HighScoreView When(RoundStarted roundStarted)
        {
            this.currentRound = roundStarted.Round; 
            return this;
        }

        public GameStatus GameStatus { get; set; }

        public ScoreRow[] Rows { get; set; }
        public class ScoreRow
        {
            public int Rank { get; set; }
            public string PlayerId { get; set; }
            public int GamesWon { get; set; }
            public int RoundsWon { get; set; }
            public int GamesPlayed { get; set; }
            public int RoundsPlayed { get; set; }
        }
    }
}
