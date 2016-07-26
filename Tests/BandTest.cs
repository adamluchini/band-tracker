using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public void Dispose()
  {
    Band.DeleteAll();
    Venue.DeleteAll();
  }
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Empty_DatabaseEmptyandGetAll()
    {
      int resultValue = Band.GetAll().Count;
      Assert.Equal(0, resultValue);
    }

    [Fact]
    public void Test_Equal_DatabaseHasEqualValues()
    {
      Band firstBand = new Band ("The Bananas");
      Band secondBand = new Band ("The Bananas");
      Assert.Equal(firstBand, secondBand);
    }

    [Fact]
    public void Test_SavesBandToDataBase()
    {
      Band newBand = new Band ("The Bananas");
      newBand.Save();

      List<Band> testList = new List<Band> {newBand};
      List<Band> resultList = Band.GetAll();

      Assert.Equal(testList, resultList);
    }

    [Fact]
    public void Test_Find_FindsBandNameByBandId()
    {
      Band newBand = new Band ("The Bananas");
      newBand.Save();

      Band foundBand = Band.Find(newBand.GetId());

      Assert.Equal(newBand, foundBand);
    }

    [Fact]
    public void Test_Update_UpdatesBandName()
    {
      Band newBand = new Band("The Bananas");
      newBand.Save();
      string newName = "The Apples";

      newBand.Update(newName);
      string updatedName = Band.Find(newBand.GetId()).GetName();

      Assert.Equal(newName, updatedName);
    }

    [Fact]
  public void Test_AddVenue_SavesVenueToBand()
  {
    Band newBand = new Band ("The Bananas");
    newBand.Save();
    Venue newVenue = new Venue ("The Rage Hut");
    newVenue.Save();
    newBand.AddVenue(newVenue);
    List<Venue> testVenueList = new List<Venue> {newVenue};
    List<Venue> resultVenueList = newBand.GetVenues();
    Assert.Equal(testVenueList, resultVenueList);
  }
  [Fact]
  public void Test_Delete_DeletesBand()
    {
      Band testBand = new Band ("The Bananas");
      testBand.Save();
      List<Band> testList = new List<Band> {};
      testBand.Delete();
      List<Band> resultList = Band.GetAll();
      Assert.Equal(testList, resultList);
    }


  }
}
