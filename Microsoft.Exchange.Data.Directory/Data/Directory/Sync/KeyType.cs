using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000924 RID: 2340
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum KeyType
	{
		// Token: 0x0400485C RID: 18524
		AsymmetricPublicKey,
		// Token: 0x0400485D RID: 18525
		AsymmetricX509Cert,
		// Token: 0x0400485E RID: 18526
		Password,
		// Token: 0x0400485F RID: 18527
		Symmetric,
		// Token: 0x04004860 RID: 18528
		X509CertAndPassword,
		// Token: 0x04004861 RID: 18529
		Salt,
		// Token: 0x04004862 RID: 18530
		Hash
	}
}
