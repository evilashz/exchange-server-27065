using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000160 RID: 352
	internal sealed class MessageEventSourceItemSchema
	{
		// Token: 0x0400069C RID: 1692
		internal static readonly HygienePropertyDefinition SourceItemIdProperty = CommonMessageTraceSchema.SourceItemIdProperty;

		// Token: 0x0400069D RID: 1693
		internal static readonly HygienePropertyDefinition EventIdProperty = CommonMessageTraceSchema.EventIdProperty;

		// Token: 0x0400069E RID: 1694
		internal static readonly HygienePropertyDefinition NameProperty = new HygienePropertyDefinition("Name", typeof(string));
	}
}
