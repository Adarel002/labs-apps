// Базовий клас для бронювання
class BookingBase {
    constructor(cost) {
      this._cost = cost;
    }
  
    cost() {
      return this._cost;
    }
  }
  
  // Декоратор для бронювання
  class BookingDecorator {
    constructor(bookingBase) {
      this._bookingBase = bookingBase;
    }
  
    cost() {
      return this._bookingBase.cost();
    }
  
    description() {
      return "Unknown Booking Type";
    }
  }
  
  // Декоратор для стандартного номера
  class StandardRoom extends BookingDecorator {
    constructor(cost, bookingBase) {
      super(bookingBase);
      this._cost = cost;
    }
  
    cost() {
      return this._bookingBase.cost() + this._cost;
    }
  
    description() {
      return "Standard Room";
    }
  }
  
  // Декоратор для VIP номера
  class VIPRoom extends BookingDecorator {
    constructor(cost, bookingBase) {
      super(bookingBase);
      this._cost = cost;
    }
  
    cost() {
      return this._bookingBase.cost() + this._cost;
    }
  
    description() {
      return "VIP Room";
    }
  }
  
  // Декоратор для дитячого номера
  class ChildRoom extends BookingDecorator {
    constructor(cost, bookingBase) {
      super(bookingBase);
      this._cost = cost;
    }
  
    cost() {
      return this._bookingBase.cost() + this._cost;
    }
  
    description() {
      return "Child Room";
    }
  }
  
  // Функція для виведення інформації про бронювання
  function printBookingInfo(booking) {
    console.log(`Room Type: ${booking.description()} - Cost: ${booking.cost()}`);
  }
  
  // Основна функція для демонстрації використання
  function main() {
    const baseBooking = new BookingBase(50); // базова вартість бронювання
    console.log(`Base booking cost = ${baseBooking.cost()}`);
  
    const standardRoom = new StandardRoom(30, baseBooking);
    printBookingInfo(standardRoom);
  
    const vipRoom = new VIPRoom(100, baseBooking);
    printBookingInfo(vipRoom);
  
    const childRoom = new ChildRoom(20, baseBooking);
    printBookingInfo(childRoom);
  }
  
  main();
  