using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000477 RID: 1143
	[XmlType(TypeName = "SearchMailboxesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Name = "SearchMailboxesRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SearchMailboxesRequest : BaseRequest
	{
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x000A287F File Offset: 0x000A0A7F
		// (set) Token: 0x060021B6 RID: 8630 RVA: 0x000A2887 File Offset: 0x000A0A87
		[IgnoreDataMember]
		[XmlArrayItem(ElementName = "MailboxQuery", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(MailboxQuery))]
		[XmlArray(ElementName = "SearchQueries", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
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

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x000A2890 File Offset: 0x000A0A90
		// (set) Token: 0x060021B8 RID: 8632 RVA: 0x000A2898 File Offset: 0x000A0A98
		[DataMember(Name = "SearchId", IsRequired = true)]
		[XmlIgnore]
		public string SearchId
		{
			get
			{
				return this.searchId;
			}
			set
			{
				this.searchId = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x000A28A1 File Offset: 0x000A0AA1
		// (set) Token: 0x060021BA RID: 8634 RVA: 0x000A28A9 File Offset: 0x000A0AA9
		[DataMember(Name = "MailboxId", IsRequired = false)]
		[XmlIgnore]
		public string MailboxId
		{
			get
			{
				return this.mailboxId;
			}
			set
			{
				this.mailboxId = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x000A28B2 File Offset: 0x000A0AB2
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x000A28BA File Offset: 0x000A0ABA
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

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x000A28C3 File Offset: 0x000A0AC3
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x000A28D0 File Offset: 0x000A0AD0
		[XmlIgnore]
		[DataMember(Name = "ResultType", IsRequired = true)]
		public string ResultTypeString
		{
			get
			{
				return EnumUtilities.ToString<SearchResultType>(this.resultType);
			}
			set
			{
				this.resultType = EnumUtilities.Parse<SearchResultType>(value);
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x000A28DE File Offset: 0x000A0ADE
		// (set) Token: 0x060021C0 RID: 8640 RVA: 0x000A28E6 File Offset: 0x000A0AE6
		[DataMember(Name = "PreviewItemResponseShape", IsRequired = false)]
		[XmlElement("PreviewItemResponseShape")]
		public PreviewItemResponseShape PreviewItemResponseShape
		{
			get
			{
				return this.previewItemResponseShape;
			}
			set
			{
				this.previewItemResponseShape = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x000A28EF File Offset: 0x000A0AEF
		// (set) Token: 0x060021C2 RID: 8642 RVA: 0x000A28F7 File Offset: 0x000A0AF7
		[XmlElement("SortBy")]
		[DataMember(Name = "SortBy", IsRequired = false)]
		public SortResults SortBy
		{
			get
			{
				return this.sortBy;
			}
			set
			{
				this.sortBy = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x000A2900 File Offset: 0x000A0B00
		// (set) Token: 0x060021C4 RID: 8644 RVA: 0x000A2908 File Offset: 0x000A0B08
		[DataMember(Name = "Language", IsRequired = false)]
		[XmlElement("Language")]
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x000A2911 File Offset: 0x000A0B11
		// (set) Token: 0x060021C6 RID: 8646 RVA: 0x000A2919 File Offset: 0x000A0B19
		[XmlElement("Deduplication")]
		[DataMember(Name = "Deduplication", IsRequired = false)]
		public bool Deduplication
		{
			get
			{
				return this.deduplication;
			}
			set
			{
				this.deduplication = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x000A2922 File Offset: 0x000A0B22
		// (set) Token: 0x060021C8 RID: 8648 RVA: 0x000A292A File Offset: 0x000A0B2A
		[XmlElement("PageSize")]
		[DataMember(Name = "PageSize", IsRequired = false)]
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				this.pageSize = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x000A2933 File Offset: 0x000A0B33
		// (set) Token: 0x060021CA RID: 8650 RVA: 0x000A293B File Offset: 0x000A0B3B
		[XmlElement("PageItemReference")]
		[DataMember(Name = "PageItemReference", IsRequired = false)]
		public string PageItemReference
		{
			get
			{
				return this.pageItemReference;
			}
			set
			{
				this.pageItemReference = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x000A2944 File Offset: 0x000A0B44
		// (set) Token: 0x060021CC RID: 8652 RVA: 0x000A294C File Offset: 0x000A0B4C
		[IgnoreDataMember]
		[XmlElement("PageDirection")]
		public SearchPageDirectionType PageDirection
		{
			get
			{
				return this.pageDirection;
			}
			set
			{
				this.pageDirection = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x000A2955 File Offset: 0x000A0B55
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x000A2962 File Offset: 0x000A0B62
		[XmlIgnore]
		[DataMember(Name = "PageDirection", IsRequired = true)]
		public string PageDirectionString
		{
			get
			{
				return EnumUtilities.ToString<SearchPageDirectionType>(this.pageDirection);
			}
			set
			{
				this.pageDirection = EnumUtilities.Parse<SearchPageDirectionType>(value);
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000A2970 File Offset: 0x000A0B70
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SearchMailboxes(callContext, this);
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000A2979 File Offset: 0x000A0B79
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000A297C File Offset: 0x000A0B7C
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return null;
		}

		// Token: 0x040014AE RID: 5294
		private MailboxQuery[] searchQueries;

		// Token: 0x040014AF RID: 5295
		private string searchId;

		// Token: 0x040014B0 RID: 5296
		private string mailboxId;

		// Token: 0x040014B1 RID: 5297
		private SearchResultType resultType;

		// Token: 0x040014B2 RID: 5298
		private PreviewItemResponseShape previewItemResponseShape;

		// Token: 0x040014B3 RID: 5299
		private SortResults sortBy;

		// Token: 0x040014B4 RID: 5300
		private string language;

		// Token: 0x040014B5 RID: 5301
		private bool deduplication;

		// Token: 0x040014B6 RID: 5302
		private int pageSize;

		// Token: 0x040014B7 RID: 5303
		private string pageItemReference;

		// Token: 0x040014B8 RID: 5304
		private SearchPageDirectionType pageDirection;
	}
}
