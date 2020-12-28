using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(DatePicker))]
	public class PdfDatePickerRenderer : PdfRendererBase<DatePicker>
	{
		public override void CreatePDFLayout(XGraphics page, DatePicker datePicker, XRect bounds, double scaleFactor)
		{
			XFont font = new XFont(GlobalFontSettings.FontResolver.DefaultFontName, 14 * scaleFactor);

			Color textColor = datePicker.TextColor != default(Color) ? datePicker.TextColor : Color.Black;
			string format = datePicker.Format;
			if (string.IsNullOrEmpty(format))
				format = "MM.DD.YYYY";

			page.DrawRectangle(new XPen(Color.LightGray.ToXColor(), 2 * scaleFactor), bounds);

			page.DrawString(datePicker.Date.ToString(format), font, textColor.ToXBrush(), bounds, new XStringFormat {
				Alignment = XStringAlignment.Center,
				LineAlignment = XLineAlignment.Center,
			});

		}
	}
}
