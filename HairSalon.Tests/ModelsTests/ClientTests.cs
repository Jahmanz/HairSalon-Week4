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
    public void GetAll_EmptyClients_0()
    {
      int result = Client.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ClientsAreEqual_Client()
    {
      Client firstClient = new Client("Laura", "email@email.com");
      Client secondClient = new Client("Laura", "email@email.com");

      Assert.AreEqual(firstClient, secondClient);
    }

    [TestMethod]
    public void GetName_ReturnName_String()
    {
      string name= "Laura";
      string email = "email@email.com";
      Client newClient = new Client(name, email);

      string result = newClient.GetName();

      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void Save_ClientSavesToDatabase_List()
    {
      Client testClient = new Client("Laura", "email@email.com");
      testClient.Save();

      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindsClientsInDatabase_Client()
    {
      Client testClient = new Client("Laura", "email@email.com");
      testClient.Save();

      Client foundClient = Client.Find(testClient.GetId());

      Assert.AreEqual(testClient, foundClient);
    }
    [TestMethod]
    public void Update_UpdateClientInDatabase_String()
    {
      Client testClient = new Client("Lauri","email@email.com");
      testClient.Save();
      string updateName = "Laura";

      testClient.UpdateName(updateName);

      string result = Client.Find(testClient.GetId()).GetName();

      Assert.AreEqual(updateName, result);
    }

  }
}
