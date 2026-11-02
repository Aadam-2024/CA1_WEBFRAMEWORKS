using System;

namespace CA_NIHARIKA_WEBSHOP.Models
{
    public class Registration
    {
        private string firstName;
        private int age;
        private string workshopName;
        private bool requiresLaptop;

        public string FirstName { get => firstName; set => firstName = value; }
        public int Age { get => age; set => age = value; }
        public string WorkshopName { get => workshopName; set => workshopName = value; }
        public bool RequiresLaptop { get => requiresLaptop; set => requiresLaptop = value; }

        public Registration()
        {
            this.firstName = "";
            this.age = 0;
            this.workshopName = "";
            this.requiresLaptop = false;
        }

        public Registration(string firstName, int age, string workshopName, bool requiresLaptop)
        {
            this.firstName = firstName;
            this.age = age;
            this.workshopName = workshopName;
            this.requiresLaptop = requiresLaptop;
        }

        public override string ToString()
        {
            string laptopStatus = this.requiresLaptop ? "Yes" : "No";
            return $"{this.firstName},{this.age},{this.workshopName},{laptopStatus}";
        }
    }
}
