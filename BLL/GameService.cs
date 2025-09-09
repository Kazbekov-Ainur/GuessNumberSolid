using GuessNumberSolid.DAL;
using System;

namespace GuessNumberSolid.BLL
{
    public class GameService : IGameService
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly IGameRepository _gameRepository;
        private readonly Random _random;

        public GameService(ISettingsRepository settingsRepository, IGameRepository gameRepository)
        {
            _settingsRepository = settingsRepository;
            _gameRepository = gameRepository;
            _random = new Random();
        }

        public GameSettings GetSettings()
        {
            return _settingsRepository.GetSettings();
        }

        public GameSession StartNewGame()
        {
            var settings = GetSettings();
            var targetNumber = _random.Next(settings.MinNumber, settings.MaxNumber + 1);

            var gameSession = new GameSession
            {
                GameId = Guid.NewGuid(),
                TargetNumber = targetNumber,
                MaxAttempts = settings.MaxAttempts,
                Attempts = 0,
                IsGameOver = false
            };

            _gameRepository.SaveGame(gameSession);
            return gameSession;
        }

        public GuessResult MakeGuess(Guid gameId, int number)
        {
            var gameSession = _gameRepository.GetGame(gameId);

            if (gameSession == null || gameSession.IsGameOver)
            {
                throw new InvalidOperationException("Игра не найдена или уже завершена");
            }

            gameSession.Attempts++;

            if (number == gameSession.TargetNumber)
            {
                gameSession.IsGameOver = true;
                _gameRepository.SaveGame(gameSession);
                return new GuessResult { IsCorrect = true, Hint = "" };
            }

            var hint = number < gameSession.TargetNumber ? "Загаданное число больше" : "Загаданное число меньше";

            if (gameSession.Attempts >= gameSession.MaxAttempts)
            {
                gameSession.IsGameOver = true;
                _gameRepository.SaveGame(gameSession);
            }

            return new GuessResult { IsCorrect = false, Hint = hint };
        }
    }
}