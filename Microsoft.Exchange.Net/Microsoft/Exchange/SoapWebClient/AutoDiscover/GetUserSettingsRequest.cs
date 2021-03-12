using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200012D RID: 301
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetUserSettingsRequest : AutodiscoverRequest
	{
		// Token: 0x040005EA RID: 1514
		[XmlArray(IsNullable = true)]
		public User[] Users;

		// Token: 0x040005EB RID: 1515
		[XmlArray(IsNullable = true)]
		[XmlArrayItem("Setting")]
		public string[] RequestedSettings;

		// Token: 0x040005EC RID: 1516
		[XmlElement(IsNullable = true)]
		public ExchangeVersion? RequestedVersion;
	}
}
