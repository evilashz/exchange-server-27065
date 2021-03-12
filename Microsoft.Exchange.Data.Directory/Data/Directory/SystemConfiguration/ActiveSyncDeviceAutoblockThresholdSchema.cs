using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000302 RID: 770
	internal class ActiveSyncDeviceAutoblockThresholdSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001637 RID: 5687
		public static readonly ADPropertyDefinition BehaviorType = new ADPropertyDefinition("BehaviorType", ExchangeObjectVersion.Exchange2010, typeof(AutoblockThresholdType), "msExchActiveSyncDeviceAutoblockThresholdType", ADPropertyDefinitionFlags.None, AutoblockThresholdType.UserAgentsChanges, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001638 RID: 5688
		public static readonly ADPropertyDefinition BehaviorTypeIncidenceLimit = new ADPropertyDefinition("BehaviorTypeIncidenceLimit", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchActiveSyncDeviceAutoblockThresholdIncidenceLimit", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 65535)
		}, null, null);

		// Token: 0x04001639 RID: 5689
		public static readonly ADPropertyDefinition BehaviorTypeIncidenceDuration = new ADPropertyDefinition("BehaviorTypeIncidenceDuration", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan), "msExchActiveSyncDeviceAutoblockThresholdIncidenceDuration", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.Zero, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneMinute)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400163A RID: 5690
		public static readonly ADPropertyDefinition DeviceBlockDuration = new ADPropertyDefinition("DeviceBlockDuration", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan), "msExchActiveSyncDeviceAutoBlockDuration", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.Zero, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneMinute)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400163B RID: 5691
		public static readonly ADPropertyDefinition AdminEmailInsert = new ADPropertyDefinition("AdminEmailInsert", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchMobileOTANotificationMailInsert2", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
