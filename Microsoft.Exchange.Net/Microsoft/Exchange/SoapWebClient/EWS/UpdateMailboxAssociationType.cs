using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000463 RID: 1123
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class UpdateMailboxAssociationType : BaseRequestType
	{
		// Token: 0x04001726 RID: 5926
		public MailboxAssociationType Association;

		// Token: 0x04001727 RID: 5927
		public MasterMailboxType Master;
	}
}
