using System;
using System.IO;
using System.Windows;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;

namespace PdfSharp.Xamarin.Forms.WPF
{
	class WPFImageSource : ImageSource
	{
		protected override IImageSource FromBinaryImpl(string name, Func<byte[]> imageSource, int? quality = 75)
		{
			return new WPFImageSourceImpl(name, () => new MemoryStream(imageSource.Invoke()),
				(int) quality);
		}

		protected override IImageSource FromFileImpl(string path, int? quality = 75)
		{
			return new WPFImageSourceImpl(Path.GetFileName(path), () =>
			{
				var uri = new Uri(path, UriKind.Relative);
				var info = Application.GetResourceStream(uri);
				return info.Stream;
			}, (int) quality);
		}

		protected override IImageSource FromStreamImpl(string name, Func<Stream> imageStream, int? quality = 75)
		{
			return new WPFImageSourceImpl(name, imageStream.Invoke, (int) quality);
		}
	}
}
