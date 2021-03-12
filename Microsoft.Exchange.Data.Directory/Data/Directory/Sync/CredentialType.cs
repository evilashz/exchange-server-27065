using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000946 RID: 2374
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum CredentialType
	{
		// Token: 0x040048BA RID: 18618
		Shared,
		// Token: 0x040048BB RID: 18619
		Dedicated
	}
}
