using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(BoxView))]
	public class PdfBoxViewRenderer : PdfRendererBase<BoxView>
	{
		public override void CreatePDFLayout(XGraphics page, BoxView box, XRect bounds, double scaleFactor)
		{
			if (box.BackgroundColor != default(Color))
				page.DrawRectangle(box.BackgroundColor.ToXBrush(), bounds);

			if (box.Color != default(Color))
				page.DrawRectangle(box.Color.ToXBrush(), bounds);
		}
	}
}
