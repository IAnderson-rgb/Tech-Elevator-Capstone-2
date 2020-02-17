using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class DepartmentSqlDAO : IDepartmentDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public DepartmentSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the departments.
        /// </summary>
        /// <returns></returns>
        public IList<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM department;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Department department = new Department();
                        department.Id = Convert.ToInt32(reader["department_id"]);
                        department.Name = Convert.ToString(reader["name"]);
                        

                        departments.Add(department);
                    }
                }
            }
            catch (Exception)
            {

                throw;

            }

            return departments;
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="newDepartment">The department object.</param>
        /// <returns>The id of the new department (if successful).</returns>
        public int CreateDepartment(Department newDepartment)
        {
            int department_id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO department (name) VALUES(@name);", conn);



                    cmd.Parameters.AddWithValue("@name", newDepartment.Name);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(department_id) FROM department", conn);
                    department_id = Convert.ToInt32(cmd.ExecuteScalar());
                    //Console.WriteLine(project_id);



                }
            }
            catch (Exception)
            {

                //return false;
            }

            return (int)department_id;
        }
        
        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="updatedDepartment">The department object.</param>
        /// <returns>True, if successful.</returns>
        public bool UpdateDepartment(Department updatedDepartment)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE department SET name = @newName WHERE department_id = @deptID;", conn);

                    cmd.Parameters.AddWithValue("@newName", updatedDepartment.Name);
                    cmd.Parameters.AddWithValue("@deptID", updatedDepartment.Id);

                    cmd.ExecuteNonQuery();
                    result = true;

                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

    }
}
