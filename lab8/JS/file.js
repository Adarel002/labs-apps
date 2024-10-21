// Представляє номер у готелі
class HotelRoom {
    constructor(roomNumber, type, price) {
      this.roomNumber = roomNumber;
      this.type = type; // Наприклад, "Single", "Double"
      this.price = price;
    }
  }
  
  // Представляє доступність номеру
  class RoomAvailability {
    constructor(room, date, available) {
      this.room = room;
      this.date = date;
      this.available = available;
      this.isFullyBooked = false;
    }
  
    markFullyBooked() {
      this.isFullyBooked = true;
    }
  }
  
  // Інтерфейс для операцій бронювання номерів
  class IRoomBookingImplementor {
    bookRoom(availability) {
      throw new Error("Method 'bookRoom()' must be implemented.");
    }
    cancelBooking(availability) {
      throw new Error("Method 'cancelBooking()' must be implemented.");
    }
    getAvailableRooms(availability) {
      throw new Error("Method 'getAvailableRooms()' must be implemented.");
    }
    getBookingMethod() {
      throw new Error("Method 'getBookingMethod()' must be implemented.");
    }
  }
  
  // Офлайн бронювання (на рецепції)
  class ReceptionBookingImplementor extends IRoomBookingImplementor {
    constructor(availabilities) {
      super();
      this.bookingMethod = "Reception";
      this.rooms = new Map();
  
      availabilities.forEach((availability) => {
        this.rooms.set(availability, availability.available);
      });
    }
  
    bookRoom(availability) {
      if (this.rooms.get(availability) > 0) {
        this.rooms.set(availability, this.rooms.get(availability) - 1);
        console.log(`Room booked at reception for Room ${availability.room.roomNumber}`);
        if (this.rooms.get(availability) === 0) {
          availability.markFullyBooked();
        }
      } else {
        console.log("Room is fully booked for this date.");
      }
    }
  
    cancelBooking(availability) {
      if (this.rooms.get(availability) < availability.available) {
        this.rooms.set(availability, this.rooms.get(availability) + 1);
        console.log(`Booking cancelled at reception for Room ${availability.room.roomNumber}`);
      }
    }
  
    getAvailableRooms(availability) {
      return this.rooms.get(availability) || 0;
    }
  
    getBookingMethod() {
      return this.bookingMethod;
    }
  }
  
  // Онлайн бронювання
  class OnlineBookingImplementor extends IRoomBookingImplementor {
    constructor(availabilities) {
      super();
      this.bookingMethod = "Online";
      this.rooms = new Map();
  
      availabilities.forEach((availability) => {
        this.rooms.set(availability, availability.available);
      });
    }
  
    bookRoom(availability) {
      if (this.rooms.get(availability) > 0) {
        this.rooms.set(availability, this.rooms.get(availability) - 1);
        console.log(`Room booked online for Room ${availability.room.roomNumber}`);
        if (this.rooms.get(availability) === 0) {
          availability.markFullyBooked();
        }
      } else {
        console.log("Room is fully booked for this date.");
      }
    }
  
    cancelBooking(availability) {
      if (this.rooms.get(availability) < availability.available) {
        this.rooms.set(availability, this.rooms.get(availability) + 1);
        console.log(`Booking cancelled online for Room ${availability.room.roomNumber}`);
      }
    }
  
    getAvailableRooms(availability) {
      return this.rooms.get(availability) || 0;
    }
  
    getBookingMethod() {
      return this.bookingMethod;
    }
  }
  
  // Керує бронюваннями за допомогою різних методів
  class HotelBookingSystem {
    constructor(bookingImplementor) {
      this._booking = bookingImplementor;
    }
  
    bookRoom(availability) {
      if (!availability.isFullyBooked) {
        this._booking.bookRoom(availability);
      } else {
        console.log("Cannot book room; it is fully booked.");
      }
    }
  
    cancelBooking(availability) {
      this._booking.cancelBooking(availability);
    }
  
    getAvailableRooms(availability) {
      return this._booking.getAvailableRooms(availability);
    }
  
    changeBookingImplementor(newImplementor) {
      console.log(
        `Booking method changed from ${this._booking.getBookingMethod()} to ${newImplementor.getBookingMethod()}`
      );
      this._booking = newImplementor;
    }
  
    getBookingMethod() {
      return this._booking.getBookingMethod();
    }
  }
  
  // Приклад використання
  function main() {
    const room1 = new HotelRoom(101, "Single", 100);
    const room2 = new HotelRoom(102, "Double", 150);
  
    const availability1 = new RoomAvailability(room1, new Date(2024, 8, 1), 10);
    const availability2 = new RoomAvailability(room2, new Date(2024, 8, 1), 5);
  
    const implementor = new ReceptionBookingImplementor([availability1, availability2]);
    const bookingSystem = new HotelBookingSystem(implementor);
  
    console.log(`Booking method: ${bookingSystem.getBookingMethod()}`);
    bookingSystem.bookRoom(availability1);
    bookingSystem.bookRoom(availability2);
  
    console.log(
      `Available rooms for Room ${availability1.room.roomNumber}: ${bookingSystem.getAvailableRooms(availability1)}`
    );
    console.log(
      `Available rooms for Room ${availability2.room.roomNumber}: ${bookingSystem.getAvailableRooms(availability2)}`
    );
  
    const newImplementor = new OnlineBookingImplementor([availability1, availability2]);
    bookingSystem.changeBookingImplementor(newImplementor);
  
    console.log(`Booking method: ${bookingSystem.getBookingMethod()}`);
    bookingSystem.bookRoom(availability1);
    bookingSystem.bookRoom(availability2);
  
    console.log(
      `Available rooms for Room ${availability1.room.roomNumber}: ${bookingSystem.getAvailableRooms(availability1)}`
    );
    console.log(
      `Available rooms for Room ${availability2.room.roomNumber}: ${bookingSystem.getAvailableRooms(availability2)}`
    );
  }
  
  main();
  