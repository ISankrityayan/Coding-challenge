using System;
namespace Coding_Challenge.DAL.Models
{
	public class JobApplication
	{
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public int ApplicantID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string CoverLetter { get; set; }
    }
}

