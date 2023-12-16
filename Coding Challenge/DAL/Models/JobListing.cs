using System;
namespace Coding_Challenge.DAL.Models
{
	public class JobListing
	{
		public int JobId { get; set; }
		public int CompanyId { get; set; }
		public string JobTitle { get; set; }
        public string JobDesc{ get; set; }
        public string JobLocation { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime Deadline { get; set; }
        private List<JobApplication> applications = new List<JobApplication>();

        public void Apply(int applicantId, string coverLetter)
        {
            var application = new JobApplication
            {
                ApplicationID = applicantId + 1,
                JobID = this.JobId,
                ApplicantID = applicantId,
                ApplicationDate = DateTime.Now,
                CoverLetter = coverLetter
            };
            applications.Add(application);
        }

        public List<Applicant> GetApplicants(Dictionary<int, Applicant> allApplicants)
        {
            var applicantList = new List<Applicant>();
            foreach (var application in applications)
            {
                if (allApplicants.TryGetValue(application.ApplicantID, out var applicant))
                {
                    applicantList.Add(applicant);
                }
            }
            return applicantList;
        }



    }
}

