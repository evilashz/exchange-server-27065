using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200018A RID: 394
	internal enum SessionSetupFailureReason
	{
		// Token: 0x0400092F RID: 2351
		None,
		// Token: 0x04000930 RID: 2352
		UserLookupFailure,
		// Token: 0x04000931 RID: 2353
		DnsLookupFailure,
		// Token: 0x04000932 RID: 2354
		ConnectionFailure,
		// Token: 0x04000933 RID: 2355
		ProtocolError,
		// Token: 0x04000934 RID: 2356
		SocketError,
		// Token: 0x04000935 RID: 2357
		Shutdown,
		// Token: 0x04000936 RID: 2358
		BackEndLocatorFailure
	}
}
