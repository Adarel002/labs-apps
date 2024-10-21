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

    public HotelRoom Clone()
    {
        return new HotelRoom(Name, Cost, Type, Status);
    }

    public override string ToString()
    {
        return $"Ціна номеру: {Cost}, Тип: {Type}, Статус: {Status}\n";
    }
}

public class HotelPrototype
{
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public List<HotelRoom> HotelRooms { get; private set; } = new List<HotelRoom>(); // Initialize here

    public HotelPrototype(string name = "Unnamed Hotel", decimal cost = 100, string type = RoomType.STANDARD, string status = BookingStatus.AVAILABLE, List<HotelRoom>? hotelRooms = null)
    {
        Name = name;
        Cost = cost;
        Type = type;
        Status = status;
        HotelRooms = hotelRooms ?? new List<HotelRoom>(); // Use null-coalescing operator
    }

    public static HotelPrototype FromHotel(HotelPrototype hotel)
    {
        return new HotelPrototype
        {
            Name = hotel.Name,
            Cost = hotel.Cost,
            Type = hotel.Type,
            Status = hotel.Status,
            HotelRooms = hotel.HotelRooms.ConvertAll(room => room.Clone())
        };
    }

    public HotelPrototype Clone()
    {
        return FromHotel(this);
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

// Main function equivalent
class Program
{
    static void Main()
    {
        var hotel = new HotelPrototype(
            name: "Готель Прем'єр",
            hotelRooms: new List<HotelRoom>
            {
                new HotelRoom(HotelName.BOUTIQUE, 200, RoomType.STANDARD),
                new HotelRoom(HotelName.MAIN, 400, RoomType.LUXURY),
            }
        );

        Console.WriteLine(hotel.ToString());

        Console.WriteLine("-----".PadLeft(80, '-') + "Нове бронювання" + "----".PadLeft(80, '-'));

        var newHotel = hotel.Clone();
        newHotel.HotelRooms.Add(new HotelRoom(HotelName.BUSINESS, 300, RoomType.FAMILY));

        Console.WriteLine(newHotel.ToString());
        Console.WriteLine("------".PadLeft(80, '-') + "Оригінал" + "----".PadLeft(80, '-'));
        Console.WriteLine(hotel.ToString());
    }
}
