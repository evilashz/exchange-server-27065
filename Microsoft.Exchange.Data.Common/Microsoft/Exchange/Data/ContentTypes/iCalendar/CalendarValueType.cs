using System;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000A5 RID: 165
	[Flags]
	public enum CalendarValueType
	{
		// Token: 0x04000592 RID: 1426
		Unknown = 1,
		// Token: 0x04000593 RID: 1427
		Binary = 2,
		// Token: 0x04000594 RID: 1428
		Boolean = 4,
		// Token: 0x04000595 RID: 1429
		CalAddress = 8,
		// Token: 0x04000596 RID: 1430
		Date = 16,
		// Token: 0x04000597 RID: 1431
		DateTime = 32,
		// Token: 0x04000598 RID: 1432
		Duration = 64,
		// Token: 0x04000599 RID: 1433
		Float = 128,
		// Token: 0x0400059A RID: 1434
		Integer = 256,
		// Token: 0x0400059B RID: 1435
		Period = 512,
		// Token: 0x0400059C RID: 1436
		Recurrence = 1024,
		// Token: 0x0400059D RID: 1437
		Text = 2048,
		// Token: 0x0400059E RID: 1438
		Time = 4096,
		// Token: 0x0400059F RID: 1439
		Uri = 8192,
		// Token: 0x040005A0 RID: 1440
		UtcOffset = 16384
	}
}
