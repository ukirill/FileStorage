using DBModel.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Mappings
{
    public class DocumentMap : ClassMap<Document>
    {
        public DocumentMap()
        {
            Id(d => d.Id)
                .Column("Id")
                .GeneratedBy.GuidComb();

            Map(d => d.Name)
                .Length(4000);
            Map(d => d.OriginalFileName)
                .Length(4000);
            Map(u => u.Date);

            References(d => d.Author);
            Table("Documents");
        }
    }
}
