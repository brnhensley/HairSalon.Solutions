using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Specialty
  {
    public string Type {get; set;}
    public int Id {get; set;}

    public Specialty(string type, int id = 0)
    {
      Type = type;
      Id = id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialties (type) VALUES (@type);";

      MySqlParameter type = new MySqlParameter();
      type.ParameterName = "@type";
      type.Value = this.Type;
      cmd.Parameters.Add(type);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int SpecialtyId = rdr.GetInt32(0);
        string SpecialtyType = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(SpecialtyType, SpecialtyId);
        allSpecialties.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }

    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int SpecialtyId = 0;
      string SpecialtyType = "";

      while(rdr.Read())
      {
        SpecialtyId = rdr.GetInt32(0);
        SpecialtyType = rdr.GetString(1);
      }
      Specialty newSpecialty = new Specialty(SpecialtyType, SpecialtyId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newSpecialty;
    }

    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

      MySqlParameter specialty_id = new MySqlParameter();
      specialty_id.ParameterName = "@SpecialtyId";
      specialty_id.Value = Id;
      cmd.Parameters.Add(specialty_id);

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@StylistId";
      stylist_id.Value = newStylist.Id;
      cmd.Parameters.Add(stylist_id);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM specialties
      JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
      JOIN stylists ON (stylists_specialties.stylist_id = stylists.id)
      WHERE specialties.id = @SpecialtyId;";
      MySqlParameter specialtyIdParameter = new MySqlParameter();
      specialtyIdParameter.ParameterName = "@SpecialtyId";
      specialtyIdParameter.Value = this.Id;
      cmd.Parameters.Add(specialtyIdParameter);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Stylist> stylists = new List<Stylist>{};
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        stylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylists;
    }

    // public List<Stylist> GetStylists()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT stylists.* FROM specialties
    //   JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
    //   JOIN stylists ON (stylists_specialties.stylist_id = stylists.id)
    //   WHERE specialties.id = @SpecialtyId;";
    //   MySqlParameter specialtyIdParameter = new MySqlParameter();
    //   specialtyIdParameter.ParameterName = "@SpecialtyId";
    //   specialtyIdParameter.Value = Id;
    //   cmd.Parameters.Add(specialtyIdParameter);
    //   MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   List<Stylist> stylists = new List<Stylist>{};
    //   while(rdr.Read())
    //   {
    //     int stylistId = rdr.GetInt32(0);
    //     string stylistName = rdr.GetString(1);
    //     Stylist newStylist = new Stylist(stylistName, stylistId);
    //     stylists.Add(newStylist);
    //   }
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //   return stylists;
    // }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void JoinTableClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand(@"DELETE FROM stylists_specialties;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = this.Id.Equals(newSpecialty.Id);
        bool typeEquality = this.Type.Equals(newSpecialty.Type);
        return (idEquality && typeEquality);
      }
    }

    // EDIT AND DELETE NOT NEEDED YET
    // public void Edit(string newType)
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"UPDATE specialties SET type = @newType WHERE id = @searchId;";
    //
    //   MySqlParameter searchId = new MySqlParameter();
    //   searchId.ParameterName = "@searchId";
    //   searchId.Value = Id;
    //   cmd.Parameters.Add(searchId);
    //
    //   MySqlParameter type = new MySqlParameter();
    //   type.ParameterName = "@newType";
    //   type.Value = newType;
    //   cmd.Parameters.Add(type);
    //
    //   cmd.ExecuteNonQuery();
    //   Type = newType;
    //
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    // }
    //
    // public void Delete()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"DELETE FROM specialties WHERE id = @SpecialtyId;";
    //   MySqlParameter specialtieIdParameter = new MySqlParameter();
    //   specialtieIdParameter.ParameterName = "@SpecialtyId";
    //   specialtieIdParameter.Value = this.Id;
    //   cmd.Parameters.Add(specialtieIdParameter);
    //   cmd.ExecuteNonQuery();
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }
  }
}
