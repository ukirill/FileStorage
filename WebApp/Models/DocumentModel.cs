using DBModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class DocumentModel
    {
        [Display(Name = "Операция")]
        [Required]
        public string Name { get; set; }

        public string OriginalFileName { get; set; }

        public DateTime Date { get; set; }

        public User Author { get; set; }

        public string Path { get; set; }

    }
}