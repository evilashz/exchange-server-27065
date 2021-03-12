using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003C9 RID: 969
	internal class ContainerSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04001A78 RID: 6776
		public static readonly ADPropertyDefinition EdgeSyncCookies = SharedPropertyDefinitions.EdgeSyncCookies;

		// Token: 0x04001A79 RID: 6777
		public static ADPropertyDefinition CanaryData0 = new ADPropertyDefinition("msExchCanaryData0", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchCanaryData0", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001A7A RID: 6778
		public static ADPropertyDefinition CanaryData1 = new ADPropertyDefinition("msExchCanaryData1", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchCanaryData1", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001A7B RID: 6779
		public static ADPropertyDefinition CanaryData2 = new ADPropertyDefinition("msExchCanaryData2", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchCanaryData2", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
