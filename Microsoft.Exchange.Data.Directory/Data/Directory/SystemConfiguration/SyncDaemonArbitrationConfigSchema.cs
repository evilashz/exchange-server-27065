using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005B1 RID: 1457
	internal sealed class SyncDaemonArbitrationConfigSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002D8E RID: 11662
		public static readonly ADPropertyDefinition MinVersion = new ADPropertyDefinition("MinVersion", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSyncDaemonMinVersion", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002D8F RID: 11663
		public static readonly ADPropertyDefinition MaxVersion = new ADPropertyDefinition("MaxVersion", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSyncDaemonMaxVersion", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002D90 RID: 11664
		public static readonly ADPropertyDefinition ActiveInstanceSleepInterval = new ADPropertyDefinition("ActiveInstanceSleepInterval", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchActiveInstanceSleepInterval", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002D91 RID: 11665
		public static readonly ADPropertyDefinition PassiveInstanceSleepInterval = new ADPropertyDefinition("PassiveInstanceSleepInterval", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchPassiveInstanceSleepInterval", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002D92 RID: 11666
		public static readonly ADPropertyDefinition IsEnabled = new ADPropertyDefinition("IsEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SharedPropertyDefinitions.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 1), ADObject.FlagSetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 1), null, null);

		// Token: 0x04002D93 RID: 11667
		public static readonly ADPropertyDefinition IsHalted = new ADPropertyDefinition("IsHalted", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SharedPropertyDefinitions.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 2), ADObject.FlagSetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 2), null, null);

		// Token: 0x04002D94 RID: 11668
		public static readonly ADPropertyDefinition IsHaltRecoveryDisabled = new ADPropertyDefinition("IsHaltRecoveryDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SharedPropertyDefinitions.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 4), ADObject.FlagSetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 4), null, null);
	}
}
