using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000110 RID: 272
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TokenIssuer
	{
		// Token: 0x040005A0 RID: 1440
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string Uri;

		// Token: 0x040005A1 RID: 1441
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string Endpoint;
	}
}
