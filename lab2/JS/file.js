class HotelPrototype {
    constructor({
      name = "Unnamed Hotel",
      cost = 100,
      type = RoomType.STANDARD,
      status = BookingStatus.AVAILABLE,
      hotelRooms = [],
    } = {}) {
      this.name = name;
      this.cost = cost;
      this.type = type;
      this.status = status;
      this.hotelRooms = hotelRooms;
    }
  
    static fromHotel(hotel) {
      return new HotelPrototype({
        name: hotel.name,
        cost: hotel.cost,
        type: hotel.type,
        status: hotel.status,
        hotelRooms: hotel.hotelRooms.map((room) => room.clone()),
      });
    }
  
    clone() {
      return HotelPrototype.fromHotel(this);
    }
  
    updateRoomPrice(roomName, newPrice) {
      this.hotelRooms.forEach((room) => {
        if (room.name === roomName) {
          room.updateCost(newPrice);
        }
      });
    }
  
    toString() {
      let infoStr = `Готель: ${this.name}\n`;
      infoStr += `Тип: ${this.type}\n`;
      infoStr += `Номери:\n`;
  
      this.hotelRooms.forEach((room) => {
        infoStr += `- ${room.toString()}\n`;
      });
  
      return infoStr;
    }
  }
  
  class HotelRoom {
    constructor({ name, cost, type, status = BookingStatus.AVAILABLE }) {
      this.name = name;
      this.cost = cost;
      this.type = type;
      this.status = status;
    }
  
    reserve() {
      if (this.status === BookingStatus.AVAILABLE) {
        this.status = BookingStatus.RESERVED;
      }
    }
  
    occupy() {
      if (this.status === BookingStatus.RESERVED) {
        this.status = BookingStatus.OCCUPIED;
      }
    }
  
    updateCost(newCost) {
      this.cost = newCost;
    }
  
    clone() {
      return new HotelRoom({
        name: this.name,
        cost: this.cost,
        type: this.type,
        status: this.status,
      });
    }
  
    toString() {
      return `Ціна номеру: ${this.cost}, Тип: ${this.type}, Статус: ${this.status}\n`;
    }
  }
  
  // Enums (represented as objects)
  const RoomType = {
    STANDARD: "standard",
    FAMILY: "family",
    LUXURY: "luxury",
  };
  
  const HotelName = {
    MAIN: "Main",
    BOUTIQUE: "Boutique",
    BUSINESS: "Business",
  };
  
  const BookingStatus = {
    AVAILABLE: "available",
    RESERVED: "reserved",
    OCCUPIED: "occupied",
  };
  
  // Main function equivalent
  function main() {
    const hotel = new HotelPrototype({
      name: "Готель Прем'єр",
      hotelRooms: [
        new HotelRoom({
          name: HotelName.BOUTIQUE,
          cost: 200,
          type: RoomType.STANDARD,
        }),
        new HotelRoom({
          name: HotelName.MAIN,
          cost: 400,
          type: RoomType.LUXURY,
        }),
      ],
    });
  
    console.log(hotel.toString());
  
    console.log("-----".repeat(8) + "Нове бронювання" + "----".repeat(8));
  
    const newHotel = hotel.clone();
    newHotel.hotelRooms.push(
      new HotelRoom({
        name: HotelName.BUSINESS,
        cost: 300,
        type: RoomType.FAMILY,
      })
    );
  
    console.log(newHotel.toString());
    console.log("------".repeat(8) + "Оригінал" + "----".repeat(8));
    console.log(hotel.toString());
  }
  
  main();
  