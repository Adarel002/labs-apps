const RoomType = {
    Standard: "Standard",
    Premium: "Premium",
    VIP: "VIP",
  };
  
  class Room {
    constructor(hotelName, roomNumber, stayDuration) {
      if (this.constructor === Room) {
        throw new Error("Abstract class Room cannot be instantiated directly.");
      }
      this.hotelName = hotelName;
      this.roomNumber = roomNumber;
      this.stayDuration = stayDuration; // in days
    }
  
    costRoom() {
      throw new Error("Method 'costRoom()' must be implemented.");
    }
  
    toString() {
      return `Room in hotel ${this.hotelName}, room number ${this.roomNumber}, for ${this.stayDuration} night(s)`;
    }
  }
  
  class StandardRoom extends Room {
    constructor(hotelName, roomNumber, stayDuration, basePricePerNight) {
      super(hotelName, roomNumber, stayDuration);
      this.basePricePerNight = basePricePerNight;
    }
  
    costRoom() {
      return this.basePricePerNight * this.stayDuration;
    }
  }
  
  // PremiumRoom class
  class PremiumRoom extends Room {
    constructor(hotelName, roomNumber, stayDuration, basePricePerNight, premiumMultiplier) {
      super(hotelName, roomNumber, stayDuration);
      this.basePricePerNight = basePricePerNight;
      this.premiumMultiplier = premiumMultiplier;
    }
  
    costRoom() {
      return this.basePricePerNight * this.premiumMultiplier * this.stayDuration;
    }
  
    toString() {
      return super.toString() + " with Premium services";
    }
  }
  
  class VIPRoom extends Room {
    constructor(hotelName, roomNumber, stayDuration, basePricePerNight, vipMultiplier, extraServiceFee) {
      super(hotelName, roomNumber, stayDuration);
      this.basePricePerNight = basePricePerNight;
      this.vipMultiplier = vipMultiplier;
      this.extraServiceFee = extraServiceFee;
    }
  
    costRoom() {
      return (this.basePricePerNight * this.vipMultiplier * this.stayDuration) + this.extraServiceFee;
    }
  
    toString() {
      return super.toString() + " with VIP services";
    }
  }
  
  // RoomFactory class
  class RoomFactory {
    static createRoom(roomType, hotelName, roomNumber, stayDuration) {
      switch (roomType) {
        case RoomType.Standard:
          return new StandardRoom(hotelName, roomNumber, stayDuration, 100.0);
        case RoomType.Premium:
          return new PremiumRoom(hotelName, roomNumber, stayDuration, 100.0, 1.5);
        case RoomType.VIP:
          return new VIPRoom(hotelName, roomNumber, stayDuration, 100.0, 2.0, 50.0);
        default:
          throw new Error("Invalid room type");
      }
    }
  }
  
  function main() {
    Object.values(RoomType).forEach((type) => {
      const room = RoomFactory.createRoom(type, "Grand Hotel", "101", 3);
      console.log(room.toString() + " with cost = $" + room.costRoom());
    });
  }
  
  main();
  