using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=brian_hensley_test;";
    }

    public void Dispose()
    {
      Stylist.ClearAll();
      Client.ClearAll();
      Specialty.ClearAll();
    }

    [TestMethod]
    public void SpecialtyConstructor_CreatesInstanceOfSpecialty_Specialty()
    {
      Specialty newSpecialty = new Specialty("perm");
      Assert.AreEqual(typeof(Specialty), newSpecialty.GetType());
    }

    [TestMethod]
    public void GetAll_ReturnsAllSpecialtyObjects_SpecialtyList()
    {
      //Arrange
      Specialty newSpecialty1 = new Specialty("perm");
      newSpecialty1.Save();
      Specialty newSpecialty2 = new Specialty("afro");
      newSpecialty2.Save();
      List<Specialty> newList = new List<Specialty> { newSpecialty1, newSpecialty2 };

      //Act
      List<Specialty> result = Specialty.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsSpecialtyInDatabase_Specialty()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("perm");
      testSpecialty.Save();

      //Act
      Specialty foundSpecialty = Specialty.Find(testSpecialty.Id);

      //Assert
      Assert.AreEqual(testSpecialty, foundSpecialty);
    }

    [TestMethod]
    public void GetStylists_ReturnsEmptyStylistList_StylistList()
    {
      //Arrange
      string name = "Work";
      Specialty newSpecialty = new Specialty("perm");
      List<Stylist> newList = new List<Stylist> { };

      //Act
      List<Stylist> result = newSpecialty.GetStylists();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_SpecialtiesEmptyAtFirst_List()
    {
      //Arrange, Act
      int result = Specialty.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Specialty()
    {
      //Arrange, Act
      Specialty firstSpecialty = new Specialty("perm");
      Specialty secondSpecialty = new Specialty("perm");

      //Assert
      Assert.AreEqual(firstSpecialty, secondSpecialty);
    }

    [TestMethod]
    public void Save_SavesSpecialtyToDatabase_SpecialtyList()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("perm");
      testSpecialty.Save();

      //Act
      List<Specialty> result = Specialty.GetAll();
      List<Specialty> testList = new List<Specialty>{testSpecialty};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToSpecialty_Id()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("perm");
      testSpecialty.Save();

      //Act
      Specialty savedSpecialty = Specialty.GetAll()[0];

      int result = savedSpecialty.Id;
      int testId = testSpecialty.Id;

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void GetStylists_RetrievesAllStylistsWithSpecialty_StylistList()
    {
      //Arrange, Act
      Specialty testSpecialty = new Specialty("perm");
      testSpecialty.Save();
      DateTime dob = new DateTime(1986, 12, 5);
      Stylist firstStylist = new Stylist("Larry");
      firstStylist.Save();
      testSpecialty.AddStylist(firstStylist);
      Stylist secondStylist = new Stylist("barry");
      secondStylist.Save();
      testSpecialty.AddStylist(secondStylist);
      List<Stylist> testStylistList = new List<Stylist> {firstStylist, secondStylist};
      List<Stylist> resultStylistList = testSpecialty.GetStylists();

      //Assert
      CollectionAssert.AreEqual(testStylistList, resultStylistList);
    }

    [TestMethod]
    public void Delete_DeletesSpecialtyAssociationsFromDatabase_SpecialtyList()
    {
      //Arrange
      DateTime dob = new DateTime(1986, 12, 5);
      Stylist testStylist = new Stylist("Larry");
      testStylist.Save();
      Specialty testSpecialty = new Specialty("perm");
      testSpecialty.Save();

      //Act
      testSpecialty.AddStylist(testStylist);
      testSpecialty.Delete();
      List<Specialty> resultStylistSpecialties = testStylist.GetAll();
      List<Specialty> testStylistSpecialties = new List<Specialty> {};

      //Assert
      CollectionAssert.AreEqual(testStylistSpecialties, resultStylistSpecialties);
    }

    [TestMethod]
    public void Test_AddStylist_AddsStylistToSpecialty()
    {
      //Arrange
      DateTime dob = new DateTime(1986, 12, 5);
      Specialty testSpecialty = new Specialty("perm");
      testSpecialty.Save();
      Stylist testStylist = new Stylist("Larry");
      testStylist.Save();
      Stylist testStylist2 = new Stylist("Jim");
      testStylist2.Save();

      //Act
      testSpecialty.AddStylist(testStylist);
      testSpecialty.AddStylist(testStylist2);
      List<Stylist> result = testSpecialty.GetStylists();
      List<Stylist> testList = new List<Stylist>{testStylist, testStylist2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetStylists_ReturnsAllSpecialtyStylists_StylistList()
    {
      //Arrange
      DateTime dob = new DateTime(1986, 12, 5);
      Specialty testSpecialty = new Specialty("perm");
      testSpecialty.Save();
      Stylist testStylist1 = new Stylist("Larry");
      testStylist1.Save();
      Stylist testStylist2 = new Stylist("Jim");
      testStylist2.Save();

      //Act
      testSpecialty.AddStylist(testStylist1);
      List<Stylist> savedStylists = testSpecialty.GetStylists();
      List<Stylist> testList = new List<Stylist> {testStylist1};

      //Assert
      CollectionAssert.AreEqual(testList, savedStylists);
    }



  }
}
