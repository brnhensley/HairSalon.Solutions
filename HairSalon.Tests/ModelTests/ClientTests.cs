using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {

    public void Dispose()
    {
      Client.ClearAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=brian_hensley_test;";
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_ClientList()
    {
      //Arrange
      List<Client> newList = new List<Client> { };
      Console.WriteLine(newList.Count);

      //Act
      List<Client> result = Client.GetAll();
      Console.WriteLine(result.Count);
      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsClients_ClientList()
    {
      //Arrange
      Client newClient1 = new Client("Cerebus", 1);
      newClient1.Save();
      Client newClient2 = new Client("Baal", 1);
      newClient2.Save();
      List<Client> expectedResult = new List<Client> { newClient1, newClient2 };

      //Act
      List<Client> actualResult = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNameAreTheSame_Client()
    {
      // Arrange, Act
      Client firstClient = new Client("Baal", 2);
      Client secondClient = new Client("Baal", 2);

      // Assert
      Assert.AreEqual(firstClient, secondClient);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      //Arrange
      Client testClient = new Client("Baal", 2);
      testClient.Save();

      //Act
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Client testClient = new Client("Baal", 2);

      //Act
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.Id;
      int testId = testClient.Id;

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectClient_Client()
    {
      //Arrange
      Client testClient = new Client("Baal", 2);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.Id);

      //Assert
      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Delete_DeletesClientAssociationsFromDatabase_ClientList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Larry");
      testStylist.Save();
      Client testClient = new Client("Jill", testStylist.Id);
      testClient.Save();

      //Act
      testClient.Delete();
      List<Client> resultStylistClients = testStylist.GetClients();
      List<Client> testStylistClients = new List<Client> {};

      //Assert
      CollectionAssert.AreEqual(testStylistClients, resultStylistClients);
    }

    [TestMethod]
    public void Edit_UpdatesClientInDatabase_String()
    {
      //Arrange
      Client testClient = new Client("Jake", 1);
      testClient.Save();
      string newName = "The Snake";
      int newStylist = 2;

      //Act
      testClient.Edit(newName, newStylist);
      string editedName = Client.Find(testClient.Id).Name;
      int editedStylist = Client.Find(testClient.Id).StylistId;

      //Assert
      Assert.AreEqual(newName, editedName);
      Assert.AreEqual(newStylist, editedStylist);
    }

  }
}
