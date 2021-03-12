using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200024F RID: 591
	[Flags]
	internal enum SetSearchCriteriaFlags : uint
	{
		// Token: 0x040011B6 RID: 4534
		None = 0U,
		// Token: 0x040011B7 RID: 4535
		Stop = 1U,
		// Token: 0x040011B8 RID: 4536
		Restart = 2U,
		// Token: 0x040011B9 RID: 4537
		Recursive = 4U,
		// Token: 0x040011BA RID: 4538
		Shallow = 8U,
		// Token: 0x040011BB RID: 4539
		Foreground = 16U,
		// Token: 0x040011BC RID: 4540
		Background = 32U,
		// Token: 0x040011BD RID: 4541
		UseCiForComplexQueries = 16384U,
		// Token: 0x040011BE RID: 4542
		ContentIndexed = 65536U,
		// Token: 0x040011BF RID: 4543
		NonContentIndexed = 131072U,
		// Token: 0x040011C0 RID: 4544
		Static = 262144U,
		// Token: 0x040011C1 RID: 4545
		FailOnForeignEID = 8388608U,
		// Token: 0x040011C2 RID: 4546
		StatisticsOnly = 16777216U,
		// Token: 0x040011C3 RID: 4547
		FailNonContentIndexedSearch = 33554432U,
		// Token: 0x040011C4 RID: 4548
		EstimateCountOnly = 67108864U
	}
}
