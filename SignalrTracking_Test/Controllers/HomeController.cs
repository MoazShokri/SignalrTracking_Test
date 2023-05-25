using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalrTracking_Test.Data;
using SignalrTracking_Test.Hubs;
using SignalrTracking_Test.Models;
using SignalrTracking_Test.Services;
using SignalrTracking_Test.ViewModels;
using System.Diagnostics;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SignalrTracking_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser>_userManager; // TEST NOT USED
        private readonly ApplicationDbContext _db;
        private readonly IVehicleCustomizeService _customizeService;
        private readonly IHubContext<VehicleMovementHub> _hubContext; // TEST NOT USED
        private readonly IHubContext<TrackingMsgHub> _hub;
        private readonly IHubContext<DataInformationHub> _hub1;
        private readonly IDataInformationChecker _dataInformationChecker;  // TEST NOT USED

        public HomeController(ILogger<HomeController> logger , UserManager<ApplicationUser> userManager ,
           ApplicationDbContext db, IVehicleCustomizeService customizeService , 
            IHubContext<VehicleMovementHub> hubContext  , 
            IHubContext<TrackingMsgHub> hub,
            IHubContext<DataInformationHub> hub1 ,
             IDataInformationChecker dataInformationChecker)
        {
            _logger = logger;
            this._userManager = userManager;
            this._db = db;
            this._customizeService = customizeService;
            this._hubContext = hubContext;
            this._hub = hub;
            this._hub1 = hub1;
            this._dataInformationChecker = dataInformationChecker;
        }


        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userVehicles = _db.userVehicles
                    .Where(uv => uv.UserId == userId)
                    .Select(uv => uv.VehicleId)
                    .ToList();

                var userLastMessages = _db.messageTracks
                    .Where(mt => mt.UserId == userId && userVehicles.Contains(mt.VehicleId))
                    .OrderByDescending(mt => mt.SentDateTime)
                    .ToList();

                var otherUsersLastMessages = _db.messageTracks
                    .Where(mt => mt.UserId != userId && userVehicles.Contains(mt.VehicleId))
                    .GroupBy(mt => mt.UserId)
                    .Select(g => g.OrderByDescending(mt => mt.SentDateTime).FirstOrDefault())
                    .ToList();

                var viewModel = new LastMessagesViewModel
                {
                    UserLastMessages = userLastMessages,
                    OtherUsersLastMessages = otherUsersLastMessages
                };
                return View(viewModel);
            }

            return View(new LastMessagesViewModel());

        }
        [Authorize]
        public IActionResult CheckMsg()
        {
            var latestUpdate = _db.dailyInformation
                .OrderByDescending(d => d.UpdateDateTime)
                .FirstOrDefault();

            if (latestUpdate != null)
            {
                // Get the owner(s) of the vehicle
                var vehicleOwners = _db.userVehicles
                    .Where(uv => uv.VehicleId == latestUpdate.VehicleId)
                    .Select(uv => uv.UserId)
                    .ToList();

                if (vehicleOwners.Count > 0)
                {
                    // Broadcast the update to the vehicle owners using SignalR
                    var message = latestUpdate.Message;

                    foreach (var ownerId in vehicleOwners)
                    {
                        _hub1.Clients.User(ownerId).SendAsync("ReceiveMessage", message);
                    }

                    // Return the message as a JSON response
                    return Json(new { message });
                }
            }

            // If no message is sent, return an empty JSON response
            return Json(new { });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        [ActionName("VehicleList")]
        public async Task<IActionResult>VehicleList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customizedVehicles =_customizeService.GetCustomizedVehicleForUser(userId);
            return View(customizedVehicles);
        }
      
        [HttpGet]
        public IActionResult GetAllVehicles()
        {
            var vehicles = _db.Vehicles.ToList();
            var data = vehicles.Select(v => new
            {
                id = v.Id,
                vehicletype = v.VehicleType,
                platenumber = v.PlateNumber,
                imei = v.IMEI,
                applicationuserId = v.ApplicationUserId
            });


            return Json(new { data });
        }
        //[Authorize]
        //[HttpGet]
        //public IActionResult SendMessage()
        //{
        //    var model = new MessageTrack();
        //    return View(model);
        //}
        //[Authorize]
        //[HttpPost]
        //public IActionResult SendMessage(MessageTrack model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Check if the vehicle ID exists in the database
        //        if (_db.Vehicles.Any(v => v.Id == model.VehicleId))
        //        {
        //            //Get the connection IDs of all users in the car group
        //             var userIds = _db.userVehicles
        //               .Where(uv => uv.VehicleId == model.VehicleId)
        //               .Select(uv => uv.UserId)
        //               .ToList();
        //            //Send the message to the car using the vehicle ID
                    
        //            // Send the message to the user who initiated it
        //            _hubContext.Clients.User(User.Identity.Name).SendAsync("ReceiveMessage", model.VehicleId, model.Message);

        //            // Send the message to all other users who share the same car
        //            var otherUserIds = userIds.Except(new List<string> { User.Identity.Name });
        //            _hubContext.Clients.Users(otherUserIds).SendAsync("ReceiveMessage", model.VehicleId, model.Message);
        //            // Redirect to a success page or perform any other necessary action
        //            //return RedirectToAction("Success");

        //            // Set the success message in TempData
        //            TempData["SuccessMessage"] = "Message sent successfully.";
        //        }
        //        else
        //        {
        //            // Set an error message in TempData
        //            TempData["ErrorMessage"] = "Invalid vehicle ID.";
        //        }
                

        //        return RedirectToAction("Index" , new {vehicleId = model.VehicleId , message = model.Message});
        //    }
        //    return View(model);
        //}
        [Authorize]
        [HttpGet]
        public IActionResult SendMessageToVehicle()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendMessageToVehicle(int vehicleId, string message)
        {
            // Check if the message is null or empty
            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be empty.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Check if the user owns the vehicle
                var userOwnsVehicle = _db.userVehicles.Any(uv => uv.UserId == userId && uv.VehicleId == vehicleId);
                if (!userOwnsVehicle)
                {
                    return BadRequest("Invalid vehicle or unauthorized access.");
                }

                // Create a new message
                var messageTrack = new MessageTrack
                {
                    VehicleId = vehicleId,
                    Message = message,
                    SentDateTime = DateTime.Now,
                    UserId = userId
                };

                // Add the message to the database
                _db.messageTracks.Add(messageTrack);
                await _db.SaveChangesAsync();

                // Get the vehicle group name
                var groupName = vehicleId.ToString();

                // Send the message to all clients in the vehicle group
                await _hub.Clients.Group(groupName).SendAsync("ReceiveMessage", messageTrack);

                return RedirectToAction("Index");
           
        }

      

    }
}