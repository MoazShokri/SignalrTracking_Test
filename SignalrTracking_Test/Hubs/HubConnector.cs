using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections.Client;
using SignalrTracking_Test.Data;
using SignalrTracking_Test.Models;
using System.Net;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SignalrTracking_Test.Services;

//using Hub_CustomProtocol;

namespace SignalrTracking_Test.Hubs
{
    public class HubConnector : Hub
    {
        private readonly ApplicationDbContext _db;
        private readonly IVehicleCustomizeService _customizeService;

        public HubConnector(ApplicationDbContext db  , IVehicleCustomizeService customizeService)
        {
            this._db = db;
            this._customizeService = customizeService;
        }
        public override Task OnConnectedAsync()
        {
            var UserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!String.IsNullOrEmpty(UserId))
            {
                var userName = _db.Users.FirstOrDefault(u => u.Id == UserId).UserName;
                var customizedVehicle = _customizeService.GetCustomizedVehicleForUser(UserId);

                Clients.Users(HubConnections.OnlineUsers()).SendAsync("ReceiveUserConnected", UserId, userName);
                HubConnections.AddUserConnection(UserId, Context.ConnectionId, customizedVehicle);

            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var UserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (HubConnections.HasUserConnection(UserId, Context.ConnectionId))
            {
                var UserConnections = HubConnections.Users[UserId];
                UserConnections.ConnectionIds.Remove(Context.ConnectionId);

                //HubConnections.Users.Remove(UserId);
                //if (UserConnections.Any())
                //    HubConnections.Users.Add(UserId, UserConnections);
                if (UserConnections.ConnectionIds.Count == 0)
                {
                    HubConnections.Users.Remove(UserId);
                }
            }

            if (!String.IsNullOrEmpty(UserId))
            {
                var userName = _db.Users.FirstOrDefault(u => u.Id == UserId).UserName;
                Clients.Users(HubConnections.OnlineUsers()).SendAsync("ReceiveUserDisconnected", UserId, userName);
                //HubConnections.AddUserConnection(UserId, Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }

       
    }
}
