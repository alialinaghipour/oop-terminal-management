namespace Terminal.Buses;

public sealed class VipBus : Bus
{
    public VipBus()
    {
        PathPricePercent = 30;
        SeatCount = 25;
        var seats = GenerateSeats();
        BusSeats = seats;
    }

    public override string ToString()
    {
        return Name + " (VIP)";
    }
}