using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Extensions
{
	public static class RectangleExtensions
	{
		public static XRect ToXRect(this Rectangle rect)
		{
			return new XRect(rect.X, rect.Y, rect.Width, rect.Height);
		}

		public static XRect AddMargin(this XRect rect, Thickness thickness)
		{
			return new XRect(rect.X + thickness.Left, rect.Y + thickness.Top, rect.Width - thickness.HorizontalThickness, rect.Height - thickness.VerticalThickness);
		}
	}
}
