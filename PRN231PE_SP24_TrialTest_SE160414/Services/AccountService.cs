using BusinessObjects.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepo _accountRepo;

        public AccountService()
        {
               _accountRepo = new AccountRepo();
        }

        public UserAccount Login(string email, string password)
            => _accountRepo.Login(email, password);
    }
}
