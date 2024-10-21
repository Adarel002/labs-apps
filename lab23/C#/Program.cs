using System;

// Abstract class for elements in the hotel booking system
public abstract class HotelBookingSystemElement
{
    public abstract void Accept(HotelBookingSystemVisitor visitor);
}

// Abstract class for visitors
public abstract class HotelBookingSystemVisitor
{
    public abstract void VisitRoom(Room room);
    public abstract void VisitReservation(Reservation reservation);
    public abstract void VisitUser(User user);
}

// Concrete class for Room
public class Room : HotelBookingSystemElement
{
    public int RoomNumber { get; }
    public string Type { get; }

    public Room(int roomNumber, string type)
    {
        RoomNumber = roomNumber;
        Type = type;
    }

    public override void Accept(HotelBookingSystemVisitor visitor)
    {
        visitor.VisitRoom(this);
    }

    public void Display()
    {
        Console.WriteLine($"Displaying Room: {Type} - Room Number: {RoomNumber}");
    }
}

// Concrete class for Reservation
public class Reservation : HotelBookingSystemElement
{
    public Room Room { get; }
    public User User { get; }

    public Reservation(Room room, User user)
    {
        Room = room;
        User = user;
    }

    public override void Accept(HotelBookingSystemVisitor visitor)
    {
        visitor.VisitReservation(this);
    }

    public void Display()
    {
        Console.WriteLine($"Reservation for Room: {Room.Type} - Room Number: {Room.RoomNumber} for User: {User.Name}");
    }
}

// Concrete class for User
public class User : HotelBookingSystemElement
{
    public string Name { get; }

    public User(string name)
    {
        Name = name;
    }

    public override void Accept(HotelBookingSystemVisitor visitor)
    {
        visitor.VisitUser(this);
    }

    public void Display()
    {
        Console.WriteLine($"User: {Name}");
    }
}

// Concrete class for XML Exporter
public class XMLExporter : HotelBookingSystemVisitor
{
    public override void VisitRoom(Room room)
    {
        Console.WriteLine($"Exporting Room to XML: {room.Type} - Room Number: {room.RoomNumber}");
    }

    public override void VisitReservation(Reservation reservation)
    {
        Console.WriteLine("Exporting Reservation to XML:");
        reservation.Display();
    }

    public override void VisitUser(User user)
    {
        Console.WriteLine($"Exporting User to XML: {user.Name}");
    }
}

// Example usage
public class Program
{
    public static void Main(string[] args)
    {
        var room = new Room(101, "Deluxe");
        var user = new User("Alice");
        var reservation = new Reservation(room, user);

        var xmlExporter = new XMLExporter();

        room.Accept(xmlExporter);
        user.Accept(xmlExporter);
        reservation.Accept(xmlExporter);
    }
}
