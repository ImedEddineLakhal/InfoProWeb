using Domain.Entity;
using MyReports.Data.Infrastracture;
using MyReports.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
        
        ReportContext context = new ReportContext();
        public User getByLogin(string login)
        {

            var user = context.users.FirstOrDefault(a => a.login == login);


            if (user != null)
            {

                return user;
            }


            else
            {
                return null;
            }

        }
        public List<User> getByLoginList(string login)
        {
            var usersquery = context.users.Where(a => a.login == login);

            List<User> users = new List<User>();

            if (usersquery != null)
            {
                foreach (User item in usersquery)
                {

                    users.Add(item);
                }

                return users;
            }


            else
            {
                return null;
            }

        }

    }
}