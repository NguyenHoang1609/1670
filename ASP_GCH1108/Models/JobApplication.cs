using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_GCH1108.Models
{
    public class JobApplication
    {
        [Key]
        public int ApplicationId { get; set; }
        public string Introduction { get; set; }
        public string? Description { get; set; }
        public int Experience { get; set; }
        public bool? Status { get; set; } = false;
        public string? UrlImage { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }

        //UserId
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }

        //JobListId
        public int JobId { get; set; }
        [ForeignKey("JobId")]
        public JobList? JobList { get; set; }

        //QualificationId
        public int? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile? Profile { get; set; }
    }
}
