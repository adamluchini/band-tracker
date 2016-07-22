using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace BandTracker
{
  public class BandTest
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=band_tracker;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Empty_DatabaseEmpty()
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
  }
}
