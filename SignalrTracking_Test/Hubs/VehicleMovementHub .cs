using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalrTracking_Test.Data;
using SignalrTracking_Test.ViewModels;

namespace SignalrTracking_Test.Models
{
    public class VehicleMovementHub : Hub
    {
        private readonly ApplicationDbContext _db;
        private static Dictionary<string, List<string>> userConnectionsSameCar = new Dictionary<string, List<string>>();
        public static int notificationCounter = 0;
        public static List<string> messages = new();

        public VehicleMovementHub(ApplicationDbContext db)
        {
            this._db = db;
        }
        #region part one 

        //public async Task SendCarMessage(int vehicleId, string message)
        //{
        //    //var vehicleExists = _db.Vehicles.Any(v => v.Id == vehicleId);
        //    //var userOwnsVehicle = _db.userVehicles.Any(uv => uv.VehicleId == vehicleId && uv.UserId == Context.User.Identity.Name);
        //    //// Check if the vehicle ID exists in the database
        //    //if (vehicleExists && userOwnsVehicle)
        //    //{
        //    //    // Get the connection IDs of all users in the car group
        //    //    var userIds = _db.userVehicles
        //    //    .Where(uv => uv.VehicleId == vehicleId)
        //    //    .Select(uv => uv.UserId)
        //    //    .ToList();

        //    //    var connectionIds = userIds
        //    //        .Where(userId => userConnections.ContainsKey(userId))
        //    //        .SelectMany(userId => userConnections[userId])
        //    //        .ToList();
        //    // Check if the vehicle ID exists in the database
        //    if (_db.Vehicles.Any(v => v.Id == vehicleId))
        //    {
        //        // Create a message object with vehicle ID and message content
        //        var carMessage = new MessageTrack
        //        {
        //            VehicleId = vehicleId,
        //            Message = message
        //        };
        //        // Get the connection IDs of all users in the car group
        //        var connectionIds = Context.GetHttpContext().Request.Query["connectionId"];
        //           // Logic to send the message to the users in the same car
        //         await Clients.Clients(connectionIds).SendAsync("ReceiveCarMessage", carMessage);

        //    }
        //}

        //public override async Task OnConnectedAsync()
        //{
        //    //// Get the vehicle ID from the query string
        //    //var vehicleId = Context.GetHttpContext().Request.Query["vehicleId"];

        //    //// Add the connected user to their respective car group
        //    //await Groups.AddToGroupAsync(Context.ConnectionId, vehicleId);

        //    //await base.OnConnectedAsync();
        //    //var userId = Context.User.Identity.Name;
        //    //string connectionId = Context.ConnectionId;

        //    //if (!userConnections.ContainsKey(userId))
        //    //{
        //    //    userConnections[userId] = new List<string>();
        //    //}

        //    //userConnections[userId].Add(connectionId);

        //    //await base.OnConnectedAsync();
        //    var vehicleId = Context.GetHttpContext().Request.Query["vehicleId"];

        //    // Add the current user to the appropriate car group
        //    Groups.AddToGroupAsync(Context.ConnectionId, vehicleId);

        //    await base.OnConnectedAsync();
        //}

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    //// Get the vehicle ID from the query string
        //    //var vehicleId = Context.GetHttpContext().Request.Query["vehicleId"];

        //    //// Remove the disconnected user from their car group
        //    //await Groups.RemoveFromGroupAsync(Context.ConnectionId, vehicleId);

        //    //await base.OnDisconnectedAsync(exception);
        //    //string userId = Context.UserIdentifier;
        //    //string connectionId = Context.ConnectionId;

        //    //if (userConnections.ContainsKey(userId))
        //    //{
        //    //    userConnections[userId].Remove(connectionId);

        //    //    if (userConnections[userId].Count == 0)
        //    //    {
        //    //        userConnections.Remove(userId);
        //    //    }
        //    //}

        //    //await base.OnDisconnectedAsync(exception);
        //    var vehicleId = Context.GetHttpContext().Request.Query["vehicleId"];

        //    // Remove the current user from the appropriate car group
        //    Groups.RemoveFromGroupAsync(Context.ConnectionId, vehicleId);

        //    await base.OnDisconnectedAsync(exception);
        //}
        #endregion

        //public async Task SendCarNotification(int vehicleId, string message)
        //{

        //  //  Get the connection IDs of all users in the car group
        //  //var userIds = _db.userVehicles
        //  //   .Where(uv => uv.VehicleId == vehicleId)
        //  //   .Select(uv => uv.UserId)
        //  //   .ToList();
        //  //  var connectionIds = userIds
        //  //      .Where(userId => userConnectionsSameCar.ContainsKey(userId))
        //  //      .SelectMany(userId => userConnectionsSameCar[userId])
        //  //      .ToList();

        //  //  foreach (var connectionId in connectionIds)
        //  //  {
        //  //      await Clients.Client(connectionId).SendAsync("ReceiveCarMessage", message);
        //  //  }

        //    //// Broadcast the message to all connections in the car's group
        //    //if (userConnectionsSameCar.ContainsKey(vehicleId))
        //    //{
        //    //    List<string> connections = userConnectionsSameCar[vehicleId];
        //    //    foreach (string connectionId in connections)
        //    //    {
        //    //        await Clients.Client(connectionId).SendAsync("ReceiveCarMessage", message);
        //    //    }
        //    //}
        //    //await Clients.Group(vehicleId).SendAsync("ReceiveCarMessage", notification);
        //}

        //public override async Task OnConnectedAsync()
        //{
        //    // Join the group based on the vehicle ID
        //    var vehicleId = Context.GetHttpContext().Request.Query["vehicleId"];
        //    // Add the connection to the car's group
        //    if (!userConnectionsSameCar.ContainsKey(vehicleId))
        //    {
        //        userConnectionsSameCar[vehicleId] = new List<string>();
        //    }
        //    userConnectionsSameCar[vehicleId].Add(Context.ConnectionId);

        //    await base.OnConnectedAsync();
        //}
        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    // Remove the user from the vehicle group upon disconnection
        //    var vehicleId = Context.GetHttpContext().Request.Query["vehicleId"];
        //    // Remove the connection from the car's group
        //    if (userConnectionsSameCar.ContainsKey(vehicleId))
        //    {
        //        userConnectionsSameCar[vehicleId].Remove(Context.ConnectionId);
        //        if (userConnectionsSameCar[vehicleId].Count == 0)
        //        {
        //            userConnectionsSameCar.Remove(vehicleId);
        //        }
        //    }
        //    //await Groups.RemoveFromGroupAsync(Context.ConnectionId, vehicleId);

        //    await base.OnDisconnectedAsync(exception);
        //}


        //public async Task SendMessage(int vehicleId ,string message)
        //{

        //    if (!string.IsNullOrEmpty(message))
        //    {
        //        notificationCounter++;
        //        messages.Add(message);
        //        await LoadMessages(vehicleId);
        //    }
        //}

        //public async Task LoadMessages(int vehicleId)
        //{
        //    //Get the connection IDs of all users in the car group
        //    var userIds = _db.userVehicles
        //       .Where(uv => uv.VehicleId == vehicleId)
        //       .Select(uv => uv.UserId)
        //       .ToList();
        //    var connectionIds = userIds
        //        .Where(userId => userConnectionsSameCar.ContainsKey(userId))
        //        .SelectMany(userId => userConnectionsSameCar[userId])
        //        .ToList();
        //    foreach (var connectionId in connectionIds)
        //    {
        //        await Clients.Client(connectionId).SendAsync("LoadNotification", messages , notificationCounter);
        //    }
        //    //await Clients.Client.SendAsync("LoadNotification", messages, notificationCounter);
        //}

        ////public async Task SendMessage(int vehicleId, string message)
        ////{
        ////    if (!string.IsNullOrEmpty(message))
        ////    {
        ////        Add the message to the database
        ////         ...

        ////         Get the connection IDs of all users in the car group
        ////        var userIds = _db.userVehicles
        ////            .Where(uv => uv.VehicleId == vehicleId)
        ////            .Select(uv => uv.UserId)
        ////            .ToList();
        ////        var connectionIds = userIds
        ////       .SelectMany(userId => userConnectionsSameCar.ContainsKey(userId) ? userConnectionsSameCar[userId] : new List<string>())
        ////       .ToList();
        ////        Broadcast the message to all users in the car group
        ////       await Clients.Groups(connectionIds).SendAsync("ReceiveMessage", vehicleId, message);
        ////    }
        ////}

        ////public override async Task OnConnectedAsync()
        ////{
        ////    Get the vehicle ID from the query string
        ////    string vehicleId = Context.GetHttpContext().Request.Query["vehicleId"];

        ////    Add the connection to the car group
        ////   await Groups.AddToGroupAsync(Context.ConnectionId, vehicleId);

        ////    await base.OnConnectedAsync();
        ////}

        ////public override async Task OnDisconnectedAsync(Exception exception)
        ////{
        ////    Get the vehicle ID from the query string
        ////    string vehicleId = Context.GetHttpContext().Request.Query["vehicleId"];

        ////    Remove the connection from the car group
        ////   await Groups.RemoveFromGroupAsync(Context.ConnectionId, vehicleId);

        ////    await base.OnDisconnectedAsync(exception);
        ////}
        ///
        public async Task SendMessage(MessageTrack message)
        {
            if (message != null)
            {
                // Add the message to the database 
                _db.messageTracks.Add(message);


                // Get the connection IDs of all users in the car group
                var userIds = _db.userVehicles
                    .Where(uv => uv.VehicleId == message.VehicleId)
                    .Select(uv => uv.UserId)
                    .ToList();

                var connectionIds = userIds
                    .SelectMany(userId => userConnectionsSameCar.ContainsKey(userId) ? userConnectionsSameCar[userId] : new List<string>())
                    .ToList();

                // Broadcast the message to all users in the car group
                await Clients.Groups(connectionIds).SendAsync("ReceiveMessage", message);
            }
        }

        public override async Task OnConnectedAsync()
        {
            // Get the vehicle ID from the query string
            string vehicleId = Context.GetHttpContext().Request.Query["vehicleId"];

            // Add the connection to the car group
            await Groups.AddToGroupAsync(Context.ConnectionId, vehicleId);

            // Add the connection to the dictionary of user connections
            var userId = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(userId))
            {
                if (userConnectionsSameCar.ContainsKey(userId))
                {
                    userConnectionsSameCar[userId].Add(Context.ConnectionId);
                }
                else
                {
                    userConnectionsSameCar.Add(userId, new List<string> { Context.ConnectionId });
                }
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Get the vehicle ID from the query string
            string vehicleId = Context.GetHttpContext().Request.Query["vehicleId"];

            // Remove the connection from the car group
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, vehicleId);

            // Remove the connection from the dictionary of user connections
            var userId = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(userId) && userConnectionsSameCar.ContainsKey(userId))
            {
                userConnectionsSameCar[userId].Remove(Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
