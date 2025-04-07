using Microsoft.Maui.Controls.Maps;

public class CrimeNotification
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string LocationName { get; set; }
    public DateTime Date { get; set; }
    public string Severity { get; set; }
    public string Status { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    //Returns a Pin with information set from the CrimeNotification
    public Pin GetMapPin() {
        return new Pin {
            Label = Title,
            Address = LocationName,
            Location = new Location(Latitude, Longitude),
            Type = PinType.Place
        };
    }
}
