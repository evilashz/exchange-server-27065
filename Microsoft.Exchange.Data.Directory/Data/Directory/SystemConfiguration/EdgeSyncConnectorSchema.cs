using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000431 RID: 1073
	internal class EdgeSyncConnectorSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002077 RID: 8311
		public static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2007, typeof(bool), "Enabled", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002078 RID: 8312
		public static readonly ADPropertyDefinition SynchronizationProvider = new ADPropertyDefinition("SynchronizationProvider", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncSynchronizationProvider", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002079 RID: 8313
		public static readonly ADPropertyDefinition AssemblyPath = new ADPropertyDefinition("AssemblyPath", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncProviderAssemblyPath", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
