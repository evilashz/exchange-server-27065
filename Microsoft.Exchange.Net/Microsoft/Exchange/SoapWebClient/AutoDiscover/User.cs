using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000127 RID: 295
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class User
	{
		// Token: 0x040005DE RID: 1502
		[XmlElement(IsNullable = true)]
		public string Mailbox;
	}
}
