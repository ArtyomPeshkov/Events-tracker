# Классы и Таблицы в БД

## Users Table
  - `id`: bigint, primary key
  - `login`: string
  - `password_hash` string


## Categories Table
  - `id`: bigint, primary key
  - `title`: string

## Events Table
  - `id`: bigint, primary key
  - `title`: string
  - `description`: string
  - `deletion_date`: datetime, nullable
  - `next_notification_date`: datetime
  - `prolong_period`: datetime
  - `date_offset`: datetime
  - `is_regular`: boolean
  - `is_extendable`: boolean
  - `user_id`: bigint, foreign key
  - `category_id`: bigint, foreign key


## User Class
```cs
public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string TelegramId { get; set; }
}
```

## Category Class
```cs
public class Category
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
```

## Task Class
```cs
public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DeletionDate { get; set; }
    public DateTime NextNotificationDate { get; set; }
    public DateTime ProlongPeriod { get; set; }
    public DateTime DateShift { get; set; }
    public Boolean IsRegular { get; set; }
    public Boolean IsExtendable { get; set; }
    public User User { get; set; }
    public Category User { get; set; }
}
```

## EventHandler Class
```cs
public class EventHandler
{
    public EventHandler() {}
    public Event CreateEvent(Params params) {}
    public Event UpdateEvent(int eventId) {}
    public void DeleteEvent(int eventId) {}

    // methods for dumping into database

}
```

## CategoryHandler Class
```cs
public class CategoryHandler
{
    public CategoryHandler() {}
    public Event CreateCategory(Params params) {}
    public Event UpdateCategory(int categoryId) {}
    public void DeleteCategory(int categoryId) {}

    // methods for dumping into database

}
```

## AuthModule Class
```cs
public class AuthModule
{
    public AuthModule() {}
    public User SignUp(string login, string password) {}
    public User SignIn(string login, string password) {}
    
    // methods for interacting with database
}
```

## Hasher Class
```cs
public class Hasher
{
    public Hasher() {}
    public string GetHash(string password) {}
}
```


## TaskDatabaseInteraction Interface (Almost the same for user and category)
```cs
public interface ITaskDatabaseInteractor
{
    Task Add(Task task);
    Task Update(int taskId);
    void Delete(int taskId);

    // Another getters and groupers if needed
}
```


# Паттерны проектирования

## 1 Одиночка
- Будет логично использовать для объектов типа Hasher-а

## 2 Наблюдатель
- Для уведомления о наступившем событии

## 3 Шаблонный метод
- Для работы с базой данных, чтобы абстрагировать разные этапы (подключение, заись, завершение работы и прочее)

## 4 Компоновщик
- Может быть полезен, если в какой-то момент категории образуют иерархию и мы захотим итерироваться по древовидной структуре из задач и категорий
  
## 5 Упрощённый строитель
- Может быть использован при создании событий, так как событие - большой и нагруженный объект с множеством полей

## 6 Пул объектов
- Может быть использован для эффективного подключения к БД (будем хранить пулл открытых соединений и брать оттуда, вместо того чтобы создавать новые)

## 7 Фасад
- Будет удобно выделить единый интерфейс для взаимодействия с телеграмм и уже через него взаимодействовать с остальными сущностями

## 8 Декоратор

- Для логгирования