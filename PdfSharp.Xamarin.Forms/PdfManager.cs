using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using PdfSharp.Xamarin.Forms.Contracts;
using PdfSharp.Xamarin.Forms.Utils;
using PdfSharpCore;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using Xamarin.Forms;
using ImageSource = MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

[assembly: InternalsVisibleTo("PdfSharp.Xamarin.Forms.Droid")]
[assembly: InternalsVisibleTo("PdfSharp.Xamarin.Forms.iOS")]
[assembly: InternalsVisibleTo("PdfSharp.Xamarin.Forms.UWP")]
[assembly: InternalsVisibleTo("PdfSharp.Xamarin.Forms.WPF")]
namespace PdfSharp.Xamarin.Forms
{
	public class PDFManager
	{
		internal static PDFManager Instance { get; private set; }

		private PDFManager()
		{ }

		internal Dictionary<Type, Type> Renderers { get; set; }

		internal static void Init(ImageSource handler, ICustomFontProvider customFontProvider = null, IList<Assembly> rendererAssemblies = null)
		{
			if (Instance == null)
				Instance = new PDFManager();
			else
				throw new InvalidOperationException("PdfSharp.Xamarin.Forms already initialized before");

			GlobalFontSettings.FontResolver = new FontProvider(customFontProvider);
			ImageSource.ImageSourceImpl = handler;

			Instance.Renderers = new Dictionary<Type, Type>();

			if (rendererAssemblies == null)
				rendererAssemblies = new List<Assembly>();
			var thisAssembly = typeof(PDFManager).GetTypeInfo().Assembly;

			if (!rendererAssemblies.Contains(thisAssembly))
				rendererAssemblies.Add(thisAssembly);

			foreach (var asm in rendererAssemblies)
			{
				foreach (var typeInfo in asm.DefinedTypes)
				{
					if (typeInfo.IsDefined(typeof(Attributes.PdfRendererAttribute), false))
					{
						var rInfo = typeInfo.GetCustomAttribute<Attributes.PdfRendererAttribute>();
						if (Instance.Renderers.ContainsKey(rInfo.ViewType))
							Instance.Renderers[rInfo.ViewType] = typeInfo.AsType();
						else
							Instance.Renderers.Add(rInfo.ViewType, typeInfo.AsType());
					}
				}
			}
		}


		public static PdfDocument GeneratePDFFromView(View view, PageOrientation orientation = PageOrientation.Portrait, PageSize size = PageSize.A4, bool resizeToFit = true)
		{
			if (Instance == null)
				throw new InvalidOperationException("You must call Init first");

			PdfGenerator generator = new PdfGenerator(view, orientation, size, resizeToFit);

			return generator.Generate();
		}
	}
}
