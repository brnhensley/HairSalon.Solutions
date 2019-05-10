using System;
using MySql.Data.MySqlClient;
using HairSalonDatabase;
using System.Collections.Generic;

namespace HairSalon.Models
{
  public class DB
  {
    public static MySqlConnection Connection()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
  }
}
