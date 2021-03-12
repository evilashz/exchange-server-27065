using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000252 RID: 594
	internal class LoadBalancingMiniRecipientSchema : MiniRecipientSchema
	{
		// Token: 0x04000DCF RID: 3535
		internal static readonly ADPropertyDefinition MailboxMoveStatus = ADUserSchema.MailboxMoveStatus;

		// Token: 0x04000DD0 RID: 3536
		internal static readonly ADPropertyDefinition MailboxMoveFlags = ADUserSchema.MailboxMoveFlags;

		// Token: 0x04000DD1 RID: 3537
		internal static readonly ADPropertyDefinition MailboxMoveBatchName = ADUserSchema.MailboxMoveBatchName;

		// Token: 0x04000DD2 RID: 3538
		internal static readonly ADPropertyDefinition[] LoadBalancingProperties = new ADPropertyDefinition[]
		{
			MiniRecipientSchema.ConfigurationXML,
			LoadBalancingMiniRecipientSchema.MailboxMoveStatus,
			LoadBalancingMiniRecipientSchema.MailboxMoveFlags,
			LoadBalancingMiniRecipientSchema.MailboxMoveBatchName
		};
	}
}
