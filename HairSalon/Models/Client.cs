using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    public string Name {get; set;}
    public int Id {get; set;}

    public Client (string name, int id = 0)
    {
      Name = name;
      Id = id;
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM  clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        string name = rdr.GetString(1);
        int id = rdr.GetInt32(0);

        Client newClient = new Client(name, id);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `clients` (`name`) VALUES (@ClientName);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@ClientName";
      name.Value = this.Name;

      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `clients` WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string clientName = "";
      int clientId = 0;

      while (rdr.Read())
      {
        clientName = rdr.GetString(1);
        clientId = rdr.GetInt32(0);
      }

      Client foundClient = new Client(clientName, clientId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundClient;
    }

    public void DeleteClient()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @ClientId;";
      MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = this.Id;
      cmd.Parameters.Add(clientIdParameter);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool nameEquality = (this.Name == newClient.Name);
        bool idEquality = (this.Id == newClient.Id);
        return (nameEquality && idEquality);
      }
    }
  }
}
