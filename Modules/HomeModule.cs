using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/bands"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Get["/venues"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Get["/bands/new"] = _ => {
        return View["bands_form.cshtml"];
      };
      Post["/bands/new"] = _ => {
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        return View["index.cshtml"];
      };
      Get["/venues/new"] = _ => {
        return View["venues_form.cshtml"];
      };
      Post["/venues/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        return View["index.cshtml"];
      };
      Get["bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band SelectedBand = Band.Find(parameters.id);
        List<Venue> BandVenues = SelectedBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("band", SelectedBand);
        model.Add("bandVenues", BandVenues);
        model.Add("allVenues", allVenues);
        return View["band.cshtml", model];
      };

      Get["venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue SelectedVenue = Venue.Find(parameters.id);
        List<Band> VenueBands = SelectedVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        model.Add("venue", SelectedVenue);
        model.Add("venueBands", VenueBands);
        model.Add("allBands", allBands);
        return View["venue.cshtml", model];
      };

      Post["band/add_venue"] = _ => {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        band.AddVenue(venue);
        return View["bands.cshtml"];
      };
      Post["venue/add_band"] = _ => {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        venue.AddBand(band);
        return View["venues.cshtml"];
      };
      Delete["/delete_venues/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Delete();
        return View["venues.cshtml", Venue.GetAll()];
      };
    }
  }
}
