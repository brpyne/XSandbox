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
			
			// Perform any additional setup after loading the view, typically from a nib.
			alertDelegate = new MyAlertDelegate ();
			this.locMgr = new CLLocationManager();

			StartLocationUpdates ();
		}

		protected CLLocationManager locMgr;

		public CLLocationManager LocMgr{
			get { return this.locMgr; }
		}


		public void StartLocationUpdates()
		{
			// we need the user’s permission to use GPS, so we check to make sure they’ve accepted
			if (CLLocationManager.LocationServicesEnabled) {
				//set the desired accuracy, in meters
				LocMgr.DesiredAccuracy = 1;
				LocMgr.StartUpdatingLocation();
			} else {
				Console.WriteLine ("Location services not enabled");
			}
		}


		public void HandleLocationChanged (object sender, LocationUpdatedEventArgs e)
		{
			/*
			// handle foreground updates
			CLLocation location = e.Location;
			IMainScreen ms = mainScreen;
			ms.LblAltitude.Text = location.Altitude + " meters";
			ms.LblLongitude.Text = location.Coordinate.Longitude.ToString ();
			ms.LblLatitude.Text = location.Coordinate.Latitude.ToString ();
			ms.LblCourse.Text = location.Course.ToString ();
			ms.LblSpeed.Text = location.Speed.ToString ();
			Console.WriteLine ("foreground updated");
			*/
		}


		public class MyAlertDelegate : UIAlertViewDelegate {
			public override void Clicked (UIAlertView alertView, int buttonIndex)
			{
				Console.WriteLine (buttonIndex.ToString () + " clicked");
			}
		}

		MyAlertDelegate alertDelegate;

		partial void btnOK(MonoTouch.Foundation.NSObject sender)
		{
			UIAlertView alert = new UIAlertView("Hello World! ", "Welcome "+this.txtInput.Text, alertDelegate, "Ok", "Cancel");
			alert.Show();
		}

		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

