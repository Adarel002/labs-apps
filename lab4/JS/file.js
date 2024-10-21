const PlatformType = {
    Web: "Web",
    Mobile: "Mobile",
  };
  
  class BookingOptions {
    constructor(platform) {
      if (this.constructor === BookingOptions) {
        throw new Error("Abstract classes can't be instantiated.");
      }
      this.platform = platform;
    }
  
    display() {
      throw new Error("Method 'display()' must be implemented.");
    }
  }
  
  class PaymentMethod {
    constructor(platform) {
      if (this.constructor === PaymentMethod) {
        throw new Error("Abstract classes can't be instantiated.");
      }
      this.platform = platform;
    }
  
    process() {
      throw new Error("Method 'process()' must be implemented.");
    }
  }
  
  class UserInterface {
    constructor(platform) {
      if (this.constructor === UserInterface) {
        throw new Error("Abstract classes can't be instantiated.");
      }
      this.platform = platform;
    }
  
    render() {
      throw new Error("Method 'render()' must be implemented.");
    }
  }
  
  class WebBookingOptions extends BookingOptions {
    constructor() {
      super("Web");
    }
  
    display() {
      console.log(`Displaying hotel booking options for ${this.platform}`);
    }
  }
  
  class WebPaymentMethod extends PaymentMethod {
    constructor() {
      super("Web");
    }
  
    process() {
      console.log(`Processing hotel payment for ${this.platform}`);
    }
  }
  
  class WebUserInterface extends UserInterface {
    constructor() {
      super("Web");
    }
  
    render() {
      console.log(`Rendering hotel booking UI for ${this.platform}`);
    }
  }
  
  class MobileBookingOptions extends BookingOptions {
    constructor() {
      super("Mobile");
    }
  
    display() {
      console.log(`Displaying hotel booking options for ${this.platform}`);
    }
  }
  
  class MobilePaymentMethod extends PaymentMethod {
    constructor() {
      super("Mobile");
    }
  
    process() {
      console.log(`Processing hotel payment for ${this.platform}`);
    }
  }
  
  class MobileUserInterface extends UserInterface {
    constructor() {
      super("Mobile");
    }
  
    render() {
      console.log(`Rendering hotel booking UI for ${this.platform}`);
    }
  }
  
  class HotelGuiAbstractFactory {
    getBookingOptions() {
      throw new Error("Method 'getBookingOptions()' must be implemented.");
    }
  
    getPaymentMethod() {
      throw new Error("Method 'getPaymentMethod()' must be implemented.");
    }
  
    getUserInterface() {
      throw new Error("Method 'getUserInterface()' must be implemented.");
    }
  }
  
  class WebGuiFactory extends HotelGuiAbstractFactory {
    getBookingOptions() {
      return new WebBookingOptions();
    }
  
    getPaymentMethod() {
      return new WebPaymentMethod();
    }
  
    getUserInterface() {
      return new WebUserInterface();
    }
  }
  
  class MobileGuiFactory extends HotelGuiAbstractFactory {
    getBookingOptions() {
      return new MobileBookingOptions();
    }
  
    getPaymentMethod() {
      return new MobilePaymentMethod();
    }
  
    getUserInterface() {
      return new MobileUserInterface();
    }
  }
  
  class HotelApplication {
    constructor(guiFactory) {
      this.guiFactory = guiFactory;
    }
  
    createHotelGui() {
      const bookingOptions = this.guiFactory.getBookingOptions();
      bookingOptions.display();
  
      const paymentMethod = this.guiFactory.getPaymentMethod();
      paymentMethod.process();
  
      const userInterface = this.guiFactory.getUserInterface();
      userInterface.render();
    }
  }
  
  // Factory creator
  class HotelGuiFactory {
    static factory(platformType) {
      switch (platformType) {
        case PlatformType.Web:
          return new WebGuiFactory();
        case PlatformType.Mobile:
          return new MobileGuiFactory();
        default:
          throw new Error("Unknown platform type");
      }
    }
  }
  
  function main() {
    const platformType = PlatformType.Web;
    const guiFactory = HotelGuiFactory.factory(platformType);
    const app = new HotelApplication(guiFactory);
    app.createHotelGui();
  }
  main();
  