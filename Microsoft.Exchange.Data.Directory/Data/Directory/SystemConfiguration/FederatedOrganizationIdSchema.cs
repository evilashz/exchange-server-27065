using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200047A RID: 1146
	internal class FederatedOrganizationIdSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002388 RID: 9096
		public static readonly ADPropertyDefinition AccountNamespace = new ADPropertyDefinition("AccountNamespace", ExchangeObjectVersion.Exchange2010, typeof(SmtpDomain), "msExchFedAccountNamespace", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 256)
		}, null, null);

		// Token: 0x04002389 RID: 9097
		public static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchFedIsEnabled", ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400238A RID: 9098
		public static readonly ADPropertyDefinition OrganizationContact = new ADPropertyDefinition("OrganizationContact", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), "msExchFedOrgAdminContact", ADPropertyDefinitionFlags.None, SmtpAddress.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 320),
			new ValidSmtpAddressConstraint()
		}, null, null);

		// Token: 0x0400238B RID: 9099
		public static readonly ADPropertyDefinition OrganizationApprovalContact = new ADPropertyDefinition("OrganizationApprovalContact", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), "msExchFedOrgApprovalContact", ADPropertyDefinitionFlags.None, SmtpAddress.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 320),
			new ValidSmtpAddressConstraint()
		}, null, null);

		// Token: 0x0400238C RID: 9100
		public static readonly ADPropertyDefinition DelegationTrustLink = new ADPropertyDefinition("DelegationTrustLink", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), null, "msExchFedDelegationTrust", null, "msExchFedDelegationTrustSL", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400238D RID: 9101
		public static readonly ADPropertyDefinition ClientTrustLink = new ADPropertyDefinition("ClientTrustLink", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchFedClientTrust", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400238E RID: 9102
		public static readonly ADPropertyDefinition AcceptedDomainsBackLink = new ADPropertyDefinition("AcceptedDomainsBackLink", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchFedAcceptedDomainBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400238F RID: 9103
		public static readonly ADPropertyDefinition DefaultSharingPolicyLink = new ADPropertyDefinition("DefaultSharingPolicyLink", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchSharingDefaultPolicyLink", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
