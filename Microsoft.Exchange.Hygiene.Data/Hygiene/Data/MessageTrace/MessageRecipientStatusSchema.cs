using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000168 RID: 360
	internal sealed class MessageRecipientStatusSchema
	{
		// Token: 0x040006B1 RID: 1713
		internal static readonly HygienePropertyDefinition RecipientStatusIdProperty = new HygienePropertyDefinition("RecipientStatusId", typeof(Guid));

		// Token: 0x040006B2 RID: 1714
		internal static readonly HygienePropertyDefinition RecipientIdProperty = CommonMessageTraceSchema.RecipientIdProperty;

		// Token: 0x040006B3 RID: 1715
		internal static readonly HygienePropertyDefinition EventIdProperty = CommonMessageTraceSchema.EventIdProperty;

		// Token: 0x040006B4 RID: 1716
		internal static readonly HygienePropertyDefinition StatusProperty = new HygienePropertyDefinition("Status", typeof(string));

		// Token: 0x040006B5 RID: 1717
		internal static readonly HygienePropertyDefinition ReferenceProperty = new HygienePropertyDefinition("Reference", typeof(string));
	}
}
