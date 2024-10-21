class RoomBooking {
    constructor(room, number, guestName, cost) {
      this.room = room;
      this.number = number; // Room number
      this.guestName = guestName;
      this.cost = cost;
      this.booked = false;
    }
  }
  
  class Room {
    constructor(roomType, availableFrom, hotel) {
      this.roomType = roomType;
      this.availableFrom = availableFrom;
      this.hotel = hotel;
    }
  }
  
  class Hotel {
    constructor(totalRooms, bedsPerRoom) {
      this.totalRooms = totalRooms;
      this.bedsPerRoom = bedsPerRoom;
    }
  }
  
  class BookingSystem {
    constructor(maximumCost) {
      this.maximumCost = maximumCost;
      this._bookings = [];
      this._availableRooms = [];
  
      // Створюємо пул номерів у готелі
      for (let i = 1; i <= 100; i++) {
        const booking = new RoomBooking(
          "Двомісний номер",
          (i % 10) + 1,
          `Гість ${i}`,
          100.0
        );
        this._bookings.push(booking);
        this._availableRooms.push(booking);
      }
    }
  
    getRoom(room) {
      for (const booking of this._availableRooms) {
        if (
          booking.room === room.roomType &&
          !booking.booked &&
          booking.cost <= this.maximumCost
        ) {
          booking.booked = true;
          this._availableRooms = this._availableRooms.filter(
            (b) => b !== booking
          );
          return booking;
        }
      }
      throw new Error("Немає доступних номерів для цього типу");
    }
  
    refundRoom(booking) {
      if (booking.booked) {
        booking.booked = false;
        this._availableRooms.push(booking);
      }
    }
  
    getAvailableRoomCount() {
      return this._availableRooms.length;
    }
  }
  
  function main() {
    const bookingSystem = new BookingSystem(150);
    const hotel = new Hotel(10, 2); // 10 номерів, по 2 ліжка в кожному
    const room = new Room(
      "Двомісний номер",
      new Date(Date.now() + 24 * 60 * 60 * 1000), // Номер доступний через 1 день
      hotel
    );
  
    try {
      const booking1 = bookingSystem.getRoom(room);
      console.log(`Номер 1: Номер ${booking1.number}, Гість ${booking1.guestName}`);
      console.log(
        `Доступні номери після першого бронювання: ${bookingSystem.getAvailableRoomCount()}`
      );
  
      // Перевіряємо, чи номер заброньовано
      console.log(`Чи заброньований номер 1? ${booking1.booked}`); // Має бути true
  
      // Відміняємо бронювання
      bookingSystem.refundRoom(booking1);
      console.log("Номер 1 повернуто.");
      console.log(
        `Доступні номери після повернення: ${bookingSystem.getAvailableRoomCount()}`
      );
  
      // Перевіряємо, чи номер знову доступний
      const booking2 = bookingSystem.getRoom(room);
      console.log(`Номер 2: Номер ${booking2.number}, Гість ${booking2.guestName}`);
      console.log(
        `Доступні номери після другого бронювання: ${bookingSystem.getAvailableRoomCount()}`
      );
    } catch (e) {
      console.error(`Помилка: ${e.message}`);
    }
  }
  
  main();
  