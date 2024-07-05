using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class JobDAO
    {
        public static EmployeeManagementContext context = new EmployeeManagementContext();

        public static List<Job> GetJobs()
        {
            return context.Jobs.ToList();
        }

        public static void InsertJob(Job job)
        {
            context.Jobs.Add(job);
            context.SaveChanges();  
        }

        public static void UpdateJob(Job job)
        {
            context.Jobs.Update(job);
            context.SaveChanges();
        }

        public static void DeleteJob(Job job)
        {
            context.Jobs.Remove(job);
            context.SaveChanges();
        }

        public static Job? GetJobById(string id)
        {
            return context.Jobs.Find(id);
        }
    }
}
