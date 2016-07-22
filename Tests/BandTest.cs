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
      DBConfiguration.ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=band_tracker;Integrated Security=SSPI;";
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
  }
}
