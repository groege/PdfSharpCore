using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Extensions
{
	public static class PointExtension
	{
		public static XPoint ToXPoint(this Point point)
		{
			return new XPoint(point.X, point.Y);
		}
	}
}
