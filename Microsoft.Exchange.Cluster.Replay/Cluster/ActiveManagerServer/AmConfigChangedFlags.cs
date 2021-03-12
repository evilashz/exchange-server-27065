using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200001E RID: 30
	[Flags]
	internal enum AmConfigChangedFlags
	{
		// Token: 0x04000072 RID: 114
		None = 0,
		// Token: 0x04000073 RID: 115
		Role = 1,
		// Token: 0x04000074 RID: 116
		DbState = 2,
		// Token: 0x04000075 RID: 117
		LastError = 4,
		// Token: 0x04000076 RID: 118
		DagConfig = 8,
		// Token: 0x04000077 RID: 119
		DagId = 16,
		// Token: 0x04000078 RID: 120
		MemberServers = 32,
		// Token: 0x04000079 RID: 121
		CurrentPAM = 64,
		// Token: 0x0400007A RID: 122
		Cluster = 128,
		// Token: 0x0400007B RID: 123
		CoreGroup = 256
	}
}
