namespace SignalrTracking_Test.ViewModels
{
    public class MessageTrack
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Message { get; set; }
        public DateTime SentDateTime { get; set; }
        public string UserId { get; set; } // Add user ID property
    }
}
