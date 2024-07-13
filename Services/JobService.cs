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

        /* New feature */

        /*
        public List<Job> GetJobBySalary(int minSalary, int maxSalary)
        {
            var jobs =
                GetJobs()
                .Where(j => j.MinSalary >= minSalary && j.MaxSalary <= maxSalary)
                .ToList();
            return jobs;
        }

        public List<Job> GetJobByTitle(string title)
        {
            var jobs = 
                GetJobs()
                .Where(j => j.JobTitle.ToLower().Contains(title))
                .ToList();
            return jobs;
        }
        */

        public bool checkIdExist(string id)
        {
            var job =
                GetJobs()
                .Any(j => j.JobId == id);
            return job;
        }

        public List<Job> FilterJob(string? title, int? minSalary, int? maxSalary)
        {
            var allJobs = GetJobs().AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                allJobs = allJobs.Where(j => j.JobTitle.Contains(title, StringComparison.OrdinalIgnoreCase));
            }
            if (minSalary.HasValue && maxSalary.HasValue)
            {
                allJobs = allJobs.Where(j => j.MinSalary >= minSalary && j.MaxSalary <= maxSalary);
            }

            return allJobs.ToList();
        }
    }
}
