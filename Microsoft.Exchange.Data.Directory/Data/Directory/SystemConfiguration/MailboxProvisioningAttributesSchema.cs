using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000596 RID: 1430
	internal class MailboxProvisioningAttributesSchema : ObjectSchema
	{
		// Token: 0x04002D50 RID: 11600
		public static readonly SimpleProviderPropertyDefinition RegionPropertyDefinition = new SimpleProviderPropertyDefinition("Region", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002D51 RID: 11601
		public static readonly SimpleProviderPropertyDefinition LocationPropertyDefinition = new SimpleProviderPropertyDefinition("Location", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002D52 RID: 11602
		public static readonly SimpleProviderPropertyDefinition DagName = new SimpleProviderPropertyDefinition("DagName", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002D53 RID: 11603
		public static readonly SimpleProviderPropertyDefinition ServerName = new SimpleProviderPropertyDefinition("ServerName", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002D54 RID: 11604
		public static readonly SimpleProviderPropertyDefinition DatabaseName = new SimpleProviderPropertyDefinition("DatabaseName", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
