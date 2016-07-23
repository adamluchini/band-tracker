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
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
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
    public void Test_Find_FindsVenueNameByVenueId()
    {
      Venue newVenue = new Venue ("The Rage Hut");
      newVenue.Save();

      Venue foundVenue = Venue.Find(newVenue.GetId());

      Assert.Equal(newVenue, foundVenue);
    }

    [Fact]
    public void Test_Update_UpdatesVenueName()
    {
      Venue newVenue = new Venue("The Rage Hut");
      newVenue.Save();
      string newName = "The Quiet Space";

      newVenue.Update(newName);
      string updatedName = Venue.Find(newVenue.GetId()).GetName();

      Assert.Equal(newName, updatedName);
    }

    [Fact]
    public void Test_AddBand_SavesBandToVenueDatabase()
    {
      Venue newVenue = new Venue ("The Rage Hut");
      newVenue.Save();
      Band newBand = new Band ("The Bananas");
      newBand.Save();
      newVenue.AddBand(newBand);
      List<Band> testBandList = new List<Band> {newBand};
      List<Band> resultBandList = newVenue.GetBands();
      Assert.Equal(testBandList, resultBandList);
    }
  }
}
