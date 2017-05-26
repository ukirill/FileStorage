using DBModel.Models;
using FluentNHibernate.Mapping;

namespace DBModel.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(u => u.Id)
                .Column("Id")
                .GeneratedBy.GuidComb();

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
