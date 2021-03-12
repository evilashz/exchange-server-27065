using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000332 RID: 818
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ResolutionType
	{
		// Token: 0x0400138A RID: 5002
		public EmailAddressType Mailbox;

		// Token: 0x0400138B RID: 5003
		public ContactItemType Contact;
	}
}
