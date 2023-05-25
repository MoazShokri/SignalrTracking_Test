using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SignalrTracking_Test.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Vehicle> vehicles { get; set; }
    }
}
