using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000122 RID: 290
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class GetFederationInformationResponse : AutodiscoverResponse
	{
		// Token: 0x040005D3 RID: 1491
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string ApplicationUri;

		// Token: 0x040005D4 RID: 1492
		[XmlArray(IsNullable = true)]
		public TokenIssuer[] TokenIssuers;

		// Token: 0x040005D5 RID: 1493
		[XmlArray(IsNullable = true)]
		[XmlArrayItem("Domain")]
		public string[] Domains;
	}
}
