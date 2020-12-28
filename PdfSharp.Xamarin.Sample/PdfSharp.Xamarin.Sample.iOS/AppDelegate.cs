using Foundation;
using UIKit;

namespace PdfSharp.Xamarin.Sample.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.SetFlags("Shapes_Experimental", "RadioButton_Experimental");
			global::Xamarin.Forms.Forms.Init();
			PdfSharp.Xamarin.Forms.iOS.Platform.Init();
			LoadApplication(new PdfSharp.Xamarin.Sample.App());

			return base.FinishedLaunching(app, options);
		}
	}
}
