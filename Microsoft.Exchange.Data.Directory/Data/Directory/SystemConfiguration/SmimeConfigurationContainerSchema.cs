using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005C4 RID: 1476
	internal class SmimeConfigurationContainerSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04002E1D RID: 11805
		public static readonly ADPropertyDefinition SmimeConfigurationXML = XMLSerializableBase.ConfigurationXmlRawProperty();
	}
}
