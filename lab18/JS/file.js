class HotelBooking {
    constructor(roomType, checkInDate, checkOutDate, userName) {
      this._roomType = roomType;
      this._checkInDate = checkInDate;
      this._checkOutDate = checkOutDate;
      this._userName = userName;
    }
  
    setRoomType(roomType) {
      this._roomType = roomType;
    }
  
    setCheckInDate(checkInDate) {
      this._checkInDate = checkInDate;
    }
  
    setCheckOutDate(checkOutDate) {
      this._checkOutDate = checkOutDate;
    }
  
    setUserName(userName) {
      this._userName = userName;
    }
  
    createSnapshot() {
      return new HotelBookingSnapshot(
        this,
        this._roomType,
        this._checkInDate,
        this._checkOutDate,
        this._userName
      );
    }
  }
  
  class HotelBookingSnapshot {
    constructor(booking, roomType, checkInDate, checkOutDate, userName) {
      this._booking = booking;
      this._roomType = roomType;
      this._checkInDate = checkInDate;
      this._checkOutDate = checkOutDate;
      this._userName = userName;
    }
  
    restore() {
      this._booking.setRoomType(this._roomType);
      this._booking.setCheckInDate(this._checkInDate);
      this._booking.setCheckOutDate(this._checkOutDate);
      this._booking.setUserName(this._userName);
    }
  }
  
  class BookingCommand {
    constructor(booking) {
      this._booking = booking;
      this._backup = null;
    }
  
    makeBackup() {
      this._backup = this._booking.createSnapshot();
    }
  
    restoreBackup() {
      if (this._backup) {
        this._backup.restore();
      } else {
        console.log("No backup to restore.");
      }
    }
  }
  
  // Example usage:
  const hotelBooking = new HotelBooking(
    "Deluxe Room",
    new Date(2024, 9, 15),
    new Date(2024, 9, 20),
    "John Doe"
  );
  
  // Update booking
  hotelBooking.setRoomType("Executive Suite");
  hotelBooking.setCheckInDate(new Date(2024, 9, 18));
  hotelBooking.setCheckOutDate(new Date(2024, 9, 25));
  hotelBooking.setUserName("Jane Smith");
  
  // Create command for managing backups
  const command = new BookingCommand(hotelBooking);
  
  // Make backup of the current state
  command.makeBackup();
  
  console.log("Current booking:");
  console.log(`Room type: ${hotelBooking._roomType}`);
  console.log(`Check-in date: ${hotelBooking._checkInDate}`);
  console.log(`Check-out date: ${hotelBooking._checkOutDate}`);
  console.log(`User name: ${hotelBooking._userName}`);
  
  // Update booking
  hotelBooking.setRoomType("Presidential Suite");
  hotelBooking.setCheckInDate(new Date(2024, 10, 1));
  hotelBooking.setCheckOutDate(new Date(2024, 10, 10));
  hotelBooking.setUserName("Alice Johnson");
  
  console.log("Updated booking:");
  console.log(`Room type: ${hotelBooking._roomType}`);
  console.log(`Check-in date: ${hotelBooking._checkInDate}`);
  console.log(`Check-out date: ${hotelBooking._checkOutDate}`);
  console.log(`User name: ${hotelBooking._userName}`);
  
  // Restore the booking to the backup state
  command.restoreBackup();
  
  console.log("Restored booking:");
  console.log(`Room type: ${hotelBooking._roomType}`);
  console.log(`Check-in date: ${hotelBooking._checkInDate}`);
  console.log(`Check-out date: ${hotelBooking._checkOutDate}`);
  console.log(`User name: ${hotelBooking._userName}`);
  