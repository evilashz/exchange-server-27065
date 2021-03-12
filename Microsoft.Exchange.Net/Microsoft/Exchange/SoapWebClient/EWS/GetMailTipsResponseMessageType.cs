using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200032E RID: 814
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetMailTipsResponseMessageType : ResponseMessageType
	{
		// Token: 0x0400137C RID: 4988
		[XmlArrayItem(IsNullable = false)]
		public MailTipsResponseMessageType[] ResponseMessages;
	}
}
