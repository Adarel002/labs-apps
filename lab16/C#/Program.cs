using System;
using System.Collections.Generic;

// Abstract class for HotelSystem
public abstract class HotelSystem
{
    public abstract RoomIterator CreateAvailableRoomsIterator();
    public abstract RoomIterator CreateBookedRoomsIterator();
}

// Abstract class for RoomIterator
public abstract class RoomIterator
{
    public abstract bool GetNext();
    public abstract bool HasMore();
}

// Hotel class implementing HotelSystem
public class Hotel : HotelSystem
{
    public override RoomIterator CreateAvailableRoomsIterator()
    {
        return new RoomIteratorImpl(this, 0, "available_rooms");
    }

    public override RoomIterator CreateBookedRoomsIterator()
    {
        return new RoomIteratorImpl(this, 0, "booked_rooms");
    }
}

// RoomIterator class implementing RoomIterator
public class RoomIteratorImpl : RoomIterator
{
    private List<string> _cache;
    private Hotel _hotel;
    private int _currentPosition;

    public RoomIteratorImpl(Hotel hotel, int currentPosition, string profileId)
    {
        _hotel = hotel;
        _currentPosition = currentPosition;
        _cache = new List<string>();

        for (int index = 0; index < 5; index++)
        {
            _cache.Add($"{profileId} Room {index + 1}");
        }
    }

    public override bool GetNext()
    {
        if (HasMore())
        {
            Console.WriteLine(_cache[_currentPosition]);
            _currentPosition++;
            return true;
        }
        return false;
    }

    public override bool HasMore()
    {
        return _currentPosition < _cache.Count;
    }
}

// BookingSystem class
public class BookingSystem
{
    public int SystemId { get; private set; }
    public string Notifier { get; private set; }

    public BookingSystem(int systemId, string notifier)
    {
        SystemId = systemId;
        Notifier = notifier;
    }

    public void NotifyAvailableRooms(RoomIterator iterator)
    {
        while (iterator.HasMore())
        {
            iterator.GetNext();
        }
    }

    public void NotifyBookedRooms(RoomIterator iterator)
    {
        while (iterator.HasMore())
        {
            iterator.GetNext();
        }
    }
}

// Main function
public class Program
{
    public static void Main(string[] args)
    {
        Hotel hotel = new Hotel();
        BookingSystem bookingSystem = new BookingSystem(1, "NotifierBot");

        RoomIterator availableRoomsIterator = hotel.CreateAvailableRoomsIterator();
        RoomIterator bookedRoomsIterator = hotel.CreateBookedRoomsIterator();

        Console.WriteLine("Available Rooms:");
        bookingSystem.NotifyAvailableRooms(availableRoomsIterator);

        Console.WriteLine("\nBooked Rooms:");
        bookingSystem.NotifyBookedRooms(bookedRoomsIterator);
    }
}
