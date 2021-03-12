using System;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BFC RID: 3068
	[Flags]
	internal enum ExecutionStage
	{
		// Token: 0x04003E37 RID: 15927
		OnPromotedMessage = 1,
		// Token: 0x04003E38 RID: 15928
		OnCreatedMessage = 2,
		// Token: 0x04003E39 RID: 15929
		OnDeliveredMessage = 4,
		// Token: 0x04003E3A RID: 15930
		OnPublicFolderBefore = 8,
		// Token: 0x04003E3B RID: 15931
		OnPublicFolderAfter = 16
	}
}
