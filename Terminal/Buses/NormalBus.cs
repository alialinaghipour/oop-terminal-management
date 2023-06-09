namespace Terminal.Buses;

public sealed class NormalBus : Bus
{
    public NormalBus()
    {
        PathPricePercent = 1;
        SeatCount = 44;
        var seats = GenerateSeats();
        BusSeats = seats;
    }

    public override string ToString()
    {
        return Name + " (Normal)";
    }
}