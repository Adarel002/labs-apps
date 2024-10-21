// Enum-like constants in JavaScript
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
  
  const CheckInTime = {
    MORNING: "morning",
    AFTERNOON: "afternoon",
    EVENING: "evening",
    NIGHT: "night",
  };
  
  class HotelRoom {
    constructor(name, cost, type, status = BookingStatus.AVAILABLE) {
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
  
    toString() {
      return `Ціна номеру: ${this.cost} (без десяткових знаків), Тип: ${this.type}\n`;
    }
  }
  
  class HotelSingleton {
    static instance = null;
  
    constructor() {
      if (HotelSingleton.instance) {
        return HotelSingleton.instance;
      }
  
      this.name = "Готель Прем'єр";
      this.cost = 100;
      this.type = "Luxury";
      this.status = BookingStatus.AVAILABLE;
  
      this.hotelRooms = [
        new HotelRoom(HotelName.MAIN, 200, RoomType.STANDARD),
      ];
  
      HotelSingleton.instance = this;
    }
  
    static getInstance() {
      if (!HotelSingleton.instance) {
        new HotelSingleton();
      }
      return HotelSingleton.instance;
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
  
  // Main function
  function main() {
    const hotel = HotelSingleton.getInstance();
    console.log(hotel.toString());
  
    console.log("---".repeat(20));
  
    hotel.name = "Готель Еліт"; // Змінюємо ім'я готелю
    hotel.type = "Бутік";
    hotel.updateRoomPrice(HotelName.MAIN, 250);
  
    const newHotel = HotelSingleton.getInstance();
    console.log(hotel === newHotel); // Перевіряємо, чи це той самий об'єкт
  
    console.log("---".repeat(20));
    console.log(newHotel.toString());
  }
  
  main();
  