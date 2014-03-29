using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;


namespace HelloiPhone
{
	public partial class HelloiPhoneViewController : UIViewController
	{
		public HelloiPhoneViewController () : base ("HelloiPhoneViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.locMgr = new CLLocationManager();
			this.txtOutput.Text = string.Empty;

			StartLocationUpdates ();
		}

		protected CLLocationManager locMgr;

		public CLLocationManager LocMgr{
			get { return this.locMgr; }
		}


		public void StartLocationUpdates()
		{
			if (CLLocationManager.LocationServicesEnabled) {
				LocMgr.DesiredAccuracy = 1;
				LocMgr.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
				{
					HandleLocationChanged (this, new LocationUpdatedEventArgs (e.Locations [e.Locations.Length - 1]));
				};

				LocMgr.StartUpdatingLocation();
			}
		}



		public void HandleLocationChanged (object sender, LocationUpdatedEventArgs e)
		{
			CLLocation location = e.Location;
			Log (string.Format("Location Updated: {0}, {1}", location.Coordinate.Longitude, location.Coordinate.Latitude));
		}

		private void Log(string message)
		{
			txtOutput.Text += message + Environment.NewLine;
		}


	}

}

