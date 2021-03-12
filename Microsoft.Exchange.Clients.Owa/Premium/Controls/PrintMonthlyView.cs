using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003EE RID: 1006
	internal sealed class PrintMonthlyView : MonthlyViewBase, IPrintCalendarViewControl
	{
		// Token: 0x060024CF RID: 9423 RVA: 0x000D5A94 File Offset: 0x000D3C94
		public static ExDateTime[] GetEffectiveDates(CalendarAdapterBase calendarAdapter, ISessionContext sessionContext, bool workingDayOnly)
		{
			List<ExDateTime> list = new List<ExDateTime>();
			bool flag = false;
			int workingDays = PrintMonthlyView.GetWorkingDays(calendarAdapter, sessionContext);
			for (int i = 0; i < calendarAdapter.DateRanges.Length; i++)
			{
				ExDateTime start = calendarAdapter.DateRanges[i].Start;
				if (start.Day == 1)
				{
					if (flag)
					{
						break;
					}
					flag = true;
				}
				if (flag && PrintMonthlyView.ShouldRenderDay(start, workingDays, workingDayOnly))
				{
					list.Add(start);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000D5AFE File Offset: 0x000D3CFE
		private static bool ShouldRenderDay(ExDateTime day, int workDays, bool workingDayOnly)
		{
			return DateTimeUtilities.IsWorkingDay(day, workDays) || !workingDayOnly;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000D5B10 File Offset: 0x000D3D10
		public PrintMonthlyView(ISessionContext sessionContext, CalendarAdapterBase calendarAdapter, bool workingDayOnly) : base(sessionContext, calendarAdapter)
		{
			this.showWeekNumbers = sessionContext.ShowWeekNumbers;
			this.calendar = new GregorianCalendar();
			this.daysFormat = (DateTimeUtilities.GetDaysFormat(sessionContext.DateFormat) ?? "%d");
			this.firstDayFormat = "MMM %d";
			if (CalendarUtilities.FullMonthNameRequired(sessionContext.UserCulture))
			{
				this.firstDayFormat = string.Format("MMMM {0}", this.daysFormat);
			}
			this.workingDayOnly = workingDayOnly;
			this.sessionContext = sessionContext;
			this.workDays = PrintMonthlyView.GetWorkingDays(calendarAdapter, sessionContext);
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000D5BA0 File Offset: 0x000D3DA0
		public static int GetWorkingDays(CalendarAdapterBase calendarAdapter, ISessionContext sessionContext)
		{
			int result;
			if (calendarAdapter.DataSource.SharedType == SharedType.AnonymousAccess)
			{
				result = sessionContext.WorkingHours.WorkDays;
			}
			else if (calendarAdapter is CalendarAdapter)
			{
				CalendarFolder folder = ((CalendarAdapter)calendarAdapter).Folder;
				OwaStoreObjectId folderId = ((CalendarAdapter)calendarAdapter).FolderId;
				if (folder != null && Utilities.IsOtherMailbox(folder) && folderId.IsGSCalendar)
				{
					result = calendarAdapter.DataSource.WorkingHours.WorkDays;
				}
				else
				{
					result = sessionContext.WorkingHours.WorkDays;
				}
			}
			else
			{
				result = sessionContext.WorkingHours.WorkDays;
			}
			return result;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000D5C2C File Offset: 0x000D3E2C
		public void RenderView(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			Dictionary<ExDateTime, List<PrintCalendarVisual>> dictionary = new Dictionary<ExDateTime, List<PrintCalendarVisual>>();
			for (int i = 0; i < base.VisualContainer.Count; i++)
			{
				EventAreaVisual eventAreaVisual = (EventAreaVisual)base.VisualContainer[i];
				if (!base.IsItemRemoved(eventAreaVisual.DataIndex))
				{
					int num = (int)eventAreaVisual.Rect.X;
					if (num >= 0 && num < base.DateRanges.Length)
					{
						for (int j = num; j < (int)(eventAreaVisual.Rect.X + eventAreaVisual.Rect.Width); j++)
						{
							ExDateTime date = base.DateRanges[j].Start.Date;
							if (this.ShouldRenderDay(date))
							{
								if (!dictionary.ContainsKey(date))
								{
									dictionary.Add(date, new List<PrintCalendarVisual>());
								}
								dictionary[date].Add(new PrintMonthlyVisual(base.SessionContext, eventAreaVisual, base.DataSource, j == (int)eventAreaVisual.Rect.X));
							}
						}
					}
				}
			}
			writer.Write("<table>");
			this.RenderDayHeader(writer);
			bool flag = false;
			for (int k = 0; k < base.DateRanges.Length / 7; k++)
			{
				int num2 = k * 7;
				writer.Write("<tr>");
				this.RenderWeekNumber(writer, num2);
				for (int l = 0; l < 7; l++)
				{
					ExDateTime date2 = base.DateRanges[num2 + l].Start.Date;
					if (date2.Day == 1)
					{
						flag = !flag;
					}
					if (this.ShouldRenderDay(date2))
					{
						this.RenderCell(writer, date2, dictionary.ContainsKey(date2) ? dictionary[date2] : null, flag);
					}
				}
				writer.Write("</tr>");
			}
			writer.Write("</table>");
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000D5E0C File Offset: 0x000D400C
		private bool ShouldRenderDay(ExDateTime day)
		{
			return PrintMonthlyView.ShouldRenderDay(day, this.workDays, this.workingDayOnly);
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000D5E20 File Offset: 0x000D4020
		private void RenderDayHeader(TextWriter writer)
		{
			writer.Write("<tr class=\"dayHeader\">");
			writer.Write("<td class=\"");
			writer.Write(this.showWeekNumbers ? "weekNumber" : "weekNumberPlaceHolder");
			writer.Write("\"></td>");
			for (int i = 0; i < 7; i++)
			{
				ExDateTime start = base.DateRanges[i].Start;
				int dayOfWeek = (int)start.DayOfWeek;
				if (this.ShouldRenderDay(start))
				{
					writer.Write("<td class=\"weekDayName\">");
					writer.Write(base.SessionContext.UserCulture.DateTimeFormat.DayNames[dayOfWeek]);
					writer.Write("</td>");
				}
			}
			writer.Write("</tr>");
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000D5ED4 File Offset: 0x000D40D4
		private void RenderWeekNumber(TextWriter writer, int startDayIndex)
		{
			writer.Write("<td>");
			if (this.showWeekNumbers)
			{
				writer.Write(this.calendar.GetWeekOfYear((DateTime)base.DateRanges[startDayIndex].Start, base.SessionContext.FirstWeekOfYear, base.SessionContext.WeekStartDay));
			}
			else
			{
				writer.Write("&nbsp;");
			}
			writer.Write("</td>");
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000D5F48 File Offset: 0x000D4148
		private void RenderCell(TextWriter writer, ExDateTime date, IList<PrintCalendarVisual> visuals, bool inThisMonth)
		{
			writer.Write("<td");
			if (date.Equals(ExDateTime.Now.Date))
			{
				writer.Write(" class=\"today\"");
			}
			writer.Write(">");
			writer.Write("<div class=\"printVisualContainer\">");
			if (!inThisMonth)
			{
				PrintCalendarVisual.RenderBackground(writer, "bgNotInThisMonth");
			}
			writer.Write("<div class=\"monthlyViewDay\">");
			writer.Write("<div class=\"monthlyViewDayName\">");
			writer.Write(date.ToString((date.Day == 1) ? this.firstDayFormat : this.daysFormat, base.SessionContext.UserCulture));
			writer.Write("</div>");
			if (visuals != null)
			{
				foreach (PrintCalendarVisual printCalendarVisual in visuals)
				{
					printCalendarVisual.Render(writer);
				}
			}
			writer.Write("</div>");
			writer.Write("</div></td>");
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000D604C File Offset: 0x000D424C
		public ExDateTime[] GetEffectiveDates()
		{
			return PrintMonthlyView.GetEffectiveDates(base.CalendarAdapter, this.sessionContext, this.workingDayOnly);
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x000D6065 File Offset: 0x000D4265
		public string DateDescription
		{
			get
			{
				return this.monthName;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x000D606D File Offset: 0x000D426D
		public string CalendarName
		{
			get
			{
				return base.CalendarAdapter.CalendarTitle;
			}
		}

		// Token: 0x0400198B RID: 6539
		public const int HeaderHeight = 18;

		// Token: 0x0400198C RID: 6540
		private Calendar calendar;

		// Token: 0x0400198D RID: 6541
		private bool showWeekNumbers;

		// Token: 0x0400198E RID: 6542
		private string daysFormat;

		// Token: 0x0400198F RID: 6543
		private string firstDayFormat;

		// Token: 0x04001990 RID: 6544
		private bool workingDayOnly;

		// Token: 0x04001991 RID: 6545
		private int workDays;

		// Token: 0x04001992 RID: 6546
		private ISessionContext sessionContext;
	}
}
