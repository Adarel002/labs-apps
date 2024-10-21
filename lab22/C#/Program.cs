using System;

// Abstract class for Hotel Booking System
public abstract class HotelBookingSystem
{
    public void Process()
    {
        CheckAvailability();
        ReserveRooms();
        ProcessPayment();
        SendConfirmation();
    }

    protected void CheckAvailability()
    {
        Console.WriteLine("Checking room availability...");
    }

    // Abstract methods to be implemented by derived classes
    protected abstract void ReserveRooms();
    protected abstract void ProcessPayment();

    protected void SendConfirmation()
    {
        Console.WriteLine("Sending confirmation email to customer...");
    }
}

// Class for Online Booking
public class OnlineBooking : HotelBookingSystem
{
    protected override void ReserveRooms()
    {
        Console.WriteLine("Reserving rooms online...");
    }

    protected override void ProcessPayment()
    {
        Console.WriteLine("Processing online payment and finalizing booking...");
    }
}

// Class for Walk-in Booking
public class WalkInBooking : HotelBookingSystem
{
    protected override void ReserveRooms()
    {
        Console.WriteLine("Reserving rooms at the reception...");
    }

    protected override void ProcessPayment()
    {
        Console.WriteLine("Processing payment at the reception...");
    }
}

// Testing the classes
public class Program
{
    public static void Main(string[] args)
    {
        HotelBookingSystem onlineBooking = new OnlineBooking();
        HotelBookingSystem walkInBooking = new WalkInBooking();

        Console.WriteLine("Online booking process:");
        onlineBooking.Process();

        Console.WriteLine("\nWalk-in booking process:");
        walkInBooking.Process();
    }
}
