using System;

// HotelBooking class
class HotelBooking
{
    private string _roomType;
    private DateTime _checkInDate;
    private DateTime _checkOutDate;
    private string _userName;

    public HotelBooking(string roomType, DateTime checkInDate, DateTime checkOutDate, string userName)
    {
        _roomType = roomType;
        _checkInDate = checkInDate;
        _checkOutDate = checkOutDate;
        _userName = userName;
    }

    public string RoomType => _roomType;
    public DateTime CheckInDate => _checkInDate;
    public DateTime CheckOutDate => _checkOutDate;
    public string UserName => _userName;

    public void SetRoomType(string roomType) => _roomType = roomType;
    public void SetCheckInDate(DateTime checkInDate) => _checkInDate = checkInDate;
    public void SetCheckOutDate(DateTime checkOutDate) => _checkOutDate = checkOutDate;
    public void SetUserName(string userName) => _userName = userName;

    public HotelBookingSnapshot CreateSnapshot()
    {
        return new HotelBookingSnapshot(this, _roomType, _checkInDate, _checkOutDate, _userName);
    }
}

// HotelBookingSnapshot class
class HotelBookingSnapshot
{
    private readonly HotelBooking _booking;
    private readonly string _roomType;
    private readonly DateTime _checkInDate;
    private readonly DateTime _checkOutDate;
    private readonly string _userName;

    public HotelBookingSnapshot(HotelBooking booking, string roomType, DateTime checkInDate, DateTime checkOutDate, string userName)
    {
        _booking = booking;
        _roomType = roomType;
        _checkInDate = checkInDate;
        _checkOutDate = checkOutDate;
        _userName = userName;
    }

    public void Restore()
    {
        _booking.SetRoomType(_roomType);
        _booking.SetCheckInDate(_checkInDate);
        _booking.SetCheckOutDate(_checkOutDate);
        _booking.SetUserName(_userName);
    }
}

// BookingCommand class
class BookingCommand
{
    private readonly HotelBooking _booking;
    private HotelBookingSnapshot? _backup; // Declare as nullable

    public BookingCommand(HotelBooking booking)
    {
        _booking = booking;
        _backup = null; // Initialize as null
    }

    public void MakeBackup()
    {
        _backup = _booking.CreateSnapshot();
    }

    public void RestoreBackup()
    {
        if (_backup != null)
        {
            _backup.Restore();
        }
        else
        {
            Console.WriteLine("No backup to restore.");
        }
    }
}

// Example usage
class Program
{
    static void Main()
    {
        var hotelBooking = new HotelBooking(
            "Deluxe Room",
            new DateTime(2024, 10, 15),
            new DateTime(2024, 10, 20),
            "John Doe"
        );

        // Update booking
        hotelBooking.SetRoomType("Executive Suite");
        hotelBooking.SetCheckInDate(new DateTime(2024, 10, 18));
        hotelBooking.SetCheckOutDate(new DateTime(2024, 10, 25));
        hotelBooking.SetUserName("Jane Smith");

        // Create command for managing backups
        var command = new BookingCommand(hotelBooking);

        // Make backup of the current state
        command.MakeBackup();

        Console.WriteLine("Current booking:");
        Console.WriteLine($"Room type: {hotelBooking.RoomType}");
        Console.WriteLine($"Check-in date: {hotelBooking.CheckInDate}");
        Console.WriteLine($"Check-out date: {hotelBooking.CheckOutDate}");
        Console.WriteLine($"User name: {hotelBooking.UserName}");

        // Update booking
        hotelBooking.SetRoomType("Presidential Suite");
        hotelBooking.SetCheckInDate(new DateTime(2024, 11, 1));
        hotelBooking.SetCheckOutDate(new DateTime(2024, 11, 10));
        hotelBooking.SetUserName("Alice Johnson");

        Console.WriteLine("Updated booking:");
        Console.WriteLine($"Room type: {hotelBooking.RoomType}");
        Console.WriteLine($"Check-in date: {hotelBooking.CheckInDate}");
        Console.WriteLine($"Check-out date: {hotelBooking.CheckOutDate}");
        Console.WriteLine($"User name: {hotelBooking.UserName}");

        // Restore the booking to the backup state
        command.RestoreBackup();

        Console.WriteLine("Restored booking:");
        Console.WriteLine($"Room type: {hotelBooking.RoomType}");
        Console.WriteLine($"Check-in date: {hotelBooking.CheckInDate}");
        Console.WriteLine($"Check-out date: {hotelBooking.CheckOutDate}");
        Console.WriteLine($"User name: {hotelBooking.UserName}");
    }
}
