using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

namespace PdfSharp.Xamarin.Forms.WPF
{
	class WPFImageSourceImpl : IImageSource
	{
		private readonly int _quality;
		private readonly Func<Stream> _getStream;

		public int Width { get; }

		public int Height { get; }

		public string Name { get; }

		public WPFImageSourceImpl(string name, Func<Stream> getStream, int quality)
		{
			Name = name;
			_getStream = getStream;
			_quality = quality;
			using (var stream = _getStream.Invoke())
			{
				var img = new BitmapImage();
				img.BeginInit();
				img.StreamSource = stream;
				img.CacheOption = BitmapCacheOption.OnLoad;
				img.EndInit();
				img.Freeze();
				Width = (int) img.Width;
				Height = (int) img.Height;
			}
		}

		public void SaveAsJpeg(MemoryStream ms, CancellationToken ct)
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			ct.Register(() => { tcs.TrySetCanceled(); });

			var task = Task.Run(() =>
			{
				ct.ThrowIfCancellationRequested();

				var image = System.Drawing.Image.FromStream(_getStream.Invoke());
				image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
				ms.Position = 0;
			}, ct);
			Task.WaitAny(task, tcs.Task);
			tcs.TrySetCanceled();
			ct.ThrowIfCancellationRequested();
			if (task.IsFaulted)
				throw task.Exception;
		}
	}
}
