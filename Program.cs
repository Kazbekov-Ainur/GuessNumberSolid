using GuessNumberSolid;
using GuessNumberSolid.BLL;
using GuessNumberSolid.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Настройка конфигурации
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Настройка DI контейнера
var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton<ISettingsRepository, JsonSettingsRepository>()
    .AddSingleton<IGameService, GameService>()
    .AddSingleton<IGameRepository, InMemoryGameRepository>()
    .BuildServiceProvider();

// Получение сервиса игры
var gameService = serviceProvider.GetService<IGameService>();

// Запуск игры
var game = new Game(gameService);
game.Start();