using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using Xamarin.Forms.Shapes;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(Ellipse))]
	public class PdfEllipseRenderer : PdfRendererBase<Ellipse>
	{
		public override void CreatePDFLayout(XGraphics page, Ellipse ellipse, XRect bounds, double scaleFactor)
		{
			if (ellipse.BackgroundColor != default)
				page.DrawRectangle(ellipse.BackgroundColor.ToXBrush(), bounds);
			if (ellipse.Stroke != null)
				page.DrawEllipse(ellipse.Stroke.ToXPen(ellipse.StrokeThickness * scaleFactor), bounds);
			if (ellipse.Fill != null)
				page.DrawEllipse(ellipse.Fill.ToXBrush(), bounds);
		}
	}
}
