using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020002AC RID: 684
	internal enum InstallState
	{
		// Token: 0x04000A87 RID: 2695
		NotUsed = -7,
		// Token: 0x04000A88 RID: 2696
		BadConfig,
		// Token: 0x04000A89 RID: 2697
		Incomplete,
		// Token: 0x04000A8A RID: 2698
		SourceAbsent,
		// Token: 0x04000A8B RID: 2699
		MoreData,
		// Token: 0x04000A8C RID: 2700
		InvalidArg,
		// Token: 0x04000A8D RID: 2701
		Unknown,
		// Token: 0x04000A8E RID: 2702
		Broken,
		// Token: 0x04000A8F RID: 2703
		Advertised,
		// Token: 0x04000A90 RID: 2704
		Removed = 1,
		// Token: 0x04000A91 RID: 2705
		Absent,
		// Token: 0x04000A92 RID: 2706
		Local,
		// Token: 0x04000A93 RID: 2707
		Source,
		// Token: 0x04000A94 RID: 2708
		Default
	}
}
