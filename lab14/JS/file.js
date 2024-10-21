// Enum to represent different room types
const RoomType = {
    SINGLE: "single",
    DOUBLE: "double",
    SUITE: "suite",
    NONE: "none",
  };
  
  // RequestBooking class to represent the booking request
  class RequestBooking {
    constructor(description, roomType) {
      this._description = description;
      this._roomType = roomType;
    }
  
    get description() {
      return this._description;
    }
  
    get roomType() {
      return this._roomType;
    }
  }
  
  // Abstract Handler class
  class Handler {
    constructor(successor = null) {
      this._successor = successor;
    }
  
    handle(request) {
      const result = this._checkRequest(request.roomType);
      if (!result && this._successor !== null) {
        this._successor.handle(request);
      }
    }
  
    _checkRequest(roomType) {
      throw new Error("_checkRequest method must be implemented");
    }
  }
  
  // RoomAvailabilityHandler checks the availability of rooms
  class RoomAvailabilityHandler extends Handler {
    _checkRequest(roomType) {
      const available = roomType !== RoomType.NONE;
      if (available) {
        console.log("RoomAvailabilityHandler processes the room request");
      } else {
        console.log("RoomAvailabilityHandler rejects the booking request");
      }
      return !available;
    }
  }
  
  // PaymentHandler processes the payment
  class PaymentHandler extends Handler {
    _checkRequest(roomType) {
      const paymentProcessed = [
        RoomType.SINGLE,
        RoomType.DOUBLE,
        RoomType.SUITE,
      ].includes(roomType);
      if (paymentProcessed) {
        console.log("Payment is being processed");
      } else {
        console.log("Payment failed or invalid room type");
      }
      return !paymentProcessed;
    }
  }
  
  // BookingConfirmationHandler confirms the booking
  class BookingConfirmationHandler extends Handler {
    _checkRequest(roomType) {
      const confirmed = [
        RoomType.SINGLE,
        RoomType.DOUBLE,
        RoomType.SUITE,
      ].includes(roomType);
      if (confirmed) {
        console.log("Booking confirmed! Enjoy your stay.");
      } else {
        console.log("Booking confirmation failed");
      }
      return !confirmed;
    }
  }
  
  // Main function to run the process
  function main() {
    // Create the handlers and link them
    const bookingConfirmation = new BookingConfirmationHandler();
    const payment = new PaymentHandler(bookingConfirmation);
    const roomAvailability = new RoomAvailabilityHandler(payment);
  
    // Create a booking request
    const requestDescription = ["Room: Double", "Check-in: 2024-12-01", "Nights: 2"];
    const requestBooking = new RequestBooking(
      requestDescription,
      RoomType.DOUBLE
    );
  
    // Start the chain of responsibility
    roomAvailability.handle(requestBooking);
  }
  
  // Run the main function
  main();
  