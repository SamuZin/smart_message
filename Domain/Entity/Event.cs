namespace Domain.Entity
{
    public class Event
    {
        DateTime Date { get; set; }
        string? EventName { get; set; }
        string? EventDescription { get; set; }
        string? EventTag { get; set; }
    }
}
