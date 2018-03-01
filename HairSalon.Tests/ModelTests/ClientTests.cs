using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ItemTests : IDisposable
  {
    public ItemTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=jahmanz_williams_test";
    }
    public void Dispose()
    {
      Item.DeleteAll();
      Category.DeleteAll();
    }
