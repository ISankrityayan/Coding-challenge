
using System;
using Coding_Challenge.DAL.Models;

namespace Coding_Challenge.BLL.Interface
{
	public interface IDatabaseManagement
	{
        
        public void InsertJobListing(JobListing job);
        public void InsertCompany(Company company);
        public void InsertApplicant(Applicant applicant);
        public void InsertJobApplication(JobApplication application);
        public List<JobListing> GetJobListings();
        public List<Company> GetCompanies();
        public List<Applicant> GetApplicants();
        public List<JobApplication> GetApplicationsForJob(int jobID);
        


    }
}

