using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200005B RID: 91
	internal enum AirSyncV25FilterTypes
	{
		// Token: 0x040003A1 RID: 929
		InvalidFilter = -1,
		// Token: 0x040003A2 RID: 930
		NoFilter,
		// Token: 0x040003A3 RID: 931
		OneDayFilter,
		// Token: 0x040003A4 RID: 932
		ThreeDayFilter,
		// Token: 0x040003A5 RID: 933
		OneWeekFilter,
		// Token: 0x040003A6 RID: 934
		TwoWeekFilter,
		// Token: 0x040003A7 RID: 935
		OneMonthFilter,
		// Token: 0x040003A8 RID: 936
		ThreeMonthFilter,
		// Token: 0x040003A9 RID: 937
		SixMonthFilter,
		// Token: 0x040003AA RID: 938
		IncompleteFilter,
		// Token: 0x040003AB RID: 939
		MinValid = 0,
		// Token: 0x040003AC RID: 940
		MaxValid = 8
	}
}
