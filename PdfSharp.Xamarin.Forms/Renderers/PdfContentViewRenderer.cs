using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(ContentView))]
	public class PdfContentViewRenderer : PdfRendererBase<ContentView>
	{
		public override void CreatePDFLayout(XGraphics page, ContentView view, XRect bounds, double scaleFactor)
		{
			if (view.BackgroundColor != null)
				page.DrawRectangle(view.BackgroundColor.ToXBrush(), bounds);
		}
	}
}
