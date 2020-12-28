using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(Switch))]
	public class PdfSwitchRenderer : PdfRendererBase<Switch>
	{
		public override void CreatePDFLayout(XGraphics page, Switch toggleSwitch, XRect bounds, double scaleFactor)
		{
			if (toggleSwitch.BackgroundColor != default)
				page.DrawRectangle(toggleSwitch.BackgroundColor.ToXBrush(), bounds);

			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
				case Device.macOS:
					DrawForiOS(page, toggleSwitch, bounds, scaleFactor);
					break;
				case Device.Android:
					DrawForAndroid(page, toggleSwitch, bounds, scaleFactor);
					break;
				case Device.UWP:
				case Device.WPF:
				default:
					DrawForUWP(page, toggleSwitch, bounds, scaleFactor);
					break;
			}
		}

		#region Platform Helpers

		private void DrawForiOS(XGraphics page, Switch toggleSwitch, XRect bounds, double scaleFactor)
		{
			var thumbColor = toggleSwitch.ThumbColor != default ? toggleSwitch.ThumbColor : Color.WhiteSmoke;
			if (toggleSwitch.IsToggled)
			{
				var onColor = toggleSwitch.OnColor != default ? toggleSwitch.OnColor : Color.Green;
				page.DrawRoundedRectangle(onColor.ToXBrush(), bounds.X, bounds.Y, 2 * bounds.Height, bounds.Height,
					bounds.Height, bounds.Height);
				page.DrawEllipse(thumbColor.ToXBrush(), bounds.X + bounds.Height * 1.1, bounds.Y + bounds.Height * 0.1,
					bounds.Height * 0.8, bounds.Height * 0.8);
			}
			else
			{
				page.DrawRoundedRectangle(new XPen(XColors.LightGray, scaleFactor), bounds.X, bounds.Y,
					2 * bounds.Height, bounds.Height, bounds.Height, bounds.Height);

				page.DrawEllipse(new XPen(Color.LightGray.ToXColor(), scaleFactor), bounds.X + bounds.Height * 0.1,
					bounds.Y + bounds.Height * 0.1, bounds.Height * 0.8, bounds.Height * 0.8);
			}
		}

		private void DrawForAndroid(XGraphics page, Switch toggleSwitch, XRect bounds, double scaleFactor)
		{
			var thumbColor = toggleSwitch.ThumbColor != default ? toggleSwitch.ThumbColor : Color.WhiteSmoke;
			if (toggleSwitch.IsToggled)
			{
				var onColor = toggleSwitch.OnColor != default ? toggleSwitch.OnColor : Color.DodgerBlue;
				page.DrawRoundedRectangle(onColor.ToXBrush(), bounds.X, bounds.Y + bounds.Height * 0.25,
					2 * bounds.Height, bounds.Height * 0.5, bounds.Height / 2, bounds.Height / 2);
				page.DrawEllipse(thumbColor.ToXBrush(), bounds.X + bounds.Height * 1.1, bounds.Y + bounds.Height * 0.1,
					bounds.Height * 0.8, bounds.Height * 0.8);
			}
			else
			{
				page.DrawRoundedRectangle(Color.DarkGray.ToXBrush(), bounds.X, bounds.Y + bounds.Height * 0.25,
					2 * bounds.Height, bounds.Height * 0.5, bounds.Height / 2, bounds.Height / 2);
				page.DrawEllipse(Color.LightGray.ToXBrush(), bounds.X + bounds.Height * 0.1,
					bounds.Y + bounds.Height * 0.1, bounds.Height * 0.8, bounds.Height * 0.8);
			}
		}

		private void DrawForUWP(XGraphics page, Switch toggleSwitch, XRect bounds, double scaleFactor)
		{
			var thumbColor = toggleSwitch.ThumbColor != default ? toggleSwitch.ThumbColor : Color.WhiteSmoke;
			if (toggleSwitch.IsToggled)
			{
				var onColor = toggleSwitch.OnColor != default ? toggleSwitch.OnColor : Color.DodgerBlue;
				page.DrawRoundedRectangle(onColor.ToXBrush(), bounds.X, bounds.Y, 2 * bounds.Height, bounds.Height,
					bounds.Height, bounds.Height);
				page.DrawEllipse(thumbColor.ToXBrush(), bounds.X + bounds.Height * 1.2, bounds.Y + bounds.Height * 0.2,
					bounds.Height * 0.6, bounds.Height * 0.6);
			}
			else
			{
				page.DrawRoundedRectangle(new XPen(XColors.LightGray, scaleFactor), bounds.X, bounds.Y,
					2 * bounds.Height, bounds.Height, bounds.Height, bounds.Height);

				page.DrawEllipse(Color.Black.ToXBrush(), bounds.X + bounds.Height * 0.2, bounds.Y + bounds.Height * 0.2,
					bounds.Height * 0.6, bounds.Height * 0.6);
			}
		}

		#endregion
	}
}
