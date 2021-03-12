using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000231 RID: 561
	[Flags]
	public enum OwaRecurrenceType
	{
		// Token: 0x04000CFC RID: 3324
		None = 1,
		// Token: 0x04000CFD RID: 3325
		Daily = 2,
		// Token: 0x04000CFE RID: 3326
		Weekly = 4,
		// Token: 0x04000CFF RID: 3327
		Monthly = 8,
		// Token: 0x04000D00 RID: 3328
		Yearly = 16,
		// Token: 0x04000D01 RID: 3329
		CoreTypeMask = 255,
		// Token: 0x04000D02 RID: 3330
		DailyEveryWeekday = 256,
		// Token: 0x04000D03 RID: 3331
		MonthlyTh = 512,
		// Token: 0x04000D04 RID: 3332
		YearlyTh = 1024,
		// Token: 0x04000D05 RID: 3333
		DailyRegenerating = 2048,
		// Token: 0x04000D06 RID: 3334
		WeeklyRegenerating = 4096,
		// Token: 0x04000D07 RID: 3335
		MonthlyRegenerating = 8192,
		// Token: 0x04000D08 RID: 3336
		YearlyRegenerating = 16384,
		// Token: 0x04000D09 RID: 3337
		ValidValuesMask = 32543
	}
}
