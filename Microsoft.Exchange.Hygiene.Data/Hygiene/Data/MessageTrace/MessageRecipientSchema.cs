using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000165 RID: 357
	internal class MessageRecipientSchema
	{
		// Token: 0x040006A9 RID: 1705
		internal static readonly HygienePropertyDefinition ExMessageIdProperty = CommonMessageTraceSchema.ExMessageIdProperty;

		// Token: 0x040006AA RID: 1706
		internal static readonly HygienePropertyDefinition RecipientIdProperty = CommonMessageTraceSchema.RecipientIdProperty;

		// Token: 0x040006AB RID: 1707
		internal static readonly HygienePropertyDefinition ToEmailPrefixProperty = new HygienePropertyDefinition("ToEmailPrefix", typeof(string));

		// Token: 0x040006AC RID: 1708
		internal static readonly HygienePropertyDefinition ToEmailDomainProperty = new HygienePropertyDefinition("ToEmailDomain", typeof(string));

		// Token: 0x040006AD RID: 1709
		internal static readonly HygienePropertyDefinition MailDeliveryStatusProperty = new HygienePropertyDefinition("MailDeliveryStatus", typeof(MailDeliveryStatus));
	}
}
