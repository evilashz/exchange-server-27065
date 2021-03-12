using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200091E RID: 2334
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public enum AsymmetricKeyType
	{
		// Token: 0x04004845 RID: 18501
		AsymmetricPublicKey,
		// Token: 0x04004846 RID: 18502
		AsymmetricX509Cert,
		// Token: 0x04004847 RID: 18503
		AsymmetricX509Cert2
	}
}
