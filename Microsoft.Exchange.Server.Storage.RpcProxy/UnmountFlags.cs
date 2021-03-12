using System;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000003 RID: 3
	[Flags]
	internal enum UnmountFlags : uint
	{
		// Token: 0x04000006 RID: 6
		ForceDatabaseDeletion = 8U,
		// Token: 0x04000007 RID: 7
		SkipCacheFlush = 16U
	}
}
