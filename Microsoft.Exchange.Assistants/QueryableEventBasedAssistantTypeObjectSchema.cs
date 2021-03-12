using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000A7 RID: 167
	internal class QueryableEventBasedAssistantTypeObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040002F5 RID: 757
		public static readonly SimpleProviderPropertyDefinition AssistantGuid = QueryableObjectSchema.AssistantGuid;

		// Token: 0x040002F6 RID: 758
		public static readonly SimpleProviderPropertyDefinition AssistantName = QueryableObjectSchema.AssistantName;

		// Token: 0x040002F7 RID: 759
		public static readonly SimpleProviderPropertyDefinition MailboxType = QueryableObjectSchema.MailboxType;

		// Token: 0x040002F8 RID: 760
		public static readonly SimpleProviderPropertyDefinition MapiEventType = QueryableObjectSchema.MapiEventType;

		// Token: 0x040002F9 RID: 761
		public static readonly SimpleProviderPropertyDefinition NeedMailboxSession = QueryableObjectSchema.NeedMailboxSession;
	}
}
