// Інтерфейси для номерів і бронювання
class IRoom {
    getRoomInfo() {
      throw new Error("Method not implemented.");
    }
  
    setRoomInfo(info) {
      throw new Error("Method not implemented.");
    }
  }
  
  class IBooking {
    bookRoom(room) {
      throw new Error("Method not implemented.");
    }
  
    cancelRoom(room) {
      throw new Error("Method not implemented.");
    }
  
    getBookedRooms() {
      throw new Error("Method not implemented.");
    }
  }
  
  // Клас для номерів готелю
  class HotelRoom extends IRoom {
    constructor(info) {
      super();
      this.info = info;
    }
  
    getRoomInfo() {
      return this.info;
    }
  
    setRoomInfo(info) {
      this.info = info;
    }
  }
  
  // Клас для системи бронювання
  class BookingSystem extends IBooking {
    constructor() {
      super();
      this._bookedRooms = [];
    }
  
    bookRoom(room) {
      this._bookedRooms.push(room);
      console.log(`Room booked: ${room.getRoomInfo()}`);
    }
  
    cancelRoom(room) {
      const index = this._bookedRooms.indexOf(room);
      if (index > -1) {
        this._bookedRooms.splice(index, 1);
        console.log(`Room cancelled: ${room.getRoomInfo()}`);
      } else {
        console.log(`Room not found: ${room.getRoomInfo()}`);
      }
    }
  
    getBookedRooms() {
      return [...this._bookedRooms];
    }
  }
  
  // Функція для виведення заброньованих номерів
  function printBookedRooms(bookingSystem) {
    console.log("Booked Rooms:");
    bookingSystem.getBookedRooms().forEach((room) => {
      console.log(room.getRoomInfo());
    });
  }
  
  // Головна функція
  function main() {
    const bookingSystem = new BookingSystem();
  
    const room1 = new HotelRoom("Room A - Guest 1");
    const room2 = new HotelRoom("Room B - Guest 2");
  
    bookingSystem.bookRoom(room1);
    bookingSystem.bookRoom(room2);
  
    printBookedRooms(bookingSystem);
  
    console.log("--------");
    console.log("Cancelling Room");
    console.log("-------------");
  
    bookingSystem.cancelRoom(room1);
  
    printBookedRooms(bookingSystem);
  }
  
  // Запуск основної функції
  main();
  