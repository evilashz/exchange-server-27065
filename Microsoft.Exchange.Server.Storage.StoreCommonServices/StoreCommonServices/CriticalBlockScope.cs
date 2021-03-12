using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200002D RID: 45
	public enum CriticalBlockScope
	{
		// Token: 0x0400020B RID: 523
		None,
		// Token: 0x0400020C RID: 524
		StoreObject,
		// Token: 0x0400020D RID: 525
		MailboxSession,
		// Token: 0x0400020E RID: 526
		MailboxShared,
		// Token: 0x0400020F RID: 527
		Database
	}
}
