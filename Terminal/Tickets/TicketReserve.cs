using Terminal.Buses;
using Exception = System.Exception;

namespace Terminal.Tickets;

public sealed class TicketReserve : Ticket
{
    private readonly double _reservationPricePercent;

    public TicketReserve(
        int seatNumber,
        Bus bus,
        Path path)
        : base(seatNumber, bus, path)
    {
        _reservationPricePercent = 20;
        CancellationPercent = 30;
        SetPrice();
        UpdateSeat();
    }

    protected override void UpdateSeat()
    {
        var seats = Bus.BusSeats
            .Where(_ => _.TypeSeat == TypeBusSeat.Empty)
            .ToList();
        var seat = seats.SingleOrDefault(_ => _.Number == SeatNumber);
        if (seat == null)
        {
            throw new Exception("This seat is not available");
        }

        seat.TypeSeat = TypeBusSeat.Reserved;
    }

    protected override void SetPrice()
    {
        var a = Path.BasePrice * Bus.PricePercent / 100;
        a += Path.BasePrice;

        var b = a * _reservationPricePercent / 100;

        Price = b;
    }

    public override void Cancelled()
    {
        Path.Tickets.Remove(this);
        var ticketCancellation = new TicketCancellation(this);
        Path.Tickets.Add(ticketCancellation);
    }
}