using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200032F RID: 815
	internal sealed class ADDomainSchema : ADNonExchangeObjectSchema
	{
		// Token: 0x0400170E RID: 5902
		internal const long NoMaxPasswordAge = -9223372036854775808L;

		// Token: 0x0400170F RID: 5903
		public static readonly ADPropertyDefinition Sid = IADSecurityPrincipalSchema.Sid;

		// Token: 0x04001710 RID: 5904
		public static readonly ADPropertyDefinition Fqdn = new ADPropertyDefinition("Fqdn", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADDomain.FqdnGetter), null, null, null);

		// Token: 0x04001711 RID: 5905
		public static readonly ADPropertyDefinition MaximumPasswordAgeRaw = new ADPropertyDefinition("MaximumPasswordAgeRaw", ExchangeObjectVersion.Exchange2003, typeof(long?), "maxPwdAge", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001712 RID: 5906
		public static readonly ADPropertyDefinition MaximumPasswordAge = new ADPropertyDefinition("MaximumPasswordAge", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADDomainSchema.MaximumPasswordAgeRaw
		}, null, new GetterDelegate(ADDomain.MaximumPasswordAgeGetter), null, null, null);

		// Token: 0x04001713 RID: 5907
		public static readonly ADPropertyDefinition ReplicationCursors = new ADPropertyDefinition("ReplicationCursors", ExchangeObjectVersion.Exchange2003, typeof(ReplicationCursor), "msDS-NCReplCursors;binary", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001714 RID: 5908
		public static readonly ADPropertyDefinition RepsFrom = new ADPropertyDefinition("RepsFrom", ExchangeObjectVersion.Exchange2003, typeof(ReplicationNeighbor), "msDS-NCReplInboundNeighbors;binary", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
