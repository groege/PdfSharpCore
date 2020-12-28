using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(Slider))]
	public class PdfSliderRenderer : PdfRendererBase<Slider>
	{
		public override void CreatePDFLayout(XGraphics page, Slider slider, XRect bounds, double scaleFactor)
		{
			double valueXPosition = slider.Value / (slider.Maximum - slider.Minimum) * bounds.Width;
			var minimumTrackColor = slider.MinimumTrackColor != default
				? slider.MinimumTrackColor.ToXBrush()
				: XBrushes.LightBlue;
			var maximumTrackColor = slider.MaximumTrackColor != default
				? slider.MaximumTrackColor.ToXBrush()
				: XBrushes.LightGray;
			var thumbColor = slider.ThumbColor != default ? slider.ThumbColor.ToXBrush() : XBrushes.LightSlateGray;

			if (slider.BackgroundColor != default)
				page.DrawRectangle(slider.BackgroundColor.ToXBrush(), bounds);

			page.DrawRectangle(minimumTrackColor, bounds.X, bounds.Y, valueXPosition, 8 * scaleFactor);
			page.DrawRectangle(maximumTrackColor, valueXPosition + bounds.X, bounds.Y, bounds.Width - valueXPosition,
				8 * scaleFactor);
			page.DrawEllipse(thumbColor, bounds.X + valueXPosition - 10 * scaleFactor,
				bounds.Y + bounds.Height % 2 - 6 * scaleFactor, 20 * scaleFactor, 20 * scaleFactor);
		}
	}
}
