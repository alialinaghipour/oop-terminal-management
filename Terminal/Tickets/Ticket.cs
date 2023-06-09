using Terminal.Buses;

namespace Terminal.Tickets;

public abstract class Ticket
{
    protected int CancellationPercent;
    protected double Price;
    protected Ticket(
        int seatNumber,
        Bus bus,
        Path path)
    {
        SeatNumber = seatNumber;
        Bus = bus;
        Path = path;
    }

    public Bus Bus { get; }
    public Path Path { get; }
    public int SeatNumber { get; }
    protected abstract void UpdateSeat();

    public double GetPrice()
    {
        return Price;
    }
    public double GetCancellationPercent()
    {
        return CancellationPercent;
    }
    protected abstract void SetPrice();
    public abstract void Cancelled();
    public override string ToString()
    {
        return $"{Path} - {Bus} - seat : {SeatNumber}";
    }
}