using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Sharing
{
	// Token: 0x02000D8B RID: 3467
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WindowsLiveIdApplicationIdentitySchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400404E RID: 16462
		public static readonly SimpleProviderPropertyDefinition InstanceType = new SimpleProviderPropertyDefinition("InstanceType", ExchangeObjectVersion.Exchange2007, typeof(LiveIdInstanceType), PropertyDefinitionFlags.None, LiveIdInstanceType.Consumer, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400404F RID: 16463
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04004050 RID: 16464
		public static readonly SimpleProviderPropertyDefinition Id = new SimpleProviderPropertyDefinition("Id", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04004051 RID: 16465
		public static readonly SimpleProviderPropertyDefinition Status = new SimpleProviderPropertyDefinition("Status", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04004052 RID: 16466
		public static readonly SimpleProviderPropertyDefinition UriCollection = new SimpleProviderPropertyDefinition("URIs", ExchangeObjectVersion.Exchange2007, typeof(Uri), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04004053 RID: 16467
		public static readonly SimpleProviderPropertyDefinition CertificateCollection = new SimpleProviderPropertyDefinition("Certificates", ExchangeObjectVersion.Exchange2007, typeof(WindowsLiveIdApplicationCertificate), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04004054 RID: 16468
		public static readonly SimpleProviderPropertyDefinition RawXml = new SimpleProviderPropertyDefinition("RawXml", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
