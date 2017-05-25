using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class StorageModel
    {
        public IEnumerable<DocumentModel> DocumentsList { get; set; }

        public string SearchString { get; set; }
    }
}