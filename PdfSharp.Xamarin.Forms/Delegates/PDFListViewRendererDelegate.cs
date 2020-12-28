using System.Collections;
using System.Linq;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Delegates
{
	public class PdfListViewRendererDelegate
	{
		#region Items Calculation

		public virtual int GetNumberOfSections(ListView listView)
		{
			if (!listView.IsGroupingEnabled)
				return 1;

			return listView.ItemsSource?.Cast<object>()?.Count() ?? 1;
		}

		public virtual int GetNumberOfRowsInSection(ListView listView, int section)
		{
			if (!listView.IsGroupingEnabled)
				return listView.ItemsSource?.Cast<object>().Count() ?? 0;

			var sectionItems = listView.ItemsSource?.Cast<IEnumerable>().ElementAt(section);

			return sectionItems?.Cast<object>()?.Count() ?? 0;
		}

		#endregion

		#region Size calculation

		public virtual double GetHeaderHeight(ListView listView, int section)
		{
			if (listView.HeaderTemplate == null)
				return 0;

			var view = listView.HeaderTemplate.CreateContent() as View;
			return view.HeightRequest > 0 ? view.HeightRequest : 44;
		}

		public virtual double GetFooterHeight(ListView listView, int section)
		{
			if (listView.FooterTemplate == null)
				return 0;

			var view = listView.FooterTemplate.CreateContent() as View;
			return view.HeightRequest > 0 ? view.HeightRequest : 44;
		}

		public virtual double GetCellHeight(ListView listView, int section, int index)
		{
			return listView.RowHeight > 0 ? listView.RowHeight : 44;
		}

		public virtual double GetTotalHeight(ListView listView)
		{
			double height = 0;
			for (int section = 0; section < GetNumberOfSections(listView); section++)
			{
				height += GetHeaderHeight(listView, section);
				height += GetFooterHeight(listView, section);

				for (int row = 0; row < GetNumberOfRowsInSection(listView, section); row++)
				{
					height += GetCellHeight(listView, section, row);
				}
			}

			return height;
		}

		#endregion

		#region Template Drawing

		public virtual void DrawHeader(ListView listView, int section, XGraphics page, XRect bounds, double scaleFactor)
		{ }

		public virtual void DrawFooter(ListView listView, int section, XGraphics page, XRect bounds, double scaleFactor)
		{ }

		public virtual void DrawCell(ListView listView, int section, int row, XGraphics page, XRect bounds,
			double scaleFactor)
		{
			object bindingContext = null;
			if (!listView.IsGroupingEnabled)
				bindingContext = listView.ItemsSource?.Cast<object>().ElementAt(row);
			else
			{
				var grounSource = listView.ItemsSource?.Cast<IEnumerable>().ElementAt(section);
				bindingContext = grounSource?.Cast<object>().ElementAt(row);
			}

			if (bindingContext != null)
			{
				XFont font = new XFont(GlobalFontSettings.FontResolver.DefaultFontName, 15 * scaleFactor);

				page.DrawString(bindingContext.ToString(), font, Color.Black.ToXBrush(), bounds, new XStringFormat
				{
					Alignment = XStringAlignment.Near,
					LineAlignment = XLineAlignment.Center,
				});
			}
		}

		#endregion
	}
}
