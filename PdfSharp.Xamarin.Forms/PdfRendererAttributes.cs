using PdfSharp.Xamarin.Forms.Delegates;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms
{
	public class PdfRendererAttributes : BindableObject
	{
		public bool ShouldRender
		{
			get => (bool) GetValue(ShouldRenderProperty);
			set => SetValue(ShouldRenderProperty, value);
		}

		public PdfListViewRendererDelegate ListRendererDelegate
		{
			get => (PdfListViewRendererDelegate) GetValue(ListRendererDelegateProperty);
			set => SetValue(ListRendererDelegateProperty, value);
		}

		public static readonly BindableProperty ShouldRenderProperty =
			BindableProperty.CreateAttached(nameof(ShouldRender), typeof(bool), typeof(PdfRendererAttributes), true);

		public static readonly BindableProperty ListRendererDelegateProperty =
			BindableProperty.CreateAttached(nameof(ListRendererDelegate), typeof(PdfListViewRendererDelegate),
				typeof(PdfRendererAttributes), new PdfListViewRendererDelegate());

		public static bool ShouldRenderView(BindableObject bindable)
		{
			return (bool) bindable.GetValue(ShouldRenderProperty);
		}
	}
}
