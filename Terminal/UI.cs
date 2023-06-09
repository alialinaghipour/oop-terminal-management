using System.Globalization;
using Terminal.Buses;
using Terminal.Tickets;

namespace Terminal;

public static class Ui
{
    public static void Run()
    {
        var isRunning = true;
        while (isRunning)
        {
            ShowMainMenu();
            var mainMenuInput = Console.ReadLine()!;

            try
            {
                switch (mainMenuInput)
                {
                    case MenuTools.MenuManagementBus:
                    {
                        AddBus();
                        continue;
                    }
                    case MenuTools.MenuManagementPath:
                    {
                        PathManagement();
                        continue;
                    }
                    case MenuTools.MenuTicketReservation:
                    {
                        TicketReservationManagement();
                        continue;
                    }
                    case MenuTools.MenuBuyingTicket:
                    {
                        TicketPurchaseManagement();
                        continue;
                    }
                    case MenuTools.MenuCancelledTicket:
                    {
                        var path = GetPath();
                        
                        //کنسلی ها نباید بیاد
                        var ticket = GetTicketInPath(path);
                        ticket.Cancelled();
                        ConsoleTools.ModelWarning(ticket + "/ cancelled");
                        continue;
                    }
                    case MenuTools.MenuIncomeReport:
                    {
                        var path = GetPath();

                        var input = ConsoleTools
                            .GetStringValue("1 : total income\n" +
                                            "2 : income of VIP buses\n" +
                                            "3 : Income from purchased VIP buses\n" +
                                            "4 : Income from reservation VIP buses\n" +
                                            "5 : income of Normal buses\n" +
                                            "6 : Income from purchased Normal buses\n" +
                                            "7 : Income from reservation Normal buses\n" +
                                            "8 : Income from cancellation");
                        switch (input)
                        {
                            case "1":
                            {
                                var totalPrice = path.GetTicketsNormalBusPrice() +
                                        path.GetTicketsVipBusPrice();
                                ConsoleTools.ModelPrimary(totalPrice
                                    .ToString(CultureInfo.CurrentCulture));
                                break;
                            }
                            case "2":
                            {
                                var price = path.GetTicketsVipBusPrice();
                                ConsoleTools.ModelPrimary(price
                                    .ToString(CultureInfo.InvariantCulture));
                                break;
                            }
                            case "3":
                            {
                                var price = path.GetTicketsPurchasedVipBusPrice();
                                ConsoleTools.ModelPrimary(price
                                    .ToString(CultureInfo.InvariantCulture));
                                break;
                            }
                            case "4":
                            {
                                var price = path.GetTicketsReservationVipBusPrice();
                                ConsoleTools.ModelPrimary(price
                                    .ToString(CultureInfo.InvariantCulture));
                                break;
                            }
                            case "5":
                            {
                                var price = path.GetTicketsNormalBusPrice();
                                ConsoleTools.ModelPrimary(price
                                    .ToString(CultureInfo.InvariantCulture));
                                break;
                            }
                            case "6":
                            {
                                var price = path.GetTicketsPurchasedNormalBusPrice();
                                ConsoleTools.ModelPrimary(price
                                    .ToString(CultureInfo.InvariantCulture));
                                break;
                            }
                            case "7":
                            {
                                var price = path.GetTicketsReservationNormalBusPrice();
                                ConsoleTools.ModelPrimary(price
                                    .ToString(CultureInfo.InvariantCulture));
                                break;
                            }
                            case "8":
                            {
                                var price = path.GetTicketsCancellationPrice();
                                ConsoleTools.ModelPrimary(price
                                    .ToString(CultureInfo.InvariantCulture));
                                break;
                            }
                        }
                        
                        continue;
                    }
                    case MenuTools.MenuExit:
                    {
                        isRunning = false;
                        continue;
                    }
                }
            }
            catch (Exception exception)
            {
                exception.Message.ShowError();
            }
        }
    }

    private static Ticket GetTicketInPath(Path path)
    {
        var tickets = path.GetTicketsNotCanceled();
        tickets.ShowListWithIndex("tickets");
        var userInputIndex = ConsoleTools
            .GetStringValue("Enter Index ticket?");
        tickets.CheckIndexValid(userInputIndex);

        var indexTicket = Convert.ToInt32(userInputIndex);
        var ticket = tickets[indexTicket];
        return ticket;
    }

    private static void TicketPurchaseManagement()
    {
        var path = GetPath();
        var busInput = ConsoleTools
            .GetStringValue("1 : VIP\n" +
                            "2 : Normal");
        switch (busInput)
        {
            case "1":
            {
                var bus = GetVipBusInPath(path);
                var userInputSeat = ConsoleTools
                    .GetStringValue("Enter seat number?");
                Application.AddTicketPurchase(
                    userInputSeat,
                    bus,
                    path);
                break;
            }
            case "2":
            {
                var bus = GetNormalBusInPath(path);
                var userInputSeat = ConsoleTools
                    .GetStringValue("Enter seat number?");
                Application.AddTicketPurchase(
                    userInputSeat,
                    bus,
                    path);
                break;
            }
        }
    }

    private static void TicketReservationManagement()
    {
        var path = GetPath();
        var busInput = ConsoleTools
            .GetStringValue("1 : VIP\n" +
                            "2 : Normal");
        switch (busInput)
        {
            case "1":
            {
                var bus = GetVipBusInPath(path);
                var userInputSeat = ConsoleTools
                    .GetStringValue("Enter seat number?");
                Application.AddTicketReservation(
                    userInputSeat,
                    bus,
                    path);
                break;
            }
            case "2":
            {
                var bus = GetNormalBusInPath(path);
                var userInputSeat = ConsoleTools
                    .GetStringValue("Enter seat number?");
                Application.AddTicketReservation(
                    userInputSeat,
                    bus,
                    path);
                break;
            }
        }
    }

    private static NormalBus GetNormalBusInPath(Path path)
    {
        ConsoleTools.ModelWarning(
            $"Normal bus price : {path.GetNormalBusPrice()}");
        var buses = path.GetNormalBuses();
        buses.ShowListWithIndex("Normal buses");
        var userBusInput = ConsoleTools
            .GetStringValue("Enter index bus ?");
        buses.CheckIndexValid(userBusInput);
        var indexBus = Convert.ToInt32(userBusInput);

        var bus = buses[indexBus];
        return bus;
    }

    private static VipBus GetVipBusInPath(Path path)
    {
        ConsoleTools.ModelWarning($"Vip bus price : {path.GetVipBusPrice()}");
        var buses = path.GetVipBuses();
        buses.ShowListWithIndex("VIP buses");
        var userBusInput = ConsoleTools
            .GetStringValue("Enter index bus ?");
        buses.CheckIndexValid(userBusInput);
        var indexBus = Convert.ToInt32(userBusInput);

        var bus = buses[indexBus];
        return bus;
    }

    private static Path GetPath()
    {
        var paths = Application.GetPaths();
        paths.ShowListWithIndex("paths");
        var userInput = ConsoleTools
            .GetStringValue("Enter index path ?");
        paths.CheckIndexValid(userInput);
        var index = Convert.ToInt32(userInput);
        var path = paths[index];
        return path;
    }

    private static void AddBus()
    {
        var menuBus = ConsoleTools
            .GetStringValue("1 : Add bus\n" +
                            "2 : Show buses");
        switch (menuBus)
        {
            case "1":
            {
                var busName = ConsoleTools.GetStringValue("Enter Bus name ?");
                var busType = ConsoleTools.GetStringValue("Enter Type Bus ?\n" +
                                                          $"1 : VIP\n" +
                                                          $"2 : Normal\n");

                switch (busType)
                {
                    case "1":
                    {
                        var newVipBus = new VipBus()
                        {
                            Name = busName
                        };
                        Application.Buses.Add(newVipBus);
                        break;
                    }
                    case "2":
                    {
                        var newNormalBus = new NormalBus()
                        {
                            Name = busName
                        };
                        Application.Buses.Add(newNormalBus);
                        break;
                    }
                }

                break;
            }
            case "2":
            {
                var buses = Application.Buses;
                buses.ShowListWithIndex("buses");
                break;
            }
        }
    }

    private static void ShowMainMenu()
    {
        const string mainMenu = $"{MenuTools.MenuManagementBus} : Management Bus\n" +
                                $"{MenuTools.MenuManagementPath} : Management Path\n" +
                                $"{MenuTools.MenuTicketReservation} : Reservation Ticket\n" +
                                $"{MenuTools.MenuBuyingTicket} : Buying Ticket\n" +
                                $"{MenuTools.MenuCancelledTicket} : Cancelled Ticket\n" +
                                $"{MenuTools.MenuIncomeReport} : Income Report\n" +
                                $"{MenuTools.MenuExit} : Exit";
        mainMenu.ShowSuccessful();
    }

    private static void PathManagement()
    {
        var input = ConsoleTools.GetStringValue("1 : Add new path\n" +
                                                "2 : Add a bus to the path\n" +
                                                "3 : Show paths");
        switch (input)
        {
            case MenuTools.AddNewPath:
            {
                var origin = ConsoleTools
                    .GetStringValue("Enter Origin ?");
                var destination = ConsoleTools
                    .GetStringValue("Enter Destination ?");
                var basePrice = ConsoleTools
                    .GetStringValue("Enter base price ?");
                var buses = Application.Buses;
                buses.ShowListWithIndex("buses");
                var userInput = ConsoleTools
                    .GetStringValue("Enter index bus ?");
                buses.CheckIndexValid(userInput);

                var busIndex = Convert.ToInt32(userInput);

                var bus = buses[busIndex];
                var path = new Path(
                    Convert.ToInt32(basePrice),
                    origin,
                    destination);
                Application.Paths.Add(path);

                path.Buses.Add(bus);
                bus.Paths.Add(path);
                break;
            }
            case MenuTools.AddBusToPath:
            {
                var paths = Application.GetPaths();
                paths.ShowListWithIndex("paths");
                var userIndex = ConsoleTools
                    .GetStringValue("Enter index path ?");
                paths.CheckIndexValid(userIndex);
                var pathIndex = Convert.ToInt32(userIndex);

                var path = paths[pathIndex];

                var buses = Application.Buses
                    .Where(_ => path.Buses.Any(p => p.Code != _.Code))
                    .ToList();

                buses.ShowListWithIndex("buses");
                var inputBus = ConsoleTools
                    .GetStringValue("Enter index bus ?");
                buses.CheckIndexValid(inputBus);

                var busIndex = Convert.ToInt32(inputBus);

                var bus = buses[busIndex];

                bus.Paths.Add(path);
                path.Buses.Add(bus);

                break;
            }
            case MenuTools.ShowPaths:
            {
                var paths = Application.Paths;
                paths.ShowListWithIndex("paths");
                break;
            }
        }
    }
}