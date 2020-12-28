using System;
using System.IO;
using System.Linq;
using System.Reflection;
using PdfSharp.Xamarin.Forms.Contracts;
using PdfSharpCore.Fonts;

namespace PdfSharp.Xamarin.Forms.Utils
{
	internal class FontProvider : IFontResolver
	{
		#region Properties
		public string DefaultFontName => "OpenSans";

		#endregion

		#region Fields
		public ICustomFontProvider _fontProvider;
		public static readonly string[] DefaultFontFiles =
		{
			"OpenSans-Regular.ttf",
			"OpenSans-Bold.ttf",
			"OpenSans-Italic.ttf",
			"OpenSans-BoldItalic.ttf",
		};
		#endregion

		#region Ctor
		public FontProvider(ICustomFontProvider fontProvider)
		{
			_fontProvider = fontProvider;
		}
		#endregion

		#region IFontResolver implementation
		public byte[] GetFont(string faceName)
		{
			if (DefaultFontFiles.Contains(faceName) || _fontProvider == null)
			{
				var assembly = typeof(FontProvider).GetTypeInfo().Assembly;
				Stream stream = assembly.GetManifestResourceStream($"PdfSharp.Xamarin.Forms.DefaultFonts.{faceName}");
				using (var reader = new StreamReader(stream))
				{
					byte[] bytes;
					using (var memstream = new MemoryStream())
					{
						reader.BaseStream.CopyTo(memstream);
						bytes = memstream.ToArray();
					}
					return bytes;
				}
			}
			else
			{
				return _fontProvider.GetFont(faceName);
			}
		}

		public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
		{
			string fontName;
			if (familyName == DefaultFontName || _fontProvider == null)
				fontName = DefaultFontFiles[Convert.ToInt32(isBold) + 2 * Convert.ToInt32(isItalic)];
			else
				fontName = _fontProvider.ProvideFont(familyName, isBold, isItalic);

			return new FontResolverInfo(fontName);
		}
		#endregion
	}
}
