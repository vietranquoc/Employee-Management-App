using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class JobRepository : IJobRepository
    {
        public void DeleteJob(Job job) => JobDAO.DeleteJob(job);

        public Job? GetJobById(string id) => JobDAO.GetJobById(id);

        public List<Job> GetJobs() => JobDAO.GetJobs();

        public void InsertJob(Job job) => JobDAO.InsertJob(job);

        public void UpdateJob(Job job) => JobDAO.UpdateJob(job);
    }
}
