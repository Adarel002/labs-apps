// Command interface
class ICommand {
    execute() {}
  }
  
  // Hotel entities
  class Room {
    constructor(type, availability) {
      this.type = type;
      this.availability = availability;
    }
  
    showRoomDetails() {
      console.log(`Room Type: ${this.type}`);
      console.log(`Available: ${this.availability}`);
    }
  }
  
  class Guest {
    constructor(name) {
      this.name = name;
    }
  
    browseRooms(rooms) {
      console.log(`${this.name} is browsing available rooms`);
      rooms.forEach((room) => room.showRoomDetails());
    }
  
    selectRoom(room) {
      console.log(`${this.name} selected room type: ${room.type}`);
    }
  
    makePayment() {
      console.log(`${this.name} is making payment for the room`);
    }
  }
  
  class BookingSystem {
    confirmBooking() {
      console.log("Room booking confirmed");
    }
  
    cancelBooking() {
      console.log("Room booking cancelled");
    }
  }
  
  // Commands
  class BrowseRoomsCommand extends ICommand {
    constructor(guest, rooms) {
      super();
      this.guest = guest;
      this.rooms = rooms;
    }
  
    execute() {
      this.guest.browseRooms(this.rooms);
    }
  }
  
  class SelectRoomCommand extends ICommand {
    constructor(guest, room) {
      super();
      this.guest = guest;
      this.room = room;
    }
  
    execute() {
      this.guest.selectRoom(this.room);
    }
  }
  
  class MakePaymentCommand extends ICommand {
    constructor(guest) {
      super();
      this.guest = guest;
    }
  
    execute() {
      this.guest.makePayment();
    }
  }
  
  class ConfirmBookingCommand extends ICommand {
    constructor(bookingSystem) {
      super();
      this.bookingSystem = bookingSystem;
    }
  
    execute() {
      this.bookingSystem.confirmBooking();
    }
  }
  
  // History Manager
  class BookingProcess {
    constructor() {
      this.history = [];
    }
  
    addCommand(command) {
      this.history.push(command);
    }
  
    completeBooking() {
      if (this.history.length > 0) {
        this.history.forEach((command) => command.execute());
      } else {
        console.log("No actions to execute");
      }
      this.history = []; // Clear the history after execution
    }
  }
  
  // Example usage
  const main = () => {
    const guest = new Guest("Alice");
    const room1 = new Room("Double", true);
    const room2 = new Room("Suite", true);
  
    const bookingProcess = new BookingProcess();
  
    // Browse available rooms
    bookingProcess.addCommand(new BrowseRoomsCommand(guest, [room1, room2]));
  
    // Select a room type
    bookingProcess.addCommand(new SelectRoomCommand(guest, room1));
  
    // Make payment
    bookingProcess.addCommand(new MakePaymentCommand(guest));
  
    // Confirm booking
    const bookingSystem = new BookingSystem();
    bookingProcess.addCommand(new ConfirmBookingCommand(bookingSystem));
  
    // Execute all actions in the process
    bookingProcess.completeBooking();
  };
  
  // Run the example
  main();
  