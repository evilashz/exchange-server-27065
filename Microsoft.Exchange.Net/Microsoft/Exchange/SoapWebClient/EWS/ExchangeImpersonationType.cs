using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200048E RID: 1166
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlRoot("ExchangeImpersonation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[Serializable]
	public class ExchangeImpersonationType : SoapHeader
	{
		// Token: 0x040017B3 RID: 6067
		public ConnectingSIDType ConnectingSID;

		// Token: 0x040017B4 RID: 6068
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr;
	}
}
