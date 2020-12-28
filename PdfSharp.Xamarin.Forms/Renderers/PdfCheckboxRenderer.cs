using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(RadioButton))]
	public class PdfCheckBoxRenderer : PdfRendererBase<CheckBox>
	{
		public override void CreatePDFLayout(XGraphics page, CheckBox checkBox, XRect bounds, double scaleFactor)
		{
			if (checkBox.BackgroundColor != default)
				page.DrawRectangle(checkBox.BackgroundColor.ToXBrush(), bounds);

			var adjustedBounds = new XRect(bounds.Location, new XSize(bounds.Height, bounds.Height));
			var checkPath = new[]
			{
				new XPoint(adjustedBounds.Width * 0.15, adjustedBounds.Height * 0.3),
				new XPoint(adjustedBounds.Width * 0.3, adjustedBounds.Height * 0.7),
				new XPoint(adjustedBounds.Width * 0.85, adjustedBounds.Height * 0.8)
			};

			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
				case Device.macOS:
					if (checkBox.IsChecked)
					{
						page.DrawEllipse(checkBox.Color.ToXBrush(), adjustedBounds);
						page.DrawLines(new XPen(XColors.White, scaleFactor * 3), checkPath);
					}
					else
						page.DrawEllipse(new XPen(checkBox.Color.ToXColor(), scaleFactor), adjustedBounds);

					break;

				case Device.Android:
					if (checkBox.IsChecked)
					{
						page.DrawRectangle(checkBox.Color.ToXBrush(), adjustedBounds);
						page.DrawLines(new XPen(XColors.White, scaleFactor * 3), checkPath);
					}
					else
						page.DrawRectangle(new XPen(checkBox.Color.ToXColor(), scaleFactor), adjustedBounds);

					break;
				case Device.UWP:
				case Device.WPF:
					if (checkBox.IsChecked)
					{
						page.DrawRectangle(checkBox.Color.ToXBrush(), adjustedBounds);
						page.DrawLines(new XPen(XColors.LightBlue, scaleFactor * 3), checkPath);
					}
					else
						page.DrawRectangle(new XPen(checkBox.Color.ToXColor(), scaleFactor), adjustedBounds);

					break;
			}
		}
	}
}
