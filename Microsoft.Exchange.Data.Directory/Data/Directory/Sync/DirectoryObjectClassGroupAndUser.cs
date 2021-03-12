using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000958 RID: 2392
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum DirectoryObjectClassGroupAndUser
	{
		// Token: 0x0400490F RID: 18703
		Group,
		// Token: 0x04004910 RID: 18704
		User
	}
}
