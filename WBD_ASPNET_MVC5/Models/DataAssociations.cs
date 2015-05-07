using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WBD_ASPNET_MVC5.Models
{
    public class DataProjectAssoc
    {
        [Required]
        public int DataId { get; set; }

        [Required]
        public string ProjectId { get; set; }
    }

    public class DataUserAssoc
    {
        [Required]
        public int DataId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}