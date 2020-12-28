using System.Reflection;
using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(SearchBar))]
	public class PdfSearchBarRenderer : PdfRendererBase<SearchBar>
	{
		public override void CreatePDFLayout(XGraphics page, SearchBar searchBar, XRect bounds, double scaleFactor)
		{
			switch (Device.RuntimePlatform)
			{
				case Device.Android:
					DrawForAndroid(page, searchBar, bounds, scaleFactor);
					break;
				case Device.iOS:
				case Device.macOS:
					DrawForiOS(page, searchBar, bounds, scaleFactor);
					break;
				case Device.UWP:
				case Device.WPF:
				default:
					DrawForUWP(page, searchBar, bounds, scaleFactor);
					break;
			}
		}

		#region Platform Helpers
		private void DrawForiOS(XGraphics page, SearchBar searchBar, XRect bounds, double scaleFactor)
		{
			Color bgColor = searchBar.BackgroundColor != default(Color) ? searchBar.BackgroundColor : Color.Gray;
			Color textColor = searchBar.TextColor != default(Color) ? searchBar.TextColor : Color.Gray;
			XFont font = new XFont(searchBar.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, searchBar.FontSize * scaleFactor);

			XImage searchIcon = XImage.FromStream(() => {
				var assembly = typeof(PdfSearchBarRenderer).GetTypeInfo().Assembly;
				return assembly.GetManifestResourceStream($"PdfSharp.Xamarin.Forms.Icons.search.png");
			});

			page.DrawRectangle(new XPen(bgColor.ToXColor(), 2 * scaleFactor), bounds);

			double iconSize = bounds.Height * 0.8;
			page.DrawImage(searchIcon, new XRect(bounds.X + 5 * scaleFactor, bounds.Y + bounds.Height * 0.1, iconSize, iconSize), new System.Threading.CancellationToken());

			if (!string.IsNullOrEmpty(searchBar.Text))
			{
				page.DrawString(searchBar.Text, font, textColor.ToXBrush(), new XRect(bounds.X + iconSize + 12 * scaleFactor, bounds.Y, bounds.Width - iconSize, bounds.Height), new XStringFormat {
					Alignment = XStringAlignment.Near,
					LineAlignment = XLineAlignment.Center
				});
			}

		}
		private void DrawForAndroid(XGraphics page, SearchBar searchBar, XRect bounds, double scaleFactor)
		{
			Color bgColor = searchBar.BackgroundColor != default(Color) ? searchBar.BackgroundColor : Color.Black;
			Color textColor = searchBar.TextColor != default(Color) ? searchBar.TextColor : Color.Gray;
			XFont font = new XFont(searchBar.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, searchBar.FontSize * scaleFactor);

			XImage searchIcon = XImage.FromStream(() => {
				var assembly = typeof(PdfSearchBarRenderer).GetTypeInfo().Assembly;
				return assembly.GetManifestResourceStream($"PdfSharp.Xamarin.Forms.Icons.search.png");
			});
			double iconSize = bounds.Height * 0.8;

			page.DrawRectangle(bgColor.ToXBrush(), bounds);
			page.DrawLine(new XPen(Color.LightBlue.ToXColor(), 1 * scaleFactor),
						  new XPoint(bounds.X + iconSize + 6 * scaleFactor, bounds.Y + bounds.Height - 2 * scaleFactor),
						  new XPoint(bounds.X + bounds.Width - 2 * scaleFactor, bounds.Y + bounds.Height - 2 * scaleFactor));

			page.DrawImage(searchIcon, new XRect(bounds.X + 5 * scaleFactor, bounds.Y + bounds.Height * 0.1, iconSize, iconSize), new System.Threading.CancellationToken());

			if (!string.IsNullOrEmpty(searchBar.Text))
			{
				page.DrawString(searchBar.Text, font, textColor.ToXBrush(), new XRect(bounds.X + iconSize + 12 * scaleFactor, bounds.Y, bounds.Width - iconSize, bounds.Height), new XStringFormat {
					Alignment = XStringAlignment.Near,
					LineAlignment = XLineAlignment.Center
				});
			}
		}
		private void DrawForUWP(XGraphics page, SearchBar searchBar, XRect bounds, double scaleFactor)
		{
			Color bgColor = searchBar.BackgroundColor != default(Color) ? searchBar.BackgroundColor : Color.White;
			XFont font = new XFont(searchBar.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, searchBar.FontSize * scaleFactor);
			XImage searchIcon = XImage.FromStream(() => {
				var assembly = typeof(PdfSearchBarRenderer).GetTypeInfo().Assembly;
				return assembly.GetManifestResourceStream($"PdfSharp.Xamarin.Forms.Icons.search.png");
			});

			page.DrawRectangle(bgColor.ToXBrush(), bounds);
			page.DrawRectangle(new XPen(Color.Gray.ToXColor(), 2 * scaleFactor), bounds);

			if (!string.IsNullOrEmpty(searchBar.Text))
			{
				Color textColor = searchBar.TextColor != default(Color) ? searchBar.TextColor : Color.Black;
				page.DrawString(searchBar.Text, font, textColor.ToXBrush(), new XRect(5 * scaleFactor + bounds.X, bounds.Y, bounds.Width, bounds.Height), new XStringFormat {
					Alignment = XStringAlignment.Near,
					LineAlignment = XLineAlignment.Center
				});
			}
			else if (!string.IsNullOrEmpty(searchBar.Placeholder))
			{
				Color placeholderColor = searchBar.PlaceholderColor != default(Color) ? searchBar.PlaceholderColor : Color.Gray;
				page.DrawString(searchBar.Placeholder, font, placeholderColor.ToXBrush(), new XRect(5 * scaleFactor + bounds.X, bounds.Y, bounds.Width, bounds.Height), new XStringFormat {
					Alignment = XStringAlignment.Near,
					LineAlignment = XLineAlignment.Center
				});
			}

			double imgSize = bounds.Height - 4 * scaleFactor;
			page.DrawImage(searchIcon, new XRect(bounds.X + bounds.Width - imgSize - 2 * scaleFactor, bounds.Y + 2 * scaleFactor, imgSize, imgSize), new System.Threading.CancellationToken());

		}
		#endregion
	}
}
