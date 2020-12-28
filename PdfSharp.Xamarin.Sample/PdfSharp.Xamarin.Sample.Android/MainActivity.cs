using Android.App;
using Android.Content.PM;
using Android.OS;

namespace PdfSharp.Xamarin.Sample.Droid
{
	[Activity(Label = "PdfSharp.Xamarin.Sample", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			PdfSharp.Xamarin.Forms.Droid.Platform.Init();

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.SetFlags("Shapes_Experimental", "RadioButton_Experimental");
			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new App());
		}
	}
}

