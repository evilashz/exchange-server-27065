using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000096 RID: 150
	[Flags]
	internal enum OlcCategoryAttributeFlags
	{
		// Token: 0x0400038B RID: 907
		None = 0,
		// Token: 0x0400038C RID: 908
		Disabled = 1,
		// Token: 0x0400038D RID: 909
		Hidden = 2,
		// Token: 0x0400038E RID: 910
		Deleted = 4,
		// Token: 0x0400038F RID: 911
		Harddeleted = 8,
		// Token: 0x04000390 RID: 912
		Reserved1 = 16,
		// Token: 0x04000391 RID: 913
		Reserved2 = 32,
		// Token: 0x04000392 RID: 914
		Reserved3 = 64,
		// Token: 0x04000393 RID: 915
		Reserved4 = 128
	}
}
