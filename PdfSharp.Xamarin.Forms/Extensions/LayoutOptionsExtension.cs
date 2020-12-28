using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Extensions
{
	public static class LayoutOptionsExtension
	{
		public static XStringAlignment ToXStringAlignment(this TextAlignment alignment)
		{
			switch (alignment)
			{
				case TextAlignment.Start:
					return XStringAlignment.Near;
				case TextAlignment.Center:
					return XStringAlignment.Center;
				case TextAlignment.End:
					return XStringAlignment.Far;
				default:
					return XStringAlignment.Near;
			}
		}

		public static XLineAlignment ToXLineAlignment(this TextAlignment alignment)
		{
			switch (alignment)
			{
				case TextAlignment.Start:
					return XLineAlignment.Near;
				case TextAlignment.Center:
					return XLineAlignment.Center;
				case TextAlignment.End:
					return XLineAlignment.Far;
				default:
					return XLineAlignment.BaseLine;
			}
		}

		public static XStringFormat ToXStringFormat(this TextAlignment alignment)
		{
			return new XStringFormat
			{
				Alignment = alignment.ToXStringAlignment(),
				LineAlignment = alignment.ToXLineAlignment(),
			};
		}
	}
}
