using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using CA_NIHARIKA_WEBSHOP.Models;

namespace CA_NIHARIKA_WEBSHOP.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; } = "";

        [BindProperty]
        public int? Age { get; set; }

        [BindProperty]
        public string WorkshopName { get; set; } = "";

        [BindProperty]
        public bool RequiresLaptop { get; set; }

        public string SuccessMessage { get; set; } = "";
        public string ErrorMessage { get; set; } = "";

        private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "registrations.txt");

        public void OnGet() {}

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                ErrorMessage = "First name is strictly required.";
                return Page();
            }

            if (Age == null || Age < 16 || Age > 120)
            {
                ErrorMessage = "Age must be a valid number between 16 and 120.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(WorkshopName))
            {
                ErrorMessage = "Please select a valid workshop option from the list.";
                return Page();
            }

            try
            {
                Registration newRegistration = new Registration(FirstName, Age.Value, WorkshopName, RequiresLaptop);

                using (StreamWriter sw = System.IO.File.AppendText(filePath))
                {
                    sw.WriteLine(newRegistration.ToString());
                }

                SuccessMessage = $"Registration Confirmed! Summary: Participant {newRegistration.FirstName} (Age: {newRegistration.Age}) successfully registered for '{newRegistration.WorkshopName}'. Laptop Requirement: {(newRegistration.RequiresLaptop ? "Yes" : "No")}.";
                
                ModelState.Clear();
                FirstName = "";
                Age = null;
                WorkshopName = "";
                RequiresLaptop = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected server error occurred saving data: {ex.Message}";
            }

            return Page();
        }
    }
}
