using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000AF RID: 175
	internal class QueryableMailboxDispatcherObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000314 RID: 788
		public static readonly SimpleProviderPropertyDefinition MailboxGuid = QueryableObjectSchema.MailboxGuid;

		// Token: 0x04000315 RID: 789
		public static readonly SimpleProviderPropertyDefinition DecayedEventCounter = QueryableObjectSchema.DecayedEventCounter;

		// Token: 0x04000316 RID: 790
		public static readonly SimpleProviderPropertyDefinition NumberOfActiveDispatchers = QueryableObjectSchema.NumberOfActiveDispatchers;

		// Token: 0x04000317 RID: 791
		public static readonly SimpleProviderPropertyDefinition IsMailboxDead = QueryableObjectSchema.IsMailboxDead;

		// Token: 0x04000318 RID: 792
		public static readonly SimpleProviderPropertyDefinition IsIdle = QueryableObjectSchema.IsIdle;
	}
}
