using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Models
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual byte[] Salt { get; set; }
        public virtual IList<Document> Documents { get; set; }
    }
}
