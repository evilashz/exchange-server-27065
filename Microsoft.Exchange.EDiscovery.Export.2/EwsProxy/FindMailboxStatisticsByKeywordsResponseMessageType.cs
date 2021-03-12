using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001A9 RID: 425
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FindMailboxStatisticsByKeywordsResponseMessageType : ResponseMessageType
	{
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x00024830 File Offset: 0x00022A30
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x00024838 File Offset: 0x00022A38
		public MailboxStatisticsSearchResultType MailboxStatisticsSearchResult
		{
			get
			{
				return this.mailboxStatisticsSearchResultField;
			}
			set
			{
				this.mailboxStatisticsSearchResultField = value;
			}
		}

		// Token: 0x04000C4F RID: 3151
		private MailboxStatisticsSearchResultType mailboxStatisticsSearchResultField;
	}
}
