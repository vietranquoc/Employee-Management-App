using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        List<AccountMember> GetAccountMembers();
        AccountMember? GetAccountById(string id);
    }
}
