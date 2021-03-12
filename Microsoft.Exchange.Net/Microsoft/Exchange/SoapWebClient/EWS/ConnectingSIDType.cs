using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200048D RID: 1165
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ConnectingSIDType
	{
		// Token: 0x040017B2 RID: 6066
		[XmlElement("SID", typeof(SIDType))]
		[XmlElement("PrincipalName", typeof(PrincipalNameType))]
		[XmlElement("SmtpAddress", typeof(SmtpAddressType))]
		[XmlElement("PrimarySmtpAddress", typeof(PrimarySmtpAddressType))]
		public object Item;
	}
}
