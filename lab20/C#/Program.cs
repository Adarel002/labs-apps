using System;

// Abstract state class for Booking
public abstract class BookingState
{
    public abstract void Handle(BookingContext context);
    
    // Override the ToString method from the object class
    public override string ToString()
    {
        return "Booking State";
    }
}

// RoomSelection state
public class RoomSelection : BookingState
{
    public override void Handle(BookingContext context)
    {
        Console.WriteLine("Room selected! Moving to date selection.");
        context.State = new DateSelection();
    }

    // Override the ToString method
    public override string ToString()
    {
        return "Selecting Room";
    }
}

// DateSelection state
public class DateSelection : BookingState
{
    public override void Handle(BookingContext context)
    {
        Console.WriteLine("Dates selected! Moving to payment confirmation.");
        context.State = new PaymentConfirmation();
    }

    // Override the ToString method
    public override string ToString()
    {
        return "Selecting Dates";
    }
}

// PaymentConfirmation state
public class PaymentConfirmation : BookingState
{
    public override void Handle(BookingContext context)
    {
        Console.WriteLine("Payment confirmed! Booking complete.");
        context.State = new BookingComplete();
    }

    // Override the ToString method
    public override string ToString()
    {
        return "Confirming Payment";
    }
}

// BookingComplete state
public class BookingComplete : BookingState
{
    public override void Handle(BookingContext context)
    {
        Console.WriteLine("Booking is already complete.");
    }

    // Override the ToString method
    public override string ToString()
    {
        return "Booking Complete";
    }
}

// Context class for Booking
public class BookingContext
{
    private BookingState _state;

    public BookingContext(BookingState state)
    {
        _state = state;
    }

    public BookingState State
    {
        get => _state;
        set => _state = value;
    }

    public void NextStep()
    {
        Console.WriteLine("Proceeding to the next step...");
        _state.Handle(this);
    }
}

// Main function to run the application
public class Program
{
    public static void Main(string[] args)
    {
        BookingContext booking = new BookingContext(new RoomSelection());

        Console.WriteLine($"Current state: {booking.State}");
        booking.NextStep(); // Select room

        Console.WriteLine($"Current state: {booking.State}");
        booking.NextStep(); // Select dates

        Console.WriteLine($"Current state: {booking.State}");
        booking.NextStep(); // Confirm payment

        Console.WriteLine($"Current state: {booking.State}");
    }
}
