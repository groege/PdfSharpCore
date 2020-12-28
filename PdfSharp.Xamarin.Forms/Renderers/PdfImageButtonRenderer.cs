using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(ImageButton))]
	public class PdfImageButtonRenderer : PdfRendererBase<ImageButton>
	{
		public override async void CreatePDFLayout(XGraphics page, ImageButton imageButton, XRect bounds,
			double scaleFactor)
		{
			if (imageButton.BackgroundColor != default)
				page.DrawRectangle(imageButton.BackgroundColor.ToXBrush(), bounds);

			if (imageButton.Source == null)
				return;

			XImage img = null;

			switch (imageButton.Source)
			{
				case FileImageSource fileImageSource:
					img = XImage.FromFile(fileImageSource.File);
					break;
				case UriImageSource uriImageSource:
					img = XImage.FromFile(uriImageSource.Uri.AbsolutePath);
					break;
				case StreamImageSource streamImageSource:
				{
					var stream = await streamImageSource.Stream.Invoke(new System.Threading.CancellationToken());
					img = XImage.FromStream(() => stream);
					break;
				}
			}

			XRect desiredBounds = bounds;
			switch (imageButton.Aspect)
			{
				case Aspect.Fill:
					desiredBounds = bounds;
					break;
				case Aspect.AspectFit:
				{
					double aspectRatio = (double) img.PixelWidth / img.PixelHeight;
					if (aspectRatio > bounds.Width / bounds.Height)
						desiredBounds.Height = desiredBounds.Width * aspectRatio;
					else
						desiredBounds.Width = desiredBounds.Height * aspectRatio;
				}
					break;
				//PdfSharp does not support drawing a portion pf image, its not supported
				case Aspect.AspectFill:
					desiredBounds = bounds;
					break;
			}

			if (imageButton.BorderColor != default)
				page.DrawRectangle(new XPen(imageButton.BorderColor.ToXColor(), imageButton.BorderWidth * scaleFactor),
					bounds);

			var centeredBounds = new XRect(bounds.X + (bounds.Width - desiredBounds.Width) / 2,
				bounds.Y + (bounds.Height - desiredBounds.Height) / 2, desiredBounds.Width, desiredBounds.Height);
			page.DrawImage(img, centeredBounds, new System.Threading.CancellationToken());
		}
	}
}
