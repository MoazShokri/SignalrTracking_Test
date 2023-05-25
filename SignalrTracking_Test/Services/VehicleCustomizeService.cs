using Microsoft.EntityFrameworkCore;
using SignalrTracking_Test.Data;
using SignalrTracking_Test.Models;

namespace SignalrTracking_Test.Services
{
    public class VehicleCustomizeService : IVehicleCustomizeService
    {
        private readonly ApplicationDbContext _db;

        public VehicleCustomizeService(ApplicationDbContext db)
        {
            this._db = db;
        }
        public List<Vehicle> GetCustomizedVehicleForUser(string userId)
        {
            //var user = _db.Users.Include(u => u.vehicles).FirstOrDefault(u => u.Id == userId);
            //if (user != null)
            //{
            //    return user.vehicles.ToList();
            //}
            //return null;
            var user = _db.Users.Include(u => u.vehicles).FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                var userVehicleIds = _db.userVehicles.Where(vc => vc.UserId == userId).Select(vc => vc.VehicleId).ToList();
                var vehicles = _db.Vehicles.Where(v => userVehicleIds.Contains(v.Id)).ToList();

                //foreach (var vehicle in vehicles)
                //{
                //    var vehicleId = vehicle.Id;
                //    var vehicleType = vehicle.VehicleType;
                //    var vehicleIMEI = vehicle.IMEI;
                //    var User = vehicle.ApplicationUserId;
                //}
                var allVehicles = user.vehicles.Concat(vehicles).ToList();
                var distinctVehicles = allVehicles.GroupBy(v => v.Id).Select(g => g.First()).ToList();

                return distinctVehicles;

            }
            return null;


        }
    }
}
