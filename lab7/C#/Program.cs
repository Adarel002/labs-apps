public interface IRoom
{
    string GetRoomInfo();
    void SetRoomInfo(string info);
}

public interface IBooking
{
    void BookRoom(IRoom room);
    void CancelRoom(IRoom room);
    List<IRoom> GetBookedRooms();
}

// Клас для номерів готелю
public class HotelRoom : IRoom
{
    private string _info;

    public HotelRoom(string info)
    {
        _info = info;
    }

    public string GetRoomInfo()
    {
        return _info;
    }

    public void SetRoomInfo(string info)
    {
        _info = info;
    }
}

// Клас для системи бронювання
public class BookingSystem : IBooking
{
    private List<IRoom> _bookedRooms = new List<IRoom>();

    public void BookRoom(IRoom room)
    {
        _bookedRooms.Add(room);
        Console.WriteLine($"Room booked: {room.GetRoomInfo()}");
    }

    public void CancelRoom(IRoom room)
    {
        if (_bookedRooms.Remove(room))
        {
            Console.WriteLine($"Room cancelled: {room.GetRoomInfo()}");
        }
        else
        {
            Console.WriteLine($"Room not found: {room.GetRoomInfo()}");
        }
    }

    public List<IRoom> GetBookedRooms()
    {
        return new List<IRoom>(_bookedRooms);
    }
}

// Функція для виведення заброньованих номерів
public static class BookingUtilities
{
    public static void PrintBookedRooms(IBooking bookingSystem)
    {
        Console.WriteLine("Booked Rooms:");
        foreach (var room in bookingSystem.GetBookedRooms())
        {
            Console.WriteLine(room.GetRoomInfo());
        }
    }
}

// Головна функція
public class Program
{
    public static void Main()
    {
        var bookingSystem = new BookingSystem();

        var room1 = new HotelRoom("Room A - Guest 1");
        var room2 = new HotelRoom("Room B - Guest 2");

        bookingSystem.BookRoom(room1);
        bookingSystem.BookRoom(room2);

        BookingUtilities.PrintBookedRooms(bookingSystem);

        Console.WriteLine("--------");
        Console.WriteLine("Cancelling Room");
        Console.WriteLine("-------------");

        bookingSystem.CancelRoom(room1);

        BookingUtilities.PrintBookedRooms(bookingSystem);
    }
}
