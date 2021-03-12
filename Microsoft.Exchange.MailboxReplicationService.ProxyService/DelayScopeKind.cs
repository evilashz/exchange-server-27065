using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000009 RID: 9
	internal enum DelayScopeKind
	{
		// Token: 0x04000022 RID: 34
		NoDelay,
		// Token: 0x04000023 RID: 35
		CPUOnly,
		// Token: 0x04000024 RID: 36
		DbRead,
		// Token: 0x04000025 RID: 37
		DbWrite
	}
}
