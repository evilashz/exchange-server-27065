using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003F8 RID: 1016
	internal class DepartmentSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001F1E RID: 7966
		public static readonly ADPropertyDefinition DisplayName = SharedPropertyDefinitions.MandatoryDisplayName;

		// Token: 0x04001F1F RID: 7967
		public static readonly ADPropertyDefinition HABSeniorityIndex = new ADPropertyDefinition("HABSeniorityIndex", ExchangeObjectVersion.Exchange2007, typeof(int), "msDS-HABSeniorityIndex", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F20 RID: 7968
		public static readonly ADPropertyDefinition PhoneticDisplayName = new ADPropertyDefinition("PhoneticDisplayName", ExchangeObjectVersion.Exchange2007, typeof(string), "msDS-PhoneticDisplayName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F21 RID: 7969
		public static readonly ADPropertyDefinition HABChildDepartments = new ADPropertyDefinition("HABChildDepartments", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchHABChildDepartmentsLink", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F22 RID: 7970
		public static readonly ADPropertyDefinition HABChildDepartmentsBL = new ADPropertyDefinition("HABChildDepartmentsBL", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchHABChildDepartmentsBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F23 RID: 7971
		public static readonly ADPropertyDefinition HABRootDepartmentBL = new ADPropertyDefinition("HABRootDepartmentBL", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchHABRootDepartmentBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001F24 RID: 7972
		public static readonly ADPropertyDefinition HABShowInDepartmentsBL = new ADPropertyDefinition("HABShowInDepartmentsBL", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchHABShowInDepartmentsBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
