Время выполнения: 4 часа!

📊 Принципы SOLID в реализации
1. 🎯 Принцип единственной ответственности (Single Responsibility Principle)
Что реализовано:

Каждый класс имеет четко определенную зону ответственности

GameService → игровая логика

JsonSettingsRepository → работа с настройками

InMemoryGameRepository → хранение игровых сессий

Game → взаимодействие с пользователем

Пример реализации:

csharp
// GameService отвечает исключительно за игровую логику
public class GameService : IGameService
{
    public GameSession StartNewGame() { /* генерация чисел и начало игры */ }
    public GuessResult MakeGuess(Guid gameId, int number) { /* проверка попыток */ }
}

2. 🔄 Принцип открытости/закрытости (Open/Closed Principle)
Что реализовано:

Система спроектирована для расширения без модификации существующего кода

Легко добавлять новые реализации репозиториев

Возможность изменения источников настроек без изменения бизнес-логики

Пример реализации:

csharp
// Базовый интерфейс позволяет добавлять новые реализации
public interface ISettingsRepository
{
    GameSettings GetSettings();
}

// Новые реализации добавляются без изменения существующего кода
public class DatabaseSettingsRepository : ISettingsRepository { /* ... */ }
public class EnvironmentSettingsRepository : ISettingsRepository { /* ... */ }

3. 🔁 Принцип подстановки Барбары Лисков (Liskov Substitution Principle)
Что реализовано:

Все реализации интерфейсов полностью взаимозаменяемы

Возможность замены реализаций без нарушения работы программы

Сохранение корректности при использовании наследников вместо родителей

Пример реализации:

csharp
// Легкая замена реализации в конфигурации DI
services.AddSingleton<ISettingsRepository, DatabaseSettingsRepository>();

// GameService работает с любой реализацией ISettingsRepository
public GameService(ISettingsRepository settingsRepository, IGameRepository gameRepository)
{
    // Не зависит от конкретной реализации
}

4. 🧩 Принцип разделения интерфейса (Interface Segregation Principle)
Что реализовано:

Специализированные интерфейсы для каждой области ответственности

Отсутствие "жирных" интерфейсов с избыточными методами

Четкое разделение обязанностей между интерфейсами

Пример реализации:

csharp
// Специализированные интерфейсы
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

5. 📡 Принцип инверсии зависимостей (Dependency Inversion Principle)
Что реализовано:

Модули высокого уровня не зависят от модулей низкого уровня

Зависимость от абстракций, а не от конкретных реализаций

Использование Dependency Injection для управления зависимостями

Пример реализации:

csharp
// Зависимости внедряются через конструктор
public class GameService : IGameService
{
    private readonly ISettingsRepository _settingsRepository;
    private readonly IGameRepository _gameRepository;

    public GameService(ISettingsRepository settingsRepository, IGameRepository gameRepository)
    {
        _settingsRepository = settingsRepository;
        _gameRepository = gameRepository;
    }
}

// Настройка зависимостей в DI-контейнере
var serviceProvider = new ServiceCollection()
    .AddSingleton<ISettingsRepository, JsonSettingsRepository>()
    .AddSingleton<IGameService, GameService>()
    .AddSingleton<IGameRepository, InMemoryGameRepository>()
    .BuildServiceProvider();
