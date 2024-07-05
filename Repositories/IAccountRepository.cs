using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories
{
    public interface IAccountRepository
    {
        List<AccountMember> GetAccountMembers();
        AccountMember? GetAccountById(string id);
    }
}
