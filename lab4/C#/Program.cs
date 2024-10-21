public static class PlatformType
{
    public const string Web = "Web";
    public const string Mobile = "Mobile";
}

public abstract class BookingOptions
{
    public string Platform { get; }

    protected BookingOptions(string platform)
    {
        if (GetType() == typeof(BookingOptions))
        {
            throw new InvalidOperationException("Abstract classes can't be instantiated.");
        }
        Platform = platform;
    }

    public abstract void Display();
}

public abstract class PaymentMethod
{
    public string Platform { get; }

    protected PaymentMethod(string platform)
    {
        if (GetType() == typeof(PaymentMethod))
        {
            throw new InvalidOperationException("Abstract classes can't be instantiated.");
        }
        Platform = platform;
    }

    public abstract void Process();
}

public abstract class UserInterface
{
    public string Platform { get; }

    protected UserInterface(string platform)
    {
        if (GetType() == typeof(UserInterface))
        {
            throw new InvalidOperationException("Abstract classes can't be instantiated.");
        }
        Platform = platform;
    }

    public abstract void Render();
}

public class WebBookingOptions : BookingOptions
{
    public WebBookingOptions() : base(PlatformType.Web) { }

    public override void Display()
    {
        Console.WriteLine($"Displaying hotel booking options for {Platform}");
    }
}

public class WebPaymentMethod : PaymentMethod
{
    public WebPaymentMethod() : base(PlatformType.Web) { }

    public override void Process()
    {
        Console.WriteLine($"Processing hotel payment for {Platform}");
    }
}

public class WebUserInterface : UserInterface
{
    public WebUserInterface() : base(PlatformType.Web) { }

    public override void Render()
    {
        Console.WriteLine($"Rendering hotel booking UI for {Platform}");
    }
}

public class MobileBookingOptions : BookingOptions
{
    public MobileBookingOptions() : base(PlatformType.Mobile) { }

    public override void Display()
    {
        Console.WriteLine($"Displaying hotel booking options for {Platform}");
    }
}

public class MobilePaymentMethod : PaymentMethod
{
    public MobilePaymentMethod() : base(PlatformType.Mobile) { }

    public override void Process()
    {
        Console.WriteLine($"Processing hotel payment for {Platform}");
    }
}

public class MobileUserInterface : UserInterface
{
    public MobileUserInterface() : base(PlatformType.Mobile) { }

    public override void Render()
    {
        Console.WriteLine($"Rendering hotel booking UI for {Platform}");
    }
}

public abstract class HotelGuiAbstractFactory
{
    public abstract BookingOptions GetBookingOptions();
    public abstract PaymentMethod GetPaymentMethod();
    public abstract UserInterface GetUserInterface();
}

public class WebGuiFactory : HotelGuiAbstractFactory
{
    public override BookingOptions GetBookingOptions()
    {
        return new WebBookingOptions();
    }

    public override PaymentMethod GetPaymentMethod()
    {
        return new WebPaymentMethod();
    }

    public override UserInterface GetUserInterface()
    {
        return new WebUserInterface();
    }
}

public class MobileGuiFactory : HotelGuiAbstractFactory
{
    public override BookingOptions GetBookingOptions()
    {
        return new MobileBookingOptions();
    }

    public override PaymentMethod GetPaymentMethod()
    {
        return new MobilePaymentMethod();
    }

    public override UserInterface GetUserInterface()
    {
        return new MobileUserInterface();
    }
}

public class HotelApplication
{
    private readonly HotelGuiAbstractFactory _guiFactory;

    public HotelApplication(HotelGuiAbstractFactory guiFactory)
    {
        _guiFactory = guiFactory;
    }

    public void CreateHotelGui()
    {
        var bookingOptions = _guiFactory.GetBookingOptions();
        bookingOptions.Display();

        var paymentMethod = _guiFactory.GetPaymentMethod();
        paymentMethod.Process();

        var userInterface = _guiFactory.GetUserInterface();
        userInterface.Render();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var platformType = PlatformType.Web; // Change this to PlatformType.Mobile for mobile interface
        HotelGuiAbstractFactory guiFactory;

        switch (platformType)
        {
            case PlatformType.Web:
                guiFactory = new WebGuiFactory();
                break;
            case PlatformType.Mobile:
                guiFactory = new MobileGuiFactory();
                break;
            default:
                throw new ArgumentException("Unknown platform type");
        }

        var app = new HotelApplication(guiFactory);
        app.CreateHotelGui();
    }
}
