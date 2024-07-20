using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private readonly WatercolorsPainting2024DBContext _context = null;

        private AccountDAO()
        {
            if (_context == null)
            {
                _context = new WatercolorsPainting2024DBContext();
            }
        }

        public static AccountDAO Instance
        {
           get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public UserAccount Login(string email, string password)
        {
            return _context.UserAccounts.SingleOrDefault(a => a.UserEmail == email && a.UserPassword == password);
        }
    }
}
