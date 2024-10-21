// Інтерфейс для бронювання
class IBooking {
    cost() {
      throw new Error("Method not implemented.");
    }
  
    description() {
      throw new Error("Method not implemented.");
    }
  }
  
  // Клас для бронювання номера в готелі
  class RoomBooking extends IBooking {
    constructor({ cost, roomType, roomNumber }) {
      super();
      this._cost = cost;
      this._roomType = roomType;
      this._roomNumber = roomNumber;
    }
  
    cost() {
      return this._cost;
    }
  
    description() {
      return `Room Type: ${this._roomType}, Room Number: ${this._roomNumber}`;
    }
  }
  
  // Клас для загального замовлення (наприклад, групи бронювань номерів)
  class BookingOrder extends IBooking {
    constructor({ orderId }) {
      super();
      this._orderId = orderId;
      this.bookings = [];
    }
  
    cost() {
      return this.bookings.reduce(
        (totalCost, booking) => totalCost + booking.cost(),
        0
      );
    }
  
    addBooking(booking) {
      this.bookings.push(booking);
    }
  
    removeBooking(booking) {
      const index = this.bookings.indexOf(booking);
      if (index > -1) {
        this.bookings.splice(index, 1);
      }
    }
  
    description() {
      return `Booking Order ID: ${this._orderId}`;
    }
  }
  
  // Клас для замовлення готельних номерів
  class HotelBookingOrder extends BookingOrder {
    constructor(orderId) {
      super({ orderId });
    }
  
    cost() {
      let totalCost = 0;
      this.bookings.forEach((booking) => {
        const currentCost = booking.cost();
        console.log(
          `Cost of ${booking.description()} = ${currentCost} USD`
        );
        totalCost += currentCost;
      });
      console.log(`Total cost of ${this.description()} = ${totalCost} USD`);
      return totalCost;
    }
  }
  
  // Приклад використання
  const room1 = new RoomBooking({
    roomType: "Single",
    roomNumber: "101",
    cost: 100,
  });
  const room2 = new RoomBooking({
    roomType: "Double",
    roomNumber: "102",
    cost: 150,
  });
  const room3 = new RoomBooking({
    roomType: "Suite",
    roomNumber: "301",
    cost: 300,
  });
  
  const order1 = new HotelBookingOrder("Booking001");
  order1.addBooking(room1);
  order1.addBooking(room2);
  order1.addBooking(room3);
  
  order1.cost();
  