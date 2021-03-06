using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005C9 RID: 1481
	internal class RemoteAccountPolicySchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002EA5 RID: 11941
		public static readonly PropertyDefinition PollingInterval = new ADPropertyDefinition("PollingInterval", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan), "msExchSyncAccountsPollingInterval", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromHours(1.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002EA6 RID: 11942
		public static readonly PropertyDefinition TimeBeforeInactive = new ADPropertyDefinition("TimeBeforeInactive", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan), "msExchSyncAccountsTimeBeforeInactive", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(7.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002EA7 RID: 11943
		public static readonly PropertyDefinition TimeBeforeDormant = new ADPropertyDefinition("TimeBeforeDormant", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan), "msExchSyncAccountsTimeBeforeDormant", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002EA8 RID: 11944
		public static readonly PropertyDefinition MaxSyncAccounts = new ADPropertyDefinition("MaxSyncAccounts", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchSyncAccountsMax", ADPropertyDefinitionFlags.PersistDefaultValue, 5, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002EA9 RID: 11945
		public static readonly ADPropertyDefinition SyncFlags = new ADPropertyDefinition("SyncFlags", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchSyncAccountsFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002EAA RID: 11946
		public static readonly PropertyDefinition SyncNowAllowed = new ADPropertyDefinition("SyncNowAllowed", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			RemoteAccountPolicySchema.SyncFlags
		}, null, ADObject.FlagGetterDelegate(RemoteAccountPolicySchema.SyncFlags, 2), ADObject.FlagSetterDelegate(RemoteAccountPolicySchema.SyncFlags, 2), null, null);
	}
}
