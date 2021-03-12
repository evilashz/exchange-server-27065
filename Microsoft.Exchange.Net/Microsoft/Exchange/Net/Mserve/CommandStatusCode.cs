using System;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x0200088A RID: 2186
	internal enum CommandStatusCode
	{
		// Token: 0x0400287E RID: 10366
		Success = 1,
		// Token: 0x0400287F RID: 10367
		NotAuthorized = 3103,
		// Token: 0x04002880 RID: 10368
		UserDoesNotExist = 3201,
		// Token: 0x04002881 RID: 10369
		UserAlreadyExists = 3215,
		// Token: 0x04002882 RID: 10370
		ConcurrentOperation = 4108,
		// Token: 0x04002883 RID: 10371
		InvalidAccountName = 4202,
		// Token: 0x04002884 RID: 10372
		MserveCacheServiceChannelFaulted = 5101
	}
}
