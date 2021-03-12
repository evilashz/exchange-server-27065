using System;

namespace Microsoft.Exchange.Transport.Agent.ContentFilter
{
	// Token: 0x0200000B RID: 11
	internal enum ScanMessageResult : uint
	{
		// Token: 0x04000043 RID: 67
		Error,
		// Token: 0x04000044 RID: 68
		OK,
		// Token: 0x04000045 RID: 69
		Pending,
		// Token: 0x04000046 RID: 70
		FilterNotInitialized,
		// Token: 0x04000047 RID: 71
		UnableToProcessMessage,
		// Token: 0x04000048 RID: 72
		Timedout = 4294967294U,
		// Token: 0x04000049 RID: 73
		Shutdown
	}
}
