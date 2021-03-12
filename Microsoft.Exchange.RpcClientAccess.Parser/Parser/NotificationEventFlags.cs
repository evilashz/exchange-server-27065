using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000321 RID: 801
	[Flags]
	internal enum NotificationEventFlags : byte
	{
		// Token: 0x04000A2F RID: 2607
		None = 0,
		// Token: 0x04000A30 RID: 2608
		OstAdded = 1,
		// Token: 0x04000A31 RID: 2609
		OstRemoved = 2,
		// Token: 0x04000A32 RID: 2610
		RowFound = 4,
		// Token: 0x04000A33 RID: 2611
		Reconnect = 8
	}
}
