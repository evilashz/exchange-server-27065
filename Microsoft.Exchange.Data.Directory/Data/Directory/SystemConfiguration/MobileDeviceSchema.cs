using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004F0 RID: 1264
	internal class MobileDeviceSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002637 RID: 9783
		public static readonly ADPropertyDefinition FriendlyName = new ADPropertyDefinition("FriendlyName", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceFriendlyName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 64)
		}, null, null);

		// Token: 0x04002638 RID: 9784
		public static readonly ADPropertyDefinition DeviceId = new ADPropertyDefinition("DeviceID", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceID", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 32)
		}, null, null);

		// Token: 0x04002639 RID: 9785
		public static readonly ADPropertyDefinition DeviceImei = new ADPropertyDefinition("DeviceImei", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceIMEI", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400263A RID: 9786
		public static readonly ADPropertyDefinition DeviceMobileOperator = new ADPropertyDefinition("DeviceMobileOperator", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceMobileOperator", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400263B RID: 9787
		public static readonly ADPropertyDefinition DeviceOS = new ADPropertyDefinition("DeviceOS", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceOS", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400263C RID: 9788
		public static readonly ADPropertyDefinition DeviceOSLanguage = new ADPropertyDefinition("DeviceOSLanguage", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceOSLanguage", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400263D RID: 9789
		public static readonly ADPropertyDefinition DeviceTelephoneNumber = new ADPropertyDefinition("DeviceTelephoneNumber", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceTelephoneNumber", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400263E RID: 9790
		public static readonly ADPropertyDefinition DeviceType = new ADPropertyDefinition("DeviceType", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceType", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400263F RID: 9791
		public static readonly ADPropertyDefinition DeviceUserAgent = new ADPropertyDefinition("DeviceUserAgent", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceUserAgent", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002640 RID: 9792
		public static readonly ADPropertyDefinition DeviceModel = new ADPropertyDefinition("DeviceModel", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceModel", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002641 RID: 9793
		public static readonly ADPropertyDefinition FirstSyncTime = new ADPropertyDefinition("FirstSyncTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), "msExchFirstSyncTime", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002642 RID: 9794
		public static readonly ADPropertyDefinition UserDisplayName = new ADPropertyDefinition("UserDisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchUserDisplayName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 64)
		}, null, null);

		// Token: 0x04002643 RID: 9795
		public static readonly ADPropertyDefinition DeviceAccessState = new ADPropertyDefinition("DeviceAccessState", ExchangeObjectVersion.Exchange2010, typeof(DeviceAccessState), "msExchDeviceAccessState", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.SystemConfiguration.DeviceAccessState.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002644 RID: 9796
		public static readonly ADPropertyDefinition DeviceAccessStateReason = new ADPropertyDefinition("DeviceAccessStateReason", ExchangeObjectVersion.Exchange2010, typeof(DeviceAccessStateReason), "msExchDeviceAccessStateReason", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.SystemConfiguration.DeviceAccessStateReason.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002645 RID: 9797
		public static readonly ADPropertyDefinition DeviceAccessControlRule = new ADPropertyDefinition("DeviceAccessControlRule", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchDeviceAccessControlRuleLink", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002646 RID: 9798
		public static readonly ADPropertyDefinition ClientVersion = new ADPropertyDefinition("DeviceActiveSyncVersion", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceEASVersion", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002647 RID: 9799
		public static readonly ADPropertyDefinition ClientType = new ADPropertyDefinition("ClientType", ExchangeObjectVersion.Exchange2010, typeof(MobileClientType), "msExchDeviceClientType", ADPropertyDefinitionFlags.None, MobileClientType.EAS, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002648 RID: 9800
		public static readonly ADPropertyDefinition ProvisioningFlags = new ADPropertyDefinition("ProvisioningFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchProvisioningFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002649 RID: 9801
		public static readonly ADPropertyDefinition IsManaged = new ADPropertyDefinition("IsManaged", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileDeviceSchema.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(2, MobileDeviceSchema.ProvisioningFlags), ADObject.FlagSetterDelegate(2, MobileDeviceSchema.ProvisioningFlags), null, null);

		// Token: 0x0400264A RID: 9802
		public static readonly ADPropertyDefinition IsCompliant = new ADPropertyDefinition("IsCompliant", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileDeviceSchema.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(4, MobileDeviceSchema.ProvisioningFlags), ADObject.FlagSetterDelegate(4, MobileDeviceSchema.ProvisioningFlags), null, null);

		// Token: 0x0400264B RID: 9803
		public static readonly ADPropertyDefinition IsDisabled = new ADPropertyDefinition("IsDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MobileDeviceSchema.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(1, MobileDeviceSchema.ProvisioningFlags), ADObject.FlagSetterDelegate(1, MobileDeviceSchema.ProvisioningFlags), null, null);
	}
}
