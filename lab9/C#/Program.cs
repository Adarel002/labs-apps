public interface IBooking
{
    decimal Cost();
    string Description();
}

// Class for booking a hotel room
public class RoomBooking : IBooking
{
    private readonly decimal _cost;
    private readonly string _roomType;
    private readonly string _roomNumber;

    public RoomBooking(string roomType, string roomNumber, decimal cost)
    {
        _roomType = roomType;
        _roomNumber = roomNumber;
        _cost = cost;
    }

    public decimal Cost()
    {
        return _cost;
    }

    public string Description()
    {
        return $"Room Type: {_roomType}, Room Number: {_roomNumber}";
    }
}

// Class for a general booking order (e.g., a group of room bookings)
public class BookingOrder : IBooking
{
    private readonly string _orderId;
    protected readonly List<IBooking> Bookings;

    public BookingOrder(string orderId)
    {
        _orderId = orderId;
        Bookings = new List<IBooking>();
    }

    public decimal Cost()
    {
        decimal totalCost = 0;
        foreach (var booking in Bookings)
        {
            totalCost += booking.Cost();
        }
        return totalCost;
    }

    public void AddBooking(IBooking booking)
    {
        Bookings.Add(booking);
    }

    public void RemoveBooking(IBooking booking)
    {
        Bookings.Remove(booking);
    }

    public string Description()
    {
        return $"Booking Order ID: {_orderId}";
    }
}

// Class for hotel room booking orders
public class HotelBookingOrder : BookingOrder
{
    public HotelBookingOrder(string orderId) : base(orderId) { }

    public new decimal Cost()
    {
        decimal totalCost = 0;
        foreach (var booking in Bookings)
        {
            var currentCost = booking.Cost();
            Console.WriteLine($"Cost of {booking.Description()} = {currentCost} USD");
            totalCost += currentCost;
        }
        Console.WriteLine($"Total cost of {Description()} = {totalCost} USD");
        return totalCost;
    }
}

// Example usage
public class Program
{
    public static void Main()
    {
        var room1 = new RoomBooking("Single", "101", 100);
        var room2 = new RoomBooking("Double", "102", 150);
        var room3 = new RoomBooking("Suite", "301", 300);

        var order1 = new HotelBookingOrder("Booking001");
        order1.AddBooking(room1);
        order1.AddBooking(room2);
        order1.AddBooking(room3);

        order1.Cost();
    }
}
