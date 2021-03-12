using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.EhfHybridMailflow
{
	// Token: 0x02000879 RID: 2169
	internal class HybridMailflowDatacenterIPsSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04002D22 RID: 11554
		internal static SimpleProviderPropertyDefinition DatacenterIPs = new SimpleProviderPropertyDefinition("DatacenterIPs", ExchangeObjectVersion.Exchange2010, typeof(IPRange), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
