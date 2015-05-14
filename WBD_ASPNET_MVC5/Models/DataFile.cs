using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WBD_ASPNET_MVC5.Models
{
    public class DataFile
    {
        [Required]
        public string Id { get; set; }

        // A descriptive name for the file so users can easily understand it
        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string DataName { get; set; }

        // The name + extension that the server saved the file under
        [Required]
        [StringLength(100)]
        [Display(Name = "File Reference")]
        public string FileReference { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Data Category")]
        public string DataCategory { get; set; }

        [StringLength(140)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Upload Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime UploadDate { get; set; }

        [Required]
        public string UploaderID { get; set; }
    }
}