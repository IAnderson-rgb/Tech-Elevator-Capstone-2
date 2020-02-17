using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class ProjectSqlDAO : IProjectDAO
    {
        private string connectionString;
        
        

        // Single Parameter Constructor
        public ProjectSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns></returns>
        public IList<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM project;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) 
                    {
                        Project project = new Project();
                        project.ProjectId = Convert.ToInt32(reader["project_id"]);
                        project.Name = Convert.ToString(reader["name"]);
                        project.StartDate = Convert.ToDateTime(reader["from_date"]);
                        project.EndDate = Convert.ToDateTime(reader["to_date"]);

                        projects.Add(project);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return projects;
        }

        /// <summary>
        /// Assigns an employee to a project using their IDs.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            //throw new NotImplementedException();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO project_employee(project_id, employee_id) VALUES(@project_id, @employee_id); ", conn);

                    //SqlDataReader reader = cmd.ExecuteReader();

                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);
                    

                    cmd.ExecuteNonQuery();

                    

                }
            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }

        /// <summary>
        /// Removes an employee from a project.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM project_employee WHERE project_id =@project_id AND employee_id =@employee_id;", conn);

                    //SqlDataReader reader = cmd.ExecuteReader();

                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);


                    cmd.ExecuteNonQuery();



                }
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="newProject">The new project object.</param>
        /// <returns>The new id of the project.</returns>
        public int CreateProject(Project newProject)
        {
            int project_id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO project (name, from_date, to_date) VALUES(@name, @start_date, @to_date);", conn);

                    

                    cmd.Parameters.AddWithValue("@name", newProject.Name);
                    cmd.Parameters.AddWithValue("@start_date", newProject.StartDate);
                    cmd.Parameters.AddWithValue("@to_date", newProject.EndDate);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                    cmd = new SqlCommand("SELECT MAX(project_id) FROM project",conn);
                    project_id = Convert.ToInt32(cmd.ExecuteScalar());
                    //Console.WriteLine(project_id);



                }
            }
            catch (Exception)
            {

               //return false;
            }

            return (int)project_id;
        }

    }
}
