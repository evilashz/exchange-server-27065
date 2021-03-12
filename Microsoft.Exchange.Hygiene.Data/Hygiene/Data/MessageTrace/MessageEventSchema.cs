using System;
using System.Data.SqlTypes;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200015D RID: 349
	internal class MessageEventSchema
	{
		// Token: 0x04000694 RID: 1684
		internal static readonly HygienePropertyDefinition ExMessageIdProperty = CommonMessageTraceSchema.ExMessageIdProperty;

		// Token: 0x04000695 RID: 1685
		internal static readonly HygienePropertyDefinition EventIdProperty = CommonMessageTraceSchema.EventIdProperty;

		// Token: 0x04000696 RID: 1686
		internal static readonly HygienePropertyDefinition EventTypeProperty = new HygienePropertyDefinition("EventType", typeof(MessageTrackingEvent));

		// Token: 0x04000697 RID: 1687
		internal static readonly HygienePropertyDefinition EventSourceProperty = new HygienePropertyDefinition("EventSource", typeof(MessageTrackingSource));

		// Token: 0x04000698 RID: 1688
		internal static readonly HygienePropertyDefinition TimeStampProperty = new HygienePropertyDefinition("TimeStamp", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
