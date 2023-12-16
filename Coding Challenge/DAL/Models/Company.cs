using System;
namespace Coding_Challenge.DAL.Models
{
	public class Company
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; }
		public string Location { get; set; }

		private List<JobListing> jobListings = new List<JobListing>();

		public void PostJob(string jobTitle, string jobDesc, string jobLocation,decimal salary,string jobType)
		{
			var jobList = new JobListing
			{
				JobTitle = jobTitle,
				JobDesc = jobDesc,
				JobLocation=jobLocation,
				Salary=salary,
				JobType=jobType

			};
			jobListings.Add(jobList);

		}

		public List<JobListing> GetJobs()
		{
			return jobListings;
		}
	}
}

