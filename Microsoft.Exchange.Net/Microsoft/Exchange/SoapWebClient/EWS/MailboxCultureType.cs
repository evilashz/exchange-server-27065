using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200048F RID: 1167
	[XmlRoot("MailboxCulture", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class MailboxCultureType : SoapHeader
	{
		// Token: 0x040017B5 RID: 6069
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr;

		// Token: 0x040017B6 RID: 6070
		[XmlText(DataType = "language")]
		public string Value;
	}
}
