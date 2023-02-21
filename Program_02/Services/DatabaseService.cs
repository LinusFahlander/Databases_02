using Microsoft.Data.SqlClient;
using Program_2.Models;
using Program_2.Models.Entitites;
using System.Net;

namespace Program_2.Services
{
    internal class DatabaseService
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Skola\Databases\Databases_02\Databases_02\Program_02\Data\local_sql.mdf;Integrated Security=True;Connect Timeout=30";
        public void SaveEmployee(Employee employee)
        {
            var employeeEntity = new EmployeeEntity
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                AddressId = GetOrSaveAddress(employee.Address)
            };

            SaveEmployeeToDatabase(employeeEntity);
        }

        public IEnumerable<Employee> GetAllEmployees() 
        {
            var employees = new List<Employee>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT e.Id, e.FirstName, e.LastName, e.Email, e.PhoneNumber, a.StreetName, a.PostalCode, a.City FROM Employees e JOIN Addresses a ON e.AddressId = a.Id", conn);
            var result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    employees.Add(new Employee
                    {
                        Id = result.GetInt32(0),
                        FirstName = result.GetString(1),
                        LastName = result.GetString(2),
                        Email = result.GetString(3),
                        PhoneNumber = result.GetString(4),
                        Address = new Address
                        {
                            StreetName = result.GetString(5),
                            PostalCode = result.GetString(6),
                            City = result.GetString(7),
                        }
                    });
                }
            }
            return employees;
        }

        public Employee GetOneEmployee(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT e.Id, e.FirstName, e.LastName, e.Email, e.PhoneNumber, a.StreetName, a.PostalCode, a.City FROM Employees e JOIN Addresses a ON e.AddressId = a.Id WHERE e.Email = @Email", conn);
            cmd.Parameters.AddWithValue("@Email", email);

            var employee = new Employee();
            var result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    employee.Id = result.GetInt32(0);
                    employee.FirstName = result.GetString(1);
                    employee.LastName = result.GetString(2);
                    employee.Email = result.GetString(3);
                    employee.PhoneNumber = result.GetString(4);
                    employee.Address = new Address
                    {
                        StreetName = result.GetString(5),
                        PostalCode = result.GetString(6),
                        City = result.GetString(7),
                    };
                }

                return employee;
            }

            return null!;
            

        }

        public int GetOrSaveAddress(Address address)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("IF NOT EXISTS (SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City) INSERT INTO Addresses OUTPUT INSERTED.Id VALUES (@StreetName, @PostalCode, @City) ELSE SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City", conn);
            cmd.Parameters.AddWithValue("@StreetName", address.StreetName);
            cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
            cmd.Parameters.AddWithValue("@City", address.City);

            return int.Parse(cmd.ExecuteScalar().ToString()!);
        }

        public void SaveEmployeeToDatabase(EmployeeEntity employeeEntity)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("IF NOT EXISTS (SELECT Id FROM Employees WHERE Email = @Email) INSERT INTO Employees VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @AddressId)", conn);
            cmd.Parameters.AddWithValue("@FirstName", employeeEntity.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employeeEntity.LastName);
            cmd.Parameters.AddWithValue("@Email", employeeEntity.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", employeeEntity.PhoneNumber);
            cmd.Parameters.AddWithValue("@AddressId", employeeEntity.AddressId);

            cmd.ExecuteNonQuery();
        }
    }
}
