using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000361 RID: 865
	internal sealed class ADE4eVirtualDirectorySchema : ExchangeWebAppVirtualDirectorySchema
	{
		// Token: 0x04001842 RID: 6210
		public static readonly ADPropertyDefinition E4EConfigurationXML = new ADPropertyDefinition("E4EConfigurationXML", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchConfigurationXML", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
