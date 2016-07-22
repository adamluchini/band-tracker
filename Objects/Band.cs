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
  }
}
