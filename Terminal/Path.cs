using Terminal.Buses;
using Terminal.Tickets;

namespace Terminal;

public class Path
{
    private readonly string _origin;
    private readonly string _destination;

    public Path(
        double basePrice,
        string origin,
        string destination)
    {
        BasePrice = basePrice;
        _origin = origin;
        _destination = destination;
        Buses = new List<Bus>();
        Tickets = new List<Ticket>();
    }

    public double BasePrice { get; }
    public List<Bus> Buses { get; }
    public List<Ticket> Tickets { get; }

    public double GetTicketsVipBusPrice()
    {
        var price = Tickets
            .Where(_ => _.Bus is VipBus)
            .Select(_ => _.GetPrice())
            .Sum();

        return price;
    }

    public double GetVipBusPrice()
    {
        var bus = Buses.OfType<VipBus>()
            .FirstOrDefault();
        if (bus == null)
        {
            throw new Exception("Vip bus not found");
        }

        var pricePercent = BasePrice * bus.PricePercent / 100;
        return BasePrice + pricePercent;
    }

    public double GetNormalBusPrice()
    {
        var bus = Buses.OfType<NormalBus>()
            .FirstOrDefault();
        if (bus == null)
        {
            throw new Exception("Normal bus not found");
        }

        var pricePercent = BasePrice * bus.PricePercent / 100;
        return BasePrice + pricePercent;
    }

    public double GetTicketsPurchasedVipBusPrice()
    {
        var price = Tickets
            .Where(_ =>
                _ is TicketPurchase &&
                _.Bus is VipBus)
            .Select(_ => _.GetPrice())
            .Sum();
        return price;
    }

    public double GetTicketsReservationVipBusPrice()
    {
        var price = Tickets
            .Where(_ =>
                _ is TicketReserve &&
                _.Bus is VipBus)
            .Select(_ => _.GetPrice())
            .Sum();
        return price;
    }

    public double GetTicketsPurchasedNormalBusPrice()
    {
        var price = Tickets
            .Where(_ =>
                _ is TicketPurchase &&
                _.Bus is NormalBus)
            .Select(_ => _.GetPrice())
            .Sum();
        return price;
    }

    public double GetTicketsReservationNormalBusPrice()
    {
        var price = Tickets
            .Where(_ =>
                _ is TicketReserve &&
                _.Bus is NormalBus)
            .Select(_ => _.GetPrice())
            .Sum();
        return price;
    }

    public double GetTicketsCancellationPrice()
    {
        var price = Tickets
            .Where(_ =>
                _ is TicketCancellation)
            .Select(_ => _.GetPrice())
            .Sum();
        return price;
    }

    public double GetTicketsNormalBusPrice()
    {
        var price = Tickets
            .Where(_ => _.Bus is NormalBus)
            .Select(_ => _.GetPrice())
            .Sum();

        return price;
    }

    public List<VipBus> GetVipBuses()
    {
        return Buses.OfType<VipBus>().ToList();
    }

    public List<NormalBus> GetNormalBuses()
    {
        return Buses.OfType<NormalBus>().ToList();
    }

    public List<Ticket> GetTicketsNotCanceled()
    {
        return Tickets
            .Where(_ => _ is not TicketCancellation)
            .ToList();
    }

    public override string ToString()
    {
        return "Path " + _origin + "-" + _destination;
    }
}