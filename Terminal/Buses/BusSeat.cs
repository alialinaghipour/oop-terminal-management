namespace Terminal.Buses; 
public class BusSeat
{
    private readonly byte _number;

    public required byte Number
    {
        get => _number;
        init
        {
            if (value >= 1)
            {
                _number = value;
            }
            else
            {
                const string error = "The seat number entered is not valid";
                throw new Exception(error);
            }
        }
    }
    public TypeBusSeat TypeSeat { get; set; } = TypeBusSeat.Empty;
}