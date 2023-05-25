using Microsoft.AspNet.Identity;

namespace SignalrTracking_Test.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string VehicleType { get; set; }
        public string PlateNumber { get; set; }
        public char LeftLetter { get; set; }
        public char MiddleLetter { get; set; }
        public char RightLetter { get; set; }
        public string IMEI { get; set; }
        public string ApplicationUserId { get; set; } 




    }
}
