using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000388 RID: 904
	internal sealed class ADServerSchema : ADNonExchangeObjectSchema
	{
		// Token: 0x0400195C RID: 6492
		public static readonly ADPropertyDefinition DnsHostName = new ADPropertyDefinition("DnsHostName", ExchangeObjectVersion.Exchange2003, typeof(string), "dnsHostName", ADPropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400195D RID: 6493
		public static readonly ADPropertyDefinition Sid = new ADPropertyDefinition("Sid", ExchangeObjectVersion.Exchange2003, typeof(SecurityIdentifier), "objectSid", ADPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400195E RID: 6494
		public static readonly ADPropertyDefinition ServerReference = new ADPropertyDefinition("ServerReference", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "serverReference", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
