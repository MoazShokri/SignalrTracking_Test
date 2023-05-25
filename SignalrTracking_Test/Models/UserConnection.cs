namespace SignalrTracking_Test.Models
{
    public class UserConnection
    {

        public string UserId { get; set; }
        public List<string> ConnectionIds { get; set; }
        public List<Vehicle> CustomizedVehicle { get; set; }

        public UserConnection(string userId, List<string> connectionIds, List<Vehicle> customizedVehicle)
        {
            UserId = userId;
            ConnectionIds = connectionIds;
            CustomizedVehicle = customizedVehicle;
        }

       
    }
}
