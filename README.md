PdfSharp.Xamarin.Forms
======================

**PdfSharp.Xamarin.Forms** is a Xamarin.Forms library for **converting any Xamarin.Forms UI into PDF**. It uses [PdfSharp](http://www.pdfsharp.net/), which is a partial port of [PdfSharpCore](https://github.com/groege/PdfSharpCore).

> ### Supported Platforms
> - UWP
> - Android
> - iOS
> - WPF


### Screenshots ([see all](https://github.com/akgulebubekir/PDFSharp.Xamarin.Forms/tree/master/Screenshots))
![App vs PDF](https://raw.githubusercontent.com/akgulebubekir/PDFSharp.Xamarin.Forms/master/Screenshots/table.PNG)


### Usage
> - **Init** : For each platform you need to init seperately: `PdfSharp.Xamarin.Forms.{Platform}.Platform.Init()`
> - **Generate** : `var pdf = PDFManager.GeneratePDFFromView(yourView)`
> - **Save** :  `DependencyService.Get<IPdfSave>().Save(pdf, "pdfName.pdf")`


### Features
> - Custom Fonts (You should provide Font Types and font files via `IPDFHandler`)
> - Image rendering
> - Custom renderer ( You can write your own renderer for your customView)
> - Paper size & orientation support
> - Do not render option : by using `pdf:PdfRendererAttributes.ShouldRender="False"` you can ignore that view in PDF


### Limitations
> - Images renders only Jpeg format (It converts PNG to JPEG automatically)
> - ListView does not renders automatically. You should write a renderer.

### ListView Rendering
> - Due ListView Cell is not accesible from parent, you should implement a `PdfListViewRendererDelegate` for the `ListView`.


### Known Issues
> - Fonts are bigger than usual for WPF Platform

```xml

  <ContentPage  xmlns:pdf="clr-namespace:PdfSharp.Xamarin.Forms;assembly=PdfSharp.Xamarin.Forms">
  	<ListView pdf:ListRendererDelegate="{StaticResource YourRendererDelegate}" .../>
  </ContentPage>
```

<u>Renderer:</u>
```cs
	public class PDFSampleListRendererDelegate: PdfListViewRendererDelegate
	{
		public override void DrawCell(ListView listView, int section, int row, XGraphics page, XRect bounds, double scaleFactor)
		{
			base.DrawCell(listView, section, row, page, bounds, scaleFactor);
		}

		public override void DrawFooter(ListView listView, int section, XGraphics page, XRect bounds, double scaleFactor)
		{
			base.DrawFooter(listView, section, page, bounds, scaleFactor);
		}

		public override double GetFooterHeight(ListView listView, int section)
		{
			return base.GetFooterHeight(listView, section);
		}
	}
```


### Custom PDF Renderer
> Its possible to write your own renderer, it will use it while renderering your View.

 <u>Register :</u> `PDFManager.RegisterRenderer(typeof(Label), typeof(PDFCustomLabelRenderer))`

<u>Render:</u>

```cs
	public class PDFCustomLabelRenderer : PdfRendererBase<Label>
	{
		public override void CreatePDFLayout(XGraphics page, Label label, XRect bounds, double scaleFactor)
		{
			XFont font = new XFont(label.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, label.FontSize * scaleFactor);
			Color textColor = label.TextColor != default(Color) ? label.TextColor : Color.Black;

			if (label.BackgroundColor != default(Color))
				page.DrawRectangle(label.BackgroundColor.ToXBrush(), bounds);

			if (!string.IsNullOrEmpty(label.Text))
				page.DrawString(label.Text, font, textColor.ToXBrush(), bounds,
					new XStringFormat()
					{
						Alignment = label.HorizontalTextAlignment.ToXStringAlignment(),
						LineAlignment = label.VerticalTextAlignment.ToXLineAlignment(),
					});
		}
	}
```