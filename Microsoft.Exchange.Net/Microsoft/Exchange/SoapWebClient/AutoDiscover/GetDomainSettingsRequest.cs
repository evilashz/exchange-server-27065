using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200012B RID: 299
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetDomainSettingsRequest : AutodiscoverRequest
	{
		// Token: 0x040005E1 RID: 1505
		[XmlArrayItem("Domain")]
		[XmlArray(IsNullable = true)]
		public string[] Domains;

		// Token: 0x040005E2 RID: 1506
		[XmlArray(IsNullable = true)]
		[XmlArrayItem("Setting")]
		public string[] RequestedSettings;

		// Token: 0x040005E3 RID: 1507
		[XmlElement(IsNullable = true)]
		public ExchangeVersion? RequestedVersion;
	}
}
