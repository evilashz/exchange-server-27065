using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003EB RID: 1003
	internal class PrintEventList : IPrintCalendarViewControl
	{
		// Token: 0x060024C1 RID: 9409 RVA: 0x000D523C File Offset: 0x000D343C
		public PrintEventList(ISessionContext sessionContext, CalendarAdapterBase calendarAdapter, CalendarViewType viewType, bool workingDayOnly)
		{
			if (sessionContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (calendarAdapter == null)
			{
				throw new ArgumentNullException("calendarAdapter");
			}
			this.calendarAdapter = calendarAdapter;
			this.sessionContext = sessionContext;
			this.workingDayOnly = workingDayOnly;
			this.viewType = viewType;
			this.visuals = new Dictionary<ExDateTime, List<PrintCalendarVisual>>();
			foreach (ExDateTime exDateTime in this.GetEffectiveDates())
			{
				this.visuals.Add(exDateTime.Date, new List<PrintCalendarVisual>());
			}
			for (int j = 0; j < calendarAdapter.DataSource.Count; j++)
			{
				ExDateTime startTime = calendarAdapter.DataSource.GetStartTime(j);
				ExDateTime endTime = calendarAdapter.DataSource.GetEndTime(j);
				ExDateTime date = startTime.Date;
				ExDateTime date2 = endTime.Date;
				ExDateTime exDateTime2 = date;
				while (exDateTime2 <= date2 && (!(exDateTime2 >= endTime) || !(startTime != endTime)))
				{
					if (this.visuals.ContainsKey(exDateTime2))
					{
						this.visuals[exDateTime2].Add(this.GetVisual(j, date.Equals(exDateTime2)));
					}
					exDateTime2 = exDateTime2.IncrementDays(1);
				}
			}
			foreach (List<PrintCalendarVisual> list in this.visuals.Values)
			{
				list.Sort((PrintCalendarVisual a, PrintCalendarVisual b) => a.StartTime.CompareTo(b.StartTime));
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000D53E8 File Offset: 0x000D35E8
		public virtual void RenderView(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table class=\"eventListTable\">");
			writer.Write("<col class=\"eventListFB\">");
			writer.Write("<col>");
			foreach (ExDateTime key in this.visuals.Keys)
			{
				writer.Write("<tr><td colspan=\"2\"><span class=\"eventListDay\">");
				writer.Write(key.ToString("D", this.sessionContext.UserCulture));
				writer.Write("</span></td></tr>");
				foreach (PrintCalendarVisual printCalendarVisual in this.visuals[key])
				{
					PrintEventListVisual printEventListVisual = (PrintEventListVisual)printCalendarVisual;
					printEventListVisual.Render(writer);
				}
				writer.Write("<tr><td>&nbsp;</td></tr>");
			}
			writer.Write("</table>");
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000D5504 File Offset: 0x000D3704
		protected virtual PrintCalendarVisual GetVisual(int index, bool isFirst)
		{
			return new PrintEventListVisual(this.sessionContext, index, this.calendarAdapter.DataSource, isFirst);
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x000D5520 File Offset: 0x000D3720
		public string DateDescription
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				DateRange[] dateRanges = this.calendarAdapter.DateRanges;
				ExDateTime start = dateRanges[0].Start;
				ExDateTime start2 = dateRanges[dateRanges.Length - 1].Start;
				if (dateRanges.Length == 1)
				{
					stringBuilder.Append(start.ToString("D", this.sessionContext.UserCulture));
				}
				else
				{
					stringBuilder.Append(start.ToString("d", this.sessionContext.UserCulture));
					stringBuilder.Append(" - ");
					stringBuilder.Append(start2.ToString("d", this.sessionContext.UserCulture));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x000D55C9 File Offset: 0x000D37C9
		public string CalendarName
		{
			get
			{
				return this.calendarAdapter.CalendarTitle;
			}
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000D55D6 File Offset: 0x000D37D6
		public ExDateTime[] GetEffectiveDates()
		{
			if (this.viewType == CalendarViewType.Monthly)
			{
				return PrintMonthlyView.GetEffectiveDates(this.calendarAdapter, this.sessionContext, this.workingDayOnly);
			}
			return PrintDailyView.GetEffectiveDates(this.calendarAdapter.DateRanges);
		}

		// Token: 0x04001981 RID: 6529
		protected ISessionContext sessionContext;

		// Token: 0x04001982 RID: 6530
		protected CalendarAdapterBase calendarAdapter;

		// Token: 0x04001983 RID: 6531
		protected Dictionary<ExDateTime, List<PrintCalendarVisual>> visuals;

		// Token: 0x04001984 RID: 6532
		protected bool workingDayOnly;

		// Token: 0x04001985 RID: 6533
		protected CalendarViewType viewType;
	}
}
