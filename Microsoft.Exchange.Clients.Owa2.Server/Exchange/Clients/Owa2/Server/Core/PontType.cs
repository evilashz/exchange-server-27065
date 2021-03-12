using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000EE RID: 238
	[Flags]
	public enum PontType
	{
		// Token: 0x04000547 RID: 1351
		None = 0,
		// Token: 0x04000548 RID: 1352
		ExternalLink = 1,
		// Token: 0x04000549 RID: 1353
		DeleteFlaggedMessage = 2,
		// Token: 0x0400054A RID: 1354
		DeleteFlaggedContacts = 4,
		// Token: 0x0400054B RID: 1355
		DeleteFlaggedItems = 8,
		// Token: 0x0400054C RID: 1356
		DeleteOutlookDisabledRules = 16,
		// Token: 0x0400054D RID: 1357
		DisabledRulesLeft = 32,
		// Token: 0x0400054E RID: 1358
		DeleteConversation = 64,
		// Token: 0x0400054F RID: 1359
		IgnoreConversation = 128,
		// Token: 0x04000550 RID: 1360
		CancelIgnoreConversation = 256,
		// Token: 0x04000551 RID: 1361
		FilteredByUnread = 512,
		// Token: 0x04000552 RID: 1362
		DeleteAllClutter = 1024,
		// Token: 0x04000553 RID: 1363
		All = 2147483647
	}
}
