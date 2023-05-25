namespace SignalrTracking_Test.Models
{
    public class DailyInformation
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Message { get; set; }

        public DateTime UpdateDateTime { get; set; }
    }
}
