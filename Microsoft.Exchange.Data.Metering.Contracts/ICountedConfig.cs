using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000002 RID: 2
	internal interface ICountedConfig
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		bool IsPromotable { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		int MinActivityThreshold { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		TimeSpan TimeToLive { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4
		TimeSpan IdleTimeToLive { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5
		bool IsRemovable { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000006 RID: 6
		TimeSpan IdleCleanupInterval { get; }
	}
}
