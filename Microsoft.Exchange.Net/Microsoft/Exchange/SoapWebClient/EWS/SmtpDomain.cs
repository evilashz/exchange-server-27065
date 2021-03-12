using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002A5 RID: 677
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SmtpDomain
	{
		// Token: 0x040011C4 RID: 4548
		[XmlAttribute]
		public string Name;

		// Token: 0x040011C5 RID: 4549
		[XmlAttribute]
		public bool IncludeSubdomains;

		// Token: 0x040011C6 RID: 4550
		[XmlIgnore]
		public bool IncludeSubdomainsSpecified;
	}
}
