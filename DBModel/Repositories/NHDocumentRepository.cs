using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel.Models;
using NHibernate;
using DBModel.Helpers;
using NHibernate.Criterion;
using NHibernate.Linq;
using System.Web;
using System.IO;

namespace DBModel.Repositories
{
    public class NHDocumentRepository : IDocumentRepository
    {
        public void Create(Document entity)
        {
            using (ISession session = NHHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(entity);
                transaction.Commit();
            }
        }

        public bool CreateWithFile(Document entity, HttpPostedFileBase file, string saveDirectory)
        {
            using (ISession session = NHHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        session.Save(entity);
                        string path = Path.Combine(
                            saveDirectory,
                            entity.Author.Id.ToString(),
                            entity.Id.ToString());

                        file.SaveAs(path);

                        transaction.Commit();

                        return true;
                    }
                    else
                        return false;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool CreateWithFile(Document entity, HttpPostedFileBase file,
                                   string connectionString, string saveDirectory
                                )
        {
            var res = DBDocumentOperation.CreateUsingProcedure(entity, file, connectionString, saveDirectory);
            return false;
        }

        public void Delete(Document entity)
        {
            using (ISession session = NHHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(entity);
                transaction.Commit();
            }
        }

        public IEnumerable<Document> FindDocuments(User user, string search)
        {
            IList<Document> result;
            using (ISession session = NHHelper.OpenSession())
            {
                result = session.CreateCriteria<Document>()
                    .Add(Expression.Eq("Author", user))
                    .Add(Expression.Or(
                        Restrictions.InsensitiveLike("Name", search, MatchMode.Anywhere),
                        Restrictions.InsensitiveLike("OriginalFileName", search, MatchMode.Anywhere)
                        ))
                    .List<Document>();
            }
            return result;
        }

        public IQueryable<Document> GetAll()
        {
            IQueryable<Document> result;
            using (ISession session = NHHelper.OpenSession())
            {
                result = session.Query<Document>();
            }
            return result; ;
        }

        public void Update(Document entity)
        {
            using (ISession session = NHHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(entity);
                transaction.Commit();
            }
        }
    }
}
