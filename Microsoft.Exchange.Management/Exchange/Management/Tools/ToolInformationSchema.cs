using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Tools
{
	// Token: 0x02000D05 RID: 3333
	internal class ToolInformationSchema : ObjectSchema
	{
		// Token: 0x04003ECE RID: 16078
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(ToolId), PropertyDefinitionFlags.None, ToolId.CSVParser, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003ECF RID: 16079
		public static readonly SimpleProviderPropertyDefinition VersionStatus = new SimpleProviderPropertyDefinition("VersionStatus", ExchangeObjectVersion.Exchange2010, typeof(ToolVersionStatus), PropertyDefinitionFlags.None, ToolVersionStatus.LatestVersion, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003ED0 RID: 16080
		public static readonly SimpleProviderPropertyDefinition MinimumSupportedVersion = new SimpleProviderPropertyDefinition("MinimumSupportedVersion", ExchangeObjectVersion.Exchange2010, typeof(Version), PropertyDefinitionFlags.None, SupportedToolsData.MinimumVersion, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003ED1 RID: 16081
		public static readonly SimpleProviderPropertyDefinition LatestVersion = new SimpleProviderPropertyDefinition("LatestVersion", ExchangeObjectVersion.Exchange2010, typeof(Version), PropertyDefinitionFlags.None, SupportedToolsData.MaximumVersion, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003ED2 RID: 16082
		public static readonly SimpleProviderPropertyDefinition UpdateInformationUrl = new SimpleProviderPropertyDefinition("UpdateInformationUrl", ExchangeObjectVersion.Exchange2010, typeof(Uri), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
