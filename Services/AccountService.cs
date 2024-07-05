using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iAccountRepository;

        public AccountService()
        {
            iAccountRepository = new AccountRepository();
        }

        public AccountMember? GetAccountById(string id)
        {
            return iAccountRepository.GetAccountById(id);
        }

        public List<AccountMember> GetAccountMembers()
        {
            return iAccountRepository.GetAccountMembers();
        }
    }
}
