using Terminal.Buses;
using Terminal.Tickets;

namespace Terminal;

public static class Application
{
    public static readonly List<Path> Paths = new();
    public static readonly List<Bus> Buses = new();

    public static List<Path> GetPaths()
    {
        return Paths;
    }
    
    public static void AddTicketReservation(
        string userInputSeat,
        Bus bus,
        Path path)
    {
        var ticket = new TicketReserve(
            Convert.ToInt32(userInputSeat),
            bus,
            path);
        path.Tickets.Add(ticket);

        (path + " - " + bus + " - " +
         $"price : {ticket.GetPrice()}/ reserved ").ShowSuccessful();
    }

    public static void AddTicketPurchase(
        string userInputSeat,
        Bus bus,
        Path path)
    {
        var ticket = new TicketPurchase(
            Convert.ToInt32(userInputSeat),
            bus,
            path);
        path.Tickets.Add(ticket);
        
        (path + " - " + bus + " - " +
         $"price : {ticket.GetPrice()}/ purchased ").ShowSuccessful();
    }
    
}