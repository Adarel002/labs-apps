public enum RoomType
{
    SINGLE,
    DOUBLE,
    SUITE,
    NONE
}

// RequestBooking class to represent the booking request
public class RequestBooking
{
    private List<string> _description;
    private RoomType _roomType;

    public RequestBooking(List<string> description, RoomType roomType)
    {
        _description = description;
        _roomType = roomType;
    }

    public List<string> Description
    {
        get { return _description; }
    }

    public RoomType RoomType
    {
        get { return _roomType; }
    }
}

// Abstract Handler class
public abstract class Handler
{
    protected Handler? _successor; // Nullable reference type

    public Handler(Handler? successor = null) // Allow null by using nullable reference
    {
        _successor = successor;
    }

    public void Handle(RequestBooking request)
    {
        bool result = _checkRequest(request.RoomType);
        if (!result && _successor != null)
        {
            _successor.Handle(request);
        }
    }

    protected abstract bool _checkRequest(RoomType roomType);
}

// RoomAvailabilityHandler checks the availability of rooms
public class RoomAvailabilityHandler : Handler
{
    public RoomAvailabilityHandler(Handler? successor = null) : base(successor) { }

    protected override bool _checkRequest(RoomType roomType)
    {
        bool available = roomType != RoomType.NONE;
        if (available)
        {
            Console.WriteLine("RoomAvailabilityHandler processes the room request");
        }
        else
        {
            Console.WriteLine("RoomAvailabilityHandler rejects the booking request");
        }
        return !available;
    }
}

// PaymentHandler processes the payment
public class PaymentHandler : Handler
{
    public PaymentHandler(Handler? successor = null) : base(successor) { }

    protected override bool _checkRequest(RoomType roomType)
    {
        bool paymentProcessed = roomType == RoomType.SINGLE || roomType == RoomType.DOUBLE || roomType == RoomType.SUITE;
        if (paymentProcessed)
        {
            Console.WriteLine("Payment is being processed");
        }
        else
        {
            Console.WriteLine("Payment failed or invalid room type");
        }
        return !paymentProcessed;
    }
}

// BookingConfirmationHandler confirms the booking
public class BookingConfirmationHandler : Handler
{
    public BookingConfirmationHandler(Handler? successor = null) : base(successor) { }

    protected override bool _checkRequest(RoomType roomType)
    {
        bool confirmed = roomType == RoomType.SINGLE || roomType == RoomType.DOUBLE || roomType == RoomType.SUITE;
        if (confirmed)
        {
            Console.WriteLine("Booking confirmed! Enjoy your stay.");
        }
        else
        {
            Console.WriteLine("Booking confirmation failed");
        }
        return !confirmed;
    }
}

// Main function to run the process
class Program
{
    static void Main(string[] args)
    {
        // Create the handlers and link them
        Handler bookingConfirmation = new BookingConfirmationHandler();
        Handler payment = new PaymentHandler(bookingConfirmation);
        Handler roomAvailability = new RoomAvailabilityHandler(payment);

        // Create a booking request
        List<string> requestDescription = new List<string> { "Room: Double", "Check-in: 2024-12-01", "Nights: 2" };
        RequestBooking requestBooking = new RequestBooking(requestDescription, RoomType.DOUBLE);

        // Start the chain of responsibility
        roomAvailability.Handle(requestBooking);
    }
}
