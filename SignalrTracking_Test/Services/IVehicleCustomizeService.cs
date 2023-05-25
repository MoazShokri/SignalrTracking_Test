using SignalrTracking_Test.Models;

namespace SignalrTracking_Test.Services
{
    public interface IVehicleCustomizeService
    {
        public List<Vehicle> GetCustomizedVehicleForUser(string userId);
    }
}
