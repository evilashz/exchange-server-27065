using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000985 RID: 2437
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11")]
	[Serializable]
	public enum ResultCode
	{
		// Token: 0x04004982 RID: 18818
		Success,
		// Token: 0x04004983 RID: 18819
		PartitionUnavailable,
		// Token: 0x04004984 RID: 18820
		ObjectNotFound,
		// Token: 0x04004985 RID: 18821
		UnspecifiedError
	}
}
