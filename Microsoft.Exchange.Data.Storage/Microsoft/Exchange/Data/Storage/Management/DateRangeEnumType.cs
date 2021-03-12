using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009DC RID: 2524
	public enum DateRangeEnumType
	{
		// Token: 0x040033C4 RID: 13252
		[LocDescription(ServerStrings.IDs.DateRangeOneDay)]
		OneDay,
		// Token: 0x040033C5 RID: 13253
		[LocDescription(ServerStrings.IDs.DateRangeThreeDays)]
		ThreeDays,
		// Token: 0x040033C6 RID: 13254
		[LocDescription(ServerStrings.IDs.DateRangeOneWeek)]
		OneWeek,
		// Token: 0x040033C7 RID: 13255
		[LocDescription(ServerStrings.IDs.DateRangeOneMonth)]
		OneMonth,
		// Token: 0x040033C8 RID: 13256
		[LocDescription(ServerStrings.IDs.DateRangeThreeMonths)]
		ThreeMonths,
		// Token: 0x040033C9 RID: 13257
		[LocDescription(ServerStrings.IDs.DateRangeSixMonths)]
		SixMonths,
		// Token: 0x040033CA RID: 13258
		[LocDescription(ServerStrings.IDs.DateRangeOneYear)]
		OneYear
	}
}
