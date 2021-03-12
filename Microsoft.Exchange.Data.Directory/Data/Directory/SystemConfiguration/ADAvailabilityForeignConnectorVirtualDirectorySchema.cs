using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000326 RID: 806
	internal sealed class ADAvailabilityForeignConnectorVirtualDirectorySchema : ExchangeVirtualDirectorySchema
	{
		// Token: 0x040016F2 RID: 5874
		public static readonly ADPropertyDefinition AvailabilityForeignConnectorType = new ADPropertyDefinition("AvailabilityForeignConnectorType", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAvailabilityForeignConnectorType", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040016F3 RID: 5875
		public static readonly ADPropertyDefinition AvailabilityForeignConnectorDomains = new ADPropertyDefinition("AvailabilityForeignConnectorDomains", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAvailabilityForeignConnectorDomain", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
