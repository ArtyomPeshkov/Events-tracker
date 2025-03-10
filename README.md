# **Техническое задание на разработку трекера событий**

## **1\. Введение**

Разработка приложения-трекера событий, позволяющего пользователям управлять важными событиями, такими как продление проездного, оплата страховок и прием лекарств. Приложение будет включать функционал регистрации пользователей, создание и настройку событий, а также отправку уведомлений через Telegram-бота.

## **2\. Цели и задачи**

* Создание десктопного приложения для управления событиями.  
* Реализация системы регистрации и авторизации пользователей.  
* Возможность создания, редактирования и удаления событий.  
* Создание различных категорий и типов событий.  
* Настройка уведомлений о событиях.  
* Возможность продления, откладывания и отмены событий.  
* Интеграция с Telegram для отправки уведомлений и управления событиями.

## **3\. Требования**

### **3.1. Пользовательский интерфейс**

* Главный экран: отображение списка событий (возможно будет добавлена фильтрация по категориям).  
* Экран выбора типа события  
  * Выбор события из предложенных категорий  
  * Создание своей категории/типового события  
  * Создание отдельного события  
* Экран создания категории: форма для создания категории:  
  * Название категории  
  * Описание  
  * Ограничения на типы и параметры событий  
* Экран события: форма для создания и редактирования события с полями:  
  * Название события  
  * Описание  
  * Дата и время выполнения  
  * Повторяемость (никогда, ежедневно, еженедельно, ежемесячно)  
  * Уведомления (время оповещения)

### **3.2. Регистрация и авторизация**

* Регистрация нового пользователя с проверкой уникальности логина.  
* Вход в систему с использованием логина и пароля.  
* Восстановление пароля через электронную почту (опционально).

### **3.3. Уведомления**

* Настройка уведомлений для каждого события.  
* Отправка уведомлений через Telegram-бота в установленное время.  
* Возможность отключения уведомлений для отдельных событий.  
* Возможность повторной отправки уведомления.  
* Возможность смещения интервала повторной отправки.

## **4\. Технологический стек**

* Язык программирования: C\#  
* Платформа: .NET Framework/Core  
* База данных: SQL  
* Telegram API для отправки уведомлений.

## **5\. Этапы разработки**

1. Проектирование архитектуры приложения.  
2. Разработка интерфейса для управления событиями.  
3. Реализация системы регистрации и авторизации.  
4. Интеграция с Telegram API для уведомлений.  
5. Разработка интерфейса для категорий событий.  
6. Тестирование и отладка приложения.  
7. Подготовка документации.

## **6\. Заключение**

Данный трекер событий будет полезен для пользователей, позволяя им легко управлять важными событиями и получать своевременные напоминания.

