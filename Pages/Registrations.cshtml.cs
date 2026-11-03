
using Microsoft.AspNetCore.Mvc.RazorPages;
using CA_NIHARIKA_WEBSHOP.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace CA_NIHARIKA_WEBSHOP.Pages
{
    public class RegistrationsModel : PageModel
    {
        public List<Registration> Registrations { get; set; } = new List<Registration>();

        public string Message { get; set; } = "";

        private readonly string filePath = Path.Combine(
            Directory.GetCurrentDirectory(), "registrations.txt");

        public void OnGet()
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    Message = "No registrations have been saved yet.";
                    return;
                }

                string[] lines = System.IO.File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] details = line.Split(',');

                    if (details.Length == 4 &&
                        int.TryParse(details[1], out int age))
                    {
                        bool requiresLaptop = details[3] == "Yes";

                        Registration registration = new Registration(
                            details[0],
                            age,
                            details[2],
                            requiresLaptop
                        );

                        Registrations.Add(registration);
                    }
                }

                if (Registrations.Count == 0)
                {
                    Message = "No valid registrations were found.";
                }
            }
            catch (Exception)
            {
                Message = "An error occurred while reading the registration file.";
            }
        }
    }
}
