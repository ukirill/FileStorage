using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel.Models;
using NHibernate;
using System.Security.Cryptography;
using DBModel.Helpers;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace DBModel.Repositories
{
    public class NHUserRepository : IUserRepository
    {

        private string GetHashPassword(string password, byte[] salt)
        {
            byte[] saltedPassword = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();
            var saltedHash = new SHA256Managed().ComputeHash(saltedPassword);
            return Convert.ToBase64String(saltedHash);
        }

        private KeyValuePair<string, byte[]> GetSaltedHashPassword(string password)
        {
            byte[] salt = new byte[8];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(salt);
            }

            return new KeyValuePair<string, byte[]>
                (GetHashPassword(password, salt), salt);
        }

        private bool ValidatePassword(string password, string saltedHash, byte[] salt)
        {
            return saltedHash == GetHashPassword(password, salt);
        }

        public void Create(User entity)
        {
            var hs = GetSaltedHashPassword(entity.Password);
            entity.Password = hs.Key;
            entity.Salt = hs.Value;
            using (ISession session = NHHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {              
                session.Save(entity);
                transaction.Commit();
            }
        }

        public void Delete(User entity)
        {
            using (ISession session = NHHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(entity);
                transaction.Commit();
            }
        }

        public IQueryable<User> GetAll()
        {
            IQueryable<User> result;
            ISession session = NHHelper.OpenSession();
            result = session.Query<User>();
            return result;
        }

        public void Update(User entity)
        {
            using (ISession session = NHHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(entity);
                transaction.Commit();
            }
        }

        public bool Validate(string login, string password)
        {
            using (ISession session = NHHelper.OpenSession())
            {
                var u = session.QueryOver<User>().Where(user => user.Email == login).SingleOrDefault();

                if (u != null)
                {
                    return ValidatePassword(password, u.Password, u.Salt);
                }
                else return false;
            }
        }

        public User GetUserByEmail(string email)
        {
            return GetAll().FirstOrDefault(u => u.Email == email);
        }
    }
}
