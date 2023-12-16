using System;
using Coding_Challenge.BLL.Interface;
using Coding_Challenge.DAL.Models;

namespace Coding_Challenge.BLL.Implementation
{
	public class DatabaseManagementService
	{
        private readonly IDatabaseManagement _databaseManagement;

        public DatabaseManagementService(IDatabaseManagement databaseManagement)
        {
            _databaseManagement = databaseManagement;
        }

        public void DisplayJobListings()
        {
            List<JobListing> jobListings = _databaseManagement.GetJobListings();

         
            Console.WriteLine("Job Listings:");
            foreach (var job in jobListings)
            {
                Console.WriteLine($"Title: {job.JobTitle}, Job Type: {job.JobType}, Description: {job.JobDesc}," +
                    $" CompanyID: {job.CompanyId}, Salary: {job.Salary}, Posted Date: {job.PostedDate} ");
            }
        }

        public void InsertApplicantService()
        {
            Console.WriteLine("Enter Applicant Information:");

            Console.Write("Applicant ID: ");
            int applicantId;
            if (!int.TryParse(Console.ReadLine(), out applicantId))
            {
                Console.WriteLine("Invalid Applicant ID. Please enter a valid number.");
                return;
            }

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Resume Path: ");
            string resume = Console.ReadLine();

            Applicant applicant = new Applicant
            {
                ApplicantID =applicantId,
                FirstName=firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Resume = resume
            };

            _databaseManagement.InsertApplicant(applicant);

            Console.WriteLine("Applicant inserted successfully.");
        }

        public void InsertJobApplicationService()
        {
            Console.WriteLine("Enter Job Application Information:");

            Console.Write("Job ID: ");
            int jobId;
            if (!int.TryParse(Console.ReadLine(), out jobId))
            {
                Console.WriteLine("Invalid Job ID. Please enter a valid number.");
                return;
            }

            Console.Write("Applicant ID: ");
            int applicantId;
            if (!int.TryParse(Console.ReadLine(), out applicantId))
            {
                Console.WriteLine("Invalid Applicant ID. Please enter a valid number.");
                return;
            }

            Console.Write("Application Date (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime applicationDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in YYYY-MM-DD format.");
                return;
            }

            Console.Write("Cover Letter: ");
            string coverLetter = Console.ReadLine();

            
            JobApplication jobApplication = new JobApplication
            {
                JobID = jobId,
                ApplicantID = applicantId,
                ApplicationDate = applicationDate,
                CoverLetter = coverLetter
            };

            
            _databaseManagement.InsertJobApplication(jobApplication);

            Console.WriteLine("Job application submitted successfully.");
        }
        public void InsertJobListingService()
        {
            Console.WriteLine("Enter Job Listing Information:");

            Console.Write("Company ID: ");
            int companyId;
            if (!int.TryParse(Console.ReadLine(), out companyId))
            {
                Console.WriteLine("Invalid Company ID. Please enter a valid number.");
                return;
            }

            Console.Write("Job Title: ");
            string jobTitle = Console.ReadLine();

            Console.Write("Job Description: ");
            string jobDesc = Console.ReadLine();

            Console.Write("Job Location: ");
            string jobLocation = Console.ReadLine();

            Console.Write("Salary: ");
            decimal salary;
            if (!decimal.TryParse(Console.ReadLine(), out salary) || salary < 0)
            {
                Console.WriteLine("Invalid Salary. Please enter a non-negative number.");
                return;
            }

            Console.Write("Job Type: ");
            string jobType = Console.ReadLine();

            Console.Write("Posted Date (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime postedDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in YYYY-MM-DD format.");
                return;
            }

            JobListing jobListing = new JobListing
            {
                CompanyId = companyId,
                JobTitle = jobTitle,
                JobDesc = jobDesc,
                JobLocation = jobLocation,
                Salary = salary,
                JobType = jobType,
                PostedDate = postedDate
            };

            try
            {
                
                _databaseManagement.InsertJobListing(jobListing);

                Console.WriteLine("Job listing posted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



    }
}

