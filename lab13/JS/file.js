// Flyweight Pattern

class HotelRoom {
    constructor(roomType, checkInDate) {
      this.roomType = roomType;
      this.checkInDate = checkInDate;
    }
  
    static fromRoom(sharedState) {
      return new HotelRoom(sharedState.roomType, sharedState.checkInDate);
    }
  
    toString() {
      return `(${this.roomType}, Check-in: ${this.checkInDate})`;
    }
  
    equals(other) {
      return (
        this.roomType === other.roomType && this.checkInDate === other.checkInDate
      );
    }
  }
  
  class RoomFlyWeightFactory {
    constructor() {
      this.flyweights = [];
    }
  
    getFlyWeight(sharedState) {
      let state = this.flyweights.find((element) => sharedState.equals(element));
  
      if (!state) {
        state = sharedState;
        this.flyweights.push(sharedState);
      }
  
      return state;
    }
  
    get total() {
      return this.flyweights.length;
    }
  }
  
  // Proxy Pattern
  
  class IBooking {
    makeBooking(bookingID, sharedState) {
      throw new Error("Method 'makeBooking()' must be implemented.");
    }
  }
  
  class BookingMaker extends IBooking {
    constructor(flyWeightFactory) {
      super();
      this.flyWeightFactory = flyWeightFactory;
    }
  
    makeBooking(bookingID, sharedState) {
      const flyWeight = this.flyWeightFactory.getFlyWeight(sharedState);
      return new BookingContext(bookingID, flyWeight);
    }
  }
  
  class ProxyBookingMaker extends IBooking {
    constructor(subject) {
      super();
      this.subject = subject;
    }
  
    makeBooking(bookingID, sharedState) {
      this._logging(bookingID, sharedState);
      return this.subject.makeBooking(bookingID, sharedState);
    }
  
    _logging(bookingID, sharedState) {
      console.log(
        `Logging -> Booking ID: ${bookingID} || Room: ${sharedState}`
      );
    }
  }
  
  class BookingContext {
    constructor(bookingID, sharedState) {
      this.bookingID = bookingID;
      this.sharedState = sharedState;
    }
  
    toString() {
      return `Booking ID: ${this.bookingID} || Room: ${this.sharedState}`;
    }
  }
  
  // Example usage
  
  function main() {
    const flyweightFactory = new RoomFlyWeightFactory();
    const bookingMaker = new BookingMaker(flyweightFactory);
    const proxyBookingMaker = new ProxyBookingMaker(bookingMaker);
  
    const sharedRoom1 = new HotelRoom("Single", "2024-12-01");
    const sharedRoom2 = new HotelRoom("Single", "2024-12-01");
  
    const booking1 = proxyBookingMaker.makeBooking("Booking1", sharedRoom1);
    const booking2 = proxyBookingMaker.makeBooking("Booking2", sharedRoom2);
  
    console.log(booking1.toString());
    console.log(booking2.toString());
  
    console.log(`Total unique rooms: ${flyweightFactory.total}`);
  }
  
  main();
  