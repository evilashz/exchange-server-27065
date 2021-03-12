using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002DA RID: 730
	[Flags]
	internal enum SearchState : uint
	{
		// Token: 0x0400084F RID: 2127
		None = 0U,
		// Token: 0x04000850 RID: 2128
		Running = 1U,
		// Token: 0x04000851 RID: 2129
		Rebuild = 2U,
		// Token: 0x04000852 RID: 2130
		Recursive = 4U,
		// Token: 0x04000853 RID: 2131
		Foreground = 16U,
		// Token: 0x04000854 RID: 2132
		AccurateResults = 4096U,
		// Token: 0x04000855 RID: 2133
		PotentiallyInaccurateResults = 8192U,
		// Token: 0x04000856 RID: 2134
		Static = 65536U,
		// Token: 0x04000857 RID: 2135
		InstantSearch = 131072U,
		// Token: 0x04000858 RID: 2136
		StatisticsOnly = 524288U,
		// Token: 0x04000859 RID: 2137
		CiOnly = 1048576U,
		// Token: 0x0400085A RID: 2138
		FullTextIndexQueryFailed = 2097152U,
		// Token: 0x0400085B RID: 2139
		Failed = 2097152U,
		// Token: 0x0400085C RID: 2140
		EstimateCountOnly = 4194304U,
		// Token: 0x0400085D RID: 2141
		CiTotally = 16777216U,
		// Token: 0x0400085E RID: 2142
		CiWithTwirResidual = 33554432U,
		// Token: 0x0400085F RID: 2143
		TwirMostly = 67108864U,
		// Token: 0x04000860 RID: 2144
		TwirTotally = 134217728U,
		// Token: 0x04000861 RID: 2145
		Error = 268435456U
	}
}
