using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.ManageDelegation2
{
	// Token: 0x02000DBC RID: 3516
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://domains.live.com/Service/ManageDelegation2/V1.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DomainInfo
	{
		// Token: 0x0400414C RID: 16716
		public string DomainName;

		// Token: 0x0400414D RID: 16717
		public string AppId;

		// Token: 0x0400414E RID: 16718
		public DomainState DomainState;
	}
}
