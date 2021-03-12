using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000230 RID: 560
	internal enum RecurrenceType
	{
		// Token: 0x040010AD RID: 4269
		None,
		// Token: 0x040010AE RID: 4270
		Daily,
		// Token: 0x040010AF RID: 4271
		Weekly,
		// Token: 0x040010B0 RID: 4272
		Monthly,
		// Token: 0x040010B1 RID: 4273
		Yearly,
		// Token: 0x040010B2 RID: 4274
		DailyRegenerating = 100,
		// Token: 0x040010B3 RID: 4275
		WeeklyRegenerating,
		// Token: 0x040010B4 RID: 4276
		MonthlyRegenerating,
		// Token: 0x040010B5 RID: 4277
		YearlyRegenerating
	}
}
