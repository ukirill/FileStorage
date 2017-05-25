using DBModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DBModel.Repositories
{
    public interface IDocumentRepository : IBaseRepository<Document>
    {
        IEnumerable<Document> FindDocuments(User user, string search);
        bool CreateWithFile(Document entity, HttpPostedFileBase file, string saveDirectory);
    }
}
