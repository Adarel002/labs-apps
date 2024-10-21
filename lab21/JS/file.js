// Абстрактний клас для стратегії
class PricingStrategy {
    calculatePrice(basePrice, quantity) {
      throw new Error("Цей метод має бути реалізованим!");
    }
  }
  
  // Стратегія для звичайного бронювання
  class RegularBookingStrategy extends PricingStrategy {
    calculatePrice(basePrice, quantity) {
      return basePrice * quantity;
    }
  }
  
  // Стратегія для студентського бронювання зі знижкою
  class StudentDiscountStrategy extends PricingStrategy {
    calculatePrice(basePrice, quantity) {
      return basePrice * 0.8 * quantity; // 20% знижка для студентів
    }
  }
  
  // Стратегія для сімейного бронювання зі знижкою
  class FamilyDiscountStrategy extends PricingStrategy {
    calculatePrice(basePrice, quantity) {
      return basePrice * 0.75 * quantity; // 25% знижка для сімейного бронювання
    }
  }
  
  // Контекст для обчислення ціни бронювання
  class BookingContext {
    setPricingStrategy(strategy) {
      this._pricingStrategy = strategy;
    }
  
    executePricingStrategy(basePrice, quantity) {
      if (!this._pricingStrategy) {
        throw new Error("Стратегія не встановлена!");
      }
      return this._pricingStrategy.calculatePrice(basePrice, quantity);
    }
  }
  
  // Використання стратегії
  const context = new BookingContext();
  
  // Стратегія для звичайного бронювання
  context.setPricingStrategy(new RegularBookingStrategy());
  console.log(
    `Ціна за звичайне бронювання: ${context.executePricingStrategy(100.0, 3)}`
  ); // Вихід: 300.0
  
  // Стратегія для студентського бронювання зі знижкою
  context.setPricingStrategy(new StudentDiscountStrategy());
  console.log(
    `Ціна за студентське бронювання: ${context.executePricingStrategy(100.0, 3)}`
  ); // Вихід: 240.0
  
  // Стратегія для сімейного бронювання зі знижкою
  context.setPricingStrategy(new FamilyDiscountStrategy());
  console.log(
    `Ціна за сімейне бронювання: ${context.executePricingStrategy(100.0, 3)}`
  ); // Вихід: 225.0
  