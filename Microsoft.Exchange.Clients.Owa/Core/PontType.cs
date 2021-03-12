using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000210 RID: 528
	[Flags]
	public enum PontType
	{
		// Token: 0x04000C17 RID: 3095
		None = 0,
		// Token: 0x04000C18 RID: 3096
		ExternalLink = 1,
		// Token: 0x04000C19 RID: 3097
		DeleteFlaggedMessage = 2,
		// Token: 0x04000C1A RID: 3098
		DeleteFlaggedContacts = 4,
		// Token: 0x04000C1B RID: 3099
		DeleteFlaggedItems = 8,
		// Token: 0x04000C1C RID: 3100
		DeleteOutlookDisabledRules = 16,
		// Token: 0x04000C1D RID: 3101
		DisabledRulesLeft = 32,
		// Token: 0x04000C1E RID: 3102
		DeleteConversation = 64,
		// Token: 0x04000C1F RID: 3103
		IgnoreConversation = 128,
		// Token: 0x04000C20 RID: 3104
		CancelIgnoreConversation = 256,
		// Token: 0x04000C21 RID: 3105
		FilteredByUnread = 512,
		// Token: 0x04000C22 RID: 3106
		All = 2147483647
	}
}
