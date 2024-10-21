public class BookingBase
{
    private readonly decimal _cost;

    public BookingBase(decimal cost)
    {
        _cost = cost;
    }

    public virtual decimal Cost()
    {
        return _cost;
    }

    public virtual string Description() // Add Description method
    {
        return "Base Booking";
    }
}

// Decorator for booking
public class BookingDecorator : BookingBase
{
    protected readonly BookingBase _bookingBase;

    public BookingDecorator(BookingBase bookingBase) : base(bookingBase.Cost())
    {
        _bookingBase = bookingBase;
    }

    public override decimal Cost()
    {
        return _bookingBase.Cost();
    }

    public override string Description() // Ensure it's overridden
    {
        return _bookingBase.Description();
    }
}

// Decorator for standard room
public class StandardRoom : BookingDecorator
{
    private readonly decimal _cost;

    public StandardRoom(decimal cost, BookingBase bookingBase) : base(bookingBase)
    {
        _cost = cost;
    }

    public override decimal Cost()
    {
        return _bookingBase.Cost() + _cost;
    }

    public override string Description()
    {
        return "Standard Room";
    }
}

// Decorator for VIP room
public class VIPRoom : BookingDecorator
{
    private readonly decimal _cost;

    public VIPRoom(decimal cost, BookingBase bookingBase) : base(bookingBase)
    {
        _cost = cost;
    }

    public override decimal Cost()
    {
        return _bookingBase.Cost() + _cost;
    }

    public override string Description()
    {
        return "VIP Room";
    }
}

// Decorator for child room
public class ChildRoom : BookingDecorator
{
    private readonly decimal _cost;

    public ChildRoom(decimal cost, BookingBase bookingBase) : base(bookingBase)
    {
        _cost = cost;
    }

    public override decimal Cost()
    {
        return _bookingBase.Cost() + _cost;
    }

    public override string Description()
    {
        return "Child Room";
    }
}

// Function to print booking information
public static class BookingInfoPrinter
{
    public static void PrintBookingInfo(BookingBase booking)
    {
        Console.WriteLine($"Room Type: {booking.Description()} - Cost: {booking.Cost()}");
    }
}

// Main function to demonstrate usage
public class Program
{
    public static void Main()
    {
        var baseBooking = new BookingBase(50); // Base booking cost
        Console.WriteLine($"Base booking cost = {baseBooking.Cost()}");

        var standardRoom = new StandardRoom(30, baseBooking);
        BookingInfoPrinter.PrintBookingInfo(standardRoom);

        var vipRoom = new VIPRoom(100, baseBooking);
        BookingInfoPrinter.PrintBookingInfo(vipRoom);

        var childRoom = new ChildRoom(20, baseBooking);
        BookingInfoPrinter.PrintBookingInfo(childRoom);
    }
}
