using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200030D RID: 781
	internal class ActiveSyncDeviceAccessRuleSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001659 RID: 5721
		public static readonly ADPropertyDefinition QueryString = new ADPropertyDefinition("QueryString", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDeviceAccessRuleQueryString", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400165A RID: 5722
		public static readonly ADPropertyDefinition Characteristic = new ADPropertyDefinition("Characteristic", ExchangeObjectVersion.Exchange2010, typeof(DeviceAccessCharacteristic), "msExchDeviceAccessRuleCharacteristic", ADPropertyDefinitionFlags.PersistDefaultValue, DeviceAccessCharacteristic.DeviceType, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400165B RID: 5723
		public static readonly ADPropertyDefinition AccessLevel = new ADPropertyDefinition("AccessLevel", ExchangeObjectVersion.Exchange2010, typeof(DeviceAccessLevel), "msExchMobileAccessControl", ADPropertyDefinitionFlags.PersistDefaultValue, DeviceAccessLevel.Allow, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400165C RID: 5724
		public static readonly ADPropertyDefinition BackLink = new ADPropertyDefinition("BackLink", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchDeviceAccessRuleBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
