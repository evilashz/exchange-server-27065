using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003EF RID: 1007
	internal class PrintMonthlyVisual : PrintCalendarVisual
	{
		// Token: 0x060024DB RID: 9435 RVA: 0x000D607A File Offset: 0x000D427A
		public PrintMonthlyVisual(ISessionContext sessionContext, EventAreaVisual visual, ICalendarDataSource dataSource, bool isFirst) : base(sessionContext, visual, dataSource)
		{
			this.isFirst = isFirst;
			this.leftBreak = visual.LeftBreak;
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x000D609C File Offset: 0x000D429C
		protected override string TimeDescription
		{
			get
			{
				string text = string.Empty;
				if (this.isFirst)
				{
					if (this.leftBreak)
					{
						text = base.StartTime.ToString(base.SessionContext.DateFormat);
					}
					if (base.StartTime.Minute != 0 || base.StartTime.Hour != 0)
					{
						if (!string.IsNullOrEmpty(text))
						{
							text += " ";
						}
						text += base.StartTime.ToString(base.SessionContext.TimeFormat);
					}
				}
				return text;
			}
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000D6130 File Offset: 0x000D4330
		public override void Render(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<div class=\"monthlyVisual ");
			writer.Write(base.CssClass);
			writer.Write("\">");
			writer.Write("<table><tr>");
			if (base.BusyType != BusyTypeWrapper.Busy)
			{
				writer.Write("<td class=\"freeBusy\">");
				base.RenderFreeBusy(writer, false);
				writer.Write("</td>");
			}
			writer.Write("<td class=\"monthlyViewTextContainer\">");
			writer.Write("<div class=\"noFixHeightBGContainer\">");
			base.RenderBackground(writer);
			writer.Write("</div>");
			base.RenderVisualContent(writer);
			writer.Write("</td></tr></table>");
			writer.Write("</div>");
		}

		// Token: 0x04001993 RID: 6547
		public const int RowHeight = 19;

		// Token: 0x04001994 RID: 6548
		private bool isFirst;

		// Token: 0x04001995 RID: 6549
		private bool leftBreak;
	}
}
