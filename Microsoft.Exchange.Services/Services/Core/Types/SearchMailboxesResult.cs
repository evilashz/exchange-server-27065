using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000871 RID: 2161
	[XmlType(TypeName = "SearchMailboxesResultType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "SearchMailboxesResult", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SearchMailboxesResult
	{
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x000D80B9 File Offset: 0x000D62B9
		// (set) Token: 0x06003DE8 RID: 15848 RVA: 0x000D80C1 File Offset: 0x000D62C1
		[XmlArray(ElementName = "SearchQueries", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(Name = "SearchQueries", EmitDefaultValue = false)]
		[XmlArrayItem(ElementName = "MailboxQuery", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(MailboxQuery))]
		public MailboxQuery[] SearchQueries
		{
			get
			{
				return this.searchQueries;
			}
			set
			{
				this.searchQueries = value;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06003DE9 RID: 15849 RVA: 0x000D80CA File Offset: 0x000D62CA
		// (set) Token: 0x06003DEA RID: 15850 RVA: 0x000D80D2 File Offset: 0x000D62D2
		[IgnoreDataMember]
		[XmlElement("ResultType")]
		public SearchResultType ResultType
		{
			get
			{
				return this.resultType;
			}
			set
			{
				this.resultType = value;
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06003DEB RID: 15851 RVA: 0x000D80DB File Offset: 0x000D62DB
		// (set) Token: 0x06003DEC RID: 15852 RVA: 0x000D80E8 File Offset: 0x000D62E8
		[DataMember(Name = "ResultType", EmitDefaultValue = false)]
		[XmlIgnore]
		public string ResultTypeString
		{
			get
			{
				return EnumUtilities.ToString<SearchResultType>(this.ResultType);
			}
			set
			{
				this.ResultType = EnumUtilities.Parse<SearchResultType>(value);
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06003DED RID: 15853 RVA: 0x000D80F6 File Offset: 0x000D62F6
		// (set) Token: 0x06003DEE RID: 15854 RVA: 0x000D80FE File Offset: 0x000D62FE
		[DataMember(Name = "ItemCount", EmitDefaultValue = true)]
		[XmlElement("ItemCount")]
		public ulong ItemCount
		{
			get
			{
				return this.itemCount;
			}
			set
			{
				this.itemCount = value;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06003DEF RID: 15855 RVA: 0x000D8107 File Offset: 0x000D6307
		// (set) Token: 0x06003DF0 RID: 15856 RVA: 0x000D810F File Offset: 0x000D630F
		[DataMember(Name = "Size", EmitDefaultValue = true)]
		[XmlElement("Size")]
		public ulong Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x000D8118 File Offset: 0x000D6318
		// (set) Token: 0x06003DF2 RID: 15858 RVA: 0x000D8120 File Offset: 0x000D6320
		[DataMember(Name = "PageItemCount", EmitDefaultValue = false)]
		[XmlElement("PageItemCount")]
		public int PageItemCount
		{
			get
			{
				return this.pageItemCount;
			}
			set
			{
				this.pageItemCount = value;
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x000D8129 File Offset: 0x000D6329
		// (set) Token: 0x06003DF4 RID: 15860 RVA: 0x000D8131 File Offset: 0x000D6331
		[DataMember(Name = "PageItemSize", EmitDefaultValue = false)]
		[XmlElement("PageItemSize")]
		public ulong PageItemSize
		{
			get
			{
				return this.pageItemSize;
			}
			set
			{
				this.pageItemSize = value;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x000D813A File Offset: 0x000D633A
		// (set) Token: 0x06003DF6 RID: 15862 RVA: 0x000D8142 File Offset: 0x000D6342
		[XmlArrayItem(ElementName = "KeywordStat", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(KeywordStatisticsSearchResult))]
		[DataMember(Name = "KeywordStats", EmitDefaultValue = false)]
		[XmlArray(ElementName = "KeywordStats", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public KeywordStatisticsSearchResult[] KeywordStats
		{
			get
			{
				return this.keywordStats;
			}
			set
			{
				this.keywordStats = value;
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003DF7 RID: 15863 RVA: 0x000D814B File Offset: 0x000D634B
		// (set) Token: 0x06003DF8 RID: 15864 RVA: 0x000D8153 File Offset: 0x000D6353
		[DataMember(Name = "Items", EmitDefaultValue = false)]
		[XmlArray(ElementName = "Items", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "SearchPreviewItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(SearchPreviewItem))]
		public SearchPreviewItem[] Items
		{
			get
			{
				return this.previewItems;
			}
			set
			{
				this.previewItems = value;
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06003DF9 RID: 15865 RVA: 0x000D815C File Offset: 0x000D635C
		// (set) Token: 0x06003DFA RID: 15866 RVA: 0x000D8164 File Offset: 0x000D6364
		[XmlArrayItem(ElementName = "FailedMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(FailedSearchMailbox))]
		[DataMember(Name = "FailedMailboxes", EmitDefaultValue = false)]
		[XmlArray(ElementName = "FailedMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public FailedSearchMailbox[] FailedMailboxes
		{
			get
			{
				return this.failedMailboxes;
			}
			set
			{
				this.failedMailboxes = value;
			}
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06003DFB RID: 15867 RVA: 0x000D816D File Offset: 0x000D636D
		// (set) Token: 0x06003DFC RID: 15868 RVA: 0x000D8175 File Offset: 0x000D6375
		[XmlArrayItem(ElementName = "Refiner", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(SearchRefinerItem))]
		[DataMember(Name = "Refiners", EmitDefaultValue = false)]
		[XmlArray(ElementName = "Refiners", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public SearchRefinerItem[] Refiners { get; set; }

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x000D817E File Offset: 0x000D637E
		// (set) Token: 0x06003DFE RID: 15870 RVA: 0x000D8186 File Offset: 0x000D6386
		[XmlArray(ElementName = "MailboxStats", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(Name = "MailboxStats", EmitDefaultValue = false)]
		[XmlArrayItem(ElementName = "MailboxStat", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(MailboxStatisticsItem))]
		public MailboxStatisticsItem[] MailboxStats { get; set; }

		// Token: 0x0400239B RID: 9115
		private MailboxQuery[] searchQueries;

		// Token: 0x0400239C RID: 9116
		private SearchResultType resultType;

		// Token: 0x0400239D RID: 9117
		private ulong itemCount;

		// Token: 0x0400239E RID: 9118
		private ulong size;

		// Token: 0x0400239F RID: 9119
		private int pageItemCount;

		// Token: 0x040023A0 RID: 9120
		private ulong pageItemSize;

		// Token: 0x040023A1 RID: 9121
		private KeywordStatisticsSearchResult[] keywordStats;

		// Token: 0x040023A2 RID: 9122
		private SearchPreviewItem[] previewItems;

		// Token: 0x040023A3 RID: 9123
		private FailedSearchMailbox[] failedMailboxes;
	}
}
