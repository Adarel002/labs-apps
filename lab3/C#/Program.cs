public static class RoomType
{
    public const string Standard = "Standard";
    public const string Premium = "Premium";
    public const string VIP = "VIP";
}

public abstract class Room
{
    public string HotelName { get; }
    public string RoomNumber { get; }
    public int StayDuration { get; } // in days

    protected Room(string hotelName, string roomNumber, int stayDuration)
    {
        HotelName = hotelName;
        RoomNumber = roomNumber;
        StayDuration = stayDuration;
    }

    public abstract decimal CostRoom();

    public override string ToString()
    {
        return $"Room in hotel {HotelName}, room number {RoomNumber}, for {StayDuration} night(s)";
    }
}

public class StandardRoom : Room
{
    private decimal BasePricePerNight { get; }

    public StandardRoom(string hotelName, string roomNumber, int stayDuration, decimal basePricePerNight)
        : base(hotelName, roomNumber, stayDuration)
    {
        BasePricePerNight = basePricePerNight;
    }

    public override decimal CostRoom()
    {
        return BasePricePerNight * StayDuration;
    }
}

public class PremiumRoom : Room
{
    private decimal BasePricePerNight { get; }
    private decimal PremiumMultiplier { get; }

    public PremiumRoom(string hotelName, string roomNumber, int stayDuration, decimal basePricePerNight, decimal premiumMultiplier)
        : base(hotelName, roomNumber, stayDuration)
    {
        BasePricePerNight = basePricePerNight;
        PremiumMultiplier = premiumMultiplier;
    }

    public override decimal CostRoom()
    {
        return BasePricePerNight * PremiumMultiplier * StayDuration;
    }

    public override string ToString()
    {
        return base.ToString() + " with Premium services";
    }
}

public class VIPRoom : Room
{
    private decimal BasePricePerNight { get; }
    private decimal VipMultiplier { get; }
    private decimal ExtraServiceFee { get; }

    public VIPRoom(string hotelName, string roomNumber, int stayDuration, decimal basePricePerNight, decimal vipMultiplier, decimal extraServiceFee)
        : base(hotelName, roomNumber, stayDuration)
    {
        BasePricePerNight = basePricePerNight;
        VipMultiplier = vipMultiplier;
        ExtraServiceFee = extraServiceFee;
    }

    public override decimal CostRoom()
    {
        return (BasePricePerNight * VipMultiplier * StayDuration) + ExtraServiceFee;
    }

    public override string ToString()
    {
        return base.ToString() + " with VIP services";
    }
}

public static class RoomFactory
{
    public static Room CreateRoom(string roomType, string hotelName, string roomNumber, int stayDuration)
    {
        switch (roomType)
        {
            case RoomType.Standard:
                return new StandardRoom(hotelName, roomNumber, stayDuration, 100.0m);
            case RoomType.Premium:
                return new PremiumRoom(hotelName, roomNumber, stayDuration, 100.0m, 1.5m);
            case RoomType.VIP:
                return new VIPRoom(hotelName, roomNumber, stayDuration, 100.0m, 2.0m, 50.0m);
            default:
                throw new ArgumentException("Invalid room type");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var roomTypes = new List<string> { RoomType.Standard, RoomType.Premium, RoomType.VIP };

        foreach (var type in roomTypes)
        {
            var room = RoomFactory.CreateRoom(type, "Grand Hotel", "101", 3);
            Console.WriteLine($"{room.ToString()} with cost = ${room.CostRoom()}");
        }
    }
}
