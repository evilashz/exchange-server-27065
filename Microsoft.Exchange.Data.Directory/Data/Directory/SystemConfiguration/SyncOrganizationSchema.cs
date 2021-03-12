using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005B5 RID: 1461
	internal sealed class SyncOrganizationSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04002D9E RID: 11678
		public static readonly ADPropertyDefinition DisableWindowsLiveID = new ADPropertyDefinition("DisableWindowsLiveID", ExchangeObjectVersion.Exchange2007, typeof(bool), "msExchGalsyncDisableLiveIdOnRemove", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002D9F RID: 11679
		public static readonly ADPropertyDefinition FederatedIdentitySourceADAttribute = new ADPropertyDefinition("FederatedIdentitySourceADAttribute", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchGalsyncFederatedTenantSourceAttribute", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint(),
			new AsciiCharactersOnlyConstraint()
		}, null, null);

		// Token: 0x04002DA0 RID: 11680
		public static readonly ADPropertyDefinition WlidUseSMTPPrimary = new ADPropertyDefinition("WlidUseSMTPPrimary", ExchangeObjectVersion.Exchange2007, typeof(bool), "msExchGalsyncWlidUseSmtpPrimary", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DA1 RID: 11681
		public static readonly ADPropertyDefinition PasswordFilePath = new ADPropertyDefinition("PasswordFilePath", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchGalsyncPasswordFilePath", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DA2 RID: 11682
		public static readonly ADPropertyDefinition ResetPasswordOnNextLogon = new ADPropertyDefinition("ResetPasswordOnNextLogon", ExchangeObjectVersion.Exchange2007, typeof(bool), "msExchGalsyncResetPasswordOnNextLogon", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DA3 RID: 11683
		public static readonly ADPropertyDefinition ProvisioningDomain = new ADPropertyDefinition("ProvisioningDomain", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomainWithSubdomains), "msExchGalsyncProvisioningDomain", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DA4 RID: 11684
		public static readonly ADPropertyDefinition EnterpriseExchangeVersion = new ADPropertyDefinition("EnterpriseExchangeVersion", ExchangeObjectVersion.Exchange2007, typeof(EnterpriseExchangeVersionEnum), "msExchGalsyncSourceActiveDirectorySchemaVersion", ADPropertyDefinitionFlags.None, EnterpriseExchangeVersionEnum.Exchange2010, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DA5 RID: 11685
		public static readonly ADPropertyDefinition FederatedTenant = OrganizationSchema.IsFederated;
	}
}
