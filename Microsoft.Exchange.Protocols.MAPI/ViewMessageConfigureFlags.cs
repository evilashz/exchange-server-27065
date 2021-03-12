using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200004F RID: 79
	[Flags]
	public enum ViewMessageConfigureFlags
	{
		// Token: 0x0400013A RID: 314
		None = 0,
		// Token: 0x0400013B RID: 315
		ViewFAI = 1,
		// Token: 0x0400013C RID: 316
		NoNotifications = 2,
		// Token: 0x0400013D RID: 317
		Conversation = 8,
		// Token: 0x0400013E RID: 318
		ConversationMembers = 16,
		// Token: 0x0400013F RID: 319
		SuppressNotifications = 128,
		// Token: 0x04000140 RID: 320
		ViewAll = 256,
		// Token: 0x04000141 RID: 321
		DoNotUseLazyIndex = 512,
		// Token: 0x04000142 RID: 322
		UseCoveringIndex = 1024,
		// Token: 0x04000143 RID: 323
		EmptyTable = 2048,
		// Token: 0x04000144 RID: 324
		MailboxScopeView = 4096,
		// Token: 0x04000145 RID: 325
		DocumentIdView = 4866,
		// Token: 0x04000146 RID: 326
		ExpandedConversations = 8192,
		// Token: 0x04000147 RID: 327
		PrereadExtendedProperties = 16384,
		// Token: 0x04000148 RID: 328
		RetrieveFromIndexOnly = 32768
	}
}
