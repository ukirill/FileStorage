using DBModel.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(u => u.Id);

            Map(u => u.FirstName);
            Map(u => u.LastName);
            Map(u => u.Email);
            Map(u => u.Password);
            Map(u => u.Salt);

            HasMany(u => u.Documents)
                .Inverse();

            Table("Users");
        }
    }
}
