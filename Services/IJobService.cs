using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IJobService
    {
        List<Job> GetJobs();
        void InsertJob(Job job);
        void UpdateJob(Job job);
        void DeleteJob(Job job);
        Job? GetJobById(string id);

        /* New feature */
        //List<Job> GetJobByTitle(string title);
        //List<Job> GetJobBySalary(int minSalary, int maxSalary);
        bool checkIdExist(string id);
        List<Job> FilterJob(string? title, int? minSalary, int? maxSalary);
    }
}
