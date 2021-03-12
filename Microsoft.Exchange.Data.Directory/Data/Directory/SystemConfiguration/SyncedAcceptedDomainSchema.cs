using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005AE RID: 1454
	internal sealed class SyncedAcceptedDomainSchema : AcceptedDomainSchema
	{
		// Token: 0x04002D8B RID: 11659
		public static readonly ADPropertyDefinition SyncErrors = new ADPropertyDefinition("SyncErrors", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncCookies", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
