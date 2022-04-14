using Manufaktura.Controls.Model;
using Manufaktura.Controls.Rendering.Implementations;
using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.App.Shared
{
	public partial class NoteViewer
	{
		[Parameter] public Score? Score { get; set; }
		[Parameter] public HtmlScoreRendererSettings? Settings { get; set; }

		private int canvasIdCount = 0;

		public string RenderScore()
		{
			IScore2HtmlBuilder builder;

			if (Score is null)
				throw new ArgumentNullException(nameof(Score));

			if (Settings is null)
				throw new ArgumentNullException(nameof(Settings));

			if (Settings.RenderSurface == HtmlScoreRendererSettings.HtmlRenderSurface.Canvas)
				builder = new Score2HtmlCanvasBuilder(Score, string.Format("scoreCanvas{0}", canvasIdCount), Settings);
			else if (Settings.RenderSurface == HtmlScoreRendererSettings.HtmlRenderSurface.Svg)
				builder = new Score2HtmlSvgBuilder(Score, string.Format("scoreCanvas{0}", canvasIdCount), Settings);
			else throw new NotImplementedException("Unsupported rendering engine.");

			string html = builder.Build();

			canvasIdCount++;

			return html;
		}
	}
}
