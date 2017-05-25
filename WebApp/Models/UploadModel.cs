using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.Models
{

    public class UploadModel
    {
        [Required]
        [Display(Name="Название документа")]
        public string Name { get; set; }
    }
}