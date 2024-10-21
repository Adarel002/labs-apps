// Інтерфейс Mediator
class Mediator {
    notify(sender, event) {}
  }
  
  class HotelMediator extends Mediator {
    constructor() {
      super();
      this.roomSales = null;
      this.bookingSystem = null;
    }
  
    setRoomSales(roomSales) {
      this.roomSales = roomSales;
    }
  
    setBookingSystem(bookingSystem) {
      this.bookingSystem = bookingSystem;
    }
  
    notify(sender, event) {
      if (sender === this.roomSales) {
        console.log(
          "Mediator reacts to RoomSales' event and updates BookingSystem."
        );
        this.bookingSystem.updateAvailability();
      } else if (sender === this.bookingSystem) {
        console.log(
          "Mediator reacts to BookingSystem's event and updates RoomSales."
        );
        this.roomSales.updateSales();
      }
    }
  }
  
  // Колеги
  class Colleague {
    constructor(mediator) {
      this.mediator = mediator;
    }
  
    doSomething() {}
  }
  
  class RoomSales extends Colleague {
    constructor(mediator) {
      super(mediator);
    }
  
    doSomething() {
      console.log("RoomSales handles a new room sale.");
      this.mediator.notify(this, "sale");
    }
  
    updateSales() {
      console.log("RoomSales updates the sales data.");
    }
  }
  
  class BookingSystem extends Colleague {
    constructor(mediator) {
      super(mediator);
    }
  
    doSomething() {
      console.log("BookingSystem processes a new booking.");
      this.mediator.notify(this, "booking");
    }
  
    updateAvailability() {
      console.log("BookingSystem updates room availability.");
    }
  }
  
  // Основна частина
  function main() {
    const mediator = new HotelMediator();
  
    const roomSales = new RoomSales(mediator);
    const bookingSystem = new BookingSystem(mediator);
  
    mediator.setRoomSales(roomSales);
    mediator.setBookingSystem(bookingSystem);
  
    roomSales.doSomething();
    bookingSystem.doSomething();
  }
  
  main();
  