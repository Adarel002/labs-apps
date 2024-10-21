public static class RoomType
{
    public const string SINGLE = "single";
    public const string DOUBLE = "double";
    public const string SUITE = "suite";
}

// Інтерфейс для номеру
public interface IRoom
{
    string GetRoomName();
}

// Класи для різних типів номерів
public class SingleRoom : IRoom
{
    public string GetRoomName()
    {
        return "Single Room";
    }
}

public class DoubleRoom : IRoom
{
    public string GetRoomName()
    {
        return "Double Room";
    }
}

public class SuiteRoom : IRoom
{
    public string GetRoomName()
    {
        return "Suite Room";
    }
}

// Інтерфейс для клієнта
public interface IGuest
{
    void RequestRoom(IRoom room);
    Dictionary<string, string> SelectDates();
    void StayInRoom();
    string GetName();
}

// Система бронювання
public class BookingSystem
{
    public void BookRoom()
    {
        Console.WriteLine("The room is being booked");
    }

    public void ConfirmBooking()
    {
        Console.WriteLine("Booking confirmed");
    }
}

// Портьє, що обробляє бронювання
public class Receptionist
{
    public void AcceptBooking(IGuest guest)
    {
        Console.WriteLine($"Receptionist accepted the booking from {guest.GetName()}");
    }

    public void ProcessBooking()
    {
        Console.WriteLine("Processing booking in the system");
    }

    public void DeliverConfirmation(IGuest guest)
    {
        Console.WriteLine($"Confirmation ready, delivering to the guest {guest.GetName()}");
    }
}

// Клієнт готелю
public class Guest : IGuest
{
    private string name;

    public Guest(string name)
    {
        this.name = name;
    }

    public void RequestRoom(IRoom room)
    {
        Console.WriteLine($"Guest {this.name} checks the details of {room.GetRoomName()}");
    }

    public Dictionary<string, string> SelectDates()
    {
        Console.WriteLine($"Guest {this.name} selects dates for their stay");
        return new Dictionary<string, string>
        {
            { "checkIn", "2024-12-01" },
            { "checkOut", "2024-12-05" }
        };
    }

    public void StayInRoom()
    {
        Console.WriteLine($"Guest {this.name} is staying in the room");
    }

    public string GetName()
    {
        return this.name;
    }
}

// Фасад готелю для спрощеного бронювання
public class HotelFacade
{
    private BookingSystem bookingSystem;
    private Receptionist receptionist;
    private Dictionary<string, IRoom> rooms;

    public HotelFacade()
    {
        bookingSystem = new BookingSystem();
        receptionist = new Receptionist();
        rooms = new Dictionary<string, IRoom>
        {
            { RoomType.SINGLE, new SingleRoom() },
            { RoomType.DOUBLE, new DoubleRoom() },
            { RoomType.SUITE, new SuiteRoom() }
        };
    }

    public IRoom GetRoom(string type)
    {
        return rooms[type];
    }

    public void BookRoom(IGuest guest)
    {
        receptionist.AcceptBooking(guest);
        receptionist.ProcessBooking();
        BookingSystemWork();
        receptionist.DeliverConfirmation(guest);
    }

    private void BookingSystemWork()
    {
        bookingSystem.BookRoom();
        bookingSystem.ConfirmBooking();
    }
}

// Використання
class Program
{
    static void Main(string[] args)
    {
        HotelFacade hotel = new HotelFacade();
        Guest guest1 = new Guest("Alice");
        Guest guest2 = new Guest("Bob");

        guest1.RequestRoom(hotel.GetRoom(RoomType.SINGLE));
        hotel.BookRoom(guest1);

        guest2.RequestRoom(hotel.GetRoom(RoomType.SUITE));
        hotel.BookRoom(guest2);

        guest1.StayInRoom();
        guest2.StayInRoom();
    }
}
