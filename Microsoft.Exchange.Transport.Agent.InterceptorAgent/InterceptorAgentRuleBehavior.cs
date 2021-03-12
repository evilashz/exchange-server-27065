using System;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x0200000B RID: 11
	[Flags]
	public enum InterceptorAgentRuleBehavior : ushort
	{
		// Token: 0x04000022 RID: 34
		NoOp = 0,
		// Token: 0x04000023 RID: 35
		PermanentReject = 1,
		// Token: 0x04000024 RID: 36
		TransientReject = 2,
		// Token: 0x04000025 RID: 37
		Drop = 4,
		// Token: 0x04000026 RID: 38
		Defer = 8,
		// Token: 0x04000027 RID: 39
		Delay = 16,
		// Token: 0x04000028 RID: 40
		Archive = 32,
		// Token: 0x04000029 RID: 41
		ArchiveHeaders = 64,
		// Token: 0x0400002A RID: 42
		ArchiveAndPermanentReject = 33,
		// Token: 0x0400002B RID: 43
		ArchiveAndTransientReject = 34,
		// Token: 0x0400002C RID: 44
		ArchiveAndDrop = 36,
		// Token: 0x0400002D RID: 45
		ArchiveHeadersAndDrop = 68,
		// Token: 0x0400002E RID: 46
		ArchiveHeadersAndTransientReject = 66
	}
}
