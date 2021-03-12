using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200012E RID: 302
	[XmlRoot(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class RequestedServerVersion : SoapHeader
	{
		// Token: 0x040005ED RID: 1517
		[XmlText]
		public string[] Text;
	}
}
