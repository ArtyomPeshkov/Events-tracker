﻿using Domain;
using System;
using System.Reflection;

class UI
{
    private static IAuthModule _authModule;
    private static IEventModule _eventModule;
    private static User _currentUser;

    static void Main(string[] args)
    {
        Assembly.Load("SQLDataAccess").GetType("SQLDataAccess.InteractorsInitializer").GetMethod("init").Invoke(null, null);
        _authModule = new AuthModule();
        _eventModule = new EventModule();

        Console.WriteLine("Welcome to the Event Manager!");
        while (true)
        {
            if (_currentUser == null)
            {
                Console.WriteLine("1. Sign Up");
                Console.WriteLine("2. Sign In");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    SignUp();
                }
                else if (choice == "2")
                {
                    SignIn();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
            else
            {
                ManageEvents();
            }
        }
    }

    private static void SignUp()
    {
        Console.Write("Enter your login: ");
        var login = Console.ReadLine();
        Console.Write("Enter your password: ");
        var password = Console.ReadLine();

        try
        {
            _currentUser = _authModule.SignUp(login, password);
            Console.WriteLine($"User  '{login}' registered successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void SignIn()
    {
        Console.Write("Enter your login: ");
        var login = Console.ReadLine();
        Console.Write("Enter your password: ");
        var password = Console.ReadLine();

        try
        {
            _currentUser = _authModule.SignIn(login, password);
            Console.WriteLine($"Welcome back, {_currentUser.Login}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void ManageEvents()
    {
        Console.WriteLine("1. Add Event");
        Console.WriteLine("2. Update Event");
        Console.WriteLine("3. Delete Event");
        Console.WriteLine("4. Get Event");
        Console.WriteLine("5. Sign Out");
        Console.Write("Choose an option: ");

        switch (Console.ReadLine())
        {
            case "1":
                AddEvent();
                break;
            case "2":
                UpdateEvent();
                break;
            case "3":
                DeleteEvent();
                break;
            case "4":
                GetEvent();
                break;
            case "5":
                _currentUser = null;
                Console.WriteLine("You have signed out.");
                break;
            default:
                Console.WriteLine("Invalid input.");
                break;
        }
    }

    private static void AddEvent()
    {
        Console.Write("Enter event name: ");
        var eventName = Console.ReadLine();
        Console.WriteLine(_eventModule.AddEvent(eventName, _currentUser.Id).Id);
    }

    private static void UpdateEvent()
    {
        Console.Write("Enter event ID to update: ");
        var eventId = long.Parse(Console.ReadLine());
        Console.Write("Enter new event name: ");
        var newEventName = Console.ReadLine();
        _eventModule.UpdateEvent(eventId, newEventName);
    }

    private static void DeleteEvent()
    {
        Console.Write("Enter event ID to delete: ");
        var eventId = long.Parse(Console.ReadLine());
        _eventModule.DeleteEvent(eventId);
    }
    private static void GetEvent()
    {
        Console.Write("Enter event ID to get: ");
        var eventId = long.Parse(Console.ReadLine());
        Console.WriteLine(_eventModule.GetEvent(eventId).Title);
    }
}
