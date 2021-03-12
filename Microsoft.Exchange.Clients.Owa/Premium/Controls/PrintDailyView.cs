using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003E9 RID: 1001
	internal sealed class PrintDailyView : DailyViewBase, IPrintCalendarViewControl
	{
		// Token: 0x060024AF RID: 9391 RVA: 0x000D4A54 File Offset: 0x000D2C54
		public static ExDateTime[] GetEffectiveDates(DateRange[] dateRanges)
		{
			ExDateTime[] array = new ExDateTime[dateRanges.Length];
			for (int i = 0; i < dateRanges.Length; i++)
			{
				array[i] = dateRanges[i].Start;
			}
			return array;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000D4A8D File Offset: 0x000D2C8D
		public PrintDailyView(ISessionContext sessionContext, CalendarAdapterBase calendarAdapter, int startTime, int endTime, bool renderNotes) : base(sessionContext, calendarAdapter)
		{
			this.startTime = startTime;
			this.endTime = endTime;
			this.renderNotes = renderNotes;
			this.printDateRange = calendarAdapter.DateRanges;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000D4ABC File Offset: 0x000D2CBC
		public void RenderView(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table>");
			writer.Write("<tr class=\"dayHeader\">");
			writer.Write("<td class=\"timeStrip\">&nbsp;</td>");
			this.RenderDayHeader(writer);
			writer.Write("</tr>");
			if (base.EventAreaRowCount > 0)
			{
				this.RenderEventArea(writer);
			}
			writer.Write("<tr><td><table class=\"dailyViewInnerTable\">");
			for (int i = this.startTime; i < this.endTime; i++)
			{
				writer.Write("<tr><td>");
				DateTime dateTime = new DateTime(2000, 1, 1, i, 0, 0);
				writer.Write(dateTime.ToString("t", base.SessionContext.UserCulture));
				writer.Write("</td></tr>");
			}
			writer.Write("</table></td>");
			for (int j = 0; j < this.printDateRange.Length; j++)
			{
				ExDateTime date = this.printDateRange[j].Start.Date;
				WorkingHours.WorkingPeriod[] workingHoursOnDay = base.DataSource.WorkingHours.GetWorkingHoursOnDay(date);
				writer.Write("<td><div class=\"printVisualContainer\">");
				writer.Write("<table class=\"dailyViewInnerTable\">");
				for (int k = this.startTime; k < this.endTime; k++)
				{
					writer.Write("<tr><td><div class=\"printVisualContainer bgContainer\">");
					ExDateTime t = new ExDateTime(date.TimeZone, date.Year, date.Month, date.Day, k, 30, 0);
					if ((workingHoursOnDay.Length > 0 && t >= workingHoursOnDay[0].Start && t <= workingHoursOnDay[0].End) || (workingHoursOnDay.Length == 2 && t >= workingHoursOnDay[1].Start && t <= workingHoursOnDay[1].End))
					{
						PrintCalendarVisual.RenderBackground(writer, "bgWorkingHour");
					}
					else
					{
						PrintCalendarVisual.RenderBackground(writer, "bgFreeHour");
					}
					writer.Write("</div>");
					if (base.SessionContext.BrowserType == BrowserType.IE)
					{
						writer.Write("&nbsp;");
					}
					writer.Write("</td></tr>");
				}
				writer.Write("</table>");
				for (int l = 0; l < base.ViewDays[j].Count; l++)
				{
					SchedulingAreaVisual schedulingAreaVisual = (SchedulingAreaVisual)base.ViewDays[j][l];
					if (!base.IsItemRemoved(schedulingAreaVisual.DataIndex))
					{
						new PrintSchedulingAreaVisual(base.SessionContext, schedulingAreaVisual, base.DataSource, this.startTime, this.endTime).Render(writer);
					}
				}
				writer.Write("</div>");
				writer.Write("</td>");
			}
			writer.Write("</tr></table>");
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000D4D68 File Offset: 0x000D2F68
		private void RenderDayHeader(TextWriter writer)
		{
			string format = DateTimeUtilities.GetDaysFormat(base.SessionContext.DateFormat) ?? "%d";
			for (int i = 0; i < this.printDateRange.Length; i++)
			{
				ExDateTime start = this.printDateRange[i].Start;
				int dayOfWeek = (int)start.DayOfWeek;
				writer.Write("<td class=\"dayHeader\"><table class=\"innerTable\">");
				writer.Write("<tr><td class=\"dayName\">");
				writer.Write(start.ToString(format, base.SessionContext.UserCulture));
				writer.Write("</td><td class=\"weekdayName\">");
				writer.Write(base.SessionContext.UserCulture.DateTimeFormat.DayNames[dayOfWeek]);
				writer.Write("</td></tr></table></td>");
			}
			if (this.renderNotes)
			{
				writer.Write("<td class=\"notes\">");
				writer.Write(SanitizedHtmlString.FromStringId(331392989));
				writer.Write("</td>");
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000D4E4C File Offset: 0x000D304C
		private void RenderEventArea(TextWriter writer)
		{
			writer.Write("<tr><td class=\"allDay\">");
			writer.Write(SanitizedHtmlString.FromStringId(1607325677));
			writer.Write("</td><td colspan=\"");
			writer.Write(this.printDateRange.Length);
			writer.Write("\" style=\"height: ");
			writer.Write(20 * base.EventAreaRowCount);
			writer.Write("px;\"><div class=\"printVisualContainer\">");
			for (int i = 0; i < base.EventArea.Count; i++)
			{
				EventAreaVisual eventAreaVisual = (EventAreaVisual)base.EventArea[i];
				if (!base.IsItemRemoved(eventAreaVisual.DataIndex))
				{
					new PrintEventAreaVisual(base.SessionContext, eventAreaVisual, base.DataSource, this.printDateRange.Length).Render(writer);
				}
			}
			writer.Write("</div></td></tr>");
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000D4F13 File Offset: 0x000D3113
		public ExDateTime[] GetEffectiveDates()
		{
			return PrintDailyView.GetEffectiveDates(this.printDateRange);
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x000D4F20 File Offset: 0x000D3120
		public override int MaxEventAreaRows
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x000D4F24 File Offset: 0x000D3124
		public override int MaxItemsPerView
		{
			get
			{
				return 300;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x000D4F2B File Offset: 0x000D312B
		public override int MaxConflictingItems
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x000D4F2F File Offset: 0x000D312F
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x000D4F37 File Offset: 0x000D3137
		public OwaStoreObjectId SelectedItemId { get; set; }

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x000D4F40 File Offset: 0x000D3140
		public int Count
		{
			get
			{
				return base.VisualCount;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x000D4F48 File Offset: 0x000D3148
		public string DateDescription
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				ExDateTime start = this.printDateRange[0].Start;
				ExDateTime start2 = this.printDateRange[this.printDateRange.Length - 1].Start;
				if (this.printDateRange.Length == 1)
				{
					stringBuilder.Append(start.ToString("D", base.SessionContext.UserCulture));
				}
				else
				{
					stringBuilder.Append(start.ToString("d", base.SessionContext.UserCulture));
					stringBuilder.Append(" - ");
					stringBuilder.Append(start2.ToString("d", base.SessionContext.UserCulture));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x000D4FF9 File Offset: 0x000D31F9
		public string CalendarName
		{
			get
			{
				return base.CalendarAdapter.CalendarTitle;
			}
		}

		// Token: 0x04001979 RID: 6521
		private int startTime;

		// Token: 0x0400197A RID: 6522
		private int endTime;

		// Token: 0x0400197B RID: 6523
		private bool renderNotes;

		// Token: 0x0400197C RID: 6524
		private DateRange[] printDateRange;
	}
}
