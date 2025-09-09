using System;

namespace GuessNumberSolid.BLL
{
    public interface IGameService
    {
        GameSettings GetSettings();
        GameSession StartNewGame();
        GuessResult MakeGuess(Guid gameId, int number);
    }

    public class GameSettings
    {
        public int MinNumber { get; set; } = 1;
        public int MaxNumber { get; set; } = 100;
        public int MaxAttempts { get; set; } = 10;
    }

    public class GameSession
    {
        public Guid GameId { get; set; }
        public int TargetNumber { get; set; }
        public int MaxAttempts { get; set; }
        public int Attempts { get; set; }
        public bool IsGameOver { get; set; }
    }

    public class GuessResult
    {
        public bool IsCorrect { get; set; }
        public string Hint { get; set; } = string.Empty;
    }
}