using System;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020000E9 RID: 233
	[Flags]
	internal enum RpcRetryType : uint
	{
		// Token: 0x0400090E RID: 2318
		None = 0U,
		// Token: 0x0400090F RID: 2319
		CallCancelled = 1U,
		// Token: 0x04000910 RID: 2320
		ServerBusy = 2U,
		// Token: 0x04000911 RID: 2321
		ServerUnavailable = 4U,
		// Token: 0x04000912 RID: 2322
		AccessDenied = 8U
	}
}
