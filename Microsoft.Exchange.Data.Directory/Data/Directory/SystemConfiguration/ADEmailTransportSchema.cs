using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000345 RID: 837
	internal class ADEmailTransportSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x040017BE RID: 6078
		public static readonly ADPropertyDefinition Server = new ADPropertyDefinition("Server", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADEmailTransport.ServerGetter), null, null, null);
	}
}
