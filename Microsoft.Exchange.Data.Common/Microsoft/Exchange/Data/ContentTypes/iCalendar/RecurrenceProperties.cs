using System;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000DA RID: 218
	[Flags]
	public enum RecurrenceProperties
	{
		// Token: 0x0400073E RID: 1854
		None = 0,
		// Token: 0x0400073F RID: 1855
		Frequency = 1,
		// Token: 0x04000740 RID: 1856
		UntilDate = 2,
		// Token: 0x04000741 RID: 1857
		Count = 4,
		// Token: 0x04000742 RID: 1858
		Interval = 8,
		// Token: 0x04000743 RID: 1859
		BySecond = 16,
		// Token: 0x04000744 RID: 1860
		ByMinute = 32,
		// Token: 0x04000745 RID: 1861
		ByHour = 64,
		// Token: 0x04000746 RID: 1862
		ByDay = 128,
		// Token: 0x04000747 RID: 1863
		ByMonthDay = 256,
		// Token: 0x04000748 RID: 1864
		ByYearDay = 512,
		// Token: 0x04000749 RID: 1865
		ByWeek = 1024,
		// Token: 0x0400074A RID: 1866
		ByMonth = 2048,
		// Token: 0x0400074B RID: 1867
		BySetPosition = 4096,
		// Token: 0x0400074C RID: 1868
		WeekStart = 8192,
		// Token: 0x0400074D RID: 1869
		UntilDateTime = 16384
	}
}
