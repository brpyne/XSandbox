// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace HelloiPhone
{
	[Register ("HelloiPhoneViewController")]
	partial class HelloiPhoneViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField txtInput { get; set; }

		[Action ("btnOK:")]
		partial void btnOK (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (txtInput != null) {
				txtInput.Dispose ();
				txtInput = null;
			}
		}
	}
}
