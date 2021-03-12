using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000A2A RID: 2602
	internal class RulePresentationObjectBaseSchema : ObjectSchema
	{
		// Token: 0x0400349D RID: 13469
		public static readonly ADPropertyDefinition Name = ADObjectSchema.Name;

		// Token: 0x0400349E RID: 13470
		public static readonly ADPropertyDefinition Guid = ADObjectSchema.Guid;

		// Token: 0x0400349F RID: 13471
		public static readonly ADPropertyDefinition DistinguishedName = ADObjectSchema.DistinguishedName;

		// Token: 0x040034A0 RID: 13472
		public static readonly ADPropertyDefinition ImmutableId = TransportRuleSchema.ImmutableId;
	}
}
