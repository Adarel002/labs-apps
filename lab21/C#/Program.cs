using System;

// Abstract class for Pricing Strategy
public abstract class PricingStrategy
{
    public abstract double CalculatePrice(double basePrice, int quantity);
}

// Strategy for Regular Booking
public class RegularBookingStrategy : PricingStrategy
{
    public override double CalculatePrice(double basePrice, int quantity)
    {
        return basePrice * quantity;
    }
}

// Strategy for Student Booking with Discount
public class StudentDiscountStrategy : PricingStrategy
{
    public override double CalculatePrice(double basePrice, int quantity)
    {
        return basePrice * 0.8 * quantity; // 20% discount for students
    }
}

// Strategy for Family Booking with Discount
public class FamilyDiscountStrategy : PricingStrategy
{
    public override double CalculatePrice(double basePrice, int quantity)
    {
        return basePrice * 0.75 * quantity; // 25% discount for family booking
    }
}

// Context for calculating booking price
public class BookingContext
{
    private PricingStrategy? _pricingStrategy; // Make it nullable

    public void SetPricingStrategy(PricingStrategy strategy)
    {
        _pricingStrategy = strategy;
    }

    public double ExecutePricingStrategy(double basePrice, int quantity)
    {
        if (_pricingStrategy == null)
        {
            throw new InvalidOperationException("Strategy not set!");
        }
        return _pricingStrategy.CalculatePrice(basePrice, quantity);
    }
}

// Usage of the strategy
public class Program
{
    public static void Main(string[] args)
    {
        BookingContext context = new BookingContext();

        // Strategy for Regular Booking
        context.SetPricingStrategy(new RegularBookingStrategy());
        Console.WriteLine($"Ціна за звичайне бронювання: {context.ExecutePricingStrategy(100.0, 3)}"); // Output: 300.0

        // Strategy for Student Booking with Discount
        context.SetPricingStrategy(new StudentDiscountStrategy());
        Console.WriteLine($"Ціна за студентське бронювання: {context.ExecutePricingStrategy(100.0, 3)}"); // Output: 240.0

        // Strategy for Family Booking with Discount
        context.SetPricingStrategy(new FamilyDiscountStrategy());
        Console.WriteLine($"Ціна за сімейне бронювання: {context.ExecutePricingStrategy(100.0, 3)}"); // Output: 225.0
    }
}
