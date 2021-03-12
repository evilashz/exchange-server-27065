using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200038A RID: 906
	internal class ADServiceConnectionPointSchema : ADNonExchangeObjectSchema
	{
		// Token: 0x04001962 RID: 6498
		public new static readonly ADPropertyDefinition ExchangeVersion = new ADPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2003, typeof(ExchangeObjectVersion), null, ADPropertyDefinitionFlags.TaskPopulated | ADPropertyDefinitionFlags.DoNotProvisionalClone, ExchangeObjectVersion.Exchange2003, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001963 RID: 6499
		public static readonly ADPropertyDefinition Keywords = new ADPropertyDefinition("Keywords", ExchangeObjectVersion.Exchange2003, typeof(string), "Keywords", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001964 RID: 6500
		public static readonly ADPropertyDefinition ServiceBindingInformation = new ADPropertyDefinition("ServiceBindingInformation", ExchangeObjectVersion.Exchange2003, typeof(string), "ServiceBindingInformation", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001965 RID: 6501
		public static readonly ADPropertyDefinition ServiceDnsName = new ADPropertyDefinition("ServiceDnsName", ExchangeObjectVersion.Exchange2003, typeof(string), "ServiceDnsName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001966 RID: 6502
		public static readonly ADPropertyDefinition ServiceClassName = new ADPropertyDefinition("ServiceClassName", ExchangeObjectVersion.Exchange2003, typeof(string), "ServiceClassName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
