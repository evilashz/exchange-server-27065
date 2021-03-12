using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200019D RID: 413
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SearchMailboxesResultType
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x000243D3 File Offset: 0x000225D3
		// (set) Token: 0x0600118B RID: 4491 RVA: 0x000243DB File Offset: 0x000225DB
		[XmlArrayItem("MailboxQuery", IsNullable = false)]
		public MailboxQueryType[] SearchQueries
		{
			get
			{
				return this.searchQueriesField;
			}
			set
			{
				this.searchQueriesField = value;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x000243E4 File Offset: 0x000225E4
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x000243EC File Offset: 0x000225EC
		public SearchResultType ResultType
		{
			get
			{
				return this.resultTypeField;
			}
			set
			{
				this.resultTypeField = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x000243F5 File Offset: 0x000225F5
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x000243FD File Offset: 0x000225FD
		public long ItemCount
		{
			get
			{
				return this.itemCountField;
			}
			set
			{
				this.itemCountField = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00024406 File Offset: 0x00022606
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x0002440E File Offset: 0x0002260E
		public long Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00024417 File Offset: 0x00022617
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x0002441F File Offset: 0x0002261F
		public int PageItemCount
		{
			get
			{
				return this.pageItemCountField;
			}
			set
			{
				this.pageItemCountField = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00024428 File Offset: 0x00022628
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x00024430 File Offset: 0x00022630
		public long PageItemSize
		{
			get
			{
				return this.pageItemSizeField;
			}
			set
			{
				this.pageItemSizeField = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00024439 File Offset: 0x00022639
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x00024441 File Offset: 0x00022641
		[XmlArrayItem("KeywordStat", IsNullable = false)]
		public KeywordStatisticsSearchResultType[] KeywordStats
		{
			get
			{
				return this.keywordStatsField;
			}
			set
			{
				this.keywordStatsField = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0002444A File Offset: 0x0002264A
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x00024452 File Offset: 0x00022652
		[XmlArrayItem("SearchPreviewItem", IsNullable = false)]
		public SearchPreviewItemType[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0002445B File Offset: 0x0002265B
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00024463 File Offset: 0x00022663
		[XmlArrayItem("FailedMailbox", IsNullable = false)]
		public FailedSearchMailboxType[] FailedMailboxes
		{
			get
			{
				return this.failedMailboxesField;
			}
			set
			{
				this.failedMailboxesField = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0002446C File Offset: 0x0002266C
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x00024474 File Offset: 0x00022674
		[XmlArrayItem("Refiner", IsNullable = false)]
		public SearchRefinerItemType[] Refiners
		{
			get
			{
				return this.refinersField;
			}
			set
			{
				this.refinersField = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x0002447D File Offset: 0x0002267D
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x00024485 File Offset: 0x00022685
		[XmlArrayItem("MailboxStat", IsNullable = false)]
		public MailboxStatisticsItemType[] MailboxStats
		{
			get
			{
				return this.mailboxStatsField;
			}
			set
			{
				this.mailboxStatsField = value;
			}
		}

		// Token: 0x04000C0B RID: 3083
		private MailboxQueryType[] searchQueriesField;

		// Token: 0x04000C0C RID: 3084
		private SearchResultType resultTypeField;

		// Token: 0x04000C0D RID: 3085
		private long itemCountField;

		// Token: 0x04000C0E RID: 3086
		private long sizeField;

		// Token: 0x04000C0F RID: 3087
		private int pageItemCountField;

		// Token: 0x04000C10 RID: 3088
		private long pageItemSizeField;

		// Token: 0x04000C11 RID: 3089
		private KeywordStatisticsSearchResultType[] keywordStatsField;

		// Token: 0x04000C12 RID: 3090
		private SearchPreviewItemType[] itemsField;

		// Token: 0x04000C13 RID: 3091
		private FailedSearchMailboxType[] failedMailboxesField;

		// Token: 0x04000C14 RID: 3092
		private SearchRefinerItemType[] refinersField;

		// Token: 0x04000C15 RID: 3093
		private MailboxStatisticsItemType[] mailboxStatsField;
	}
}
