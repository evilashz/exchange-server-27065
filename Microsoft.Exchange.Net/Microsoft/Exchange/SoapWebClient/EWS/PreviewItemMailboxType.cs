using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000286 RID: 646
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PreviewItemMailboxType
	{
		// Token: 0x04001095 RID: 4245
		public string MailboxId;

		// Token: 0x04001096 RID: 4246
		public string PrimarySmtpAddress;
	}
}
