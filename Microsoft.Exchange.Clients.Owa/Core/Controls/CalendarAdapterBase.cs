using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002AC RID: 684
	internal abstract class CalendarAdapterBase : DisposeTrackableBase
	{
		// Token: 0x06001A8D RID: 6797 RVA: 0x0009A030 File Offset: 0x00098230
		public CalendarAdapterBase()
		{
			this.DateRanges = CalendarAdapterBase.ConvertDateTimeArrayToDateRangeArray(new ExDateTime[]
			{
				ExDateTime.Now
			});
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0009A067 File Offset: 0x00098267
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CalendarAdapterBase>(this);
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0009A06F File Offset: 0x0009826F
		public bool UserCanReadItem
		{
			get
			{
				return this.DataSource != null && this.DataSource.UserCanReadItem;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0009A086 File Offset: 0x00098286
		// (set) Token: 0x06001A91 RID: 6801 RVA: 0x0009A08E File Offset: 0x0009828E
		public ICalendarDataSource DataSource { get; protected set; }

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x0009A097 File Offset: 0x00098297
		// (set) Token: 0x06001A93 RID: 6803 RVA: 0x0009A09F File Offset: 0x0009829F
		public string CalendarTitle { get; protected set; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0009A0A8 File Offset: 0x000982A8
		// (set) Token: 0x06001A95 RID: 6805 RVA: 0x0009A0B0 File Offset: 0x000982B0
		public DateRange[] DateRanges { get; protected set; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0009A0B9 File Offset: 0x000982B9
		public string ClassName
		{
			get
			{
				return this.DataSource.FolderClassName;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001A97 RID: 6807
		public abstract string IdentityString { get; }

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001A98 RID: 6808
		public abstract string CalendarOwnerDisplayName { get; }

		// Token: 0x06001A99 RID: 6809 RVA: 0x0009A0C6 File Offset: 0x000982C6
		public static DateRange[] ConvertDateTimeArrayToDateRangeArray(ExDateTime[] dateTimes)
		{
			return CalendarAdapterBase.ConvertDateTimeArrayToDateRangeArray(dateTimes, 0, 24);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0009A0D4 File Offset: 0x000982D4
		public static DateRange[] ConvertDateTimeArrayToDateRangeArray(ExDateTime[] dateTimes, int startHour, int endHour)
		{
			DateRange[] array = new DateRange[dateTimes.Length];
			for (int i = 0; i < dateTimes.Length; i++)
			{
				DateTime startTime = new DateTime(dateTimes[i].Year, dateTimes[i].Month, dateTimes[i].Day, startHour, 0, 0);
				DateTime endTime = (startHour == 0 && endHour == 24) ? startTime.AddDays(1.0) : startTime.AddHours((double)(endHour - startHour));
				array[i] = new DateRange(dateTimes[i].TimeZone, startTime, endTime);
			}
			return array;
		}
	}
}
