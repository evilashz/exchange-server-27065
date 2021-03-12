using System;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000795 RID: 1941
	internal class RecipientTemplateProvisioningPolicySchema : TemplateProvisioningPolicySchema
	{
		// Token: 0x040040D9 RID: 16601
		public static readonly ADPropertyDefinition DefaultMaxSendSize = ProvisioningHelper.FromProvisionedADProperty(MailEnabledRecipientSchema.MaxSendSize, "DefaultMaxSendSize", "msExchRecipientMaxSendSize");

		// Token: 0x040040DA RID: 16602
		public static readonly ADPropertyDefinition DefaultMaxReceiveSize = ProvisioningHelper.FromProvisionedADProperty(MailEnabledRecipientSchema.MaxReceiveSize, "DefaultMaxReceiveSize", "msExchRecipientMaxReceiveSize");

		// Token: 0x040040DB RID: 16603
		public static readonly ADPropertyDefinition DefaultProhibitSendQuota = ProvisioningHelper.FromProvisionedADProperty(MailboxSchema.ProhibitSendQuota, "DefaultProhibitSendQuota", "msExchRecipientProhibitSendQuota");

		// Token: 0x040040DC RID: 16604
		public static readonly ADPropertyDefinition DefaultProhibitSendReceiveQuota = ProvisioningHelper.FromProvisionedADProperty(MailboxSchema.ProhibitSendReceiveQuota, "DefaultProhibitSendReceiveQuota", "msExchRecipientProhibitSendReceiveQuota");

		// Token: 0x040040DD RID: 16605
		public static readonly ADPropertyDefinition DefaultIssueWarningQuota = ProvisioningHelper.FromProvisionedADProperty(MailboxSchema.IssueWarningQuota, "DefaultIssueWarningQuota", "msExchRecipientIssueWarningQuota");

		// Token: 0x040040DE RID: 16606
		public static readonly ADPropertyDefinition DefaultRulesQuota = ProvisioningHelper.FromProvisionedADProperty(MailboxSchema.RulesQuota, "DefaultRulesQuota", "msExchRecipientRulesQuota");

		// Token: 0x040040DF RID: 16607
		public static readonly ADPropertyDefinition DefaultDistributionListOU = ProvisioningHelper.FromProvisionedADProperty(ADRecipientSchema.DefaultDistributionListOU, "DefaultDistributionListOU", "msExchDistributionListOU");
	}
}
