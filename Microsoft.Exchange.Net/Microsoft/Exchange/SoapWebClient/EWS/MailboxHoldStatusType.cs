using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000277 RID: 631
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class MailboxHoldStatusType
	{
		// Token: 0x04001044 RID: 4164
		public string Mailbox;

		// Token: 0x04001045 RID: 4165
		public HoldStatusType Status;

		// Token: 0x04001046 RID: 4166
		public string AdditionalInfo;
	}
}
