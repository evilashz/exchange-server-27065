using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200005E RID: 94
	[Flags]
	public enum DaysOfWeek
	{
		// Token: 0x04000124 RID: 292
		[LocDescription(DataStrings.IDs.DaysOfWeek_None)]
		None = 0,
		// Token: 0x04000125 RID: 293
		[LocDescription(DataStrings.IDs.Sunday)]
		Sunday = 1,
		// Token: 0x04000126 RID: 294
		[LocDescription(DataStrings.IDs.Monday)]
		Monday = 2,
		// Token: 0x04000127 RID: 295
		[LocDescription(DataStrings.IDs.Tuesday)]
		Tuesday = 4,
		// Token: 0x04000128 RID: 296
		[LocDescription(DataStrings.IDs.Wednesday)]
		Wednesday = 8,
		// Token: 0x04000129 RID: 297
		[LocDescription(DataStrings.IDs.Thursday)]
		Thursday = 16,
		// Token: 0x0400012A RID: 298
		[LocDescription(DataStrings.IDs.Friday)]
		Friday = 32,
		// Token: 0x0400012B RID: 299
		[LocDescription(DataStrings.IDs.Saturday)]
		Saturday = 64,
		// Token: 0x0400012C RID: 300
		[LocDescription(DataStrings.IDs.Weekdays)]
		Weekdays = 62,
		// Token: 0x0400012D RID: 301
		[LocDescription(DataStrings.IDs.WeekendDays)]
		WeekendDays = 65,
		// Token: 0x0400012E RID: 302
		[LocDescription(DataStrings.IDs.AllDays)]
		AllDays = 127
	}
}
