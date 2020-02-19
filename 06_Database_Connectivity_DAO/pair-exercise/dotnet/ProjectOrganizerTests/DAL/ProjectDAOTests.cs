using System;
using System.Collections.Generic;
using System.Text;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;
using ProjectOrganizerTests;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectOrganizerTests.DAL
{
    [TestClass]
    public class ProjectDAOTests:ProjectOrganizerDAOTests
    {
        [TestMethod]
        public void GetAllProjects_ShouldReturnRightNumberOfProjects()
        {
            //Arrange
            const int numberOfProjectsAddedForTests = 1;//set in sql test setup file

            ProjectSqlDAO dao = new ProjectSqlDAO(ConnectionString);

            //Act
            IList<Project> projects = dao.GetAllProjects();

            //Assert
            Assert.AreEqual(numberOfProjectsAddedForTests, projects.Count, "GetAllProjects doesn't return correct number for one project");
        }

        [TestMethod]
        
        public void AssignEmployeeToProject_ShouldReturnRightNumberOfEmployees()
        {
            //Arrange
            const int numberOfEmployeesWithoutProjects = 0;//set in sql test setup file minus the one employee assigned here

            ProjectSqlDAO projectDao = new ProjectSqlDAO(ConnectionString);
            EmployeeSqlDAO empDao = new EmployeeSqlDAO(ConnectionString);
            
            IList<Project> projects = projectDao.GetAllProjects();

            IList<Employee> testEmployee = empDao.GetEmployeesWithoutProjects();//Get the employee added in sql test file that does not have a project assigned


            //Act
            projectDao.AssignEmployeeToProject(projects[0].ProjectId, testEmployee[0].EmployeeId);//assign employee withou project to the only project


            //Assert
            Assert.AreEqual(numberOfEmployeesWithoutProjects, empDao.GetEmployeesWithoutProjects().Count, "GetAllProjects doesn't return correct number for one project");
        }

        [TestMethod]

        public void RemoveEmployeeFromProject_ShouldReturnRightNumberOfEmployees()
        {
            //Arrange
            const int numberOfEmployeesWithoutProjects = 2;//set in sql test setup file plus the one employee removed here

            ProjectSqlDAO projectDao = new ProjectSqlDAO(ConnectionString);
            EmployeeSqlDAO empDao = new EmployeeSqlDAO(ConnectionString);


            IList<Project> projects = projectDao.GetAllProjects();

            IList<Employee> testEmployee = empDao.Search("John","Smith");//Get the employee added in sql test file that does have a project assigned. name from sql test file


            //Act
            projectDao.RemoveEmployeeFromProject(projects[0].ProjectId, testEmployee[0].EmployeeId);//remove employee with project from the only project


            //Assert
            Assert.AreEqual(numberOfEmployeesWithoutProjects, empDao.GetEmployeesWithoutProjects().Count, "Remove Employee doesn't return correct number when employee removed");
        }

        [TestMethod]

        public void RemoveEmployeeFromProject_ShouldReturnTrueWhenEmployeeRemoved()
        {
            //Arrange
            const bool expectedResult = true;//when employee successfully removed, function should return true

            ProjectSqlDAO projectDao = new ProjectSqlDAO(ConnectionString);
            EmployeeSqlDAO empDao = new EmployeeSqlDAO(ConnectionString);


            IList<Project> projects = projectDao.GetAllProjects();

            IList<Employee> testEmployee = empDao.Search("John", "Smith");//Get the employee added in sql test file that does have a project assigned. name from sql test file


            //Act
            bool resultOfRemove = projectDao.RemoveEmployeeFromProject(projects[0].ProjectId, testEmployee[0].EmployeeId);//remove employee with project from the only project


            //Assert
            Assert.AreEqual(expectedResult, resultOfRemove, "RemoveEmployeeFromProject doesn't return true when employee removed");
        }

        [TestMethod]
        public void CreateProject_ShouldReturnRightNumberOfProjects()
        {
            //Arrange
            const int numberOfProjectsAfterCreated= 2;//set in sql test setup file plus the one we are creating here

            ProjectSqlDAO dao = new ProjectSqlDAO(ConnectionString);

            Project newProject = new Project();
            newProject.Name = "create test";
            newProject.StartDate = Convert.ToDateTime("2020-02-19");
            newProject.EndDate = Convert.ToDateTime("2020-02-19");

            //Act
            dao.CreateProject(newProject);

            //Assert
            Assert.AreEqual(numberOfProjectsAfterCreated, dao.GetAllProjects().Count);
        }

    }
}
