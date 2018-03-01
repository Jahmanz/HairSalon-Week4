using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=jahmanz_williams_test;";
        }
        public void Dispose()
        {
            Client.DeleteAll();
            // Stylist.DeleteAll();
        }

        [TestMethod]
       public void GetAllStylists_Empty_0()
       {
         //Arrange, Act
         int result = Stylist.GetAll().Count;

         //Assert
         Assert.AreEqual(0, result);
       }
       [TestMethod]
     public void Equals_ReturnsEqual_Stylist()
     {
       //Arrange, Act
       Stylist firstStylist = new Stylist("Florida", "highlights");
       Stylist secondStylist = new Stylist("Florida", "highlights");

       //Assert
       Assert.AreEqual(firstStylist, secondStylist);
     }
     [TestMethod]
     public void SaveStylist_SavesStylistToDatabase_StylistList()
     {
       //Arrange
       Stylist testStylist = new Stylist("Florida", "highlights");
       testStylist.Save();

       //Act
       List<Stylist> result = Stylist.GetAll();
       List<Stylist> testList = new List<Stylist>{testStylist};

       //Assert
       CollectionAssert.AreEqual(testList, result);
     }
     }
   }
