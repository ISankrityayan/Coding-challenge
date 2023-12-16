using System;
namespace Coding_Challenge.DAL.Models
{
	public class Applicant
	{
        public int ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }



        public void CreateProfile(string email, string firstName, string lastName, string phone)
        {
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone; 
        }

        public void ApplyForJob(int jobId, string coverLetter)
        {
            
        }
    }


}

