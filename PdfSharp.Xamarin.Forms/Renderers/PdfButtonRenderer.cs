using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(Button))]
	public class PdfButtonRenderer : PdfRendererBase<Button>
	{
		public static readonly XStringFormat DefaultTextFormat = new XStringFormat
		{
			LineAlignment = XLineAlignment.Center,
			Alignment = XStringAlignment.Center,
		};

		public override void CreatePDFLayout(XGraphics page, Button button, XRect bounds, double scaleFactor)
		{
			var font = new XFont(button.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName,
				button.FontSize * scaleFactor);
			var textColor = button.TextColor != default ? button.TextColor : Color.Black;

			if (button.BackgroundColor != default)
				page.DrawRectangle(button.BackgroundColor.ToXBrush(), bounds);
			if (button.BorderWidth > 0 && button.BorderColor != default)
				page.DrawRectangle(new XPen(button.BorderColor.ToXColor(), button.BorderWidth * scaleFactor), bounds);

			if (!string.IsNullOrEmpty(button.Text))
				page.DrawString(button.Text, font, textColor.ToXBrush(), bounds, DefaultTextFormat);
		}
	}
}
