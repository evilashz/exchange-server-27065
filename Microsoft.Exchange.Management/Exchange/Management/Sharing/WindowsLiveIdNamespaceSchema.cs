using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Sharing
{
	// Token: 0x02000D8E RID: 3470
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WindowsLiveIdNamespaceSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04004057 RID: 16471
		public static readonly SimpleProviderPropertyDefinition InstanceType = new SimpleProviderPropertyDefinition("InstanceType", ExchangeObjectVersion.Exchange2007, typeof(LiveIdInstanceType), PropertyDefinitionFlags.None, LiveIdInstanceType.Consumer, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04004058 RID: 16472
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04004059 RID: 16473
		public static readonly SimpleProviderPropertyDefinition ID = new SimpleProviderPropertyDefinition("ID", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400405A RID: 16474
		public static readonly SimpleProviderPropertyDefinition SiteID = new SimpleProviderPropertyDefinition("SiteID", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400405B RID: 16475
		public static readonly SimpleProviderPropertyDefinition AppID = new SimpleProviderPropertyDefinition("AppID", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400405C RID: 16476
		public static readonly SimpleProviderPropertyDefinition URI = new SimpleProviderPropertyDefinition("URI", ExchangeObjectVersion.Exchange2007, typeof(Uri), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400405D RID: 16477
		public static readonly SimpleProviderPropertyDefinition Certificate = new SimpleProviderPropertyDefinition("Certificate", ExchangeObjectVersion.Exchange2007, typeof(X509Certificate), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400405E RID: 16478
		public static readonly SimpleProviderPropertyDefinition NextCertificate = new SimpleProviderPropertyDefinition("NextCertificate", ExchangeObjectVersion.Exchange2007, typeof(X509Certificate), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400405F RID: 16479
		public static readonly SimpleProviderPropertyDefinition RawXml = new SimpleProviderPropertyDefinition("RawXml", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
