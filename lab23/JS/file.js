// Abstract class for elements in the hotel booking system
class HotelBookingSystemElement {
    accept(visitor) {
      throw new Error("Method not implemented");
    }
  }
  
  // Abstract class for visitors
  class HotelBookingSystemVisitor {
    visitRoom(room) {
      throw new Error("Method not implemented");
    }
  
    visitReservation(reservation) {
      throw new Error("Method not implemented");
    }
  
    visitUser(user) {
      throw new Error("Method not implemented");
    }
  }
  
  // Concrete class for Room
  class Room extends HotelBookingSystemElement {
    constructor(roomNumber, type) {
      super();
      this.roomNumber = roomNumber;
      this.type = type;
    }
  
    accept(visitor) {
      visitor.visitRoom(this);
    }
  
    display() {
      console.log(`Displaying Room: ${this.type} - Room Number: ${this.roomNumber}`);
    }
  }
  
  // Concrete class for Reservation
  class Reservation extends HotelBookingSystemElement {
    constructor(room, user) {
      super();
      this.room = room;
      this.user = user;
    }
  
    accept(visitor) {
      visitor.visitReservation(this);
    }
  
    display() {
      console.log(
        `Reservation for Room: ${this.room.type} - Room Number: ${this.room.roomNumber} for User: ${this.user.name}`
      );
    }
  }
  
  // Concrete class for User
  class User extends HotelBookingSystemElement {
    constructor(name) {
      super();
      this.name = name;
    }
  
    accept(visitor) {
      visitor.visitUser(this);
    }
  
    display() {
      console.log(`User: ${this.name}`);
    }
  }
  
  // Concrete class for XML Exporter
  class XMLExporter extends HotelBookingSystemVisitor {
    visitRoom(room) {
      console.log(`Exporting Room to XML: ${room.type} - Room Number: ${room.roomNumber}`);
    }
  
    visitReservation(reservation) {
      console.log(`Exporting Reservation to XML:`);
      reservation.display();
    }
  
    visitUser(user) {
      console.log(`Exporting User to XML: ${user.name}`);
    }
  }
  
  // Example usage
  const room = new Room(101, "Deluxe");
  const user = new User("Alice");
  const reservation = new Reservation(room, user);
  
  const xmlExporter = new XMLExporter();
  
  room.accept(xmlExporter);
  user.accept(xmlExporter);
  reservation.accept(xmlExporter);
  