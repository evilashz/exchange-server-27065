using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000124 RID: 292
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetDomainSettingsResponse : AutodiscoverResponse
	{
		// Token: 0x040005D9 RID: 1497
		[XmlArray(IsNullable = true)]
		public DomainResponse[] DomainResponses;
	}
}
