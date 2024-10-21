class HotelRoom {
    constructor(roomType, checkInDate) {
      this.roomType = roomType;
      this.checkInDate = checkInDate;
    }
  
    static fromRoom(room) {
      return new HotelRoom(room.roomType, room.checkInDate);
    }
  
    toString() {
      return `(${this.roomType}, Check-in: ${this.checkInDate})`;
    }
  
    equals(room) {
      return (
        room instanceof HotelRoom &&
        this.roomType === room.roomType &&
        this.checkInDate === room.checkInDate
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
  
  class HotelBookingMarket {
    constructor(uniqueState, sharedState) {
      this.uniqueState = uniqueState;
      this.sharedState = sharedState;
    }
  
    static makeHotelBooking(uniqueState, sharedState, factory) {
      const flyweight = factory.getFlyWeight(sharedState);
      return new HotelBookingMarket(uniqueState, flyweight);
    }
  
    toString() {
      return `Booking for ${this.uniqueState} with room: ${this.sharedState}`;
    }
  }
  
  // Main function
  function main() {
    const flyweightFactory = new RoomFlyWeightFactory();
  
    // Create unique hotel room bookings with the same shared states
    const room1 = new HotelRoom("Single", "2024-12-01");
    const room2 = new HotelRoom("Single", "2024-12-01");
    const room3 = new HotelRoom("Suite", "2024-12-05");
  
    const booking1 = HotelBookingMarket.makeHotelBooking(
      "Booking001",
      room1,
      flyweightFactory
    );
    const booking2 = HotelBookingMarket.makeHotelBooking(
      "Booking002",
      room2,
      flyweightFactory
    );
    const booking3 = HotelBookingMarket.makeHotelBooking(
      "Booking003",
      room3,
      flyweightFactory
    );
  
    console.log(booking1.toString());
    console.log(booking2.toString());
    console.log(booking3.toString());
  
    // Show the total number of unique flyweights
    console.log(`Total unique rooms: ${flyweightFactory.total}`);
  }
  
  main();
  