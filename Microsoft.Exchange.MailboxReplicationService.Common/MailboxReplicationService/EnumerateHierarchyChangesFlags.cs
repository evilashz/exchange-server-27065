using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000024 RID: 36
	[Flags]
	internal enum EnumerateHierarchyChangesFlags
	{
		// Token: 0x0400014C RID: 332
		None = 0,
		// Token: 0x0400014D RID: 333
		Catchup = 1,
		// Token: 0x0400014E RID: 334
		FirstPage = 2
	}
}
