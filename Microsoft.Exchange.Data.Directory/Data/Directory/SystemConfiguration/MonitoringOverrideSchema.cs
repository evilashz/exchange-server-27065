using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004FC RID: 1276
	internal class MonitoringOverrideSchema : ADConfigurationObjectSchema
	{
		// Token: 0x0400269D RID: 9885
		public static readonly ADPropertyDefinition HealthSet = new ADPropertyDefinition("HealthSet", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchShadowDisplayName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 64)
		}, null, null);

		// Token: 0x0400269E RID: 9886
		public static readonly ADPropertyDefinition MonitoringItemName = new ADPropertyDefinition("MonitoringItemName", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchShadowInfo", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 128)
		}, null, null);

		// Token: 0x0400269F RID: 9887
		public static readonly ADPropertyDefinition PropertyValue = new ADPropertyDefinition("PropertyValue", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchConfigurationXML", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 102400)
		}, null, null);

		// Token: 0x040026A0 RID: 9888
		public static readonly ADPropertyDefinition ApplyVersionRaw = new ADPropertyDefinition("ApplyVersionRaw", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchMonitoringOverrideApplyVersion", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026A1 RID: 9889
		public static readonly ADPropertyDefinition ApplyVersion = new ADPropertyDefinition("ApplyVersion", ExchangeObjectVersion.Exchange2012, typeof(ServerVersion), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MonitoringOverrideSchema.ApplyVersionRaw
		}, null, new GetterDelegate(MonitoringOverride.ApplyVersionGetter), new SetterDelegate(MonitoringOverride.ApplyVersionSetter), null, null);

		// Token: 0x040026A2 RID: 9890
		public static readonly ADPropertyDefinition ExpirationTimeRaw = new ADPropertyDefinition("ExpirationTimeRaw", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchADCGlobalNames", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026A3 RID: 9891
		public static readonly ADPropertyDefinition ExpirationTime = new ADPropertyDefinition("ExpirationTime", ExchangeObjectVersion.Exchange2012, typeof(DateTime?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MonitoringOverrideSchema.ExpirationTimeRaw
		}, null, new GetterDelegate(MonitoringOverride.ExpirationTimeGetter), new SetterDelegate(MonitoringOverride.ExpirationTimeSetter), null, null);

		// Token: 0x040026A4 RID: 9892
		public static readonly ADPropertyDefinition CreatedBy = new ADPropertyDefinition("CreatedBy", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchMonitoringResources", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
