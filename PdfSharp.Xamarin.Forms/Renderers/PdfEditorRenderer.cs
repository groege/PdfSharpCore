using PdfSharp.Xamarin.Forms.Attributes;
using PdfSharp.Xamarin.Forms.Extensions;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Forms.Renderers
{
	[PdfRenderer(ViewType = typeof(Editor))]
	public class PdfEditorRenderer : PdfRendererBase<Editor>
	{
		public override void CreatePDFLayout(XGraphics page, Editor editor, XRect bounds, double scaleFactor)
		{

			if (editor.BackgroundColor != default(Color))
				page.DrawRectangle(editor.BackgroundColor.ToXBrush(), bounds);

			//Border
			page.DrawRectangle(new XPen(Color.Gray.ToXColor(), 2 * scaleFactor), bounds);

			if (!string.IsNullOrEmpty(editor.Text))
			{
				Color textColor = editor.TextColor != default(Color) ? editor.TextColor : Color.Black;
				XFont font = new XFont(editor.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, editor.FontSize * scaleFactor);
				page.DrawString(editor.Text, font, textColor.ToXBrush(), bounds.AddMargin(5 * scaleFactor),
				new XStringFormat {
					Alignment = XStringAlignment.Near,
					LineAlignment = XLineAlignment.Near,
				});
			}
		}
	}
}
