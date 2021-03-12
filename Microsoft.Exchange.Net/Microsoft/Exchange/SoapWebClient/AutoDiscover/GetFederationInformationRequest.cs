using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200012A RID: 298
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetFederationInformationRequest : AutodiscoverRequest
	{
		// Token: 0x040005E0 RID: 1504
		[XmlElement(IsNullable = true)]
		public string Domain;
	}
}
