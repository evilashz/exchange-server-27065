using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000141 RID: 321
	[Flags]
	internal enum ReplayConfigChangedFlags
	{
		// Token: 0x0400053F RID: 1343
		None = 0,
		// Token: 0x04000540 RID: 1344
		ActiveServer = 1,
		// Token: 0x04000541 RID: 1345
		Other = 2
	}
}
