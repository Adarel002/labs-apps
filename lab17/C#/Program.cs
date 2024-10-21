public abstract class Mediator
{
    public abstract void Notify(Colleague sender, string eventMessage);
}

// HotelMediator class
public class HotelMediator : Mediator
{
    private RoomSales? _roomSales;  // Made nullable
    private BookingSystem? _bookingSystem;  // Made nullable

    public void SetRoomSales(RoomSales roomSales)
    {
        _roomSales = roomSales;
    }

    public void SetBookingSystem(BookingSystem bookingSystem)
    {
        _bookingSystem = bookingSystem;
    }

    public override void Notify(Colleague sender, string eventMessage)
    {
        if (sender == _roomSales)
        {
            Console.WriteLine("Mediator reacts to RoomSales' event and updates BookingSystem.");
            _bookingSystem?.UpdateAvailability(); // Use null-conditional operator
        }
        else if (sender == _bookingSystem)
        {
            Console.WriteLine("Mediator reacts to BookingSystem's event and updates RoomSales.");
            _roomSales?.UpdateSales(); // Use null-conditional operator
        }
    }
}

// Colleague class
public abstract class Colleague
{
    protected Mediator _mediator;

    protected Colleague(Mediator mediator)
    {
        _mediator = mediator;
    }

    public abstract void DoSomething();
}

// RoomSales class
public class RoomSales : Colleague
{
    public RoomSales(Mediator mediator) : base(mediator) { }

    public override void DoSomething()
    {
        Console.WriteLine("RoomSales handles a new room sale.");
        _mediator.Notify(this, "sale");
    }

    public void UpdateSales()
    {
        Console.WriteLine("RoomSales updates the sales data.");
    }
}

// BookingSystem class
public class BookingSystem : Colleague
{
    public BookingSystem(Mediator mediator) : base(mediator) { }

    public override void DoSomething()
    {
        Console.WriteLine("BookingSystem processes a new booking.");
        _mediator.Notify(this, "booking");
    }

    public void UpdateAvailability()
    {
        Console.WriteLine("BookingSystem updates room availability.");
    }
}

// Main function
public class Program
{
    public static void Main(string[] args)
    {
        var mediator = new HotelMediator();

        var roomSales = new RoomSales(mediator);
        var bookingSystem = new BookingSystem(mediator);

        mediator.SetRoomSales(roomSales);
        mediator.SetBookingSystem(bookingSystem);

        roomSales.DoSomething();
        bookingSystem.DoSomething();
    }
}
