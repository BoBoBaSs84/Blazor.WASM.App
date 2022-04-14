using Manufaktura.Controls.Extensions;
using Manufaktura.Controls.Model;
using Manufaktura.Controls.Model.Fonts;
using Manufaktura.Controls.Rendering;
using Manufaktura.Controls.Rendering.Implementations;
using Manufaktura.Music.Model;
using Manufaktura.Music.Model.MajorAndMinor;
using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.App.Pages
{
	public partial class Index : ComponentBase
	{
		private readonly Score score = Score.CreateOneStaffScore(Clef.Treble, MajorScale.C);
		private readonly HtmlScoreRendererSettings settings = new() { RenderSurface = HtmlScoreRendererSettings.HtmlRenderSurface.Svg };

		private void AddNote() => score.FirstStaff.Elements.Add(new Note(Pitch.G4, RhythmicDuration.Quarter));

		protected override void OnInitialized()
		{
			score.FirstStaff.AddRange(StaffBuilder
					.FromPitches(Pitch.C4, Pitch.D4, Pitch.E4, Pitch.F4, Pitch.G4, Pitch.E4)
					.AddRhythm("8 8 8 8 4 4"));
			string[]? musicFontUris = new[] { "/fonts/Polihymnia.svg", "/fonts/Polihymnia.ttf", "/fonts/Polihymnia.woff" };
			settings.RenderingMode = ScoreRenderingModes.AllPages;
			settings.Fonts.Add(MusicFontStyles.MusicFont, new HtmlFontInfo("Polihymnia", 22, musicFontUris));
			settings.Fonts.Add(MusicFontStyles.StaffFont, new HtmlFontInfo("Polihymnia", 24, musicFontUris));
			settings.Fonts.Add(MusicFontStyles.GraceNoteFont, new HtmlFontInfo("Polihymnia", 14, musicFontUris));
			settings.Fonts.Add(MusicFontStyles.LyricsFont, new HtmlFontInfo("Open Sans", 9, "/fonts/OpenSans-Regular.ttf"));
			settings.Fonts.Add(MusicFontStyles.TimeSignatureFont, new HtmlFontInfo("Open Sans", 12, "/fonts/OpenSans-Regular.ttf"));
			settings.Fonts.Add(MusicFontStyles.DirectionFont, new HtmlFontInfo("Open Sans", 10, "/fonts/OpenSans-Regular.ttf"));
			settings.Scale = 1;
			settings.CustomElementPositionRatio = 0.8;
			settings.IgnorePageMargins = true;

			base.OnInitialized();
		}
	}
}
