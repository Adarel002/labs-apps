public class RoomBooking
{
    public string Room { get; }
    public int Number { get; } // Room number
    public string GuestName { get; }
    public double Cost { get; }
    public bool Booked { get; set; }

    public RoomBooking(string room, int number, string guestName, double cost)
    {
        Room = room;
        Number = number;
        GuestName = guestName;
        Cost = cost;
        Booked = false;
    }
}

public class Room
{
    public string RoomType { get; }
    public DateTime AvailableFrom { get; }
    public Hotel Hotel { get; }

    public Room(string roomType, DateTime availableFrom, Hotel hotel)
    {
        RoomType = roomType;
        AvailableFrom = availableFrom;
        Hotel = hotel;
    }
}

public class Hotel
{
    public int TotalRooms { get; }
    public int BedsPerRoom { get; }

    public Hotel(int totalRooms, int bedsPerRoom)
    {
        TotalRooms = totalRooms;
        BedsPerRoom = bedsPerRoom;
    }
}

public class BookingSystem
{
    public double MaximumCost { get; }
    private List<RoomBooking> _bookings = new List<RoomBooking>();
    private List<RoomBooking> _availableRooms = new List<RoomBooking>();

    public BookingSystem(double maximumCost)
    {
        MaximumCost = maximumCost;

        // Creating a pool of rooms in the hotel
        for (int i = 1; i <= 100; i++)
        {
            var booking = new RoomBooking(
                "Двомісний номер",
                (i % 10) + 1,
                $"Гість {i}",
                100.0
            );
            _bookings.Add(booking);
            _availableRooms.Add(booking);
        }
    }

    public RoomBooking GetRoom(Room room)
    {
        foreach (var booking in _availableRooms)
        {
            if (
                booking.Room == room.RoomType &&
                !booking.Booked &&
                booking.Cost <= MaximumCost
            )
            {
                booking.Booked = true;
                _availableRooms.Remove(booking);
                return booking;
            }
        }
        throw new InvalidOperationException("Немає доступних номерів для цього типу");
    }

    public void RefundRoom(RoomBooking booking)
    {
        if (booking.Booked)
        {
            booking.Booked = false;
            _availableRooms.Add(booking);
        }
    }

    public int GetAvailableRoomCount()
    {
        return _availableRooms.Count;
    }
}

class Program
{
    static void Main()
    {
        var bookingSystem = new BookingSystem(150);
        var hotel = new Hotel(10, 2); // 10 rooms, 2 beds per room
        var room = new Room(
            "Двомісний номер",
            DateTime.Now.AddDays(1), // Room available in 1 day
            hotel
        );

        try
        {
            var booking1 = bookingSystem.GetRoom(room);
            Console.WriteLine($"Номер 1: Номер {booking1.Number}, Гість {booking1.GuestName}");
            Console.WriteLine($"Доступні номери після першого бронювання: {bookingSystem.GetAvailableRoomCount()}");

            // Check if the room is booked
            Console.WriteLine($"Чи заброньований номер 1? {booking1.Booked}"); // Should be true

            // Cancel the booking
            bookingSystem.RefundRoom(booking1);
            Console.WriteLine("Номер 1 повернуто.");
            Console.WriteLine($"Доступні номери після повернення: {bookingSystem.GetAvailableRoomCount()}");

            // Check if the room is available again
            var booking2 = bookingSystem.GetRoom(room);
            Console.WriteLine($"Номер 2: Номер {booking2.Number}, Гість {booking2.GuestName}");
            Console.WriteLine($"Доступні номери після другого бронювання: {bookingSystem.GetAvailableRoomCount()}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Помилка: {e.Message}");
        }
    }
}
