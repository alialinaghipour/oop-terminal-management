using Terminal.Buses;

namespace Terminal.Tickets;

public sealed class TicketCancellation : Ticket
{
    private readonly double _preCancellationPrice;
    private readonly double _cancellationPercent;
    public TicketCancellation(
        Ticket ticket) 
        : base(
            ticket.SeatNumber,
            ticket.Bus,
            ticket.Path)
    {
        _preCancellationPrice = ticket.GetPrice();
        _cancellationPercent = ticket.GetCancellationPercent();
        SetPrice();
        UpdateSeat();
    }

    protected override void UpdateSeat()
    {
        var busSeat = Bus.BusSeats.FirstOrDefault(_ =>
            _.Number == SeatNumber &&
            _.TypeSeat is 
                TypeBusSeat.Reserved or
                TypeBusSeat.Sold);
        if (busSeat is null)
        {
            throw new Exception("This ticket cannot be canceled");
        }

        busSeat.TypeSeat = TypeBusSeat.Empty;
    }

    protected override void SetPrice()
    {
        Price = _preCancellationPrice * _cancellationPercent / 100;
    }

    public override void Cancelled()
    {
    }
}