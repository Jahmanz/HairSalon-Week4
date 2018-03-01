using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=jahmanz_williams_test;";
        }
        public void Dispose()
        {
            Client.DeleteAll();
            Stylist.DeleteAll();
        }

       [TestMethod]
       public void GetAll_ClientsEmptyAtFirst_0()
       {
         //Arrange, Act
         int result = Client.GetAll().Count;

         //Assert
         Assert.AreEqual(4, result);
       }
   }
 }
