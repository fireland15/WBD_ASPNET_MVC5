using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WBD_ASPNET_MVC5.Models
{
    public class UserProjectAssociation
    {
        [Required]
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }

        [Required]
        [Key]
        [Column(Order = 1)]
        public string ProjectId { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}