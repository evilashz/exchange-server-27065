using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005AB RID: 1451
	internal sealed class SmtpVirtualServerConfigurationSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04002D87 RID: 11655
		public static readonly ADPropertyDefinition SmtpFqdn = new ADPropertyDefinition("SmtpFqdn", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchSmtpFullyQualifiedDomainName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
