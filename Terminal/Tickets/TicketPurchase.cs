using Terminal.Buses;

namespace Terminal.Tickets;

public sealed class TicketPurchase : Ticket
{
    public TicketPurchase(
        int seatNumber,
        Bus bus,
        Path path)
        : base(seatNumber, bus, path)
    {
        CancellationPercent = 50;
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

        seat!.TypeSeat = TypeBusSeat.Sold;
    }

    protected override void SetPrice()
    {
        var a = Path.BasePrice * Bus.PricePercent / 100;
        Price = a + Path.BasePrice;
    }

    public override void Cancelled()
    {
        Path.Tickets.Remove(this);
        var ticketCancellation = new TicketCancellation(this);
        Path.Tickets.Add(ticketCancellation);
    }
}