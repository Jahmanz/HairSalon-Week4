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
    }

    [TestMethod]
    public void GetAllStylists_Empty_0()
    {
      int result = Stylist.GetAll().Count;

      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Equals_ReturnsEqual_Stylist()
    {
      Stylist firstStylist = new Stylist("Florida", "highlights");
      Stylist secondStylist = new Stylist("Florida", "highlights");

      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_List()
    {
      Stylist testStylist = new Stylist("Florida", "highlights");
      testStylist.Save();

      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindsStylist_Stylist()
    {
      Stylist testStylist = new Stylist("Florida", "highlights");
      testStylist.Save();

      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      Assert.AreEqual(testStylist, foundStylist);
    }
  }
}
