using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002C7 RID: 711
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class InvalidRecipientType
	{
		// Token: 0x04001217 RID: 4631
		public string SmtpAddress;

		// Token: 0x04001218 RID: 4632
		public InvalidRecipientResponseCodeType ResponseCode;

		// Token: 0x04001219 RID: 4633
		public string MessageText;
	}
}
