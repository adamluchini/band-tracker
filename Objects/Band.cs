using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Band
  {
    private int _id;
    private string _name;

    public Band (string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
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

    public static List<Band> GetAll()
  {
    List<Band> allBands = new List<Band> {};
    SqlConnection conn = DB.Connection();
    conn.Open();
    SqlDataReader rdr;
    SqlCommand cmd = new SqlCommand ("SELECT * FROM bands;", conn);
    rdr = cmd.ExecuteReader();
    while (rdr.Read())
    {
      int bandId = rdr.GetInt32(0);
      string bandName = rdr.GetString(1);
      Band newBand = new Band (bandName, bandId);
      allBands.Add(newBand);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return allBands;
  }

    public override bool Equals (System.Object otherBand)
    {
      if (otherBand is Band)
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.GetId() == newBand.GetId());
        bool nameEquality = (this.GetName() == newBand.GetName());

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
      SqlCommand cmd = new SqlCommand ("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@BandName);", conn);
      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@BandName";
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

    public static Band Find (int findId)
  {
    List<Band> allBands = new List<Band> {};
    SqlConnection conn = DB.Connection();
    conn.Open();
    SqlDataReader rdr;
    SqlCommand cmd = new SqlCommand ("SELECT * FROM bands WHERE id = @FindId;", conn);
    SqlParameter idParameter = new SqlParameter();
    idParameter.ParameterName = "@FindId";
    idParameter.Value = findId;
    cmd.Parameters.Add(idParameter);
    rdr = cmd.ExecuteReader();
    while (rdr.Read())
    {
      int bandId = rdr.GetInt32(0);
      string bandName = rdr.GetString(1);
      Band newBand = new Band (bandName, bandId);
      allBands.Add(newBand);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return allBands[0];
  }
  public void Update(string name)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();
      SqlCommand cmd = new SqlCommand("UPDATE bands SET name = @NewName WHERE id = @Id;", conn);
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
      SqlCommand cmd = new SqlCommand ("DELETE FROM bands;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
