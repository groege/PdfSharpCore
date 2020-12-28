using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(Image))]
	public class PdfImageRenderer : PdfRendererBase<Image>
	{
		public override async void CreatePDFLayout(XGraphics page, Image image, XRect bounds, double scaleFactor)
		{
			if (image.BackgroundColor != default)
				page.DrawRectangle(image.BackgroundColor.ToXBrush(), bounds);

			if (image.Source == null)
				return;

			XImage img = null;

			switch (image.Source)
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
			switch (image.Aspect)
			{
				case Aspect.Fill:
					desiredBounds = bounds;
					break;
				case Aspect.AspectFit:
				{
					double aspectRatio = ((double) img.PixelWidth) / img.PixelHeight;
					if (aspectRatio > (bounds.Width / bounds.Height))
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

			page.DrawImage(img, desiredBounds, new System.Threading.CancellationToken());
		}
	}
}
