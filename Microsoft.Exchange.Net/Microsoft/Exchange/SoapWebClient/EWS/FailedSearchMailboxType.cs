using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000272 RID: 626
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class FailedSearchMailboxType
	{
		// Token: 0x04001038 RID: 4152
		public string Mailbox;

		// Token: 0x04001039 RID: 4153
		public int ErrorCode;

		// Token: 0x0400103A RID: 4154
		public string ErrorMessage;

		// Token: 0x0400103B RID: 4155
		public bool IsArchive;
	}
}
