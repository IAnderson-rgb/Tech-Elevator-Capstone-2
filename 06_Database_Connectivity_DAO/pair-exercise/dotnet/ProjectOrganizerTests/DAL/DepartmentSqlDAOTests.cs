using System;
using System.Collections.Generic;
using System.Text;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;
using ProjectOrganizerTests;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizerTests.DAL;

namespace ProjectOrganizerTests.DAL
{
    [TestClass]
    public class DepartmentSqlDAOTests: ProjectOrganizerDAOTests
    {
        

        public void GetDepartments_ShouldReturnRightNumberOfDepartments() {
            //Arrange
            const int numberOfDeptsAddedForTests = 1;

            DepartmentSqlDAO dao = new DepartmentSqlDAO(ConnectionString);
            
            //Act
            IList<Department> departments = dao.GetDepartments();

            //Assert
            Assert.AreEqual(numberOfDeptsAddedForTests, departments.Count, "GetDepartments doesn't return correct number for one department");
        }

    }
}
