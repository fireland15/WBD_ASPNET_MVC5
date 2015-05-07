using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WBD_ASPNET_MVC5.Models
{
    public class Project
    {
        [Required] 
        public string Id { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        [StringLength(100)]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Brief Description")]
        [StringLength(140)]
        public string BriefDescription { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        [StringLength(1000)]
        public string LongDescription { get; set; }

        [Required]
        [Display(Name = "Created On")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string OwnerID { get; set; }
    }

    public class ViewProjectsModel
    {
        public Project project;
        public UserInfo UInfo;
    }

    public class ProjectUserViewModel
    {
        public Project project { get; set; }
        public ApplicationUser user { get; set; }
    }

    public class ProjectUsernameViewModel
    {
        public string projectID { get; set; }
        public string Username { get; set; }    
    }
}