using System;
using System.Collections.Generic;

// Command interface
public interface ICommand
{
    void Execute();
}

// Hotel entities
public class Room
{
    public string Type { get; private set; }
    public bool Availability { get; private set; }

    public Room(string type, bool availability)
    {
        Type = type;
        Availability = availability;
    }

    public void ShowRoomDetails()
    {
        Console.WriteLine($"Room Type: {Type}");
        Console.WriteLine($"Available: {Availability}");
    }
}

public class Guest
{
    public string Name { get; private set; }

    public Guest(string name)
    {
        Name = name;
    }

    public void BrowseRooms(List<Room> rooms)
    {
        Console.WriteLine($"{Name} is browsing available rooms");
        rooms.ForEach(room => room.ShowRoomDetails());
    }

    public void SelectRoom(Room room)
    {
        Console.WriteLine($"{Name} selected room type: {room.Type}");
    }

    public void MakePayment()
    {
        Console.WriteLine($"{Name} is making payment for the room");
    }
}

public class BookingSystem
{
    public void ConfirmBooking()
    {
        Console.WriteLine("Room booking confirmed");
    }

    public void CancelBooking()
    {
        Console.WriteLine("Room booking cancelled");
    }
}

// Commands
public class BrowseRoomsCommand : ICommand
{
    private Guest _guest;
    private List<Room> _rooms;

    public BrowseRoomsCommand(Guest guest, List<Room> rooms)
    {
        _guest = guest;
        _rooms = rooms;
    }

    public void Execute()
    {
        _guest.BrowseRooms(_rooms);
    }
}

public class SelectRoomCommand : ICommand
{
    private Guest _guest;
    private Room _room;

    public SelectRoomCommand(Guest guest, Room room)
    {
        _guest = guest;
        _room = room;
    }

    public void Execute()
    {
        _guest.SelectRoom(_room);
    }
}

public class MakePaymentCommand : ICommand
{
    private Guest _guest;

    public MakePaymentCommand(Guest guest)
    {
        _guest = guest;
    }

    public void Execute()
    {
        _guest.MakePayment();
    }
}

public class ConfirmBookingCommand : ICommand
{
    private BookingSystem _bookingSystem;

    public ConfirmBookingCommand(BookingSystem bookingSystem)
    {
        _bookingSystem = bookingSystem;
    }

    public void Execute()
    {
        _bookingSystem.ConfirmBooking();
    }
}

// History Manager
public class BookingProcess
{
    private List<ICommand> _history = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        _history.Add(command);
    }

    public void CompleteBooking()
    {
        if (_history.Count > 0)
        {
            _history.ForEach(command => command.Execute());
        }
        else
        {
            Console.WriteLine("No actions to execute");
        }

        _history.Clear(); // Clear the history after execution
    }
}

// Example usage
public class Program
{
    public static void Main(string[] args)
    {
        Guest guest = new Guest("Alice");
        Room room1 = new Room("Double", true);
        Room room2 = new Room("Suite", true);

        BookingProcess bookingProcess = new BookingProcess();

        // Browse available rooms
        bookingProcess.AddCommand(new BrowseRoomsCommand(guest, new List<Room> { room1, room2 }));

        // Select a room type
        bookingProcess.AddCommand(new SelectRoomCommand(guest, room1));

        // Make payment
        bookingProcess.AddCommand(new MakePaymentCommand(guest));

        // Confirm booking
        BookingSystem bookingSystem = new BookingSystem();
        bookingProcess.AddCommand(new ConfirmBookingCommand(bookingSystem));

        // Execute all actions in the process
        bookingProcess.CompleteBooking();
    }
}
