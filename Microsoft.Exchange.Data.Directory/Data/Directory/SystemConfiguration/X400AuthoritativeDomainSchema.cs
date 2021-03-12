using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200061F RID: 1567
	internal class X400AuthoritativeDomainSchema : AcceptedDomainSchema
	{
		// Token: 0x04003361 RID: 13153
		public new static readonly ADPropertyDefinition DomainName = new ADPropertyDefinition("DomainName", ExchangeObjectVersion.Exchange2007, typeof(X400Domain), "msExchAcceptedDomainName", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
