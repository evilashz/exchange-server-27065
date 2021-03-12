using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000A2B RID: 2603
	internal class JournalRuleObjectSchema : RulePresentationObjectBaseSchema
	{
		// Token: 0x040034A1 RID: 13473
		public static readonly ADPropertyDefinition JournalEmailAddress = new ADPropertyDefinition("JournalEmailAddress", ExchangeObjectVersion.Exchange2003, typeof(SmtpAddress), "journalEmailAddress", ADPropertyDefinitionFlags.Mandatory, SmtpAddress.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040034A2 RID: 13474
		public static ADPropertyDefinition Scope = new ADPropertyDefinition("Scope", ExchangeObjectVersion.Exchange2003, typeof(JournalRuleScope), "scope", ADPropertyDefinitionFlags.None, JournalRuleScope.Global, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040034A3 RID: 13475
		public static ADPropertyDefinition Recipient = new ADPropertyDefinition("Recipient", ExchangeObjectVersion.Exchange2003, typeof(SmtpAddress), "msExchRecipient", ADPropertyDefinitionFlags.None, SmtpAddress.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValidSmtpAddressConstraint()
		}, null, null);

		// Token: 0x040034A4 RID: 13476
		public static ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchJournalRuleEnabled", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
