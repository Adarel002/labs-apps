class HotelBookingSystem {
    process() {
      this.checkAvailability();
      this.reserveRooms();
      this.processPayment();
      this.sendConfirmation();
    }
  
    checkAvailability() {
      console.log("Checking room availability...");
    }
  
    reserveRooms() {
      // Абстрактний метод
      throw new Error("Method 'reserveRooms()' must be implemented.");
    }
  
    processPayment() {
      // Абстрактний метод
      throw new Error("Method 'processPayment()' must be implemented.");
    }
  
    sendConfirmation() {
      console.log("Sending confirmation email to customer...");
    }
  }
  
  class OnlineBooking extends HotelBookingSystem {
    reserveRooms() {
      console.log("Reserving rooms online...");
    }
  
    processPayment() {
      console.log("Processing online payment and finalizing booking...");
    }
  }
  
  class WalkInBooking extends HotelBookingSystem {
    reserveRooms() {
      console.log("Reserving rooms at the reception...");
    }
  
    processPayment() {
      console.log("Processing payment at the reception...");
    }
  }
  
  // Testing the classes
  const onlineBooking = new OnlineBooking();
  const walkInBooking = new WalkInBooking();
  
  console.log("Online booking process:");
  onlineBooking.process();
  
  console.log("\nWalk-in booking process:");
  walkInBooking.process();
  