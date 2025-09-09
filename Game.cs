using GuessNumberSolid.BLL;

namespace GuessNumberSolid
{
    public class Game
    {
        private readonly IGameService _gameService;

        public Game(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void Start()
        {
            Console.WriteLine("Добро пожаловать в игру 'Угадай число'!");

            // Получение настроек
            var settings = _gameService.GetSettings();
            Console.WriteLine($"Диапазон чисел: от {settings.MinNumber} до {settings.MaxNumber}");
            Console.WriteLine($"Количество попыток: {settings.MaxAttempts}");

            // Начало игры
            var gameSession = _gameService.StartNewGame();
            Console.WriteLine($"Я загадал число. Попробуйте угадать!");

            // Игровой цикл
            while (!gameSession.IsGameOver)
            {
                Console.Write($"Попытка {gameSession.Attempts + 1}/{settings.MaxAttempts}. Ваше число: ");

                if (int.TryParse(Console.ReadLine(), out int userGuess))
                {
                    var result = _gameService.MakeGuess(gameSession.GameId, userGuess);

                    if (result.IsCorrect)
                    {
                        Console.WriteLine($"Поздравляем! Вы угадали число за {gameSession.Attempts} попыток.");
                        return;
                    }

                    Console.WriteLine(result.Hint);
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите целое число.");
                }
            }

            Console.WriteLine($"К сожалению, вы исчерпали все попытки. Загаданное число было: {gameSession.TargetNumber}");
        }
    }
}