using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000288 RID: 648
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class MailboxStatisticsItemType
	{
		// Token: 0x0400109B RID: 4251
		public string MailboxId;

		// Token: 0x0400109C RID: 4252
		public string DisplayName;

		// Token: 0x0400109D RID: 4253
		public long ItemCount;

		// Token: 0x0400109E RID: 4254
		public long Size;
	}
}
