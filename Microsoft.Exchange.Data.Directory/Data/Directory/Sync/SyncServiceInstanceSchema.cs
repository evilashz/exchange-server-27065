using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000502 RID: 1282
	internal class SyncServiceInstanceSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040026D3 RID: 9939
		public static readonly ADPropertyDefinition AccountPartition = new ADPropertyDefinition("AccountPartition", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchAccountForestLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026D4 RID: 9940
		public static readonly ADPropertyDefinition MinVersion = new ADPropertyDefinition("MinVersion", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSyncDaemonMinVersion", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026D5 RID: 9941
		public static readonly ADPropertyDefinition MaxVersion = new ADPropertyDefinition("MaxVersion", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSyncDaemonMaxVersion", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026D6 RID: 9942
		public static readonly ADPropertyDefinition ActiveInstanceSleepInterval = new ADPropertyDefinition("ActiveInstanceSleepInterval", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchActiveInstanceSleepInterval", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x040026D7 RID: 9943
		public static readonly ADPropertyDefinition PassiveInstanceSleepInterval = new ADPropertyDefinition("PassiveInstanceSleepInterval", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchPassiveInstanceSleepInterval", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x040026D8 RID: 9944
		public static readonly ADPropertyDefinition IsEnabled = ADObject.BitfieldProperty("IsEnabled", 0, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x040026D9 RID: 9945
		public static readonly ADPropertyDefinition UseCentralConfig = ADObject.BitfieldProperty("UseCentralConfig", 3, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x040026DA RID: 9946
		public static readonly ADPropertyDefinition IsHalted = ADObject.BitfieldProperty("IsHalted", 1, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x040026DB RID: 9947
		public static readonly ADPropertyDefinition IsHaltRecoveryDisabled = ADObject.BitfieldProperty("IsHaltRecoveryDisabled", 2, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x040026DC RID: 9948
		public static readonly ADPropertyDefinition IsMultiObjectCookieEnabled = ADObject.BitfieldProperty("IsMultiObjectCookieEnabled", 4, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x040026DD RID: 9949
		public static readonly ADPropertyDefinition IsNewCookieBlocked = ADObject.BitfieldProperty("IsNewCookieBlocked", 5, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x040026DE RID: 9950
		public static readonly ADPropertyDefinition NewTenantMinVersion = new ADPropertyDefinition("NewTenantMinVersion", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSyncServiceInstanceNewTenantMinVersion", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026DF RID: 9951
		public static readonly ADPropertyDefinition NewTenantMaxVersion = new ADPropertyDefinition("NewTenantMaxVersion", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSyncServiceInstanceNewTenantMaxVersion", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026E0 RID: 9952
		public static readonly ADPropertyDefinition TargetServerMaxVersion = new ADPropertyDefinition("TargetServerMaxVersion", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchCapabilityIdentifiers", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026E1 RID: 9953
		public static readonly ADPropertyDefinition TargetServerMinVersion = new ADPropertyDefinition("TargetServerMinVersion", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSetupStatus", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026E2 RID: 9954
		public static readonly ADPropertyDefinition ForwardSyncConfigurationXML = new ADPropertyDefinition("ForwardSyncConfigurationXML", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchConfigurationXML", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026E3 RID: 9955
		public static readonly ADPropertyDefinition IsUsedForTenantToServiceInstanceAssociation = ADObject.BitfieldProperty("IsUsedForTenantToServiceInstanceAssociation", 6, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x02000503 RID: 1283
		public enum ArbitrationProvisioningBitShifts
		{
			// Token: 0x040026E5 RID: 9957
			IsEnabled,
			// Token: 0x040026E6 RID: 9958
			IsHalted,
			// Token: 0x040026E7 RID: 9959
			IsHaltRecoveryDisabled,
			// Token: 0x040026E8 RID: 9960
			IsCentralConfigEnabled,
			// Token: 0x040026E9 RID: 9961
			IsMultiObjectCookieEnabled,
			// Token: 0x040026EA RID: 9962
			IsNewCookieBlocked,
			// Token: 0x040026EB RID: 9963
			IsUsedForTenantToServiceInstanceAssociation
		}
	}
}
