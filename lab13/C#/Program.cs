class HotelRoom
{
    public string RoomType { get; }
    public string CheckInDate { get; }

    public HotelRoom(string roomType, string checkInDate)
    {
        RoomType = roomType;
        CheckInDate = checkInDate;
    }

    public static HotelRoom FromRoom(HotelRoom sharedState)
    {
        return new HotelRoom(sharedState.RoomType, sharedState.CheckInDate);
    }

    public override string ToString()
    {
        return $"({RoomType}, Check-in: {CheckInDate})";
    }

    public override bool Equals(object? obj) // Nullable object type for obj
    {
        // Check for null and type compatibility
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        // Perform the equality check
        HotelRoom other = (HotelRoom)obj;
        return RoomType == other.RoomType && CheckInDate == other.CheckInDate;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(RoomType, CheckInDate);
    }
}
class RoomFlyWeightFactory
{
    private List<HotelRoom> flyweights = new List<HotelRoom>();

    public HotelRoom GetFlyWeight(HotelRoom sharedState)
    {
        var state = flyweights.Find(element => sharedState.Equals(element));
        if (state == null)
        {
            state = sharedState;
            flyweights.Add(sharedState);
        }
        return state;
    }

    public int Total => flyweights.Count;
}

// Proxy Pattern
interface IBooking
{
    BookingContext MakeBooking(string bookingID, HotelRoom sharedState);
}

class BookingMaker : IBooking
{
    private RoomFlyWeightFactory flyWeightFactory;

    public BookingMaker(RoomFlyWeightFactory flyWeightFactory)
    {
        this.flyWeightFactory = flyWeightFactory;
    }

    public BookingContext MakeBooking(string bookingID, HotelRoom sharedState)
    {
        var flyWeight = flyWeightFactory.GetFlyWeight(sharedState);
        return new BookingContext(bookingID, flyWeight);
    }
}

class ProxyBookingMaker : IBooking
{
    private IBooking subject;

    public ProxyBookingMaker(IBooking subject)
    {
        this.subject = subject;
    }

    public BookingContext MakeBooking(string bookingID, HotelRoom sharedState)
    {
        Logging(bookingID, sharedState);
        return subject.MakeBooking(bookingID, sharedState);
    }

    private void Logging(string bookingID, HotelRoom sharedState)
    {
        Console.WriteLine($"Logging -> Booking ID: {bookingID} || Room: {sharedState}");
    }
}

class BookingContext
{
    public string BookingID { get; }
    public HotelRoom SharedState { get; }

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

// Example Usage
class Program
{
    static void Main()
    {
        var flyweightFactory = new RoomFlyWeightFactory();
        var bookingMaker = new BookingMaker(flyweightFactory);
        var proxyBookingMaker = new ProxyBookingMaker(bookingMaker);

        var sharedRoom1 = new HotelRoom("Single", "2024-12-01");
        var sharedRoom2 = new HotelRoom("Single", "2024-12-01");

        var booking1 = proxyBookingMaker.MakeBooking("Booking1", sharedRoom1);
        var booking2 = proxyBookingMaker.MakeBooking("Booking2", sharedRoom2);

        Console.WriteLine(booking1.ToString());
        Console.WriteLine(booking2.ToString());

        Console.WriteLine($"Total unique rooms: {flyweightFactory.Total}");
    }
}
