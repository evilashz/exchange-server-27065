using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000962 RID: 2402
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum DirectoryObjectClassCanBeMember
	{
		// Token: 0x04004924 RID: 18724
		Contact,
		// Token: 0x04004925 RID: 18725
		ForeignPrincipal,
		// Token: 0x04004926 RID: 18726
		Group,
		// Token: 0x04004927 RID: 18727
		ServicePrincipal,
		// Token: 0x04004928 RID: 18728
		User
	}
}
