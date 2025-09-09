Подробный анализ применения принципов SOLID в игре "Угадай число"
1. Принцип единственной ответственности (Single Responsibility Principle)
Что сделано:

GameService отвечает только за игровую логику (генерация чисел, проверка попыток)
JsonSettingsRepository отвечает только за работу с настройками из JSON-файла
InMemoryGameRepository отвечает только за хранение игровых сессий в памяти
Game класс отвечает только за взаимодействие с пользователем (ввод/вывод)

Пример из кода:

csharp
// GameService отвечает только за игровую логику
public class GameService : IGameService
{
    public GameSession StartNewGame() { /* логика начала игры */ }
    public GuessResult MakeGuess(Guid gameId, int number) { /* логика проверки попытки */ }
}

// JsonSettingsRepository отвечает только за настройки
public class JsonSettingsRepository : ISettingsRepository
{
    public GameSettings GetSettings() { /* загрузка настроек из JSON */ }
}
2. Принцип открытости/закрытости (Open/Closed Principle)
Что сделано:

Классы открыты для расширения, но закрыты для модификации

Можно добавить новые реализации репозиториев без изменения существующего кода

Можно изменить источник настроек (JSON, БД, environment variables) без изменения GameService

Пример из кода:

csharp
// Интерфейсы позволяют добавлять новые реализации
public interface ISettingsRepository
{
    GameSettings GetSettings();
}

// Можно добавить новые реализации без изменения кода
public class DatabaseSettingsRepository : ISettingsRepository { /* ... */ }
public class EnvironmentSettingsRepository : ISettingsRepository { /* ... */ }
3. Принцип подстановки Барбары Лисков (Liskov Substitution Principle)
Что сделано:

Все реализации интерфейсов взаимозаменяемы

Можно заменить JsonSettingsRepository на другую реализацию без нарушения работы программы

Наследники могут использоваться вместо родителей без изменения корректности программы

Пример из кода:

csharp
// В Program.cs мы можем легко заменить реализации
services.AddSingleton<ISettingsRepository, DatabaseSettingsRepository>(); // вместо JsonSettingsRepository

// GameService будет работать с любой реализацией ISettingsRepository
public GameService(ISettingsRepository settingsRepository, IGameRepository gameRepository)
{
    // работает с любым ISettingsRepository
}
4. Принцип разделения интерфейса (Interface Segregation Principle)
Что сделано:

Интерфейсы небольшие и специфичные

ISettingsRepository содержит только методы для работы с настройками

IGameRepository содержит только методы для работы с игровыми сессиями

IGameService содержит только методы игровой логики

Пример из кода:

csharp
// Маленькие специализированные интерфейсы
public interface ISettingsRepository
{
    GameSettings GetSettings();
    void SaveSettings(GameSettings settings);
}

public interface IGameRepository
{
    void SaveGame(GameSession game);
    GameSession GetGame(Guid gameId);
}

public interface IGameService
{
    GameSettings GetSettings();
    GameSession StartNewGame();
    GuessResult MakeGuess(Guid gameId, int number);
}
5. Принцип инверсии зависимостей (Dependency Inversion Principle)
Что сделано:

Модули высокого уровня (GameService) не зависят от модулей низкого уровня

Оба зависят от абстракций (интерфейсов)

Зависимости внедряются через конструктор (Dependency Injection)

Пример из кода:

csharp
// GameService зависит от абстракций, а не от конкретных реализаций
public class GameService : IGameService
{
    private readonly ISettingsRepository _settingsRepository;
    private readonly IGameRepository _gameRepository;

    // Зависимости внедряются через конструктор
    public GameService(ISettingsRepository settingsRepository, IGameRepository gameRepository)
    {
        _settingsRepository = settingsRepository;
        _gameRepository = gameRepository;
    }
}

// В Program.cs настраиваем зависимости
var serviceProvider = new ServiceCollection()
    .AddSingleton<ISettingsRepository, JsonSettingsRepository>()
    .AddSingleton<IGameService, GameService>()
    .AddSingleton<IGameRepository, InMemoryGameRepository>()
    .BuildServiceProvider();

На выполнение задачи ушло 4 часа