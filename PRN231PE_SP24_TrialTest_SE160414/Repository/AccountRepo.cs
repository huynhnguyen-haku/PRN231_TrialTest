using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepo : IAccountRepo
    {
        public UserAccount Login(string email, string password)
            => AccountDAO.Instance.Login(email, password);
    }
}
