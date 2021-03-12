using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003B3 RID: 947
	internal sealed class CmdletExtensionAgentSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040019F9 RID: 6649
		public static readonly ADPropertyDefinition Assembly = new ADPropertyDefinition("Assembly", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchAssembly", ADPropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040019FA RID: 6650
		public static readonly ADPropertyDefinition ClassFactory = new ADPropertyDefinition("ClassFactory", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchClassFactory", ADPropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040019FB RID: 6651
		public static readonly ADPropertyDefinition CmdletExtensionFlags = new ADPropertyDefinition("CmdletExtensionFlags", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchCmdletExtensionFlags", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040019FC RID: 6652
		public static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CmdletExtensionAgentSchema.CmdletExtensionFlags
		}, null, ADObject.FlagGetterDelegate(CmdletExtensionAgentSchema.CmdletExtensionFlags, 1), ADObject.FlagSetterDelegate(CmdletExtensionAgentSchema.CmdletExtensionFlags, 1), null, null);

		// Token: 0x040019FD RID: 6653
		public static readonly ADPropertyDefinition Priority = new ADPropertyDefinition("Priority", ExchangeObjectVersion.Exchange2010, typeof(byte), null, ADPropertyDefinitionFlags.Calculated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CmdletExtensionAgentSchema.CmdletExtensionFlags
		}, null, new GetterDelegate(CmdletExtensionAgent.PriorityGetter), new SetterDelegate(CmdletExtensionAgent.PrioritySetter), null, null);

		// Token: 0x040019FE RID: 6654
		public static readonly ADPropertyDefinition IsSystem = new ADPropertyDefinition("IsSystem", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CmdletExtensionAgentSchema.CmdletExtensionFlags
		}, null, ADObject.FlagGetterDelegate(CmdletExtensionAgentSchema.CmdletExtensionFlags, 16), ADObject.FlagSetterDelegate(CmdletExtensionAgentSchema.CmdletExtensionFlags, 16), null, null);
	}
}
