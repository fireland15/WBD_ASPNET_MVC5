using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WBD_ASPNET_MVC5.Models
{
    public class UserAccountRoles
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }
    }

    public class UserRoles
    {
        [Required]
        public int Role { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}