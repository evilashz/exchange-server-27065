using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001A6 RID: 422
	internal enum ClientPerformanceEventType : byte
	{
		// Token: 0x04000403 RID: 1027
		None,
		// Token: 0x04000404 RID: 1028
		BackgroundRpcFailed,
		// Token: 0x04000405 RID: 1029
		BackgroundRpcSucceeded,
		// Token: 0x04000406 RID: 1030
		ForegroundRpcFailed,
		// Token: 0x04000407 RID: 1031
		ForegroundRpcSucceeded,
		// Token: 0x04000408 RID: 1032
		RpcAttempted,
		// Token: 0x04000409 RID: 1033
		RpcFailed,
		// Token: 0x0400040A RID: 1034
		RpcSucceeded
	}
}
