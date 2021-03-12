using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A4A RID: 2634
	internal class HygieneFilterRuleSchema : RulePresentationObjectBaseSchema
	{
		// Token: 0x040034CA RID: 13514
		public static readonly ADPropertyDefinition Priority = RuleSchema.Priority;

		// Token: 0x040034CB RID: 13515
		public static readonly ADPropertyDefinition SentTo = RuleSchema.SentTo;

		// Token: 0x040034CC RID: 13516
		public static readonly ADPropertyDefinition SentToMemberOf = RuleSchema.SentToMemberOf;

		// Token: 0x040034CD RID: 13517
		public static readonly ADPropertyDefinition RecipientDomainIs = RuleSchema.RecipientDomainIs;

		// Token: 0x040034CE RID: 13518
		public static readonly ADPropertyDefinition ExceptIfSentTo = RuleSchema.ExceptIfSentTo;

		// Token: 0x040034CF RID: 13519
		public static readonly ADPropertyDefinition ExceptIfSentToMemberOf = RuleSchema.ExceptIfSentToMemberOf;

		// Token: 0x040034D0 RID: 13520
		public static readonly ADPropertyDefinition ExceptIfRecipientDomainIs = RuleSchema.ExceptIfRecipientDomainIs;

		// Token: 0x040034D1 RID: 13521
		public static readonly ADPropertyDefinition Conditions = RuleSchema.Conditions;

		// Token: 0x040034D2 RID: 13522
		public static readonly ADPropertyDefinition Exceptions = RuleSchema.Exceptions;
	}
}
