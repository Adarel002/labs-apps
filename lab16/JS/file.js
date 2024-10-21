// Abstract class for HotelSystem
class HotelSystem {
    createAvailableRoomsIterator() {
      throw new Error("Method not implemented");
    }
  
    createBookedRoomsIterator() {
      throw new Error("Method not implemented");
    }
  }
  
  // Abstract class for RoomIterator
  class RoomIterator {
    getNext() {
      throw new Error("Method not implemented");
    }
  
    hasMore() {
      throw new Error("Method not implemented");
    }
  }
  
  // Hotel class implementing HotelSystem
  class Hotel extends HotelSystem {
    createAvailableRoomsIterator() {
      return new RoomIteratorImpl(this, 0, "available_rooms");
    }
  
    createBookedRoomsIterator() {
      return new RoomIteratorImpl(this, 0, "booked_rooms");
    }
  }
  
  // RoomIterator class implementing RoomIterator
  class RoomIteratorImpl extends RoomIterator {
    constructor(hotel, currentPosition, profileId) {
      super();
      this._cache = Array.from(
        { length: 5 },
        (_, index) => `${profileId} Room ${index + 1}`
      );
      this.hotel = hotel;
      this._currentPosition = currentPosition;
    }
  
    getNext() {
      if (this.hasMore()) {
        console.log(this._cache[this._currentPosition]);
        this._currentPosition++;
        return true;
      }
      return false;
    }
  
    hasMore() {
      return this._currentPosition < this._cache.length;
    }
  }
  
  // BookingSystem class
  class BookingSystem {
    constructor(systemId, notifier) {
      this.systemId = systemId;
      this.notifier = notifier;
    }
  
    notifyAvailableRooms(iterator) {
      while (iterator.hasMore()) {
        iterator.getNext();
      }
    }
  
    notifyBookedRooms(iterator) {
      while (iterator.hasMore()) {
        iterator.getNext();
      }
    }
  }
  
  // Main function
  function main() {
    const hotel = new Hotel();
    const bookingSystem = new BookingSystem(1, "NotifierBot");
  
    const availableRoomsIterator = hotel.createAvailableRoomsIterator();
    const bookedRoomsIterator = hotel.createBookedRoomsIterator();
  
    console.log("Available Rooms:");
    bookingSystem.notifyAvailableRooms(availableRoomsIterator);
    
    console.log("\nBooked Rooms:");
    bookingSystem.notifyBookedRooms(bookedRoomsIterator);
  }
  
  // Run the main function
  main();
  