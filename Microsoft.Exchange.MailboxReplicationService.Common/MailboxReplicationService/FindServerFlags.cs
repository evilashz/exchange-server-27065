using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000155 RID: 341
	[Flags]
	internal enum FindServerFlags
	{
		// Token: 0x040006C2 RID: 1730
		None = 0,
		// Token: 0x040006C3 RID: 1731
		ForceRediscovery = 1,
		// Token: 0x040006C4 RID: 1732
		AllowMissing = 2,
		// Token: 0x040006C5 RID: 1733
		FindSystemMailbox = 4
	}
}
