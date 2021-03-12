using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000023 RID: 35
	[Flags]
	internal enum EnumerateContentChangesFlags
	{
		// Token: 0x04000148 RID: 328
		None = 0,
		// Token: 0x04000149 RID: 329
		Catchup = 1,
		// Token: 0x0400014A RID: 330
		FirstPage = 2
	}
}
