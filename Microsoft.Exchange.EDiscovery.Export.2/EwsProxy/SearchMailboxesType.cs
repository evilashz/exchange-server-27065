using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000341 RID: 833
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SearchMailboxesType : BaseRequestType
	{
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x00029236 File Offset: 0x00027436
		// (set) Token: 0x06001AD9 RID: 6873 RVA: 0x0002923E File Offset: 0x0002743E
		[XmlArrayItem("MailboxQuery", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
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

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x00029247 File Offset: 0x00027447
		// (set) Token: 0x06001ADB RID: 6875 RVA: 0x0002924F File Offset: 0x0002744F
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

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x00029258 File Offset: 0x00027458
		// (set) Token: 0x06001ADD RID: 6877 RVA: 0x00029260 File Offset: 0x00027460
		public PreviewItemResponseShapeType PreviewItemResponseShape
		{
			get
			{
				return this.previewItemResponseShapeField;
			}
			set
			{
				this.previewItemResponseShapeField = value;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x00029269 File Offset: 0x00027469
		// (set) Token: 0x06001ADF RID: 6879 RVA: 0x00029271 File Offset: 0x00027471
		public FieldOrderType SortBy
		{
			get
			{
				return this.sortByField;
			}
			set
			{
				this.sortByField = value;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x0002927A File Offset: 0x0002747A
		// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x00029282 File Offset: 0x00027482
		public string Language
		{
			get
			{
				return this.languageField;
			}
			set
			{
				this.languageField = value;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x0002928B File Offset: 0x0002748B
		// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x00029293 File Offset: 0x00027493
		public bool Deduplication
		{
			get
			{
				return this.deduplicationField;
			}
			set
			{
				this.deduplicationField = value;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0002929C File Offset: 0x0002749C
		// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x000292A4 File Offset: 0x000274A4
		[XmlIgnore]
		public bool DeduplicationSpecified
		{
			get
			{
				return this.deduplicationFieldSpecified;
			}
			set
			{
				this.deduplicationFieldSpecified = value;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x000292AD File Offset: 0x000274AD
		// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x000292B5 File Offset: 0x000274B5
		public int PageSize
		{
			get
			{
				return this.pageSizeField;
			}
			set
			{
				this.pageSizeField = value;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x000292BE File Offset: 0x000274BE
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x000292C6 File Offset: 0x000274C6
		[XmlIgnore]
		public bool PageSizeSpecified
		{
			get
			{
				return this.pageSizeFieldSpecified;
			}
			set
			{
				this.pageSizeFieldSpecified = value;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x000292CF File Offset: 0x000274CF
		// (set) Token: 0x06001AEB RID: 6891 RVA: 0x000292D7 File Offset: 0x000274D7
		public string PageItemReference
		{
			get
			{
				return this.pageItemReferenceField;
			}
			set
			{
				this.pageItemReferenceField = value;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x000292E0 File Offset: 0x000274E0
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x000292E8 File Offset: 0x000274E8
		public SearchPageDirectionType PageDirection
		{
			get
			{
				return this.pageDirectionField;
			}
			set
			{
				this.pageDirectionField = value;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x000292F1 File Offset: 0x000274F1
		// (set) Token: 0x06001AEF RID: 6895 RVA: 0x000292F9 File Offset: 0x000274F9
		[XmlIgnore]
		public bool PageDirectionSpecified
		{
			get
			{
				return this.pageDirectionFieldSpecified;
			}
			set
			{
				this.pageDirectionFieldSpecified = value;
			}
		}

		// Token: 0x040011EE RID: 4590
		private MailboxQueryType[] searchQueriesField;

		// Token: 0x040011EF RID: 4591
		private SearchResultType resultTypeField;

		// Token: 0x040011F0 RID: 4592
		private PreviewItemResponseShapeType previewItemResponseShapeField;

		// Token: 0x040011F1 RID: 4593
		private FieldOrderType sortByField;

		// Token: 0x040011F2 RID: 4594
		private string languageField;

		// Token: 0x040011F3 RID: 4595
		private bool deduplicationField;

		// Token: 0x040011F4 RID: 4596
		private bool deduplicationFieldSpecified;

		// Token: 0x040011F5 RID: 4597
		private int pageSizeField;

		// Token: 0x040011F6 RID: 4598
		private bool pageSizeFieldSpecified;

		// Token: 0x040011F7 RID: 4599
		private string pageItemReferenceField;

		// Token: 0x040011F8 RID: 4600
		private SearchPageDirectionType pageDirectionField;

		// Token: 0x040011F9 RID: 4601
		private bool pageDirectionFieldSpecified;
	}
}
