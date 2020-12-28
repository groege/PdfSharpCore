using System.Linq;
using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Extensions
{
	public static class BrushExtension
	{
		public static XBrush ToXBrush(this Brush brush)
		{
			switch (brush)
			{
				case LinearGradientBrush gradientBrush when gradientBrush.GradientStops.Any():
					return new XLinearGradientBrush(gradientBrush.StartPoint.ToXPoint(),
						gradientBrush.EndPoint.ToXPoint(), gradientBrush.GradientStops.First().Color.ToXColor(),
						gradientBrush.GradientStops.Last().Color.ToXColor());
				case SolidColorBrush solidColorBrush:
					return new XSolidBrush(solidColorBrush.Color.ToXColor());
				default:
					return new XSolidBrush(XColors.Black);
			}
		}

		public static XPen ToXPen(this Brush brush, double with)
		{
			XColor color;
			switch (brush)
			{
				case LinearGradientBrush gradientBrush when gradientBrush.GradientStops.Any():
					color = gradientBrush.GradientStops.First().Color.ToXColor();
					break;
				case SolidColorBrush solidColorBrush:
					color = solidColorBrush.Color.ToXColor();
					break;
				default:
					color = XColors.Black;
					break;
			}

			return new XPen(color, with);
		}
	}
}
