using System;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.AddressBook;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;

namespace HelloiPhone
{
	public partial class HelloiPhoneViewController : UIViewController
	{
		public HelloiPhoneViewController () : base ("HelloiPhoneViewController", null)
		{
		}

		private CLLocation _currentLocation;
		private CLPlacemark _closestPlacemark;

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.txtOutput.Text = string.Empty;

			UIApplication.Notifications.ObserveDidBecomeActive ((sender, args) => {
				AppDelegate.Manager.LocationUpdated += HandleLocationChanged;
			});

			UIApplication.Notifications.ObserveDidEnterBackground ((sender, args) => {
				AppDelegate.Manager.LocationUpdated -= HandleLocationChanged;
			});
		}

		public void HandleLocationChanged (object sender, LocationUpdatedEventArgs e)
		{
			CurrentLocation = e.Location;
		}

		public CLLocation CurrentLocation {
			get { 
				return _currentLocation; 
			}
			set { 
				_currentLocation = value;

				SetClosestPlacemark (_currentLocation);
			}
		}

		public CLPlacemark ClosestPlacemark {
			get {
				return _closestPlacemark;
			}
			set {
				_closestPlacemark = value;

				Log (GetCurrentCity (_closestPlacemark));
			}
		}

		public async void SetClosestPlacemark (CLLocation location)
		{
			var placemarks = await this.ReverseGeocodeAsync (location);
			ClosestPlacemark = placemarks [0];

		}

		public string GetCurrentCity (CLPlacemark placemark)
		{
			NSObject city;
			placemark.AddressDictionary.TryGetValue (ABPersonAddressKey.City, out city);

			return city.ToString();
		}

		public async Task<CLPlacemark[]> ReverseGeocodeAsync (CLLocation location)
		{
			var geoCoder = new CLGeocoder ();
			var placemarks = await geoCoder.ReverseGeocodeLocationAsync (location);

			foreach (var placemark in placemarks) {
				Console.WriteLine (placemark);
			}          

			return placemarks;
		}

		private void Log (string message)
		{
			txtOutput.Text += message + Environment.NewLine;
		}
	}
}

