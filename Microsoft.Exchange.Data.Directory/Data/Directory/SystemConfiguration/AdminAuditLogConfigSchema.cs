using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200034C RID: 844
	internal sealed class AdminAuditLogConfigSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040017CD RID: 6093
		public static readonly ADPropertyDefinition AdminLogFlags = new ADPropertyDefinition("AdminAuditLogFlags", ExchangeObjectVersion.Exchange2010, typeof(AdminAuditLogFlags), "msExchAdminAuditLogFlags", ADPropertyDefinitionFlags.None, AdminAuditLogFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017CE RID: 6094
		public static readonly ADPropertyDefinition AdminAuditLogCmdlets = new ADPropertyDefinition("AdminAuditLogCmdlets", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchAdminAuditLogCmdlets", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017CF RID: 6095
		public static readonly ADPropertyDefinition AdminAuditLogParameters = new ADPropertyDefinition("AdminAuditLogParameters", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchAdminAuditLogParameters", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017D0 RID: 6096
		public static readonly ADPropertyDefinition AdminAuditLogExcludedCmdlets = new ADPropertyDefinition("AdminAuditLogExcludedCmdlets", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchAdminAuditLogExcludedCmdlets", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017D1 RID: 6097
		public static readonly ADPropertyDefinition AdminAuditLogAgeLimit = new ADPropertyDefinition("AdminAuditLogAgeLimit", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan), "msExchAdminAuditLogAgeLimit", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(90.0), new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new NullableEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017D2 RID: 6098
		public static readonly ADPropertyDefinition AdminAuditLogMailbox = new ADPropertyDefinition("AdminAuditLogMailbox", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), "msExchAdminAuditLogMailbox", ADPropertyDefinitionFlags.None, SmtpAddress.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValidSmtpAddressConstraint()
		}, null, null);

		// Token: 0x040017D3 RID: 6099
		public static readonly ADPropertyDefinition AdminAuditLogEnabled = new ADPropertyDefinition("AdminAuditLogEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AdminAuditLogConfigSchema.AdminLogFlags
		}, null, (IPropertyBag propertyBag) => AdminAuditLogConfig.GetValueFromFlags(propertyBag, AdminAuditLogFlags.AdminAuditLogEnabled), delegate(object value, IPropertyBag propertyBag)
		{
			AdminAuditLogConfig.SetFlags(propertyBag, AdminAuditLogFlags.AdminAuditLogEnabled, (bool)value);
		}, null, null);

		// Token: 0x040017D4 RID: 6100
		public static readonly ADPropertyDefinition TestCmdletLoggingEnabled = new ADPropertyDefinition("TestCmdletsLoggingEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AdminAuditLogConfigSchema.AdminLogFlags
		}, null, (IPropertyBag propertyBag) => AdminAuditLogConfig.GetValueFromFlags(propertyBag, AdminAuditLogFlags.TestCmdletLoggingEnabled), delegate(object value, IPropertyBag propertyBag)
		{
			AdminAuditLogConfig.SetFlags(propertyBag, AdminAuditLogFlags.TestCmdletLoggingEnabled, (bool)value);
		}, null, null);

		// Token: 0x040017D5 RID: 6101
		public static readonly ADPropertyDefinition CaptureDetailsEnabled = new ADPropertyDefinition("CaptureDetailsEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AdminAuditLogConfigSchema.AdminLogFlags
		}, null, (IPropertyBag propertyBag) => AdminAuditLogConfig.GetValueFromFlags(propertyBag, AdminAuditLogFlags.CaptureDetailsEnabled), delegate(object value, IPropertyBag propertyBag)
		{
			AdminAuditLogConfig.SetFlags(propertyBag, AdminAuditLogFlags.CaptureDetailsEnabled, (bool)value);
		}, null, null);
	}
}
