using DBModel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DBModel.Repositories
{
    public class DBDocumentOperation
    {
        public static bool CreateUsingProcedure(Document entity,
                                                HttpPostedFileBase file,
                                                string connectionString,
                                                string saveDirectory
                                                )
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var command = new SqlCommand("InsertDoc", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (file.ContentLength > 0)
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@Name", entity.Name);
                        command.Parameters.AddWithValue("@OriginalFN", entity.OriginalFileName);
                        command.Parameters.AddWithValue("@Date", entity.Date);
                        command.Parameters.AddWithValue("@Author", $"{entity.Author.Id.ToString()}");

                        SqlParameter retval = command.Parameters.Add("@Newid", SqlDbType.UniqueIdentifier);
                        retval.Direction = ParameterDirection.Output;
                        command.ExecuteNonQuery();
                        var docId = retval.Value;

                        Directory.CreateDirectory(Path.Combine(saveDirectory, entity.Author.Id.ToString()));

                        string path = Path.Combine(saveDirectory, entity.Author.Id.ToString(), docId.ToString());
                        //file.SaveAs(path);
                        using (FileStream fs = File.Create(path))
                        {
                            file.InputStream.CopyTo(fs);
                        }
                        return true;
                    }

                    else
                        return false;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
