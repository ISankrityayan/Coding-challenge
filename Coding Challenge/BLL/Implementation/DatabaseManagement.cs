using System;
using Coding_Challenge.Utilities;
using System.Data.SqlClient;
using Coding_Challenge.BLL.Interface;
using Coding_Challenge.DAL.Models;
using System.Text.RegularExpressions;
using Coding_Challenge.Exceptions;

namespace Coding_Challenge.BLL.Implementation
{
    public class DatabaseManagement : IDatabaseManagement
    {
        SqlCommand command = null;
        public string connectionString;


        public DatabaseManagement()
        {
            connectionString = ConnectionStringUtility.GetConnectionString("MyConnectionString");
            command = new SqlCommand();

        }

        public SqlConnection OpenConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                return conn;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Failed to open a database connection.", ex);
            }
        }

        public decimal CalculateAverageSalary()
        {
            try
            {
                using (SqlConnection conn = OpenConnection())
                {
                    conn.Open();
                    string sql = "SELECT Salary FROM JobListing;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        decimal totalSalary = 0;
                        int validJobCount = 0;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                decimal salary = (decimal)reader["Salary"];

                                if (salary < 0)
                                {
                                    throw new InvalidSalaryException("Invalid salary: " + salary);
                                }

                                totalSalary += salary;
                                validJobCount++;
                            }
                        }



                        return totalSalary / validJobCount;
                    }
                }
            }
            catch (InvalidSalaryException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
               
                return -1; 
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
               
                return -1; 
            }
        }
            private bool IsValidEmailFormat(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

           
            return Regex.IsMatch(email, pattern);
        }

        public JobListing GetJobListingById(int jobId)
        {
            try
            {
                using (SqlConnection conn = OpenConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM JobListing WHERE JobId = @JobId;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@JobId", jobId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                JobListing job = new JobListing
                                {
                                    JobId = (int)reader["JobId"],
                                    CompanyId = (int)reader["CompanyID"],
                                    JobTitle = reader["JobTitle"].ToString(),
                                    JobDesc = reader["JobDesc"].ToString(),
                                    JobLocation = reader["JobLocation"].ToString(),
                                    Salary = (decimal)reader["Salary"],
                                    JobType = reader["JobType"].ToString(),
                                    PostedDate = (DateTime)reader["PostedDate"],
                                    Deadline = (DateTime)reader["Deadline"] 
                                };

                                return job;
                            }
                        }
                    }
                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine("Database Connection Error: " + ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }

            return null; 
        }



        public void InsertJobListing(JobListing job)
        {
            try
            {
                using (SqlConnection conn = OpenConnection())
                {
                    conn.Open();
                    string sql = @"INSERT INTO JobListing (CompanyID, JobTitle, JobDesc, JobLocation, Salary, JobType, PostedDate)
                           VALUES (@CompanyID, @JobTitle, @JobDesc, @JobLocation, @Salary, @JobType, @PostedDate);";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CompanyID", job.CompanyId);
                        cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                        cmd.Parameters.AddWithValue("@JobDesc", job.JobDesc);
                        cmd.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                        if (job.Salary < 0)
                        {
                            throw new InvalidSalaryException("Salary must be a non-negative value.");
                        }
                        cmd.Parameters.AddWithValue("@Salary", job.Salary);
                        cmd.Parameters.AddWithValue("@JobType", job.JobType);
                        cmd.Parameters.AddWithValue("@PostedDate", job.PostedDate);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine("Database Connection Error: " + ex.Message);
            }
            catch (InvalidSalaryException ex)
            {
                Console.WriteLine("salary Error: " + ex.Message);
            }
            catch (SqlException ex)
            {

                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine("General Error: " + ex.Message);
            }
        }
    

            public void InsertCompany(Company company)
            {
            try
            {
                using(SqlConnection conn = OpenConnection())
                {
                    conn.Open();
                        string sql = @"INSERT INTO Comapany(CompanyId,CompanyName,Location)
                          VALUES(@CompanyId,@CompanyName,@Location);";

                    using(SqlCommand cmd=new SqlCommand(sql,conn))
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", company.CompanyId);
                        cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);

                        cmd.Parameters.AddWithValue("@Location", company.Location);
                        cmd.ExecuteNonQuery();

                    }
                }

            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine("Database Connection Error: " + ex.Message);
            }
            catch (SqlException ex)
            {

                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine("General Error: " + ex.Message);
            }


             }

                public void InsertApplicant(Applicant applicant)
                {
                try
                {
                    using(SqlConnection conn = OpenConnection())
                    {
                        //conn.Open();
                        string sql = @"INSERT INTO Applicant( ApplicantId,FirstName, LastName,Email,Phone,Resume)
                           VALUES(@ApplicantId,@FirstName, @LastName,@Email,@Phone,@Resume)";
                        using(SqlCommand cmd=new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ApplicantId", applicant.ApplicantID);
                            cmd.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                            cmd.Parameters.AddWithValue("@LastName", applicant.LastName);
                        if (IsValidEmailFormat(applicant.Email))
                        {
                            cmd.Parameters.AddWithValue("@Email", applicant.Email);
                        }
                        else
                        {
                            
                            throw new InvalidEmailFormatExceptio("Invalid email format.");
                        }
                        cmd.Parameters.AddWithValue("@Phone", applicant.Phone);

                        if (string.IsNullOrEmpty(applicant.Resume))
                        {
                            throw new FileUploadException(FileUploadErrorType.FileNotFound, "Resume not uploaded.");
                        }

                        cmd.Parameters.AddWithValue("@Resume", applicant.Resume);

                            cmd.ExecuteNonQuery();
                        }
                   }
                }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine("Database Connection Error: " + ex.Message);
            }
            catch (InvalidEmailFormatExceptio ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                
            }
            catch (SqlException ex)
                {

                    Console.WriteLine("SQL Error: " + ex.Message);
                }

            catch (FileUploadException ex)
            {
                Console.WriteLine("File Upload Error: " + ex.Message);
            }
            catch (Exception ex)
                {

                    Console.WriteLine("General Error: " + ex.Message);
                }
                }

            public void InsertJobApplication(JobApplication application)
            {
            try
            {
                using(SqlConnection conn = OpenConnection())
                {
                    conn.Open();
                    JobListing jobListing = GetJobListingById(application.JobID);

                    if (jobListing != null && application.ApplicationDate > jobListing.Deadline)
                    {
                        throw new ApplicationDeadlineException("Application deadline has passed.");
                    }
                    string sql = @"INSERT INTO JobApplication(ApplicationId, JobId,ApplicantId,ApplicationDate,CoverLetter) VALUES
                                 (@ApplicationId, @JobId,@ApplicantId,@ApplicationDate,@CoverLetter);";

                    using(SqlCommand cmd=new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ApplicationId", application.ApplicationID);
                        cmd.Parameters.AddWithValue("@JobId", application.JobID);
                        cmd.Parameters.AddWithValue("@ApplicantId", application.ApplicantID);
                        cmd.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                        cmd.Parameters.AddWithValue("@CoverLetter", application.CoverLetter);
                        

                        cmd.ExecuteNonQuery();
                    }


                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine("Database Connection Error: " + ex.Message);
            }
            catch (SqlException ex)
            {

                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (ApplicationDeadlineException ex)
            {
                Console.WriteLine("Application Deadline Error: " + ex.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine("General Error: " + ex.Message);
            }

        }

            public List<JobListing> GetJobListings()
            {
            List<JobListing> jobListings = new List<JobListing>();
            try
            {
                using(SqlConnection conn = OpenConnection())
                {
                    conn.Open(); 
                    string sql = "SELECT * FROM JobListing;";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                JobListing job = new JobListing
                                {
                                    JobId = (int)reader["JobId"],
                                    CompanyId = (int)reader["CompanyID"],
                                    JobTitle = reader["JobTitle"].ToString(),
                                    JobDesc = reader["JobDesc"].ToString(),
                                    JobLocation = reader["JobLocation"].ToString(),
                                    Salary = (decimal)reader["Salary"],
                                    JobType = reader["JobType"].ToString(),
                                    PostedDate = (DateTime)reader["PostedDate"]
                                };

                                jobListings.Add(job);
                            }
                        }
                    }
                }
                return jobListings;
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine("Database Connection Error: " + ex.Message);
            }
            catch (SqlException ex)
            {

                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine("General Error: " + ex.Message);
            }

            return new List<JobListing>();
        }

            public List<Company> GetCompanies()
            {
            List<Company> companies = new List<Company>();
            try
            {
                using(SqlConnection conn = OpenConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM Company;";
                    using(SqlCommand cmd=new SqlCommand(sql, conn))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Company company = new Company
                                {
                                    CompanyId = (int)reader["CompanyId"],
                                    CompanyName = reader["CompanyName"].ToString(),
                                    Location = reader["Location"].ToString()

                                };

                                companies.Add(company);
                            }
                        }
                    }
                }

                return companies;
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine("Database Connection Error: " + ex.Message);
            }
            catch (SqlException ex)
            {

                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine("General Error: " + ex.Message);
            }

            return new List<Company>();

        }

            public List<Applicant> GetApplicants()
            {
            List<Applicant> applicants = new List<Applicant>();

            try
            {
                using (SqlConnection conn = OpenConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM Applicant;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Applicant applicant = new Applicant
                                {
                                    ApplicantID = (int)reader["ApplicantId"],
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Resume = reader["Resume"].ToString()
                                };

                                applicants.Add(applicant);
                            }
                        }
                    }
                }

                return applicants;
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine("Database Connection Error: " + ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }

            return new List<Applicant>();
        }

            public List<JobApplication> GetApplicationsForJob(int jobID)
            {

            List<JobApplication> jobApplications = new List<JobApplication>();

            try
            {
                using (SqlConnection conn = OpenConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM JobApplication WHERE JobId = @JobId;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@JobId", jobID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                JobApplication jobApplication = new JobApplication
                                {
                                    ApplicationID = (int)reader["ApplicationId"],
                                    JobID = (int)reader["JobId"],
                                    ApplicantID = (int)reader["ApplicantId"],
                                    ApplicationDate = (DateTime)reader["ApplicationDate"],
                                    CoverLetter = reader["CoverLetter"].ToString()
                                };

                                jobApplications.Add(jobApplication);
                            }
                        }
                    }
                }

                
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine("Database Connection Error: " + ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }

            return jobApplications;


        }
        }
    }






