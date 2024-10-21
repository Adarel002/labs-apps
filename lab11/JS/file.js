// Типи номерів у готелі
const RoomType = {
    SINGLE: "single",
    DOUBLE: "double",
    SUITE: "suite",
  };
  
  // Інтерфейс для номеру
  class IRoom {
    getRoomName() {
      throw new Error("Method getRoomName() must be implemented.");
    }
  }
  
  // Класи для різних типів номерів
  class SingleRoom extends IRoom {
    getRoomName() {
      return "Single Room";
    }
  }
  
  class DoubleRoom extends IRoom {
    getRoomName() {
      return "Double Room";
    }
  }
  
  class SuiteRoom extends IRoom {
    getRoomName() {
      return "Suite Room";
    }
  }
  
  // Інтерфейс для клієнта
  class IGuest {
    requestRoom(room) {
      throw new Error("Method requestRoom() must be implemented.");
    }
  
    selectDates() {
      throw new Error("Method selectDates() must be implemented.");
    }
  
    stayInRoom() {
      throw new Error("Method stayInRoom() must be implemented.");
    }
  
    getName() {
      throw new Error("Method getName() must be implemented.");
    }
  }
  
  // Система бронювання
  class BookingSystem {
    bookRoom() {
      console.log("The room is being booked");
    }
  
    confirmBooking() {
      console.log("Booking confirmed");
    }
  }
  
  // Портьє, що обробляє бронювання
  class Receptionist {
    acceptBooking(guest) {
      console.log(`Receptionist accepted the booking from ${guest.getName()}`);
    }
  
    processBooking() {
      console.log("Processing booking in the system");
    }
  
    deliverConfirmation(guest) {
      console.log(
        `Confirmation ready, delivering to the guest ${guest.getName()}`
      );
    }
  }
  
  // Клієнт готелю
  class Guest extends IGuest {
    constructor(name) {
      super();
      this.name = name;
    }
  
    requestRoom(room) {
      console.log(
        `Guest ${this.name} checks the details of ${room.getRoomName()}`
      );
    }
  
    selectDates() {
      console.log(`Guest ${this.name} selects dates for their stay`);
      return { checkIn: "2024-12-01", checkOut: "2024-12-05" };
    }
  
    stayInRoom() {
      console.log(`Guest ${this.name} is staying in the room`);
    }
  
    getName() {
      return this.name;
    }
  }
  
  // Фасад готелю для спрощеного бронювання
  class HotelFacade {
    constructor() {
      this.bookingSystem = new BookingSystem();
      this.receptionist = new Receptionist();
      this.rooms = {
        [RoomType.SINGLE]: new SingleRoom(),
        [RoomType.DOUBLE]: new DoubleRoom(),
        [RoomType.SUITE]: new SuiteRoom(),
      };
    }
  
    getRoom(type) {
      return this.rooms[type];
    }
  
    bookRoom(guest) {
      this.receptionist.acceptBooking(guest);
      this.receptionist.processBooking();
      this._bookingSystemWork();
      this.receptionist.deliverConfirmation(guest);
    }
  
    _bookingSystemWork() {
      this.bookingSystem.bookRoom();
      this.bookingSystem.confirmBooking();
    }
  }
  
  // Використання
  const hotel = new HotelFacade();
  const guest1 = new Guest("Alice");
  const guest2 = new Guest("Bob");
  
  guest1.requestRoom(hotel.getRoom(RoomType.SINGLE));
  hotel.bookRoom(guest1);
  
  guest2.requestRoom(hotel.getRoom(RoomType.SUITE));
  hotel.bookRoom(guest2);
  
  guest1.stayInRoom();
  guest2.stayInRoom();
  