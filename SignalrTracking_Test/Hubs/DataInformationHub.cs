using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalrTracking_Test.Data;

namespace SignalrTracking_Test.Hubs
{
    public class DataInformationHub : Hub
    {
        //private readonly ApplicationDbContext _context;

        //public DataInformationHub(ApplicationDbContext context)
        //{
        //    this._context = context;
        //}


        //public async Task SendMessageToOwner(string userId, string message)
        //{
        //    // Send the message to the owner's clients
        //    await Clients.User(userId).SendAsync("ReceiveMessage", message);
        //}
        //private void CheckForUpdatesAsync(object state)
        //{
        //    // Get the latest update from the DataInformation table
        //    var latestUpdate = _context.dailyInformation
        //        .OrderByDescending(d => d.UpdateDateTime)
        //        .FirstOrDefault();

        //    if (latestUpdate != null)
        //    {
        //        // Get the owner(s) of the vehicle
        //        var vehicleOwners = _context.userVehicles
        //            .Where(uv => uv.VehicleId == latestUpdate.VehicleId)
        //            .Select(uv => uv.UserId)
        //            .ToList();

        //        if (vehicleOwners.Count > 0)
        //        {
        //            // Broadcast the update to the vehicle owners using SignalR
        //            var message = latestUpdate.Message;

        //            foreach (var ownerId in vehicleOwners)
        //            {
        //                Clients.User(ownerId).SendAsync("ReceiveMessage", message);
        //            }
        //        }
        //    }
        //}


    }
}
