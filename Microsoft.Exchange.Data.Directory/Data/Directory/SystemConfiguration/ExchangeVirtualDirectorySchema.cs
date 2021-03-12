using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000320 RID: 800
	internal class ExchangeVirtualDirectorySchema : ADVirtualDirectorySchema
	{
		// Token: 0x040016DC RID: 5852
		public static readonly ADPropertyDefinition MetabasePath = new ADPropertyDefinition("MetabasePath", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMetabasePath", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040016DD RID: 5853
		public static readonly ADPropertyDefinition ExtendedProtectionTokenChecking = new ADPropertyDefinition("ExtendedProtectionTokenChecking", ExchangeObjectVersion.Exchange2010, typeof(ExtendedProtectionTokenCheckingMode), null, ADPropertyDefinitionFlags.TaskPopulated, ExtendedProtectionTokenCheckingMode.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040016DE RID: 5854
		public static readonly ADPropertyDefinition ExtendedProtectionFlags = new ADPropertyDefinition("ExtendedProtectionFlags", ExchangeObjectVersion.Exchange2010, typeof(ExtendedProtectionFlag), null, ADPropertyDefinitionFlags.TaskPopulated, ExtendedProtectionFlag.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040016DF RID: 5855
		public static readonly ADPropertyDefinition ExtendedProtectionSPNList = new ADPropertyDefinition("ExtendedProtectionSPNList", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
