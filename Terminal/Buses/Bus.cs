namespace Terminal.Buses;

public abstract class Bus
{
    protected double PathPricePercent;
    protected byte SeatCount;

    protected Bus()
    {
        PathPricePercent = 1;
        SeatCount = 1;
        BusSeats = new List<BusSeat>();
        var random = new Random();
        Code = random.Next(1, 9999);
        Paths = new List<Path>();
    }

    public int Code { get; }
    public required string Name { get; set; }
    public double PricePercent => PathPricePercent;
    public List<Path> Paths { get; }

    public ICollection<BusSeat> BusSeats { get; protected init; }

    protected List<BusSeat> GenerateSeats()
    {
        var seats = new List<BusSeat>();
        for (byte i = 0; i < SeatCount; i++)
        {
            seats.Add(new BusSeat
            {
                Number = (byte)(i + 1)
            });
        }

        return seats;
    }
    public override string ToString()
    {
        return $"Bus {Name}";
    }
}