using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000491 RID: 1169
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[Serializable]
	public class RequestServerVersion : SoapHeader
	{
		// Token: 0x06000FA6 RID: 4006 RVA: 0x00027077 File Offset: 0x00025277
		public RequestServerVersion()
		{
			this.Version = ExchangeVersionType.Exchange2013_SP1;
		}

		// Token: 0x040017C0 RID: 6080
		[XmlAttribute]
		public ExchangeVersionType Version;

		// Token: 0x040017C1 RID: 6081
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr;
	}
}
