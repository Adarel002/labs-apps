class BookingState {
    handle(context) {
      throw new Error("This method should be overridden!");
    }
  
    toString() {
      return "Booking State";
    }
  }
  
  class RoomSelection extends BookingState {
    handle(context) {
      console.log("Room selected! Moving to date selection.");
      context.state = new DateSelection();
    }
  
    toString() {
      return "Selecting Room";
    }
  }
  
  class DateSelection extends BookingState {
    handle(context) {
      console.log("Dates selected! Moving to payment confirmation.");
      context.state = new PaymentConfirmation();
    }
  
    toString() {
      return "Selecting Dates";
    }
  }
  
  class PaymentConfirmation extends BookingState {
    handle(context) {
      console.log("Payment confirmed! Booking complete.");
      context.state = new BookingComplete();
    }
  
    toString() {
      return "Confirming Payment";
    }
  }
  
  class BookingComplete extends BookingState {
    handle(context) {
      console.log("Booking is already complete.");
    }
  
    toString() {
      return "Booking Complete";
    }
  }
  
  class BookingContext {
    constructor(state) {
      this._state = state;
    }
  
    get state() {
      return this._state;
    }
  
    set state(newState) {
      this._state = newState;
    }
  
    nextStep() {
      console.log("Proceeding to the next step...");
      this._state.handle(this);
    }
  }
  
  function main() {
    const booking = new BookingContext(new RoomSelection());
  
    console.log(`Current state: ${booking.state}`);
    booking.nextStep(); // Select room
  
    console.log(`Current state: ${booking.state}`);
    booking.nextStep(); // Select dates
  
    console.log(`Current state: ${booking.state}`);
    booking.nextStep(); // Confirm payment
  
    console.log(`Current state: ${booking.state}`);
  }
  
  main();
  