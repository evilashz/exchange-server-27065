using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200004A RID: 74
	internal class WebDnsRecordSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000160 RID: 352
		public static readonly SimpleProviderPropertyDefinition RecordType = new SimpleProviderPropertyDefinition("RecordType", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000161 RID: 353
		public static readonly SimpleProviderPropertyDefinition TTL = new SimpleProviderPropertyDefinition("TTL", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000162 RID: 354
		public static readonly SimpleProviderPropertyDefinition DomainName = new SimpleProviderPropertyDefinition("DomainName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000163 RID: 355
		public static readonly SimpleProviderPropertyDefinition Value = new SimpleProviderPropertyDefinition("Value", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
