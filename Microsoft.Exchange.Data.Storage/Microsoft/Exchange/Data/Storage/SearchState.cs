using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007C9 RID: 1993
	[Flags]
	internal enum SearchState : uint
	{
		// Token: 0x04002897 RID: 10391
		None = 0U,
		// Token: 0x04002898 RID: 10392
		Running = 1U,
		// Token: 0x04002899 RID: 10393
		Rebuild = 2U,
		// Token: 0x0400289A RID: 10394
		Recursive = 4U,
		// Token: 0x0400289B RID: 10395
		Foreground = 8U,
		// Token: 0x0400289C RID: 10396
		UseCiForComplexQueries = 16384U,
		// Token: 0x0400289D RID: 10397
		Static = 65536U,
		// Token: 0x0400289E RID: 10398
		MaybeStatic = 131072U,
		// Token: 0x0400289F RID: 10399
		ImpliedRestrictions = 262144U,
		// Token: 0x040028A0 RID: 10400
		StatisticsOnly = 524288U,
		// Token: 0x040028A1 RID: 10401
		FailNonContentIndexedSearch = 1048576U,
		// Token: 0x040028A2 RID: 10402
		Failed = 2097152U,
		// Token: 0x040028A3 RID: 10403
		EstimateCountOnly = 4194304U,
		// Token: 0x040028A4 RID: 10404
		CiTotally = 16777216U,
		// Token: 0x040028A5 RID: 10405
		CiWithTwirResidual = 33554432U,
		// Token: 0x040028A6 RID: 10406
		TwirMostly = 67108864U,
		// Token: 0x040028A7 RID: 10407
		TwirTotally = 134217728U,
		// Token: 0x040028A8 RID: 10408
		Error = 268435456U
	}
}
