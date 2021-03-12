using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000597 RID: 1431
	internal sealed class ServersContainerSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04002D55 RID: 11605
		public static readonly ADPropertyDefinition ContainerInfo = new ADPropertyDefinition("ContainerInfo", ExchangeObjectVersion.Exchange2003, typeof(ContainerInfo), "containerInfo", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.ContainerInfo.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
