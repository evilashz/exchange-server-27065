using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000289 RID: 649
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetSearchableMailboxesResponseMessageType : ResponseMessageType
	{
		// Token: 0x0400109F RID: 4255
		[XmlArrayItem("SearchableMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public SearchableMailboxType[] SearchableMailboxes;

		// Token: 0x040010A0 RID: 4256
		[XmlArrayItem("FailedMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FailedSearchMailboxType[] FailedMailboxes;
	}
}
