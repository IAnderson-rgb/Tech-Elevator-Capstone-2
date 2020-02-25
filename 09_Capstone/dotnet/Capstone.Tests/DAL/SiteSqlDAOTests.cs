using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.Tests.DAL
{
    [TestClass]
    public class SiteSqlDAOTests : NPCampgroundDAOTests
    {
        [DataTestMethod]
        [DataRow(5,"2020-06-06","2020-06-08")]
        public void GetAvailableReservationsSingleCapmgroundTest(int campgroundId, string startDate, string endDate) 
        {
            //Arrange
            SiteSqlDAO dao = new SiteSqlDAO(ConnectionString);

            //Act
            IList<Site> sites = dao.GetAvailableReservationsSingleCapmground(campgroundId, startDate, endDate);

            //Assert
            Assert.AreEqual(1, sites.Count);

        }
    }

}
