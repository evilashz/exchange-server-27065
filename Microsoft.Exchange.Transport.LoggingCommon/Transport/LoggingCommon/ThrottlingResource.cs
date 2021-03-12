using System;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x0200000E RID: 14
	internal enum ThrottlingResource
	{
		// Token: 0x04000080 RID: 128
		SubmissionQueue,
		// Token: 0x04000081 RID: 129
		SubmissionQueueQuota,
		// Token: 0x04000082 RID: 130
		TotalQueueQuota,
		// Token: 0x04000083 RID: 131
		FreeDiskSpace,
		// Token: 0x04000084 RID: 132
		Memory,
		// Token: 0x04000085 RID: 133
		VersionBuckets,
		// Token: 0x04000086 RID: 134
		SmtpIn,
		// Token: 0x04000087 RID: 135
		MaxLinesReached,
		// Token: 0x04000088 RID: 136
		Threads,
		// Token: 0x04000089 RID: 137
		Threads_MaxPerHub,
		// Token: 0x0400008A RID: 138
		Threads_PendingConnectionTimedOut
	}
}
