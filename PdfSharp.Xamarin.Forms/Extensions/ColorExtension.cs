using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Extensions
{
	public static class ColorExtension
	{
		public static XColor ToXColor(this Color color)
		{
			if (color == default(Color))
				return XColors.Transparent;

			return XColor.FromArgb((int)(color.A * 255), (int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255));
		}

		public static XBrush ToXBrush(this Color color)
		{
			return new XSolidBrush(color.ToXColor());
		}
	}
}
