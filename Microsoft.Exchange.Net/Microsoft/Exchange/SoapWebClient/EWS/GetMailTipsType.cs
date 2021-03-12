using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000458 RID: 1112
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetMailTipsType : BaseRequestType
	{
		// Token: 0x040016F9 RID: 5881
		public EmailAddressType SendingAs;

		// Token: 0x040016FA RID: 5882
		[XmlArrayItem("Mailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public EmailAddressType[] Recipients;

		// Token: 0x040016FB RID: 5883
		public MailTipTypes MailTipsRequested;
	}
}
