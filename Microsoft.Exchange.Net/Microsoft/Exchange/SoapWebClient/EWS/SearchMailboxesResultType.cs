using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200027E RID: 638
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SearchMailboxesResultType
	{
		// Token: 0x0400105D RID: 4189
		[XmlArrayItem("MailboxQuery", IsNullable = false)]
		public MailboxQueryType[] SearchQueries;

		// Token: 0x0400105E RID: 4190
		public SearchResultType ResultType;

		// Token: 0x0400105F RID: 4191
		public long ItemCount;

		// Token: 0x04001060 RID: 4192
		public long Size;

		// Token: 0x04001061 RID: 4193
		public int PageItemCount;

		// Token: 0x04001062 RID: 4194
		public long PageItemSize;

		// Token: 0x04001063 RID: 4195
		[XmlArrayItem("KeywordStat", IsNullable = false)]
		public KeywordStatisticsSearchResultType[] KeywordStats;

		// Token: 0x04001064 RID: 4196
		[XmlArrayItem("SearchPreviewItem", IsNullable = false)]
		public SearchPreviewItemType[] Items;

		// Token: 0x04001065 RID: 4197
		[XmlArrayItem("FailedMailbox", IsNullable = false)]
		public FailedSearchMailboxType[] FailedMailboxes;

		// Token: 0x04001066 RID: 4198
		[XmlArrayItem("Refiner", IsNullable = false)]
		public SearchRefinerItemType[] Refiners;

		// Token: 0x04001067 RID: 4199
		[XmlArrayItem("MailboxStat", IsNullable = false)]
		public MailboxStatisticsItemType[] MailboxStats;
	}
}
