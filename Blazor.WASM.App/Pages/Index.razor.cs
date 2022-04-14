using Blazorise;
using Manufaktura.Controls.Model;
using Manufaktura.Controls.Model.Fonts;
using Manufaktura.Controls.Parser;
using Manufaktura.Controls.Rendering;
using Manufaktura.Controls.Rendering.Implementations;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

namespace Blazor.WASM.App.Pages
{
	public partial class Index : ComponentBase
	{
		private readonly MusicXmlParser musicXmlParser = new();
		private HtmlScoreRendererSettings Settings { get; set; } = new();
		private Score? Score { get; set; }
		private string? FileContent { get; set; }
		private bool RenderScore { get; set; } = false;

		private async Task OnFileChanged(FileChangedEventArgs e)
		{
			try
			{
				foreach (IFileEntry? file in e.Files)
				{
					// A stream is going to be the destination stream we're writing to.
					using MemoryStream? stream = new();
					// Here we're telling the FileEdit where to write the upload result
					await file.WriteToStreamAsync(stream);

					//var xDoc = XDocument.Load(stream);				

					//Score = musicXmlParser.Parse(XDocument.Load(stream));
					//if (Score is not null)
					//	RenderScore = true;

					// Once we reach this line it means the file is fully uploaded.
					// In this case we're going to offset to the beginning of file
					// so we can read it.
					stream.Seek(0, SeekOrigin.Begin);

					// Use the stream reader to read the content of uploaded file,
					// in this case we can assume it is a textual file.
					using StreamReader? reader = new(stream);
					FileContent = await reader.ReadToEndAsync();
					Console.WriteLine(FileContent);
				}
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
			}
			finally
			{
				StateHasChanged();
			}
		}

		protected override void OnInitialized()
		{
			string[]? musicFontUris = new[] { "/fonts/Polihymnia.svg", "/fonts/Polihymnia.ttf", "/fonts/Polihymnia.woff" };
			Settings.RenderSurface = HtmlScoreRendererSettings.HtmlRenderSurface.Svg;
			Settings.RenderingMode = ScoreRenderingModes.AllPages;
			Settings.Fonts.Add(MusicFontStyles.MusicFont, new HtmlFontInfo("Polihymnia", 22, musicFontUris));
			Settings.Fonts.Add(MusicFontStyles.StaffFont, new HtmlFontInfo("Polihymnia", 24, musicFontUris));
			Settings.Fonts.Add(MusicFontStyles.GraceNoteFont, new HtmlFontInfo("Polihymnia", 14, musicFontUris));
			Settings.Fonts.Add(MusicFontStyles.LyricsFont, new HtmlFontInfo("Open Sans", 9, "/fonts/OpenSans-Regular.ttf"));
			Settings.Fonts.Add(MusicFontStyles.TimeSignatureFont, new HtmlFontInfo("Open Sans", 12, "/fonts/OpenSans-Regular.ttf"));
			Settings.Fonts.Add(MusicFontStyles.DirectionFont, new HtmlFontInfo("Open Sans", 10, "/fonts/OpenSans-Regular.ttf"));
			Settings.Scale = 1;
			Settings.CustomElementPositionRatio = 0.8;
			Settings.IgnorePageMargins = true;

			base.OnInitialized();
		}
	}
}
