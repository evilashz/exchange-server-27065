using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000444 RID: 1092
	internal sealed class ExchangeConfigurationContainerSchemaWithAddressLists : ADContainerSchema
	{
		// Token: 0x0400213B RID: 8507
		public static readonly ADPropertyDefinition AddressBookRoots = new ADPropertyDefinition("AddressBookRoots", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "addressBookRoots", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400213C RID: 8508
		public static readonly ADPropertyDefinition DefaultGlobalAddressList = new ADPropertyDefinition("GlobalAddressList", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "globalAddressList", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400213D RID: 8509
		public static readonly ADPropertyDefinition AddressBookRoots2 = new ADPropertyDefinition("AddressBookRoots2", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "addressBookRoots2", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400213E RID: 8510
		public static readonly ADPropertyDefinition DefaultGlobalAddressList2 = new ADPropertyDefinition("GlobalAddressList2", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "globalAddressList2", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
