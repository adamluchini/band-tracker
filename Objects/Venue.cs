using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Venue
  {
    private int _id;
    private string _name;
    public Venue (string Name, int Id = 0)
    {
      _name = Name;
      _id = Id;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public static List<Venue> GetAll()
  {
    List<Venue> allVenues = new List<Venue> {};
    SqlConnection conn = DB.Connection();
    conn.Open();
    SqlDataReader rdr;
    SqlCommand cmd = new SqlCommand ("SELECT * FROM venues;", conn);
    rdr = cmd.ExecuteReader();
    while (rdr.Read())
    {
      int venueId = rdr.GetInt32(0);
      string venueName = rdr.GetString(1);
      Venue newVenue = new Venue (venueName, venueId);
      allVenues.Add(newVenue);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return allVenues;
  }

    public override bool Equals (System.Object otherVenue)
    {
      if (otherVenue is Venue)
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());

        return (idEquality && nameEquality);
      }
      else
      {
        return false;
      }
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@VenueName);", conn);
      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@VenueName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
      rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Venue Find (int findId)
  {
    List<Venue> allVenues = new List<Venue> {};
    SqlConnection conn = DB.Connection();
    conn.Open();
    SqlDataReader rdr;
    SqlCommand cmd = new SqlCommand ("SELECT * FROM venues WHERE id = @FindId;", conn);
    SqlParameter idParameter = new SqlParameter();
    idParameter.ParameterName = "@FindId";
    idParameter.Value = findId;
    cmd.Parameters.Add(idParameter);
    rdr = cmd.ExecuteReader();
    while (rdr.Read())
    {
      int venueId = rdr.GetInt32(0);
      string venueName = rdr.GetString(1);
      Venue newVenue = new Venue (venueName, venueId);
      allVenues.Add(newVenue);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return allVenues[0];
  }
  public void Update(string name)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();
      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewName WHERE id = @Id;", conn);
      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = name;
      cmd.Parameters.Add(newNameParameter);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@Id";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
