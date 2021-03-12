using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000955 RID: 2389
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public enum DirectoryObjectClassContainsOwner
	{
		// Token: 0x04004905 RID: 18693
		Group,
		// Token: 0x04004906 RID: 18694
		Subscription,
		// Token: 0x04004907 RID: 18695
		ServicePrincipal
	}
}
