using System;
using System.Collections.Generic;
using System.Linq;
using PdfSharp.Xamarin.Forms.Delegates;
using PdfSharp.Xamarin.Forms.Utils;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms
{
	internal class PdfGenerator
	{
		#region Fields

		readonly double _scaleFactor;
		readonly XRect _desiredPageSize;
		readonly PageOrientation _orientation;
		readonly PageSize _pageSize;
		readonly View _rootView;

		List<ViewInfo> _viewsToDraw;
		#endregion

		public PdfGenerator(View view, PageOrientation orientation, PageSize pageSize, bool resizeToFit)
		{
			_pageSize = pageSize;
			_orientation = orientation;
			_rootView = view;

			_desiredPageSize = SizeUtils.GetAvailablePageSize(pageSize, orientation);

			if (resizeToFit)
				_scaleFactor = _desiredPageSize.Width / view.Bounds.Width;
			else
				_scaleFactor = 1;
		}

		public PdfDocument Generate()
		{
			_viewsToDraw = new List<ViewInfo>();
			VisitView(_rootView, new Point(0, 0));

			return CreatePDF(_viewsToDraw);
		}

		#region Private Helpers
		Point invisiblesOffsetTreshold = new Point(0, 0);
		private void VisitView(View view, Point pageOffset)
		{
			if (!PdfRendererAttributes.ShouldRenderView(view))
				return;

			Point newOffset = new Point(pageOffset.X + view.X * _scaleFactor + invisiblesOffsetTreshold.X,
										pageOffset.Y + view.Y * _scaleFactor + invisiblesOffsetTreshold.Y);

			Rectangle bounds = new Rectangle(newOffset,
				new Size(view.Bounds.Width * _scaleFactor, view.Bounds.Height * _scaleFactor));
			_viewsToDraw.Add(new ViewInfo {View = view, Offset = newOffset, Bounds = bounds});

			if (view is ListView)
			{
				ListView listView = view as ListView;
				var listViewDelegate = listView.GetValue(PdfRendererAttributes.ListRendererDelegateProperty) as PdfListViewRendererDelegate;

				Point listOffset = newOffset;
				for (int section = 0; section < listViewDelegate.GetNumberOfSections(listView); section++)
				{
					//Get Headers
					if (listView.HeaderTemplate != null)
					{
						double headerHeight = listViewDelegate.GetHeaderHeight(listView, section) * _scaleFactor;
						_viewsToDraw.Add(new ListViewInfo {
							ItemType = ListViewItemType.Header,
							ListViewDelegate = listViewDelegate,
							Section = section,
							View = listView,
							Offset = listOffset,
							Bounds = new Rectangle(0, 0, 0, headerHeight),
						});
						listOffset.Y += headerHeight;
					}
					//Get Rows
					for (int row = 0; row < listViewDelegate.GetNumberOfRowsInSection(listView, section); row++)
					{
						double rowHeight = listViewDelegate.GetCellHeight(listView, section, row) * _scaleFactor;
						_viewsToDraw.Add(new ListViewInfo {
							ItemType = ListViewItemType.Cell,
							ListViewDelegate = listViewDelegate,
							View = listView,
							Row = row,
							Section = section,
							Offset = listOffset,
							Bounds = new Rectangle(listOffset.X, listOffset.Y, 0, rowHeight),
						});
						listOffset.Y += rowHeight;
					}
					//Get Footers
					if (listView.FooterTemplate != null)
					{
						double footerHeight = listViewDelegate.GetFooterHeight(listView, section) * _scaleFactor;
						_viewsToDraw.Add(new ListViewInfo {
							ItemType = ListViewItemType.Footer,
							ListViewDelegate = listViewDelegate,
							Section = section,
							View = listView,
							Offset = listOffset,
							Bounds = new Rectangle(0, 0, 0, footerHeight),
						});
						listOffset.Y += footerHeight;
					}
				}

				double desiredHeight = listViewDelegate.GetTotalHeight(listView);

				//add extra space for writing all listView cells into UI
				if (desiredHeight > listView.Bounds.Height)
					invisiblesOffsetTreshold.Y += (desiredHeight - listView.Bounds.Height) * _scaleFactor;
			}
			if (view is Layout<View> layout)
			{
				foreach (var v in layout.Children)
				{
					VisitView(v, newOffset);
				}
			}
			else if (view is Frame frame && frame.Content != null)
			{
				VisitView(frame.Content, newOffset);
			}
			else if (view is ContentView contentView && contentView.Content != null)
			{
				VisitView(contentView.Content, newOffset);
			}
			else if (view is ScrollView scrollView && scrollView.Content != null)
			{
				VisitView(scrollView.Content, newOffset);
			}
		}


		private PdfDocument CreatePDF(List<ViewInfo> views)
		{
			var document = new PdfDocument();

			int numberOfPages = (int) Math.Ceiling(_viewsToDraw.Max(x => x.Offset.Y + x.View.HeightRequest * _scaleFactor) / _desiredPageSize.Height);

			for (int i = 0; i < numberOfPages; i++)
			{
				var page = document.AddPage();
				page.Orientation = _orientation;
				page.Size = _pageSize;
				var gfx = XGraphics.FromPdfPage(page, XGraphicsUnit.Millimeter);

				var viewsInPage = _viewsToDraw.Where(x => x.Offset.Y >= i * _desiredPageSize.Height && (x.Offset.Y + x.Bounds.Height * _scaleFactor) <= (i + 1) * _desiredPageSize.Height).ToList();

				foreach (var v in viewsInPage)
				{
					var rList = PDFManager.Instance.Renderers.FirstOrDefault(x => x.Key == v.View.GetType());
					//Draw ListView Content With Delegate
					if (v is ListViewInfo vInfo)
					{
						XRect desiredBounds = new XRect(vInfo.Offset.X + _desiredPageSize.X,
														vInfo.Offset.Y + _desiredPageSize.Y - (i * _desiredPageSize.Height),
														vInfo.Bounds.Width,
														vInfo.Bounds.Height);
						switch (vInfo.ItemType)
						{
							case ListViewItemType.Cell:
								vInfo.ListViewDelegate.DrawCell(vInfo.View as ListView, vInfo.Section, vInfo.Row, gfx, desiredBounds, _scaleFactor);
								break;
							case ListViewItemType.Header:
								vInfo.ListViewDelegate.DrawHeader(vInfo.View as ListView, vInfo.Section, gfx, desiredBounds, _scaleFactor);
								break;
							case ListViewItemType.Footer:
								vInfo.ListViewDelegate.DrawFooter(vInfo.View as ListView, vInfo.Section, gfx, desiredBounds, _scaleFactor);
								break;
						}
					}

					//Draw all other Views
					else if (rList.Value != null && v.Bounds.Width > 0 && v.View.Height > 0)
					{
						var renderer = Activator.CreateInstance(rList.Value) as Renderers.PdfRendererBase;
						XRect desiredBounds = new XRect(v.Offset.X + _desiredPageSize.X,
														v.Offset.Y + _desiredPageSize.Y - (i * _desiredPageSize.Height),
														v.Bounds.Width,
														v.Bounds.Height);

						renderer.CreateLayout(gfx, v.View, desiredBounds, _scaleFactor);
					}
				}
			}

			return document;
		}
		#endregion
	}

	class ViewInfo
	{
		public Rectangle Bounds { get; set; }
		public View View { get; set; }
		public Point Offset { get; set; }
	}

	class ListViewInfo : ViewInfo
	{
		public int Section { get; set; }

		public int Row { get; set; }

		public ListViewItemType ItemType { get; set; }

		public PdfListViewRendererDelegate ListViewDelegate { get; set; }
	}

	enum ListViewItemType
	{
		Cell,
		Header,
		Footer,
	}
}
