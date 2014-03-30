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

		public async void ReverseGeocodeToConsoleAsync (CLLocation location) {
			var geoCoder = new CLGeocoder();
			var placemarks = await geoCoder.ReverseGeocodeLocationAsync(location);
			foreach (var placemark in placemarks) {
				Console.WriteLine(placemark);
			}          
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.txtOutput.Text = string.Empty;


			// screen subscribes to the location changed event
			UIApplication.Notifications.ObserveDidBecomeActive ((sender, args) => {
				AppDelegate.Manager.LocationUpdated += HandleLocationChanged;
			});

			// whenever the app enters the background state, we unsubscribe from the event 
			// so we no longer perform foreground updates
			UIApplication.Notifications.ObserveDidEnterBackground ((sender, args) => {
				AppDelegate.Manager.LocationUpdated -= HandleLocationChanged;
			});
		}

		public void HandleLocationChanged (object sender, LocationUpdatedEventArgs e)
		{
			// handle foreground updates
			CLLocation location = e.Location;
			Log(string.Format("{0}, {1}", location.Coordinate.Longitude.ToString (), location.Coordinate.Latitude.ToString ()));

			Console.WriteLine ("foreground updated");
		}


		private void Log(string message)
		{
			txtOutput.Text += message + Environment.NewLine;
		}


	}

}

