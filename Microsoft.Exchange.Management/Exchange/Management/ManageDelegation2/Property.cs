using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.ManageDelegation2
{
	// Token: 0x02000DBF RID: 3519
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://domains.live.com/Service/ManageDelegation2/V1.0")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class Property
	{
		// Token: 0x04004154 RID: 16724
		public string Name;

		// Token: 0x04004155 RID: 16725
		public string Value;
	}
}
