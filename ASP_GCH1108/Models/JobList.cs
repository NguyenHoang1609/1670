using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_GCH1108.Models
{
    public class JobList
    {
        [Key]
        public int JobId { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string JobDescription { get; set; }
        [Required]
        public string RequiredProfiles { get; set; }
        [Required]
        public DateTime ApplicationDeadline { get; set; }
        public bool status { get; set; } = false;

        //User Id
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }
        public string? UserName { get; set; }

        //Connect
        public ICollection<JobApplication>? Application { get; set; }
    }
}
