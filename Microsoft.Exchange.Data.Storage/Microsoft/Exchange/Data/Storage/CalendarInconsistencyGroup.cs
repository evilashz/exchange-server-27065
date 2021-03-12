using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000210 RID: 528
	internal enum CalendarInconsistencyGroup
	{
		// Token: 0x04000F3E RID: 3902
		None,
		// Token: 0x04000F3F RID: 3903
		StartTime,
		// Token: 0x04000F40 RID: 3904
		EndTime,
		// Token: 0x04000F41 RID: 3905
		Recurrence,
		// Token: 0x04000F42 RID: 3906
		Location,
		// Token: 0x04000F43 RID: 3907
		Cancellation,
		// Token: 0x04000F44 RID: 3908
		MissingItem,
		// Token: 0x04000F45 RID: 3909
		Duplicate
	}
}
