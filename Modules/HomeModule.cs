using System.Collections.Generic;
using System;
using Nancy;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get ["/"] = _ =>
      {
        return View ["index.cshtml"];
      };
      Get["/bands"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Get["/venues"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Get["/bands/add"] = _ => {
        return View["band_add.cshtml"];
      };
      Post["/bands/add"] = _ => {
        Band newBand = new Band(Request.Form["band_name"]);
        newBand.Save();
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Get["/venues/add"] = _ => {
        return View["venue_add.cshtml"];
      };
      Post["/venues/add"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue_name"]);
        newVenue.Save();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
    }
  }
}
