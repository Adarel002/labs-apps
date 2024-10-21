using System;
using System.Collections.Generic;

// Abstract class for event listeners
public abstract class EventListener
{
    public abstract void Update(string eventData);
}

// BookingListener that handles booking events
public class BookingListener : EventListener
{
    public override void Update(string eventData)
    {
        Console.WriteLine($"Booking Event: {eventData}");
    }
}

// CancellationListener that handles cancellation events
public class CancellationListener : EventListener
{
    public override void Update(string eventData)
    {
        Console.WriteLine($"Cancellation Event: {eventData}");
    }
}

// EventManager to manage listeners and notify them of events
public class EventManager
{
    private readonly Dictionary<string, List<EventListener>> _listeners = new();

    public void Subscribe(string eventType, EventListener listener)
    {
        if (!_listeners.ContainsKey(eventType))
        {
            _listeners[eventType] = new List<EventListener>();
        }
        _listeners[eventType].Add(listener);
    }

    public void Unsubscribe(string eventType, EventListener listener)
    {
        if (_listeners.ContainsKey(eventType))
        {
            _listeners[eventType].Remove(listener);
        }
    }

    public void Notify(string eventType, string eventData)
    {
        if (_listeners.ContainsKey(eventType))
        {
            foreach (var listener in _listeners[eventType])
            {
                listener.Update(eventData);
            }
        }
    }
}

// BookingManager class to manage hotel booking operations and trigger events
public class BookingManager
{
    private readonly EventManager _events;

    public BookingManager(EventManager eventManager)
    {
        _events = eventManager;
    }

    public void BookRoom(string roomType)
    {
        _events.Notify("booking", $"Booked room: {roomType}");
    }

    public void CancelRoom(string roomType)
    {
        _events.Notify("cancellation", $"Cancelled room: {roomType}");
    }
}

// Application class to configure and manage hotel booking operations
public class Application
{
    private readonly EventManager _events;
    private readonly BookingManager _bookingManager;

    public Application()
    {
        _events = new EventManager();
        _bookingManager = new BookingManager(_events);
    }

    public void Config()
    {
        _events.Subscribe("booking", new BookingListener());
        _events.Subscribe("cancellation", new CancellationListener());
    }

    public void Book(string roomType)
    {
        _bookingManager.BookRoom(roomType);
    }

    public void Cancel(string roomType)
    {
        _bookingManager.CancelRoom(roomType);
    }
}

// Main function to run the application
public class Program
{
    public static void Main(string[] args)
    {
        Application app = new Application();
        app.Config();
        app.Book("Deluxe Room");
        app.Cancel("Deluxe Room");
    }
}
