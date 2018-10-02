using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FindieServer.Models.IdentityModels
{
    public class AppUser : IdentityUser<int>
    {
        public bool? IsBanned { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(20)]
        public string Localization { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        [StringLength(250)]
        public string AccountDescription { get; set; }

        public bool? IsUserOnline { get; set; }
    }
}