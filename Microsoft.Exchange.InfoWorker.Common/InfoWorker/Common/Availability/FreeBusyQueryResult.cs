using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000FD RID: 253
	internal sealed class FreeBusyQueryResult : BaseQueryResult
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
		public FreeBusyViewType View
		{
			get
			{
				return this.view;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001E0F0 File Offset: 0x0001C2F0
		public string MergedFreeBusy
		{
			get
			{
				return this.mergedFreeBusy;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001E0F8 File Offset: 0x0001C2F8
		public CalendarEvent[] CalendarEventArray
		{
			get
			{
				if (this.view != FreeBusyViewType.MergedOnly)
				{
					return this.calendarEventArray;
				}
				return null;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001E10B File Offset: 0x0001C30B
		public WorkingHours WorkingHours
		{
			get
			{
				return this.workingHours;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001E113 File Offset: 0x0001C313
		public string CurrentOofMessage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001E116 File Offset: 0x0001C316
		internal CalendarEvent[] CalendarEventArrayInternal
		{
			get
			{
				return this.calendarEventArray;
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001E11E File Offset: 0x0001C31E
		public FreeBusyQueryResult(FreeBusyViewType view, CalendarEvent[] calendarEventArray, string mergedFreeBusy, WorkingHours workingHours)
		{
			this.view = view;
			this.calendarEventArray = calendarEventArray;
			this.mergedFreeBusy = mergedFreeBusy;
			this.workingHours = workingHours;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001E144 File Offset: 0x0001C344
		internal string GetFreeBusyByDay(Duration timeWindow, ExTimeZone timeZone)
		{
			ExDateTime windowStart = new ExDateTime(timeZone, timeWindow.StartTime);
			ExDateTime exDateTime = new ExDateTime(timeZone, timeWindow.EndTime);
			int num = (int)(exDateTime.Date - windowStart.Date).TotalDays + 1;
			char[] array = new char[num];
			char c = (base.ExceptionInfo != null) ? '5' : '0';
			for (int i = 0; i < num; i++)
			{
				array[i] = c;
			}
			if (this.CalendarEventArray != null)
			{
				for (int j = 0; j < this.CalendarEventArray.Length; j++)
				{
					CalendarEvent calendarEvent = this.CalendarEventArray[j];
					ExDateTime exDateTime2 = new ExDateTime(timeZone, calendarEvent.StartTime.Date);
					ExDateTime t = new ExDateTime(timeZone, calendarEvent.EndTime.Date);
					while (exDateTime2 < t)
					{
						this.AddEventTime(array, windowStart, exDateTime2, calendarEvent.BusyType);
						exDateTime2 = exDateTime2.AddDays(1.0);
					}
					if (calendarEvent.EndTime.TimeOfDay != TimeSpan.Zero)
					{
						this.AddEventTime(array, windowStart, new ExDateTime(timeZone, calendarEvent.EndTime), calendarEvent.BusyType);
					}
				}
			}
			return new string(array);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001E298 File Offset: 0x0001C498
		private void AddEventTime(char[] freeBusyValues, ExDateTime windowStart, ExDateTime eventTime, BusyType busyType)
		{
			int num = (int)Math.Round((eventTime.Date - windowStart.Date).TotalDays);
			if (num >= 0 && num < freeBusyValues.Length)
			{
				char c = (char)(busyType + 48);
				if (freeBusyValues[num] < c)
				{
					freeBusyValues[num] = c;
				}
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001E2E2 File Offset: 0x0001C4E2
		public FreeBusyQueryResult(LocalizedException exception) : base(exception)
		{
			this.view = FreeBusyViewType.None;
		}

		// Token: 0x04000406 RID: 1030
		private FreeBusyViewType view;

		// Token: 0x04000407 RID: 1031
		private string mergedFreeBusy;

		// Token: 0x04000408 RID: 1032
		private CalendarEvent[] calendarEventArray;

		// Token: 0x04000409 RID: 1033
		private WorkingHours workingHours;
	}
}
