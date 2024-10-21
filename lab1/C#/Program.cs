public static class RoomType
{
    public const string STANDARD = "standard";
    public const string FAMILY = "family";
    public const string LUXURY = "luxury";
}

public static class HotelName
{
    public const string MAIN = "Main";
    public const string BOUTIQUE = "Boutique";
    public const string BUSINESS = "Business";
}

public static class BookingStatus
{
    public const string AVAILABLE = "available";
    public const string RESERVED = "reserved";
    public const string OCCUPIED = "occupied";
}

public static class CheckInTime
{
    public const string MORNING = "morning";
    public const string AFTERNOON = "afternoon";
    public const string EVENING = "evening";
    public const string NIGHT = "night";
}

public class HotelRoom
{
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }

    public HotelRoom(string name, decimal cost, string type, string status = BookingStatus.AVAILABLE)
    {
        Name = name;
        Cost = cost;
        Type = type;
        Status = status;
    }

    public void Reserve()
    {
        if (Status == BookingStatus.AVAILABLE)
        {
            Status = BookingStatus.RESERVED;
        }
    }

    public void Occupy()
    {
        if (Status == BookingStatus.RESERVED)
        {
            Status = BookingStatus.OCCUPIED;
        }
    }

    public void UpdateCost(decimal newCost)
    {
        Cost = newCost;
    }

    public override string ToString()
    {
        return $"Ціна номеру: {Cost} (без десяткових знаків), Тип: {Type}\n";
    }
}

public class HotelSingleton
{
    private static HotelSingleton? _instance; // Make it nullable

    public string Name { get; set; }
    public string Type { get; set; }
    public List<HotelRoom> HotelRooms { get; private set; }

    private HotelSingleton()
    {
        Name = "Готель Прем'єр";
        Type = "Luxury";
        HotelRooms = new List<HotelRoom>
        {
            new HotelRoom(HotelName.MAIN, 200, RoomType.STANDARD)
        };
    }

    public static HotelSingleton GetInstance()
    {
        if (_instance == null)
        {
            _instance = new HotelSingleton();
        }
        return _instance;
    }

    public void UpdateRoomPrice(string roomName, decimal newPrice)
    {
        foreach (var room in HotelRooms)
        {
            if (room.Name == roomName)
            {
                room.UpdateCost(newPrice);
            }
        }
    }

    public override string ToString()
    {
        var infoStr = $"Готель: {Name}\nТип: {Type}\nНомери:\n";
        foreach (var room in HotelRooms)
        {
            infoStr += $"- {room.ToString()}\n";
        }
        return infoStr;
    }
}

// Main function
class Program
{
    static void Main()
    {
        var hotel = HotelSingleton.GetInstance();
        Console.WriteLine(hotel.ToString());

        Console.WriteLine("---".PadLeft(60, '-'));

        hotel.Name = "Готель Еліт"; // Змінюємо ім'я готелю
        hotel.Type = "Бутік";
        hotel.UpdateRoomPrice(HotelName.MAIN, 250);

        var newHotel = HotelSingleton.GetInstance();
        Console.WriteLine(hotel == newHotel); // Перевіряємо, чи це той самий об'єкт

        Console.WriteLine("---".PadLeft(60, '-'));
        Console.WriteLine(newHotel.ToString());
    }
}
