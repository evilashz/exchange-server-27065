using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001C1 RID: 449
	[Flags]
	internal enum SearchCriteriaFlags
	{
		// Token: 0x040005DD RID: 1501
		None = 0,
		// Token: 0x040005DE RID: 1502
		Stop = 1,
		// Token: 0x040005DF RID: 1503
		Restart = 2,
		// Token: 0x040005E0 RID: 1504
		Recursive = 4,
		// Token: 0x040005E1 RID: 1505
		Shallow = 8,
		// Token: 0x040005E2 RID: 1506
		Foreground = 16,
		// Token: 0x040005E3 RID: 1507
		Background = 32,
		// Token: 0x040005E4 RID: 1508
		UseCiForComplexQueries = 16384,
		// Token: 0x040005E5 RID: 1509
		ContentIndexed = 65536,
		// Token: 0x040005E6 RID: 1510
		NonContentIndexed = 131072,
		// Token: 0x040005E7 RID: 1511
		Static = 262144,
		// Token: 0x040005E8 RID: 1512
		ImpliedRestrictions = 524288,
		// Token: 0x040005E9 RID: 1513
		FailOnForeignEID = 8388608,
		// Token: 0x040005EA RID: 1514
		StatisticsOnly = 16777216,
		// Token: 0x040005EB RID: 1515
		ContentIndexedOnly = 33554432,
		// Token: 0x040005EC RID: 1516
		EstimateCountOnly = 67108864
	}
}
