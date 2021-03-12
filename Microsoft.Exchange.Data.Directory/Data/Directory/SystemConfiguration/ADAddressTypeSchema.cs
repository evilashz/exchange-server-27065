using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200031D RID: 797
	internal class ADAddressTypeSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040016CE RID: 5838
		public static readonly ADPropertyDefinition FileVersion = new ADPropertyDefinition("FileVersion", ExchangeObjectVersion.Exchange2003, typeof(Version), "FileVersion", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040016CF RID: 5839
		public static readonly ADPropertyDefinition ProxyGeneratorDll = new ADPropertyDefinition("ProxyGeneratorDll", ExchangeObjectVersion.Exchange2003, typeof(string), "ProxyGeneratorDll", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
