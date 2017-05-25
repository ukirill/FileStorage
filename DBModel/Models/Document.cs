using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Models
{
    public class Document
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string OriginalFileName { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual User Author { get; set; }
    }
}
