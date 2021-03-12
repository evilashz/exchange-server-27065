using System;

namespace Microsoft.Exchange.Rpc.SharedCache
{
	// Token: 0x020003EF RID: 1007
	public enum ResponseCode
	{
		// Token: 0x04001016 RID: 4118
		OK,
		// Token: 0x04001017 RID: 4119
		NoOp,
		// Token: 0x04001018 RID: 4120
		KeyNotFound,
		// Token: 0x04001019 RID: 4121
		InternalServerError,
		// Token: 0x0400101A RID: 4122
		CacheGuidNotFound,
		// Token: 0x0400101B RID: 4123
		RpcError,
		// Token: 0x0400101C RID: 4124
		Timeout,
		// Token: 0x0400101D RID: 4125
		InvalidInsertTimestamp,
		// Token: 0x0400101E RID: 4126
		EntryCorrupt,
		// Token: 0x0400101F RID: 4127
		TooManyOutstandingRequests
	}
}
