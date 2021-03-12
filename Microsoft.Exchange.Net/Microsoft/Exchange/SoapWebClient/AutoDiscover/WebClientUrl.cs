using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000117 RID: 279
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class WebClientUrl
	{
		// Token: 0x040005C1 RID: 1473
		[XmlElement(IsNullable = true)]
		public string AuthenticationMethods;

		// Token: 0x040005C2 RID: 1474
		[XmlElement(IsNullable = true)]
		public string Url;
	}
}
