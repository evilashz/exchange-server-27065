using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000925 RID: 2341
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public enum KeyUsage
	{
		// Token: 0x04004864 RID: 18532
		Sign,
		// Token: 0x04004865 RID: 18533
		Verify,
		// Token: 0x04004866 RID: 18534
		PairwiseIdentifier,
		// Token: 0x04004867 RID: 18535
		Delegation,
		// Token: 0x04004868 RID: 18536
		Decrypt,
		// Token: 0x04004869 RID: 18537
		Encrypt,
		// Token: 0x0400486A RID: 18538
		HashedIdentifier
	}
}
