using System;
using System.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200053C RID: 1340
	internal class PerimeterConfigSchema : ADContainerSchema
	{
		// Token: 0x040028AE RID: 10414
		internal const int SafelistingUIModeShift = 7;

		// Token: 0x040028AF RID: 10415
		internal const int SafelistingUIModeLength = 2;

		// Token: 0x040028B0 RID: 10416
		public static readonly ADPropertyDefinition GatewayIPAddresses = new ADPropertyDefinition("GatewayIPAddresses", ExchangeObjectVersion.Exchange2007, typeof(IPAddress), "msExchTenantPerimeterSettingsGatewayIPAddresses", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040028B1 RID: 10417
		public static readonly ADPropertyDefinition InternalServerIPAddresses = new ADPropertyDefinition("InternalServerIPAddresses", ExchangeObjectVersion.Exchange2007, typeof(IPAddress), "msExchTenantPerimeterSettingsInternalServerIPAddresses", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040028B2 RID: 10418
		public static readonly ADPropertyDefinition PerimeterOrgId = new ADPropertyDefinition("PerimeterOrgId", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchTenantPerimeterSettingsOrgID", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new Int32ParsableNullableEmptiableStringConstraint()
		}, null, null);

		// Token: 0x040028B3 RID: 10419
		public static readonly ADPropertyDefinition Flags = new ADPropertyDefinition("PerimeterFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTenantPerimeterSettingsFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040028B4 RID: 10420
		public static readonly ADPropertyDefinition SyncToHotmailEnabled = new ADPropertyDefinition("SyncToHotmailEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PerimeterConfigSchema.Flags
		}, null, ADObject.FlagGetterDelegate(PerimeterConfigSchema.Flags, 1), ADObject.FlagSetterDelegate(PerimeterConfigSchema.Flags, 1), null, null);

		// Token: 0x040028B5 RID: 10421
		public static readonly ADPropertyDefinition RouteOutboundViaEhfEnabled = new ADPropertyDefinition("RouteOutboundViaEhfEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PerimeterConfigSchema.Flags
		}, null, ADObject.FlagGetterDelegate(PerimeterConfigSchema.Flags, 2), ADObject.FlagSetterDelegate(PerimeterConfigSchema.Flags, 2), null, null);

		// Token: 0x040028B6 RID: 10422
		public static readonly ADPropertyDefinition IPSkiplistingEnabled = new ADPropertyDefinition("IPSkiplistingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PerimeterConfigSchema.Flags
		}, null, ADObject.FlagGetterDelegate(PerimeterConfigSchema.Flags, 4), ADObject.FlagSetterDelegate(PerimeterConfigSchema.Flags, 4), null, null);

		// Token: 0x040028B7 RID: 10423
		public static readonly ADPropertyDefinition RouteOutboundViaFfoFrontendEnabled = new ADPropertyDefinition("RouteOutboundViaFfoFrontendEnabled ", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PerimeterConfigSchema.Flags
		}, null, ADObject.FlagGetterDelegate(PerimeterConfigSchema.Flags, 512), ADObject.FlagSetterDelegate(PerimeterConfigSchema.Flags, 512), null, null);

		// Token: 0x040028B8 RID: 10424
		public static readonly ADPropertyDefinition EhfConfigSyncEnabled = new ADPropertyDefinition("EhfConfigSyncEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PerimeterConfigSchema.Flags
		}, null, ADObject.InvertFlagGetterDelegate(PerimeterConfigSchema.Flags, 16), ADObject.InvertFlagSetterDelegate(PerimeterConfigSchema.Flags, 16), null, null);

		// Token: 0x040028B9 RID: 10425
		public static readonly ADPropertyDefinition EhfAdminAccountSyncEnabled = new ADPropertyDefinition("EhfAdminAccountSyncEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PerimeterConfigSchema.Flags
		}, null, ADObject.InvertFlagGetterDelegate(PerimeterConfigSchema.Flags, 32), ADObject.InvertFlagSetterDelegate(PerimeterConfigSchema.Flags, 32), null, null);

		// Token: 0x040028BA RID: 10426
		public static readonly ADPropertyDefinition IPSafelistingSyncEnabled = new ADPropertyDefinition("IPSafelistingSyncEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PerimeterConfigSchema.Flags
		}, null, ADObject.FlagGetterDelegate(PerimeterConfigSchema.Flags, 64), ADObject.FlagSetterDelegate(PerimeterConfigSchema.Flags, 64), null, null);

		// Token: 0x040028BB RID: 10427
		public static readonly ADPropertyDefinition MigrationInProgress = new ADPropertyDefinition("MigrationInProgress", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PerimeterConfigSchema.Flags
		}, null, ADObject.FlagGetterDelegate(PerimeterConfigSchema.Flags, 1024), ADObject.FlagSetterDelegate(PerimeterConfigSchema.Flags, 1024), null, null);

		// Token: 0x040028BC RID: 10428
		public static readonly ADPropertyDefinition EheEnabled = new ADPropertyDefinition("EheEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PerimeterConfigSchema.Flags
		}, null, ADObject.FlagGetterDelegate(PerimeterConfigSchema.Flags, 2048), ADObject.FlagSetterDelegate(PerimeterConfigSchema.Flags, 2048), null, null);

		// Token: 0x040028BD RID: 10429
		public static readonly ADPropertyDefinition RMSOFwdSyncEnabled = new ADPropertyDefinition("RMSOFwdSyncEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PerimeterConfigSchema.Flags
		}, null, ADObject.InvertFlagGetterDelegate(PerimeterConfigSchema.Flags, 4096), ADObject.InvertFlagSetterDelegate(PerimeterConfigSchema.Flags, 4096), null, null);

		// Token: 0x040028BE RID: 10430
		public static readonly ADPropertyDefinition EheDecryptEnabled = new ADPropertyDefinition("EheDecryptEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PerimeterConfigSchema.Flags
		}, null, ADObject.FlagGetterDelegate(PerimeterConfigSchema.Flags, 8192), ADObject.FlagSetterDelegate(PerimeterConfigSchema.Flags, 8192), null, null);

		// Token: 0x040028BF RID: 10431
		public static readonly ADPropertyDefinition PartnerRoutingDomain = new ADPropertyDefinition("PartnerRoutingDomain", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomain), "AdminDescription", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 256)
		}, null, null);

		// Token: 0x040028C0 RID: 10432
		public static readonly ADPropertyDefinition PartnerConnectorDomain = new ADPropertyDefinition("PartnerConnectorDomain", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomain), "wWWHomePage", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 256)
		}, null, null);

		// Token: 0x040028C1 RID: 10433
		public static readonly ADPropertyDefinition MailFlowPartner = new ADPropertyDefinition("MailFlowPartner", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchTransportResellerSettingsLink", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040028C2 RID: 10434
		public static readonly ADPropertyDefinition SafelistingUIMode = ADObject.BitfieldProperty("SafelistingUIMode", 7, 2, PerimeterConfigSchema.Flags);
	}
}
