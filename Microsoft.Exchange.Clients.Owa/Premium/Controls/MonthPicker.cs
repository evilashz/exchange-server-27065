using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003C4 RID: 964
	internal class MonthPicker
	{
		// Token: 0x060023FB RID: 9211 RVA: 0x000CFB68 File Offset: 0x000CDD68
		public MonthPicker(ISessionContext sessionContext, string id)
		{
			this.sessionContext = sessionContext;
			this.id = id;
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000CFB80 File Offset: 0x000CDD80
		public void Render(TextWriter writer)
		{
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			writer.Write("<div id=\"");
			writer.Write(this.id);
			writer.Write("\" class=\"monthPicker\" _cm=\"");
			writer.Write(localTime.Month);
			writer.Write("\" _cy=\"");
			writer.Write(localTime.Year);
			writer.Write("\" style=\"display:none\">");
			writer.Write("<div id=\"divMonthPickerHeader\">");
			writer.Write("<div class=\"fltBefore monthPickerButton\" id=\"divMonthPickerPrevious\">");
			this.sessionContext.RenderThemeImage(writer, this.sessionContext.IsRtl ? ThemeFileId.NextArrow : ThemeFileId.PreviousArrow);
			writer.Write("</div>");
			writer.Write("<div class=\"fltAfter monthPickerButton\" id=\"divMonthPickerNext\">");
			this.sessionContext.RenderThemeImage(writer, this.sessionContext.IsRtl ? ThemeFileId.PreviousArrow : ThemeFileId.NextArrow);
			writer.Write("</div>");
			writer.Write("<div class=\"monthPickerYear\"><span id=\"spnMpDt\">");
			writer.Write(localTime.Year);
			writer.Write("</span></div>");
			writer.Write("</div>");
			string[] abbreviatedMonthNames = this.sessionContext.UserCulture.DateTimeFormat.AbbreviatedMonthNames;
			writer.Write("<div id=\"divMonthPickerBody\">");
			for (int i = 0; i < 12; i++)
			{
				if (i % 3 == 0)
				{
					writer.Write("<div class=\"monthPickerRow\">");
				}
				writer.Write("<div class=\"monthPickerCell fltBefore\" _m=\"");
				writer.Write(i + 1);
				writer.Write("\"><span class=\"monthlyPickerNumber\">");
				writer.Write(abbreviatedMonthNames[i]);
				writer.Write("</span></div>");
				if ((i + 1) % 3 == 0)
				{
					writer.Write("<div class=\"clear\"></div></div>");
				}
			}
			writer.Write("</div>");
			writer.Write("</div>");
		}

		// Token: 0x040018F4 RID: 6388
		private const int NumberOfMonthInYear = 12;

		// Token: 0x040018F5 RID: 6389
		private const int NumberOfMonthInOneLine = 3;

		// Token: 0x040018F6 RID: 6390
		private readonly string id;

		// Token: 0x040018F7 RID: 6391
		private ISessionContext sessionContext;
	}
}
