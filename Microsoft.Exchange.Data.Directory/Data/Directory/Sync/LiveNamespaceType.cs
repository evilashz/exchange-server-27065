using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000948 RID: 2376
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public enum LiveNamespaceType
	{
		// Token: 0x040048BE RID: 18622
		None,
		// Token: 0x040048BF RID: 18623
		Managed,
		// Token: 0x040048C0 RID: 18624
		Federated
	}
}
