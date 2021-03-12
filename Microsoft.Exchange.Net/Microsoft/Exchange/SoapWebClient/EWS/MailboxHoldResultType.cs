using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000276 RID: 630
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MailboxHoldResultType
	{
		// Token: 0x04001041 RID: 4161
		public string HoldId;

		// Token: 0x04001042 RID: 4162
		public string Query;

		// Token: 0x04001043 RID: 4163
		[XmlArrayItem("MailboxHoldStatus", IsNullable = false)]
		public MailboxHoldStatusType[] MailboxHoldStatuses;
	}
}
