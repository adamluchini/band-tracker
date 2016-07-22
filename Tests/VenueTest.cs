using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
  public void Dispose()
  {
    Venue.DeleteAll();
    Band.DeleteAll();
  }
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=band_tracker;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Empty_DatabaseEmptyandGetAll()
    {
      int resultValue = Venue.GetAll().Count;
      Assert.Equal(0, resultValue);
    }

    [Fact]
    public void Test_Equal_DatabaseHasEqualValues()
    {
      Venue firstVenue = new Venue ("The Rage Hut");
      Venue secondVenue = new Venue ("The Rage Hut");
      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void Test_SavesVenueToDataBase()
    {
      Venue newVenue = new Venue ("The Rage Hut");
      newVenue.Save();

      List<Venue> testList = new List<Venue> {newVenue};
      List<Venue> resultList = Venue.GetAll();

      Assert.Equal(testList, resultList);
    }

    [Fact]
    public void Test_Find_FindsBandNameByVenueId()
    {
      Venue newVenue = new Venue ("The Rage Hut");
      newVenue.Save();

      Venue foundVenue = Venue.Find(newVenue.GetId());

      Assert.Equal(newVenue, foundVenue);
    }
  }
}
