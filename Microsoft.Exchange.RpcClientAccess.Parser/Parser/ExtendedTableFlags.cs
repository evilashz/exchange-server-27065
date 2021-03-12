using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000096 RID: 150
	[Flags]
	internal enum ExtendedTableFlags
	{
		// Token: 0x04000241 RID: 577
		None = 0,
		// Token: 0x04000242 RID: 578
		RetrieveFromIndex = 1,
		// Token: 0x04000243 RID: 579
		Associated = 2,
		// Token: 0x04000244 RID: 580
		AclTableFreeBusy = 2,
		// Token: 0x04000245 RID: 581
		Depth = 4,
		// Token: 0x04000246 RID: 582
		ConversationView = 4,
		// Token: 0x04000247 RID: 583
		DeferredErrors = 8,
		// Token: 0x04000248 RID: 584
		NoNotifications = 16,
		// Token: 0x04000249 RID: 585
		SoftDeletes = 32,
		// Token: 0x0400024A RID: 586
		MapiUnicode = 64,
		// Token: 0x0400024B RID: 587
		SuppressNotifications = 128,
		// Token: 0x0400024C RID: 588
		ConversationViewMembers = 128,
		// Token: 0x0400024D RID: 589
		DocumentIdView = 256,
		// Token: 0x0400024E RID: 590
		ExpandedConversations = 512,
		// Token: 0x0400024F RID: 591
		PrereadExtendedProperties = 1024,
		// Token: 0x04000250 RID: 592
		MoreTableFlags = -2147483648
	}
}
