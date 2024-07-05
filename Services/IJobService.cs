﻿using BusinessObjects;
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
    }
}
