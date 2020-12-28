using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using Xamarin.Forms.Shapes;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(Line))]
	public class PdfLineRenderer : PdfRendererBase<Line>
	{
		public override void CreatePDFLayout(XGraphics page, Line line, XRect bounds, double scaleFactor)
		{
			if (line.BackgroundColor != default)
				page.DrawRectangle(line.BackgroundColor.ToXBrush(), bounds);

			var x1 = bounds.X + line.X1 * bounds.Width / line.Width;
			var x2 = bounds.X + line.X2 * bounds.Width / line.Width;
			var y1 = bounds.Y + line.Y1 * bounds.Height / line.Height;
			var y2 = bounds.Y + line.Y2 * bounds.Height / line.Height;

			if (line.Stroke != null)
				page.DrawLine(line.Stroke.ToXPen(line.StrokeThickness * scaleFactor), x1, y1, x2, y2);
		}
	}
}
