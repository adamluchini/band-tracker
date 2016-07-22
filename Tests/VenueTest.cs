using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace BandTracker
{
  public class VenueTest
  {
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
  }
}
