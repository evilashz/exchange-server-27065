using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000095 RID: 149
	[Flags]
	internal enum TableFlags : byte
	{
		// Token: 0x04000234 RID: 564
		None = 0,
		// Token: 0x04000235 RID: 565
		RetrieveFromIndex = 1,
		// Token: 0x04000236 RID: 566
		Associated = 2,
		// Token: 0x04000237 RID: 567
		AclTableFreeBusy = 2,
		// Token: 0x04000238 RID: 568
		Depth = 4,
		// Token: 0x04000239 RID: 569
		ConversationView = 4,
		// Token: 0x0400023A RID: 570
		DeferredErrors = 8,
		// Token: 0x0400023B RID: 571
		NoNotifications = 16,
		// Token: 0x0400023C RID: 572
		SoftDeletes = 32,
		// Token: 0x0400023D RID: 573
		MapiUnicode = 64,
		// Token: 0x0400023E RID: 574
		SuppressNotifications = 128,
		// Token: 0x0400023F RID: 575
		ConversationViewMembers = 128
	}
}
