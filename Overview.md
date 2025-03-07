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

## Event Class
```cs
public class Event
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


## EventDatabaseInteraction Interface (Almost the same for user and category)
```cs
public interface IEventDatabaseInteractor
{
    Event Add(Event event);
    Event Update(int eventId);
    void Delete(int eventId);

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

# Использования принципов SOLID:

## SRP

- Вместо того, чтобы добавлять логику взаимодействия с базой данных в классы Event и Category эта логика была выделена в отдельные обработчики (EventHandler и CategoryHandler), что и позволяет соблюсти SRP

## OCP

- Планируется выделить минимальный достаточный интерфейс для класса Event, например:
```cs
public class Event
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DeletionDate { get; set; }
    public DateTime NextNotificationDate { get; set; }
    public User User { get; set; }
    public Category User { get; set; }

    public virtual void Notify();
    public virtual void Update();
    // etc.
}
```
- Таким образом если нам понадобится возобновляемое событие или любое другое событие с особым поведением, мы просто отнаследуемся от базового класса и нам не придётся его менять

## LSP

- Также при создании новых событий надо будет следить за тем, что события всё ещё сохраняют своё базовое поведение: уведомления приходят вовремя, событие не меняет категорию само по себе
- Также это будет важно при взаимодействии с базой данных, классы отвечающие за эту логику должны корректно добавлять, удалять и изменять задачи вне зависимости от конкретной реализации и дополнительных функций, выполняемых этими сущностями
```cs
public class RepeatableEvent
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DeletionDate { get; set; }
    public DateTime NextNotificationDate { get; set; }
    public DateTime ProlongPeriod { get; set; }
    public User User { get; set; }
    public Category User { get; set; }
}
```

## ISP

- EventHandler и IEventDatabaseInteractor предоставляет возможность полного взаимодействия с базой данных, но если нам захочется несколькими разными способами обновлять данные, то создавая новый Handler или Interactor нам придётся реализовывать и Delete и Add
- Вместо этого можно ввести EventUpdater

```cs
public class EventUpdater
{
    public EventUpdater() {}
    public Event UpdateEvent(int eventId) {}

}
```

- И унаследовать EventHandler от EventUpdater-а, чтобы изменять и реализовывать только нужные части

## DIP

- Вместо того, чтобы делать AuthModule зависимым более низкоуровневого Hasher-а мы можем выделить интерфейс IHasher:

```cs
public class IHasher
{
    public virtual string GetHash(string password) {}
}
```

- И сделать AuthModule зависимым от него, таким образом модуль более высокого уровня не будет зависеть от модуля более низкого уровня