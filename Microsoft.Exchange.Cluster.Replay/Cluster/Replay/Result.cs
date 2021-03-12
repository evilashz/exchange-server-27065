using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000379 RID: 889
	internal enum Result
	{
		// Token: 0x04000F45 RID: 3909
		Success,
		// Token: 0x04000F46 RID: 3910
		ShortWaitRetry,
		// Token: 0x04000F47 RID: 3911
		LongWaitRetry,
		// Token: 0x04000F48 RID: 3912
		GiveUp
	}
}
