using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002BF RID: 703
	internal enum IncReseedOperation
	{
		// Token: 0x04000B20 RID: 2848
		IsIncrementalReseedRequiredOverall = 1,
		// Token: 0x04000B21 RID: 2849
		CheckForDivergenceAfterSeeding,
		// Token: 0x04000B22 RID: 2850
		CheckSourceDatabaseMountedFirst,
		// Token: 0x04000B23 RID: 2851
		QueryLogRangeFirst,
		// Token: 0x04000B24 RID: 2852
		PerformIncrementalReseedOverall,
		// Token: 0x04000B25 RID: 2853
		FindDivergencePoint,
		// Token: 0x04000B26 RID: 2854
		PrepareIncReseedV2Overall,
		// Token: 0x04000B27 RID: 2855
		RedirtyDatabase,
		// Token: 0x04000B28 RID: 2856
		PauseTruncation,
		// Token: 0x04000B29 RID: 2857
		GeneratePageListSinceDivergence,
		// Token: 0x04000B2A RID: 2858
		ReadDatabasePagesFromActive,
		// Token: 0x04000B2B RID: 2859
		CopyAndInspectRequiredLogFiles,
		// Token: 0x04000B2C RID: 2860
		PatchDatabaseOverall,
		// Token: 0x04000B2D RID: 2861
		ReplaceLogFiles,
		// Token: 0x04000B2E RID: 2862
		ReplaceE00LogTransacted,
		// Token: 0x04000B2F RID: 2863
		EnsureTargetDismounted,
		// Token: 0x04000B30 RID: 2864
		IsLogfileEqual,
		// Token: 0x04000B31 RID: 2865
		IsLogFileSubset
	}
}
