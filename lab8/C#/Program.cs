public class HotelRoom
{
    public int RoomNumber { get; }
    public string Type { get; } // For example, "Single", "Double"
    public decimal Price { get; }

    public HotelRoom(int roomNumber, string type, decimal price)
    {
        RoomNumber = roomNumber;
        Type = type;
        Price = price;
    }
}

// Represents the availability of a room
public class RoomAvailability
{
    public HotelRoom Room { get; }
    public DateTime Date { get; }
    public int Available { get; private set; }
    public bool IsFullyBooked { get; private set; }

    public RoomAvailability(HotelRoom room, DateTime date, int available)
    {
        Room = room;
        Date = date;
        Available = available;
        IsFullyBooked = false;
    }

    public void MarkFullyBooked()
    {
        IsFullyBooked = true;
    }
}

// Interface for room booking operations
public interface IRoomBookingImplementor
{
    void BookRoom(RoomAvailability availability);
    void CancelBooking(RoomAvailability availability);
    int GetAvailableRooms(RoomAvailability availability);
    string GetBookingMethod();
}

// Reception booking (offline)
public class ReceptionBookingImplementor : IRoomBookingImplementor
{
    private readonly Dictionary<RoomAvailability, int> _rooms;
    public string BookingMethod { get; }

    public ReceptionBookingImplementor(IEnumerable<RoomAvailability> availabilities)
    {
        BookingMethod = "Reception";
        _rooms = new Dictionary<RoomAvailability, int>();

        foreach (var availability in availabilities)
        {
            _rooms[availability] = availability.Available;
        }
    }

    public void BookRoom(RoomAvailability availability)
    {
        if (_rooms[availability] > 0)
        {
            _rooms[availability]--;
            Console.WriteLine($"Room booked at reception for Room {availability.Room.RoomNumber}");
            if (_rooms[availability] == 0)
            {
                availability.MarkFullyBooked();
            }
        }
        else
        {
            Console.WriteLine("Room is fully booked for this date.");
        }
    }

    public void CancelBooking(RoomAvailability availability)
    {
        if (_rooms[availability] < availability.Available)
        {
            _rooms[availability]++;
            Console.WriteLine($"Booking cancelled at reception for Room {availability.Room.RoomNumber}");
        }
    }

    public int GetAvailableRooms(RoomAvailability availability)
    {
        return _rooms.ContainsKey(availability) ? _rooms[availability] : 0;
    }

    public string GetBookingMethod()
    {
        return BookingMethod;
    }
}

// Online booking
public class OnlineBookingImplementor : IRoomBookingImplementor
{
    private readonly Dictionary<RoomAvailability, int> _rooms;
    public string BookingMethod { get; }

    public OnlineBookingImplementor(IEnumerable<RoomAvailability> availabilities)
    {
        BookingMethod = "Online";
        _rooms = new Dictionary<RoomAvailability, int>();

        foreach (var availability in availabilities)
        {
            _rooms[availability] = availability.Available;
        }
    }

    public void BookRoom(RoomAvailability availability)
    {
        if (_rooms[availability] > 0)
        {
            _rooms[availability]--;
            Console.WriteLine($"Room booked online for Room {availability.Room.RoomNumber}");
            if (_rooms[availability] == 0)
            {
                availability.MarkFullyBooked();
            }
        }
        else
        {
            Console.WriteLine("Room is fully booked for this date.");
        }
    }

    public void CancelBooking(RoomAvailability availability)
    {
        if (_rooms[availability] < availability.Available)
        {
            _rooms[availability]++;
            Console.WriteLine($"Booking cancelled online for Room {availability.Room.RoomNumber}");
        }
    }

    public int GetAvailableRooms(RoomAvailability availability)
    {
        return _rooms.ContainsKey(availability) ? _rooms[availability] : 0;
    }

    public string GetBookingMethod()
    {
        return BookingMethod;
    }
}

// Manages bookings using various methods
public class HotelBookingSystem
{
    private IRoomBookingImplementor _booking;

    public HotelBookingSystem(IRoomBookingImplementor bookingImplementor)
    {
        _booking = bookingImplementor;
    }

    public void BookRoom(RoomAvailability availability)
    {
        if (!availability.IsFullyBooked)
        {
            _booking.BookRoom(availability);
        }
        else
        {
            Console.WriteLine("Cannot book room; it is fully booked.");
        }
    }

    public void CancelBooking(RoomAvailability availability)
    {
        _booking.CancelBooking(availability);
    }

    public int GetAvailableRooms(RoomAvailability availability)
    {
        return _booking.GetAvailableRooms(availability);
    }

    public void ChangeBookingImplementor(IRoomBookingImplementor newImplementor)
    {
        Console.WriteLine($"Booking method changed from {_booking.GetBookingMethod()} to {newImplementor.GetBookingMethod()}");
        _booking = newImplementor;
    }

    public string GetBookingMethod()
    {
        return _booking.GetBookingMethod();
    }
}

// Example usage
public class Program
{
    public static void Main()
    {
        var room1 = new HotelRoom(101, "Single", 100);
        var room2 = new HotelRoom(102, "Double", 150);

        var availability1 = new RoomAvailability(room1, new DateTime(2024, 9, 1), 10);
        var availability2 = new RoomAvailability(room2, new DateTime(2024, 9, 1), 5);

        var implementor = new ReceptionBookingImplementor(new[] { availability1, availability2 });
        var bookingSystem = new HotelBookingSystem(implementor);

        Console.WriteLine($"Booking method: {bookingSystem.GetBookingMethod()}");
        bookingSystem.BookRoom(availability1);
        bookingSystem.BookRoom(availability2);

        Console.WriteLine($"Available rooms for Room {availability1.Room.RoomNumber}: {bookingSystem.GetAvailableRooms(availability1)}");
        Console.WriteLine($"Available rooms for Room {availability2.Room.RoomNumber}: {bookingSystem.GetAvailableRooms(availability2)}");

        var newImplementor = new OnlineBookingImplementor(new[] { availability1, availability2 });
        bookingSystem.ChangeBookingImplementor(newImplementor);

        Console.WriteLine($"Booking method: {bookingSystem.GetBookingMethod()}");
        bookingSystem.BookRoom(availability1);
        bookingSystem.BookRoom(availability2);

        Console.WriteLine($"Available rooms for Room {availability1.Room.RoomNumber}: {bookingSystem.GetAvailableRooms(availability1)}");
        Console.WriteLine($"Available rooms for Room {availability2.Room.RoomNumber}: {bookingSystem.GetAvailableRooms(availability2)}");
    }
}
