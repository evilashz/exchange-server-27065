using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000482 RID: 1154
	internal sealed class GALSyncOrganizationSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x040023C9 RID: 9161
		public static readonly ADPropertyDefinition GALSyncClientData = new ADPropertyDefinition("GALSyncClientData", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchDirsyncID", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);
	}
}
