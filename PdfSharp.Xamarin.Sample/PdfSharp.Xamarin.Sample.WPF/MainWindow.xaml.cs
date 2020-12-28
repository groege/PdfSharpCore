using Xamarin.Forms.Platform.WPF;

namespace Xamarin.Sample.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : FormsApplicationPage
	{
		public MainWindow()
		{
			InitializeComponent();

			global::Xamarin.Forms.Forms.SetFlags("Shapes_Experimental", "RadioButton_Experimental");
			global::Xamarin.Forms.Forms.Init();
			PdfSharp.Xamarin.Forms.WPF.Platform.Init();
			LoadApplication(new PdfSharp.Xamarin.Sample.App());
		}
	}
}
