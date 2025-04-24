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

    public Pin GetMapPin() {
        return new Pin {
            Label = string.IsNullOrWhiteSpace(Title) ? "Unknown Location" : Title,
            Address = LocationName,
            Location = new Location(Latitude, Longitude),
            Type = PinType.Place
        };
    }
}
