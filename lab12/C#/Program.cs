public class HotelRoom
{
    public string RoomType { get; private set; }
    public string CheckInDate { get; private set; }

    public HotelRoom(string roomType, string checkInDate)
    {
        RoomType = roomType;
        CheckInDate = checkInDate;
    }

    public static HotelRoom FromRoom(HotelRoom room)
    {
        return new HotelRoom(room.RoomType, room.CheckInDate);
    }

    public override string ToString()
    {
        return $"({RoomType}, Check-in: {CheckInDate})";
    }

    public override bool Equals(object? obj) // Use 'object?' to allow null values
{
    // Check if the obj is null
    if (obj is null) return false;
    
    if (obj is HotelRoom room)
    {
        return RoomType == room.RoomType && CheckInDate == room.CheckInDate;
    }

    return false;
}


    public override int GetHashCode()
    {
        return (RoomType, CheckInDate).GetHashCode();
    }
}

// Контекст для бронювання
public class BookingContext
{
    public string BookingID { get; private set; }
    public HotelRoom SharedState { get; private set; }

    public BookingContext(string bookingID, HotelRoom sharedState)
    {
        BookingID = bookingID;
        SharedState = sharedState;
    }

    public override string ToString()
    {
        return $"Booking ID: {BookingID} || Room: {SharedState}";
    }
}

// Фабрика для шаблонів номерів (Flyweight)
public class RoomFlyWeightFactory
{
    private List<HotelRoom> flyweights = new List<HotelRoom>();

    public HotelRoom GetFlyWeight(HotelRoom sharedState)
    {
        var state = flyweights.FirstOrDefault(f => f.Equals(sharedState));

        if (state == null)
        {
            state = sharedState;
            flyweights.Add(sharedState);
        }

        return state;
    }

    public int Total => flyweights.Count;
}

// Клас для бронювання номеру
public class HotelBookingMarket
{
    private string UniqueState { get; set; }
    private HotelRoom SharedState { get; set; }

    private HotelBookingMarket(string uniqueState, HotelRoom sharedState)
    {
        UniqueState = uniqueState;
        SharedState = sharedState;
    }

    public static HotelBookingMarket MakeHotelBooking(string uniqueState, HotelRoom sharedState, RoomFlyWeightFactory factory)
    {
        var flyweight = factory.GetFlyWeight(sharedState);
        return new HotelBookingMarket(uniqueState, flyweight);
    }

    public override string ToString()
    {
        return $"Booking for {UniqueState} with room: {SharedState}";
    }
}

// Основна програма
class Program
{
    static void Main(string[] args)
    {
        var flyweightFactory = new RoomFlyWeightFactory();

        // Створення унікальних бронювань номерів з однаковими загальними станами
        var room1 = new HotelRoom("Single", "2024-12-01");
        var room2 = new HotelRoom("Single", "2024-12-01");
        var room3 = new HotelRoom("Suite", "2024-12-05");

        var booking1 = HotelBookingMarket.MakeHotelBooking("Booking001", room1, flyweightFactory);
        var booking2 = HotelBookingMarket.MakeHotelBooking("Booking002", room2, flyweightFactory);
        var booking3 = HotelBookingMarket.MakeHotelBooking("Booking003", room3, flyweightFactory);

        Console.WriteLine(booking1.ToString());
        Console.WriteLine(booking2.ToString());
        Console.WriteLine(booking3.ToString());

        // Виведення загальної кількості унікальних номерів
        Console.WriteLine($"Total unique rooms: {flyweightFactory.Total}");
    }
}
