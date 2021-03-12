using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DCF RID: 3535
	public enum DetailLevelEnumType
	{
		// Token: 0x040053F4 RID: 21492
		[LocDescription(ServerStrings.IDs.AvailabilityOnly)]
		AvailabilityOnly = 1,
		// Token: 0x040053F5 RID: 21493
		[LocDescription(ServerStrings.IDs.LimitedDetails)]
		LimitedDetails,
		// Token: 0x040053F6 RID: 21494
		[LocDescription(ServerStrings.IDs.FullDetails)]
		FullDetails,
		// Token: 0x040053F7 RID: 21495
		[LocDescription(ServerStrings.IDs.Editor)]
		Editor
	}
}
