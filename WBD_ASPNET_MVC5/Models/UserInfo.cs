using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WBD_ASPNET_MVC5.Models
{
    public class UserInfo
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Phone Number")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:(###)###-####}")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Sign-up Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime SignUpDate { get; set; }
    }
}