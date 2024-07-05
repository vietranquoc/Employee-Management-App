using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class AccountDAO
    {
        public static EmployeeManagementContext context = new EmployeeManagementContext();

        public static List<AccountMember> GetAccountMembers()
        {
            return context.AccountMembers.ToList();
        }

        public static AccountMember? GetAccountById(string id)
        {
            return context.AccountMembers.Find(id);
        }
    }
}
