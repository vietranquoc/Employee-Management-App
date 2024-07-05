using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository iJobRepository;

        public JobService()
        {
            iJobRepository = new JobRepository();
        }

        public void DeleteJob(Job job)
        {
            iJobRepository.DeleteJob(job);
        }

        public Job? GetJobById(string id)
        {
            return iJobRepository.GetJobById(id);
        }

        public List<Job> GetJobs()
        {
            return iJobRepository.GetJobs();
        }

        public void InsertJob(Job job)
        {
            iJobRepository.InsertJob(job);
        }

        public void UpdateJob(Job job)
        {
            iJobRepository.UpdateJob(job);
        }
    }
}
