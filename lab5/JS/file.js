class RoomType {
    static SINGLE = "single";
    static DOUBLE = "double";
    static SUITE = "suite";
  }
  
  class BookingStatus {
    static AVAILABLE = "available";
    static BOOKED = "booked";
    static CLOSED = "closed";
  }
  
  class RoomBooking {
    constructor(roomType, pricePerNight) {
      this.roomType = roomType;
      this.pricePerNight = pricePerNight;
    }
  
    toString() {
      return `Room Type: ${this.roomType}, Price per Night: ${this.pricePerNight}`;
    }
  }
  
  class HotelBuilder {
    constructor({
      name = "Unnamed Hotel",
      costPerNight = 50,
      type = RoomType.SINGLE,
      status = BookingStatus.AVAILABLE,
      roomBookings = [],
    } = {}) {
      this._name = name;
      this._costPerNight = costPerNight;
      this._type = type;
      this._status = status;
      this._roomBookings = roomBookings;
    }
  
    get name() {
      return this._name;
    }
    set name(value) {
      this._name = value;
    }
  
    get costPerNight() {
      return this._costPerNight;
    }
    set costPerNight(value) {
      this._costPerNight = value;
    }
  
    get type() {
      return this._type;
    }
    set type(value) {
      this._type = value;
    }
  
    get status() {
      return this._status;
    }
    set status(value) {
      this._status = value;
    }
  
    get roomBookings() {
      return this._roomBookings;
    }
    set roomBookings(value) {
      this._roomBookings = value;
    }
  
    build() {
      return new Hotel(this);
    }
  
    static get builder() {
      return new HotelBuilder();
    }
  }
  
  class Hotel {
    constructor(builder) {
      this.name = builder.name;
      this.costPerNight = builder.costPerNight;
      this.type = builder.type;
      this.status = builder.status;
      this.roomBookings = builder.roomBookings;
    }
  
    toString() {
      let infoStr = `Hotel: ${this.name}\n`;
      infoStr += `Type: ${this.type}\n`;
      infoStr += `Rooms:\n`;
      for (const room of this.roomBookings) {
        infoStr += `- ${room.toString()}\n`;
      }
      return infoStr;
    }
  }
  
  function main() {
    let hotelBuilder = HotelBuilder.builder;
  
    hotelBuilder.name = "Sunset Hotel";
    hotelBuilder.costPerNight = 100;
    hotelBuilder.type = RoomType.SUITE;
    hotelBuilder.status = BookingStatus.AVAILABLE;
    hotelBuilder.roomBookings = [
      new RoomBooking(RoomType.SINGLE, 75.0),
      new RoomBooking(RoomType.SUITE, 200.0),
    ];
  
    let hotel = hotelBuilder.build();
    console.log(hotel.toString());
    console.log("----".repeat(20));
    
    let newHotelBuilder = HotelBuilder.builder;
    newHotelBuilder.name = "Oceanview Hotel";
    newHotelBuilder.costPerNight = 150;
    newHotelBuilder.type = RoomType.DOUBLE;
    newHotelBuilder.status = BookingStatus.AVAILABLE;
    newHotelBuilder.roomBookings = [
      new RoomBooking(RoomType.SINGLE, 75.0),
      new RoomBooking(RoomType.DOUBLE, 100.0),
      new RoomBooking(RoomType.SUITE, 200.0),
      new RoomBooking(RoomType.SINGLE, 75.0),
    ];
  
    let newHotel = newHotelBuilder.build();
    console.log(newHotel.toString());
  }
  
  main();
  