using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SignalrTracking_Test.Data;
using SignalrTracking_Test.Hubs;
using SignalrTracking_Test.Models;

namespace SignalrTracking_Test.Services
{
    public class DataInformationChecker : IDataInformationChecker
    {
        private readonly IHubContext<DataInformationHub> _hubContext;
        private readonly ApplicationDbContext _context;

        public DataInformationChecker(IHubContext<DataInformationHub> hubContext, ApplicationDbContext context)
        {
            this._hubContext = hubContext;
            this._context = context;
        }
        public void Start()
        {
            //var timer =  new Timer(  CheckForUpdates, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            var timer = new Timer(async state => await CheckForUpdatesAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

        }
        // CODE NOT USED Synchronous
        //private  void CheckForUpdatesAsync(object state)
        //{
        //    // Get the latest update from the DataInformation table
        //    var latestUpdate =  _context.dailyInformation
        //        .OrderByDescending(d => d.UpdateDateTime)
        //        .FirstOrDefault();

        //    if (latestUpdate != null)
        //    {
        //        // Get the owner(s) of the vehicle
        //        var vehicleOwners =  _context.userVehicles
        //            .Where(uv => uv.VehicleId == latestUpdate.VehicleId)
        //            .Select(uv => uv.UserId)
        //            .ToList();

        //        if (vehicleOwners.Count > 0)
        //        {
        //            // Broadcast the update to the vehicle owners using SignalR
        //            var message = latestUpdate.Message;

        //            foreach (var ownerId in vehicleOwners)
        //            {
        //                _hubContext.Clients.User(ownerId).SendAsync("ReceiveMessage", message);
        //            }
        //        }
        //    }
        //}
        public async Task CheckForUpdatesAsync()
        {

            // Get the latest update from the DataInformation table asynchronously
            var latestUpdate =  await GetLatestUpdate();

            if (latestUpdate != null)
            {
                // Get the owner(s) of the vehicle asynchronously
                var vehicleOwners = await _context.userVehicles
                    .Where(uv => uv.VehicleId == latestUpdate.VehicleId)
                    .Select(uv => uv.UserId)
                    .ToListAsync();

                if (vehicleOwners.Count > 0)
                {
                    // Broadcast the update to the vehicle owners using SignalR
                    var message = latestUpdate.Message;

                    foreach (var ownerId in vehicleOwners)
                    {
                        await _hubContext.Clients.User(ownerId).SendAsync("ReceiveMessage", message);
                    }
                }
            }

        }
        private async Task<DailyInformation> GetLatestUpdate()
        {
            var latestUpdate = await _context.dailyInformation
               .OrderByDescending(d => d.UpdateDateTime)
               .FirstOrDefaultAsync();
            return latestUpdate;
        }


    }


    
}
