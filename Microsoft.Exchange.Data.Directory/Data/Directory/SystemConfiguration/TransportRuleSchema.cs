using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005DB RID: 1499
	internal sealed class TransportRuleSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002FA4 RID: 12196
		public static readonly ADPropertyDefinition Priority = new ADPropertyDefinition("RulePriority", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportRulePriority", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FA5 RID: 12197
		public static readonly ADPropertyDefinition Xml = new ADPropertyDefinition("RuleXml", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchTransportRuleXml", ADPropertyDefinitionFlags.PersistDefaultValue, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FA6 RID: 12198
		internal static readonly ADPropertyDefinition ImmutableId = new ADPropertyDefinition("ImmutableId", ExchangeObjectVersion.Exchange2007, typeof(Guid), "msExchImmutableid", ADPropertyDefinitionFlags.WriteOnce, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
