using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(Label))]
	public class PDFLabelRenderer : PdfRendererBase<Label>
	{
		public override void CreatePDFLayout(XGraphics page, Label label, XRect bounds, double scaleFactor)
		{
			var font = new XFont(label.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, label.FontSize * scaleFactor);
			Color textColor = label.TextColor != default ? label.TextColor : Color.Black;

			if (label.BackgroundColor != default)
				page.DrawRectangle(label.BackgroundColor.ToXBrush(), bounds);

			if (!string.IsNullOrEmpty(label.Text))
				page.DrawString(label.Text, font, textColor.ToXBrush(), bounds, label.HorizontalTextAlignment.ToXStringFormat());
		}
	}
}
