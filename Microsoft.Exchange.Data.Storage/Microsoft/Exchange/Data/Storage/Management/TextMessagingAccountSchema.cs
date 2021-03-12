using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A89 RID: 2697
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TextMessagingAccountSchema : VersionedXmlConfigurationObjectSchema
	{
		// Token: 0x040037DE RID: 14302
		public static readonly SimpleProviderPropertyDefinition RawTextMessagingSettings = new SimpleProviderPropertyDefinition("RawTextMessagingSettings", ExchangeObjectVersion.Exchange2010, typeof(TextMessagingSettingsVersion1Point0), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037DF RID: 14303
		public static readonly SimpleProviderPropertyDefinition TextMessagingSettings = new SimpleProviderPropertyDefinition("TextMessagingSettings", ExchangeObjectVersion.Exchange2010, typeof(TextMessagingSettingsVersion1Point0), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TextMessagingAccountSchema.RawTextMessagingSettings
		}, null, new GetterDelegate(TextMessagingAccount.TextMessagingSettingsGetter), new SetterDelegate(TextMessagingAccount.TextMessagingSettingsSetter));

		// Token: 0x040037E0 RID: 14304
		public static readonly SimpleProviderPropertyDefinition CountryRegionId = new SimpleProviderPropertyDefinition("CountryRegionId", ExchangeObjectVersion.Exchange2010, typeof(RegionInfo), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TextMessagingAccountSchema.RawTextMessagingSettings
		}, null, new GetterDelegate(TextMessagingAccount.CountryRegionIdGetter), new SetterDelegate(TextMessagingAccount.CountryRegionIdSetter));

		// Token: 0x040037E1 RID: 14305
		public static readonly SimpleProviderPropertyDefinition MobileOperatorId = new SimpleProviderPropertyDefinition("MobileOperatorId", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.Calculated, -1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TextMessagingAccountSchema.RawTextMessagingSettings
		}, null, new GetterDelegate(TextMessagingAccount.MobileOperatorIdGetter), new SetterDelegate(TextMessagingAccount.MobileOperatorIdSetter));

		// Token: 0x040037E2 RID: 14306
		public static readonly SimpleProviderPropertyDefinition NotificationPhoneNumber = new SimpleProviderPropertyDefinition("NotificationPhoneNumber", ExchangeObjectVersion.Exchange2010, typeof(E164Number), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TextMessagingAccountSchema.RawTextMessagingSettings
		}, null, new GetterDelegate(TextMessagingAccount.NotificationPhoneNumberGetter), new SetterDelegate(TextMessagingAccount.NotificationPhoneNumberSetter));

		// Token: 0x040037E3 RID: 14307
		public static readonly SimpleProviderPropertyDefinition NotificationPhoneNumberVerified = new SimpleProviderPropertyDefinition("NotificationPhoneNumberVerified", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TextMessagingAccountSchema.RawTextMessagingSettings
		}, null, new GetterDelegate(TextMessagingAccount.NotificationPhoneNumberVerifiedGetter), null);

		// Token: 0x040037E4 RID: 14308
		public static readonly SimpleProviderPropertyDefinition EasEnabled = new SimpleProviderPropertyDefinition("EasEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TextMessagingAccountSchema.RawTextMessagingSettings
		}, null, new GetterDelegate(TextMessagingAccount.EasEnabledGetter), null);

		// Token: 0x040037E5 RID: 14309
		public static readonly SimpleProviderPropertyDefinition EasPhoneNumber = new SimpleProviderPropertyDefinition("EasPhoneNumber", ExchangeObjectVersion.Exchange2010, typeof(E164Number), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TextMessagingAccountSchema.RawTextMessagingSettings
		}, null, new GetterDelegate(TextMessagingAccount.EasPhoneNumberGetter), null);

		// Token: 0x040037E6 RID: 14310
		public static readonly SimpleProviderPropertyDefinition EasDeviceProtocol = new SimpleProviderPropertyDefinition("EasDeviceProtocol", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TextMessagingAccountSchema.RawTextMessagingSettings
		}, null, new GetterDelegate(TextMessagingAccount.EasDeviceProtocolGetter), null);

		// Token: 0x040037E7 RID: 14311
		public static readonly SimpleProviderPropertyDefinition EasDeviceType = new SimpleProviderPropertyDefinition("EasDeviceType", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TextMessagingAccountSchema.RawTextMessagingSettings
		}, null, new GetterDelegate(TextMessagingAccount.EasDeviceTypeGetter), null);

		// Token: 0x040037E8 RID: 14312
		public static readonly SimpleProviderPropertyDefinition EasDeviceId = new SimpleProviderPropertyDefinition("EasDeviceId", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TextMessagingAccountSchema.RawTextMessagingSettings
		}, null, new GetterDelegate(TextMessagingAccount.EasDeviceIdGetter), null);

		// Token: 0x040037E9 RID: 14313
		public static readonly SimpleProviderPropertyDefinition EasDeviceName = new SimpleProviderPropertyDefinition("EasDeviceName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TextMessagingAccountSchema.RawTextMessagingSettings
		}, null, new GetterDelegate(TextMessagingAccount.EasDeviceFriendlyNameGetter), null);
	}
}
