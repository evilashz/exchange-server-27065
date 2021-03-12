using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000315 RID: 789
	internal class ActiveSyncOrganizationSettingsSchema : ADContainerSchema
	{
		// Token: 0x04001672 RID: 5746
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x04001673 RID: 5747
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<ActiveSyncOrganizationConfigXml>(ActiveSyncOrganizationSettingsSchema.ConfigurationXMLRaw);

		// Token: 0x04001674 RID: 5748
		public static readonly ADPropertyDefinition AccessLevel = new ADPropertyDefinition("AccessLevel", ExchangeObjectVersion.Exchange2010, typeof(DeviceAccessLevel), "msExchMobileAccessControl", ADPropertyDefinitionFlags.PersistDefaultValue, DeviceAccessLevel.Allow, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001675 RID: 5749
		public static readonly ADPropertyDefinition UserMailInsert = new ADPropertyDefinition("UserMailInsert", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchMobileUserMailInsert", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001676 RID: 5750
		public static readonly ADPropertyDefinition ProvisioningFlags = new ADPropertyDefinition("ProvisioningFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchProvisioningFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001677 RID: 5751
		public static readonly ADPropertyDefinition AllowAccessForUnSupportedPlatform = new ADPropertyDefinition("AllowAccessForUnSupportedPlatform", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveSyncOrganizationSettingsSchema.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(1, ActiveSyncOrganizationSettingsSchema.ProvisioningFlags), ADObject.FlagSetterDelegate(1, ActiveSyncOrganizationSettingsSchema.ProvisioningFlags), null, null);

		// Token: 0x04001678 RID: 5752
		internal static readonly ADPropertyDefinition AdminMailRecipients = new ADPropertyDefinition("AdminMailRecipients", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), "msExchMobileAdminRecipients", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001679 RID: 5753
		public static readonly ADPropertyDefinition OtaNotificationMailInsert = new ADPropertyDefinition("OTANotificationMailInsert", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchMobileOTANotificationMailInsert2", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400167A RID: 5754
		public static readonly ADPropertyDefinition DeviceFiltering = XMLSerializableBase.ConfigXmlProperty<ActiveSyncOrganizationConfigXml, ActiveSyncDeviceFilterArray>("DeviceFilters", ExchangeObjectVersion.Exchange2010, ActiveSyncOrganizationSettingsSchema.ConfigurationXML, null, (ActiveSyncOrganizationConfigXml configXml) => configXml.DeviceFiltering, delegate(ActiveSyncOrganizationConfigXml configXml, ActiveSyncDeviceFilterArray value)
		{
			configXml.DeviceFiltering = value;
		}, null, null);
	}
}
