using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200042C RID: 1068
	internal sealed class TeamMailboxProvisioningPolicySchema : MailboxPolicySchema
	{
		// Token: 0x04002063 RID: 8291
		public static readonly ADPropertyDefinition IsDefault = new ADPropertyDefinition("IsDefault", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxPolicySchema.MailboxPolicyFlags
		}, new CustomFilterBuilderDelegate(TeamMailboxProvisioningPolicy.IsDefaultFilterBuilder), ADObject.FlagGetterDelegate(MailboxPolicySchema.MailboxPolicyFlags, 1), ADObject.FlagSetterDelegate(MailboxPolicySchema.MailboxPolicyFlags, 1), null, null);

		// Token: 0x04002064 RID: 8292
		public static readonly ADPropertyDefinition MaxReceiveSize = new ADPropertyDefinition("MaxReceiveSize", ExchangeObjectVersion.Exchange2010, typeof(ByteQuantifiedSize), ByteQuantifiedSize.KilobyteQuantifierProvider, "delivContLength", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromMB(36UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2097151UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002065 RID: 8293
		public static readonly ADPropertyDefinition IssueWarningQuota = new ADPropertyDefinition("IssueWarningQuota", ExchangeObjectVersion.Exchange2010, typeof(ByteQuantifiedSize), ByteQuantifiedSize.KilobyteQuantifierProvider, "mDBStorageQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromMB(4608UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002066 RID: 8294
		public static readonly ADPropertyDefinition ProhibitSendReceiveQuota = new ADPropertyDefinition("ProhibitSendReceiveQuota", ExchangeObjectVersion.Exchange2010, typeof(ByteQuantifiedSize), ByteQuantifiedSize.KilobyteQuantifierProvider, "mDBOverHardQuotaLimit", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromGB(5UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002067 RID: 8295
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x04002068 RID: 8296
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<TeamMailboxProvisioningPolicyConfigXML>(TeamMailboxProvisioningPolicySchema.ConfigurationXMLRaw);

		// Token: 0x04002069 RID: 8297
		public static readonly ADPropertyDefinition AliasPrefix = XMLSerializableBase.ConfigXmlProperty<TeamMailboxProvisioningPolicyConfigXML, string>("AliasPrefix", ExchangeObjectVersion.Exchange2003, TeamMailboxProvisioningPolicySchema.ConfigurationXML, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 8),
			new WindowsLiveIDLocalPartConstraint(true)
		}, (TeamMailboxProvisioningPolicyConfigXML configXml) => configXml.AliasPrefix, delegate(TeamMailboxProvisioningPolicyConfigXML configXml, string value)
		{
			configXml.AliasPrefix = value;
		}, null, null);

		// Token: 0x0400206A RID: 8298
		public static readonly ADPropertyDefinition DefaultAliasPrefixEnabled = XMLSerializableBase.ConfigXmlProperty<TeamMailboxProvisioningPolicyConfigXML, bool>("DefaultAliasPrefixEnabled", ExchangeObjectVersion.Exchange2003, TeamMailboxProvisioningPolicySchema.ConfigurationXML, true, (TeamMailboxProvisioningPolicyConfigXML configXml) => configXml.DefaultAliasPrefixEnabled, delegate(TeamMailboxProvisioningPolicyConfigXML configXml, bool value)
		{
			configXml.DefaultAliasPrefixEnabled = value;
		}, null, null);
	}
}
