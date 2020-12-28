using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using Rectangle = Xamarin.Forms.Shapes.Rectangle;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(Rectangle))]
	public class PdfRectangleRenderer : PdfRendererBase<Rectangle>
	{
		public override void CreatePDFLayout(XGraphics page, Rectangle rectangle, XRect bounds, double scaleFactor)
		{
			if (rectangle.BackgroundColor != default)
				page.DrawRectangle(rectangle.BackgroundColor.ToXBrush(), bounds);

			if (rectangle.Fill != null)
				page.DrawRectangle(rectangle.Fill.ToXBrush(), bounds);

			if (rectangle.Stroke != null)
				page.DrawRectangle(rectangle.Stroke.ToXPen(rectangle.StrokeThickness * scaleFactor), bounds);
		}
	}
}
