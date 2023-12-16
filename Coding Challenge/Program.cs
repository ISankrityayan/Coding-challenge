using System;
using Coding_Challenge.BLL.Implementation;
using Coding_Challenge.BLL.Interface;

namespace Coding_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabaseManagement databaseManagement = new DatabaseManagement(); 

            DatabaseManagementService service = new DatabaseManagementService(databaseManagement);

            while (true)
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Display Job Listings");
                Console.WriteLine("2. Insert Applicant");
                Console.WriteLine("3. Insert Job Application");
                Console.WriteLine("4. Insert Job Listing");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice (1-5): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            service.DisplayJobListings();
                            break;
                        case 2:
                            service.InsertApplicantService();
                            break;
                        case 3:
                            service.InsertJobApplicationService();
                            break;
                        case 4:
                            service.InsertJobListingService();
                            break;
                        case 5:
                            Console.WriteLine("Exiting the program. Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option (1-5).");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option (1-5).");
                }

                Console.WriteLine();
            }
        }
    }
}
