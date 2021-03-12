using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.EhfHybridMailflow
{
	// Token: 0x02000876 RID: 2166
	internal class HybridMailflowConfigurationSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04002D0B RID: 11531
		internal static SimpleProviderPropertyDefinition OutboundDomains = new SimpleProviderPropertyDefinition("OutboundDomains", ExchangeObjectVersion.Exchange2010, typeof(SmtpDomainWithSubdomains), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002D0C RID: 11532
		internal static SimpleProviderPropertyDefinition InboundIPs = new SimpleProviderPropertyDefinition("InboundIPs", ExchangeObjectVersion.Exchange2010, typeof(IPRange), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002D0D RID: 11533
		internal static SimpleProviderPropertyDefinition OnPremisesFQDN = new SimpleProviderPropertyDefinition("OnPremisesFQDN", ExchangeObjectVersion.Exchange2010, typeof(Fqdn), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002D0E RID: 11534
		internal static SimpleProviderPropertyDefinition CertificateSubject = new SimpleProviderPropertyDefinition("CertificateSubject", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002D0F RID: 11535
		internal static SimpleProviderPropertyDefinition SecureMailEnabled = new SimpleProviderPropertyDefinition("SecureMailEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002D10 RID: 11536
		internal static SimpleProviderPropertyDefinition CentralizedTransportEnabled = new SimpleProviderPropertyDefinition("CentralizedTransportEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
