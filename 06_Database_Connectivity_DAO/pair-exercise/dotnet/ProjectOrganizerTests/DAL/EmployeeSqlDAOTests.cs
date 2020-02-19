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
    public class EmployeeSqlDAOTests : ProjectOrganizerDAOTests
    {
        [TestMethod]
        public void GetAllEmployees_ShouldReturnRightNumberOfEmployees()
        {
            //Arrange
            const int numberOfEmployeesAddedForTests = 2;//set in sql test setup file

            EmployeeSqlDAO dao = new EmployeeSqlDAO(ConnectionString);

            //Act
            IList<Employee> employees = dao.GetAllEmployees();

            //Assert
            Assert.AreEqual(numberOfEmployeesAddedForTests, employees.Count, "GetAllEmployees doesn't return correct number for one employee");
        }

        [DataTestMethod]
        [DataRow("John","Smith",true)]
        [DataRow("J", "h", true)]
        [DataRow("S", "J", false)]
        public void SearchEmployee_ShouldFindEmployeeName(string firstName, string lastName, bool isMatchExpected)
        {
            //Arrange
            EmployeeSqlDAO dao = new EmployeeSqlDAO(ConnectionString);
            
            const string testValFirstName = "John";//Set in sql test setup file
            const string testValLastName = "Smith";

            //Act

            IList<Employee> employees = dao.Search(firstName, lastName);
            
            //list will be empty when search terms aren't in table and when table not empty then check name matches expected
            bool isMatch = employees.Count!=0 && (testValFirstName == employees[0].FirstName) && (testValLastName== employees[0].LastName);//employee[0] set in sql test file

            //Assert
            
            Assert.AreEqual(isMatchExpected, isMatch);

        }

        [TestMethod]
        public void GetEmployeesWithoutProjects_ShouldGetCorrectNumEmployees()
        {
            //Arrange
            EmployeeSqlDAO dao = new EmployeeSqlDAO(ConnectionString);
            const int numberOfEmployeesAddedWithoutPojects = 1;//set in sql test setup file


            //Act

            IList<Employee> employees = dao.GetEmployeesWithoutProjects();

            
            //Assert

            Assert.AreEqual(numberOfEmployeesAddedWithoutPojects, employees.Count);

        }

    }
}
