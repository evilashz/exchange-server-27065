using System;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000EC6 RID: 3782
	[Flags]
	public enum DaysOfWeek
	{
		// Token: 0x0400579A RID: 22426
		None = 0,
		// Token: 0x0400579B RID: 22427
		Sunday = 1,
		// Token: 0x0400579C RID: 22428
		Monday = 2,
		// Token: 0x0400579D RID: 22429
		Tuesday = 4,
		// Token: 0x0400579E RID: 22430
		Wednesday = 8,
		// Token: 0x0400579F RID: 22431
		Thursday = 16,
		// Token: 0x040057A0 RID: 22432
		Friday = 32,
		// Token: 0x040057A1 RID: 22433
		Saturday = 64,
		// Token: 0x040057A2 RID: 22434
		Weekdays = 62,
		// Token: 0x040057A3 RID: 22435
		WeekendDays = 65,
		// Token: 0x040057A4 RID: 22436
		AllDays = 127
	}
}
