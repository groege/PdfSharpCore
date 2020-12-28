﻿using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(Entry))]
	public class PdfEntryRenderer : PdfRendererBase<Entry>
	{
		public override void CreatePDFLayout(XGraphics page, Entry entry, XRect bounds, double scaleFactor)
		{
			var font = new XFont(entry.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName,
				entry.FontSize * scaleFactor);

			if (entry.BackgroundColor != default)
				page.DrawRectangle(entry.BackgroundColor.ToXBrush(), bounds);

			// Border
			page.DrawRectangle(new XPen(Color.LightGray.ToXColor(), 1.5 * scaleFactor), bounds);

			if (!string.IsNullOrEmpty(entry.Text))
			{
				Color textColor = entry.TextColor != default ? entry.TextColor : Color.Black;
				page.DrawString(entry.Text, font, textColor.ToXBrush(), bounds.AddMargin(5 * scaleFactor),
					entry.HorizontalTextAlignment.ToXStringFormat());
			}
			else if (!string.IsNullOrEmpty(entry.Placeholder))
			{
				Color placeholderColor = entry.PlaceholderColor != default ? entry.PlaceholderColor : Color.Gray;
				page.DrawString(entry.Placeholder, font, placeholderColor.ToXBrush(), bounds.AddMargin(5 * scaleFactor),
					entry.HorizontalTextAlignment.ToXStringFormat());
			}
		}
	}
}
