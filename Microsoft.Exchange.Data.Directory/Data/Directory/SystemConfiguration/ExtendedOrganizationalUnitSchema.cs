using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000621 RID: 1569
	internal sealed class ExtendedOrganizationalUnitSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04003366 RID: 13158
		public static readonly ADPropertyDefinition UPNSuffixes = SharedPropertyDefinitions.UPNSuffixes;

		// Token: 0x04003367 RID: 13159
		public static readonly ADPropertyDefinition DirSyncStatusAck = new ADPropertyDefinition("DirSyncStatusAck", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchDirsyncStatusAck", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);
	}
}
