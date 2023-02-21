using Program_2.Models;

namespace Program_2.Services
{
    internal class MenuService
    { 
        public void CreateNewEmployee()
        {
            var employee = new Employee();

            Console.Write("Förnamn: ");
            employee.FirstName = Console.ReadLine() ?? "";

            Console.Write("Efternamn: ");
            employee.LastName = Console.ReadLine() ?? "";

            Console.Write("E-postadress: ");
            employee.Email = Console.ReadLine() ?? "";

            Console.Write("Telefonnummer: ");
            employee.PhoneNumber = Console.ReadLine() ?? "";

            Console.Write("Gatuadress: ");
            employee.Address.StreetName = Console.ReadLine() ?? "";

            Console.Write("Postnummer: ");
            employee.Address.PostalCode = Console.ReadLine() ?? "";

            Console.Write("City: ");
            employee.Address.City = Console.ReadLine() ?? "";

            var database = new DatabaseService();
            database.SaveEmployee(employee);
        }
        public void ViewAllEmployees()
        {
            var database = new DatabaseService();
            var employees = database.GetAllEmployees();

            if(employees.Any())
            {
                foreach (Employee employee in employees)
                {
                    Console.WriteLine($"Anställningsnr: {employee.Id}");
                    Console.WriteLine($"Namn: {employee.FirstName} {employee.LastName}");
                    Console.WriteLine($"E-postadress: {employee.Email}");
                    Console.WriteLine($"PhoneNumber: {employee.PhoneNumber}");
                    Console.WriteLine($"Address: {employee.Address.StreetName}, {employee.Address.PostalCode} {employee.Address.City}");
                    Console.WriteLine("");
                }
            } else
            {
                Console.Clear();
                Console.WriteLine("Inga anställda finns i databasen...");
                Console.WriteLine("");
            }
        }
        public void ViewSpecificEmployee()
        {
            var database = new DatabaseService();

            Console.Write("Ange e-postadress för den anställde: ");
            var email = Console.ReadLine();
            
            if (!string.IsNullOrEmpty(email))
            {
                var employee = database.GetOneEmployee(email);

                if (employee != null)
                {
                    Console.WriteLine($"Anställningsnr: {employee.Id}");
                    Console.WriteLine($"Namn: {employee.FirstName} {employee.LastName}");
                    Console.WriteLine($"E-postadress: {employee.Email}");
                    Console.WriteLine($"PhoneNumber: {employee.PhoneNumber}");
                    Console.WriteLine($"Address: {employee.Address.StreetName}, {employee.Address.PostalCode} {employee.Address.City}");
                    Console.WriteLine("");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Ingen anställd med e-postadressen {email} hittades...");
                    Console.WriteLine("");
                }
            } else
            {
                Console.Clear();
                Console.WriteLine($"Ingen e-postadress angiven...");
                Console.WriteLine("");
            }
        }
    }
}
