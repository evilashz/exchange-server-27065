using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000118 RID: 280
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ProtocolConnection
	{
		// Token: 0x040005C3 RID: 1475
		[XmlElement(IsNullable = true)]
		public string Hostname;

		// Token: 0x040005C4 RID: 1476
		public int Port;

		// Token: 0x040005C5 RID: 1477
		[XmlElement(IsNullable = true)]
		public string EncryptionMethod;
	}
}
