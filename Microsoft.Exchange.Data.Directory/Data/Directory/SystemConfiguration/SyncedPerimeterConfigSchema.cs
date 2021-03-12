using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005B0 RID: 1456
	internal sealed class SyncedPerimeterConfigSchema : PerimeterConfigSchema
	{
		// Token: 0x04002D8D RID: 11661
		public static readonly ADPropertyDefinition SyncErrors = new ADPropertyDefinition("SyncErrors", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncCookies", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
