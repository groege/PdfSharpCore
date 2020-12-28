using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(RadioButton))]
	public class PdfRadioButtonRenderer : PdfRendererBase<RadioButton>
	{
		public static readonly XStringFormat DefaultTextFormat = new XStringFormat
		{
			LineAlignment = XLineAlignment.Center,
			Alignment = XStringAlignment.Near,
		};

		public override void CreatePDFLayout(XGraphics page, RadioButton button, XRect bounds, double scaleFactor)
		{
			var font = new XFont(button.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName,
				button.FontSize * scaleFactor);
			var textColor = button.TextColor != default ? button.TextColor : Color.Black;

			if (button.BackgroundColor != default)
				page.DrawRectangle(button.BackgroundColor.ToXBrush(), bounds);
			if (button.BorderWidth > 0 && button.BorderColor != default)
				page.DrawRectangle(new XPen(button.BorderColor.ToXColor(), button.BorderWidth * scaleFactor), bounds);

			page.DrawEllipse(new XPen(Color.Black.ToXColor(), scaleFactor * 2), bounds.X + bounds.Height * 0.1,
				bounds.Y + bounds.Height * 0.1, bounds.Height * 0.8, bounds.Height * 0.8);

			if (button.IsChecked)
				page.DrawEllipse(Color.Black.ToXBrush(), bounds.X + bounds.Height * 0.3, bounds.Y + bounds.Height * 0.3,
					bounds.Height * 0.4, bounds.Height * 0.4);

			if (!string.IsNullOrEmpty(button.Text))
				page.DrawString(button.Text, font, textColor.ToXBrush(),
					new XRect(new XPoint(bounds.X + bounds.Height, bounds.Y),
						new XSize(bounds.Width - bounds.Height, bounds.Height)), DefaultTextFormat);
		}
	}
}
