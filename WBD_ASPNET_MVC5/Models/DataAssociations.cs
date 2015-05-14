using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WBD_ASPNET_MVC5.Models
{
    public class DataProjectAssoc
    {
        [Required]
        [Key]
        [Column(Order = 0)]
        public string DataId { get; set; }

        [Required]
        [Key]
        [Column(Order = 1)]
        public string ProjectId { get; set; }
    }

    public class DataUserAssoc
    {
        [Required]
        [Key]
        [Column(Order = 0)]
        public string DataId { get; set; }

        [Required]
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }
    }

    public class AttachData
    {
        public bool AddToProject { get; set; }
        public DataFile datafile { get; set; }
    }

    public class DataProjectListViewModel
    {
        public Project project { get; set; }
        public DataFile data { get; set; }
    }
}