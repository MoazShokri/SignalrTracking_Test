using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SignalrTracking_Test.Data;
using SignalrTracking_Test.Models;
using SignalrTracking_Test.Services;
using SignalrTracking_Test.ViewModels;
using System.Security.Claims;

namespace SignalrTracking_Test.Hubs
{
    public class TrackingMsgHub : Hub
    {
        private readonly ApplicationDbContext _db;
        private readonly IVehicleCustomizeService _customizeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TrackingMsgHub(ApplicationDbContext db, IVehicleCustomizeService customizeService, UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            this._customizeService = customizeService;
            this._userManager = userManager;
        }

        public TrackingMsgHub(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        //public async Task SendMessage(MessageTrack message)
        //{
        //    await Clients.Group(message.VehicleId.ToString()).SendAsync("ReceiveMessage", message);
        //}

        //public async Task JoinVehicleGroup(string vehicleId)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, vehicleId);
        //}

        //public async Task LeaveVehicleGroup(string vehicleId)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, vehicleId);
        //}

        //public override async Task OnConnectedAsync()
        //{
        //    var user = await _userManager.GetUserAsync(Context.User);
        //    var userId = user?.Id;

        //    // Generate a unique user-specific identifier for the connection
        //    var connectionId = $"{userId}_{Context.ConnectionId}";

        //    // Add the user to the corresponding group based on the vehicle ID
        //    if (int.TryParse(Context.User.Identity.Name, out int vehicleId))
        //    {
        //        await Groups.AddToGroupAsync(connectionId, vehicleId.ToString());
        //    }

        //    await base.OnConnectedAsync();
        //}

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    var user = await _userManager.GetUserAsync(Context.User);
        //    var userId = user?.Id;

        //    // Generate a unique user-specific identifier for the connection
        //    var connectionId = $"{userId}_{Context.ConnectionId}";

        //    if (int.TryParse(Context.User.Identity.Name, out int vehicleId))
        //    {
        //        await Groups.RemoveFromGroupAsync(connectionId, vehicleId.ToString());
        //    }
        //    await base.OnDisconnectedAsync(exception);
        //}


        /* New Code */
        [HubMethodName("SendMessage")]
        public async Task SendMessage(int vehicleId, string message)
        {
            var senderUsername = Context.User.Identity.Name;

            await Clients.Group(vehicleId.ToString()).SendAsync("ReceiveMessage", senderUsername, message);
        }


        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var userId = user?.Id;

            // Generate a unique user-specific identifier for the connection
            var connectionId = $"{userId}_{Context.ConnectionId}";
            // Add the user to the corresponding group based on the vehicle ID
            if (int.TryParse(Context.User.Identity.Name, out int vehicleId))
            {
                await Groups.AddToGroupAsync(connectionId, vehicleId.ToString());
            }

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var userId = user?.Id;

            // Generate a unique user-specific identifier for the connection
            var connectionId = $"{userId}_{Context.ConnectionId}";

            if (int.TryParse(Context.User.Identity.Name, out int vehicleId))
            {
                await Groups.RemoveFromGroupAsync(connectionId, vehicleId.ToString());
            }
            await base.OnDisconnectedAsync(exception);
        }
    }



} 

