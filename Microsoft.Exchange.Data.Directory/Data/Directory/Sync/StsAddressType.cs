using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000930 RID: 2352
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public enum StsAddressType
	{
		// Token: 0x04004879 RID: 18553
		FedMetadataUrl,
		// Token: 0x0400487A RID: 18554
		FederationRealm,
		// Token: 0x0400487B RID: 18555
		SignInUrl,
		// Token: 0x0400487C RID: 18556
		SignOutUrl,
		// Token: 0x0400487D RID: 18557
		ImageUrl,
		// Token: 0x0400487E RID: 18558
		AuthUrl,
		// Token: 0x0400487F RID: 18559
		MetadataExchangeUrl
	}
}
