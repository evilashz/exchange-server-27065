using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200008C RID: 140
	[Flags]
	internal enum SetSearchCriteriaFlags : uint
	{
		// Token: 0x040001EC RID: 492
		None = 0U,
		// Token: 0x040001ED RID: 493
		Stop = 1U,
		// Token: 0x040001EE RID: 494
		Restart = 2U,
		// Token: 0x040001EF RID: 495
		Recursive = 4U,
		// Token: 0x040001F0 RID: 496
		Shallow = 8U,
		// Token: 0x040001F1 RID: 497
		Foreground = 16U,
		// Token: 0x040001F2 RID: 498
		Background = 32U,
		// Token: 0x040001F3 RID: 499
		UseCIForComplexQueries = 16384U,
		// Token: 0x040001F4 RID: 500
		ContentIndexed = 65536U,
		// Token: 0x040001F5 RID: 501
		NonContentIndexed = 131072U,
		// Token: 0x040001F6 RID: 502
		Static = 262144U,
		// Token: 0x040001F7 RID: 503
		FailOnForeignEID = 8388608U,
		// Token: 0x040001F8 RID: 504
		StatisticsOnly = 16777216U,
		// Token: 0x040001F9 RID: 505
		FailNonContentIndexedSearch = 33554432U,
		// Token: 0x040001FA RID: 506
		EstimateCountOnly = 67108864U
	}
}
