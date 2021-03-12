using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000523 RID: 1315
	internal class CasTransactionOutcomeSchema : TransactionOutcomeBaseSchema
	{
		// Token: 0x040021DB RID: 8667
		public static readonly SimpleProviderPropertyDefinition LocalSite = new SimpleProviderPropertyDefinition("LocalSite", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040021DC RID: 8668
		public static readonly SimpleProviderPropertyDefinition SecureAccess = new SimpleProviderPropertyDefinition("SecureAccess", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040021DD RID: 8669
		public static readonly SimpleProviderPropertyDefinition Url = new SimpleProviderPropertyDefinition("Url", ExchangeObjectVersion.Exchange2010, typeof(Uri), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040021DE RID: 8670
		public static readonly SimpleProviderPropertyDefinition VirtualDirectoryName = new SimpleProviderPropertyDefinition("VirtualDirectoryName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040021DF RID: 8671
		public static readonly SimpleProviderPropertyDefinition UrlType = new SimpleProviderPropertyDefinition("UrlType", ExchangeObjectVersion.Exchange2010, typeof(VirtualDirectoryUriScope), PropertyDefinitionFlags.None, VirtualDirectoryUriScope.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040021E0 RID: 8672
		public static readonly SimpleProviderPropertyDefinition ConnectionType = new SimpleProviderPropertyDefinition("Latency", ExchangeObjectVersion.Exchange2010, typeof(ProtocolConnectionType), PropertyDefinitionFlags.None, ProtocolConnectionType.Plaintext, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040021E1 RID: 8673
		public static readonly SimpleProviderPropertyDefinition Port = new SimpleProviderPropertyDefinition("Port", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
