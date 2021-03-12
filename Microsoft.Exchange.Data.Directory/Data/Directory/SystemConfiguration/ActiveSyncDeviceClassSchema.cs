using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000308 RID: 776
	internal class ActiveSyncDeviceClassSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001647 RID: 5703
		public static readonly ADPropertyDefinition LastUpdateTime = new ADPropertyDefinition("LastUpdateTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), "msExchLastUpdateTime", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001648 RID: 5704
		public static readonly ADPropertyDefinition DeviceType = MobileDeviceSchema.DeviceType;

		// Token: 0x04001649 RID: 5705
		public static readonly ADPropertyDefinition DeviceModel = MobileDeviceSchema.DeviceModel;
	}
}
