using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
  {
    public SpecialtyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=jahmanz_williams_test;";
    }
    public void Dispose()
    {
      Client.DeleteAll();
    }

    [TestMethod]
    public void GetAllSpecialties_Empty_0()
    {
      int result = Specialty.GetAll().Count;

      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Equals_ReturnsEqual_Specialty()
    {
      Specialty firstSpecialty = new Specialty("braids");
      Specialty secondSpecialty = new Specialty("braids");

      Assert.AreEqual(firstSpecialty, secondSpecialty);
    }

    [TestMethod]
    public void Save_SavesSpecialtyToDatabase_List()
    {
      Specialty testSpecialty = new Specialty("braids");
      testSpecialty.Save();

      List<Specialty> result = Specialty.GetAll();
      List<Specialty> testList = new List<Specialty>{testSpecialty};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindsSpecialty_Specialty()
    {
      Specialty testSpecialty = new Specialty("braids");
      testSpecialty.Save();

      Specialty foundSpecialty = Specialty.Find(testSpecialty.GetId());

      Assert.AreEqual(testSpecialty, foundSpecialty);
    }
  }
}
