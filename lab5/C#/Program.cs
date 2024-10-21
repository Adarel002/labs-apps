public static class RoomType
{
    public const string SINGLE = "single";
    public const string DOUBLE = "double";
    public const string SUITE = "suite";
}

public static class BookingStatus
{
    public const string AVAILABLE = "available";
    public const string BOOKED = "booked";
    public const string CLOSED = "closed";
}

public class RoomBooking
{
    public string RoomType { get; }
    public decimal PricePerNight { get; }

    public RoomBooking(string roomType, decimal pricePerNight)
    {
        RoomType = roomType;
        PricePerNight = pricePerNight;
    }

    public override string ToString()
    {
        return $"Room Type: {RoomType}, Price per Night: {PricePerNight}";
    }
}

public class HotelBuilder
{
    private string _name = "Unnamed Hotel";
    private decimal _costPerNight = 50;
    private string _type = RoomType.SINGLE;
    private string _status = BookingStatus.AVAILABLE;
    private List<RoomBooking> _roomBookings = new List<RoomBooking>();

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public decimal CostPerNight
    {
        get => _costPerNight;
        set => _costPerNight = value;
    }

    public string Type
    {
        get => _type;
        set => _type = value;
    }

    public string Status
    {
        get => _status;
        set => _status = value;
    }

    public List<RoomBooking> RoomBookings
    {
        get => _roomBookings;
        set => _roomBookings = value;
    }

    public Hotel Build()
    {
        return new Hotel(this);
    }

    public static HotelBuilder Builder => new HotelBuilder();
}

public class Hotel
{
    public string Name { get; }
    public decimal CostPerNight { get; }
    public string Type { get; }
    public string Status { get; }
    public List<RoomBooking> RoomBookings { get; }

    public Hotel(HotelBuilder builder)
    {
        Name = builder.Name;
        CostPerNight = builder.CostPerNight;
        Type = builder.Type;
        Status = builder.Status;
        RoomBookings = builder.RoomBookings;
    }

    public override string ToString()
    {
        var infoStr = $"Hotel: {Name}\n";
        infoStr += $"Type: {Type}\n";
        infoStr += $"Rooms:\n";
        foreach (var room in RoomBookings)
        {
            infoStr += $"- {room.ToString()}\n";
        }
        return infoStr;
    }
}

class Program
{
    static void Main()
    {
        var hotelBuilder = HotelBuilder.Builder;

        hotelBuilder.Name = "Sunset Hotel";
        hotelBuilder.CostPerNight = 100;
        hotelBuilder.Type = RoomType.SUITE;
        hotelBuilder.Status = BookingStatus.AVAILABLE;
        hotelBuilder.RoomBookings = new List<RoomBooking>
        {
            new RoomBooking(RoomType.SINGLE, 75.0m),
            new RoomBooking(RoomType.SUITE, 200.0m),
        };

        var hotel = hotelBuilder.Build();
        Console.WriteLine(hotel.ToString());
        
        // Replacing the usage of Repeat with a loop to print separator
        for (int i = 0; i < 20; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine();

        var newHotelBuilder = HotelBuilder.Builder;
        newHotelBuilder.Name = "Oceanview Hotel";
        newHotelBuilder.CostPerNight = 150;
        newHotelBuilder.Type = RoomType.DOUBLE;
        newHotelBuilder.Status = BookingStatus.AVAILABLE;
        newHotelBuilder.RoomBookings = new List<RoomBooking>
        {
            new RoomBooking(RoomType.SINGLE, 75.0m),
            new RoomBooking(RoomType.DOUBLE, 100.0m),
            new RoomBooking(RoomType.SUITE, 200.0m),
            new RoomBooking(RoomType.SINGLE, 75.0m),
        };

        var newHotel = newHotelBuilder.Build();
        Console.WriteLine(newHotel.ToString());
    }
}
