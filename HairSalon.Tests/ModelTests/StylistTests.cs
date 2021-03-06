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
    public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
    {
      Stylist newStylist = new Stylist("test stylist");
      Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string testStylist = "Joe";
      Stylist newStylist = new Stylist(testStylist);

      //Act
      string result = newStylist.Name;

      //Assert
      Assert.AreEqual(testStylist, result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllStylistObjects_StylistList()
    {
      //Arrange
      string name01 = "Joe";
      string name02 = "Jim";
      Stylist newStylist1 = new Stylist(name01);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist(name02);
      newStylist2.Save();
      List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };

      //Act
      List<Stylist> result = Stylist.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsStylistInDatabase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("Joe");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.Id);

      //Assert
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void GetClients_ReturnsEmptyClientList_ClientList()
    {
      //Arrange
      string name = "Reg";
      Stylist newStylist = new Stylist(name);
      List<Client> newList = new List<Client> { };

      //Act
      List<Client> result = newStylist.GetClients();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_SytlistsEmptyAtFirst_List()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Stylist()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Larry");
      Stylist secondStylist = new Stylist("Larry");

      //Assert
      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Larry");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToStylist_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("Larry");
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.Id;
      int testId = testStylist.Id;

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void GetClients_RetrievesAllClientsWithStylist_ClientList()
    {
      //Arrange, Act
      Stylist testStylist = new Stylist("Larry");
      testStylist.Save();
      Client firstClient = new Client("Curly", testStylist.Id);
      firstClient.Save();
      Client secondClient = new Client("Moe", testStylist.Id);
      secondClient.Save();
      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      //Assert
      CollectionAssert.AreEqual(testClientList, resultClientList);
    }

    [TestMethod]
    public void Delete_DeletesStylistAssociationsFromDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Mopey");
      testStylist.Save();

      //Act
      testStylist.Delete();
      List<Stylist> resultClientStylists = Stylist.GetAll();
      List<Stylist> testClientStylists = new List<Stylist> {};

      //Assert
      CollectionAssert.AreEqual(testClientStylists, resultClientStylists);
    }

    [TestMethod]
    public void Edit_UpdatesClientInDatabase_String()
    {
      //Arrange
      Stylist testStylist = new Stylist("Jake");
      testStylist.Save();
      string newName = "The Snake";

      //Act
      testStylist.Edit(newName);
      string editedName = Stylist.Find(testStylist.Id).Name;

      //Assert
      Assert.AreEqual(newName, editedName);
    }

    [TestMethod]
    public void AddSpecialty_AddsSpecialtyToStylist_SpecialtyList()
    {
      //Arrange
      Stylist testStylist = new Stylist("perm");
      testStylist.Save();
      Specialty testSpecialty = new Specialty("Larry");
      testSpecialty.Save();
      Specialty testSpecialty2 = new Specialty("Jim");
      testSpecialty2.Save();

      //Act
      testStylist.AddSpecialty(testSpecialty);
      testStylist.AddSpecialty(testSpecialty2);
      List<Specialty> result = testStylist.GetSpecialties();
      List<Specialty> testList = new List<Specialty>{testSpecialty, testSpecialty2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetSpecialties_RetrievesAllSpecialtiesWithStylist_SpecialtyList()
    {
      //Arrange, Act
      Stylist testStylist = new Stylist("perm");
      testStylist.Save();
      Specialty firstSpecialty = new Specialty("Larry");
      firstSpecialty.Save();
      Specialty secondSpecialty = new Specialty("barry");
      secondSpecialty.Save();
      testStylist.AddSpecialty(firstSpecialty);
      testStylist.AddSpecialty(secondSpecialty);
      List<Specialty> testSpecialtyList = new List<Specialty> {firstSpecialty, secondSpecialty};
      List<Specialty> resultSpecialtyList = testStylist.GetSpecialties();

      //Assert
      CollectionAssert.AreEqual(testSpecialtyList, resultSpecialtyList);
    }

    // Tests that Specialties are not added by default
    [TestMethod]
    public void GetSpecialties_ReturnsAllStylistSpecialties_SpecialtyList()
    {
      //Arrange
      Stylist testStylist = new Stylist("perm");
      testStylist.Save();
      Specialty testSpecialty1 = new Specialty("Larry");
      testSpecialty1.Save();
      Specialty testSpecialty2 = new Specialty("Jim");
      testSpecialty2.Save();

      //Act
      testStylist.AddSpecialty(testSpecialty1);
      List<Specialty> savedSpecialties = testStylist.GetSpecialties();
      List<Specialty> testList = new List<Specialty> {testSpecialty1};

      //Assert
      CollectionAssert.AreEqual(testList, savedSpecialties);
    }

  }
}
