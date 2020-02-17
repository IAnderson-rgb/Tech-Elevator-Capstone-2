using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class EmployeeSqlDAO : IEmployeeDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public EmployeeSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        public IList<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM employee;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);


                        employees.Add(employee);
                    }
                }
            }
            catch (Exception)
            {

                throw;

            }

            return employees;
        }

        /// <summary>
        /// Searches the system for an employee by first name or last name.
        /// </summary>
        /// <remarks>The search performed is a wildcard search.</remarks>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns>A list of employees that match the search.</returns>
        public IList<Employee> Search(string firstname, string lastname)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM employee WHERE first_name LIKE @fname OR last_name LIKE @lname;", conn);
                    cmd.Parameters.AddWithValue("@fname", '%'+firstname+'%');
                    cmd.Parameters.AddWithValue("@lname", '%'+lastname+'%');
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);


                        employees.Add(employee);
                    }
                }
            }
            catch (Exception)
            {

                throw;

            }

            return employees;
        }

        /// <summary>
        /// Gets a list of employees who are not assigned to any active projects.
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM employee LEFT JOIN project_employee ON employee.employee_id = project_employee.employee_id WHERE project_employee.project_id IS NULL", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);


                        employees.Add(employee);
                    }
                }
            }
            catch (Exception)
            {

                throw;

            }

            return employees;
        }
    }
}
