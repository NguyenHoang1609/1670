using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_GCH1108.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        //User Id
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }
        //Connect
        public ICollection<JobApplication>? Application { get; set; }
    }
}
