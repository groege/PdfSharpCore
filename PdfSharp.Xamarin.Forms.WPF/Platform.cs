using System.Collections.Generic;
using System.Reflection;
using PdfSharp.Xamarin.Forms.Contracts;

namespace PdfSharp.Xamarin.Forms.WPF
{
	public class Platform
	{
		public static void Init(ICustomFontProvider customFontProvider = null,
			IList<Assembly> rendererAssemblies = null)
		{
			PDFManager.Init(new WPFImageSource(), customFontProvider, rendererAssemblies);
		}
	}
}
