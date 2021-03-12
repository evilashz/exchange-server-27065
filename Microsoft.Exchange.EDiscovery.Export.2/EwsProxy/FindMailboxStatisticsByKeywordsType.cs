using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000343 RID: 835
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class FindMailboxStatisticsByKeywordsType : BaseRequestType
	{
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x00029345 File Offset: 0x00027545
		// (set) Token: 0x06001AF9 RID: 6905 RVA: 0x0002934D File Offset: 0x0002754D
		[XmlArrayItem("UserMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public UserMailboxType[] Mailboxes
		{
			get
			{
				return this.mailboxesField;
			}
			set
			{
				this.mailboxesField = value;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x00029356 File Offset: 0x00027556
		// (set) Token: 0x06001AFB RID: 6907 RVA: 0x0002935E File Offset: 0x0002755E
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Keywords
		{
			get
			{
				return this.keywordsField;
			}
			set
			{
				this.keywordsField = value;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x00029367 File Offset: 0x00027567
		// (set) Token: 0x06001AFD RID: 6909 RVA: 0x0002936F File Offset: 0x0002756F
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

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x00029378 File Offset: 0x00027578
		// (set) Token: 0x06001AFF RID: 6911 RVA: 0x00029380 File Offset: 0x00027580
		[XmlArrayItem("SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Senders
		{
			get
			{
				return this.sendersField;
			}
			set
			{
				this.sendersField = value;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x00029389 File Offset: 0x00027589
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x00029391 File Offset: 0x00027591
		[XmlArrayItem("SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Recipients
		{
			get
			{
				return this.recipientsField;
			}
			set
			{
				this.recipientsField = value;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0002939A File Offset: 0x0002759A
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x000293A2 File Offset: 0x000275A2
		public DateTime FromDate
		{
			get
			{
				return this.fromDateField;
			}
			set
			{
				this.fromDateField = value;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x000293AB File Offset: 0x000275AB
		// (set) Token: 0x06001B05 RID: 6917 RVA: 0x000293B3 File Offset: 0x000275B3
		[XmlIgnore]
		public bool FromDateSpecified
		{
			get
			{
				return this.fromDateFieldSpecified;
			}
			set
			{
				this.fromDateFieldSpecified = value;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x000293BC File Offset: 0x000275BC
		// (set) Token: 0x06001B07 RID: 6919 RVA: 0x000293C4 File Offset: 0x000275C4
		public DateTime ToDate
		{
			get
			{
				return this.toDateField;
			}
			set
			{
				this.toDateField = value;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x000293CD File Offset: 0x000275CD
		// (set) Token: 0x06001B09 RID: 6921 RVA: 0x000293D5 File Offset: 0x000275D5
		[XmlIgnore]
		public bool ToDateSpecified
		{
			get
			{
				return this.toDateFieldSpecified;
			}
			set
			{
				this.toDateFieldSpecified = value;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x000293DE File Offset: 0x000275DE
		// (set) Token: 0x06001B0B RID: 6923 RVA: 0x000293E6 File Offset: 0x000275E6
		[XmlArrayItem("SearchItemKind", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public SearchItemKindType[] MessageTypes
		{
			get
			{
				return this.messageTypesField;
			}
			set
			{
				this.messageTypesField = value;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000293EF File Offset: 0x000275EF
		// (set) Token: 0x06001B0D RID: 6925 RVA: 0x000293F7 File Offset: 0x000275F7
		public bool SearchDumpster
		{
			get
			{
				return this.searchDumpsterField;
			}
			set
			{
				this.searchDumpsterField = value;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x00029400 File Offset: 0x00027600
		// (set) Token: 0x06001B0F RID: 6927 RVA: 0x00029408 File Offset: 0x00027608
		[XmlIgnore]
		public bool SearchDumpsterSpecified
		{
			get
			{
				return this.searchDumpsterFieldSpecified;
			}
			set
			{
				this.searchDumpsterFieldSpecified = value;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x00029411 File Offset: 0x00027611
		// (set) Token: 0x06001B11 RID: 6929 RVA: 0x00029419 File Offset: 0x00027619
		public bool IncludePersonalArchive
		{
			get
			{
				return this.includePersonalArchiveField;
			}
			set
			{
				this.includePersonalArchiveField = value;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x00029422 File Offset: 0x00027622
		// (set) Token: 0x06001B13 RID: 6931 RVA: 0x0002942A File Offset: 0x0002762A
		[XmlIgnore]
		public bool IncludePersonalArchiveSpecified
		{
			get
			{
				return this.includePersonalArchiveFieldSpecified;
			}
			set
			{
				this.includePersonalArchiveFieldSpecified = value;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x00029433 File Offset: 0x00027633
		// (set) Token: 0x06001B15 RID: 6933 RVA: 0x0002943B File Offset: 0x0002763B
		public bool IncludeUnsearchableItems
		{
			get
			{
				return this.includeUnsearchableItemsField;
			}
			set
			{
				this.includeUnsearchableItemsField = value;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x00029444 File Offset: 0x00027644
		// (set) Token: 0x06001B17 RID: 6935 RVA: 0x0002944C File Offset: 0x0002764C
		[XmlIgnore]
		public bool IncludeUnsearchableItemsSpecified
		{
			get
			{
				return this.includeUnsearchableItemsFieldSpecified;
			}
			set
			{
				this.includeUnsearchableItemsFieldSpecified = value;
			}
		}

		// Token: 0x040011FD RID: 4605
		private UserMailboxType[] mailboxesField;

		// Token: 0x040011FE RID: 4606
		private string[] keywordsField;

		// Token: 0x040011FF RID: 4607
		private string languageField;

		// Token: 0x04001200 RID: 4608
		private string[] sendersField;

		// Token: 0x04001201 RID: 4609
		private string[] recipientsField;

		// Token: 0x04001202 RID: 4610
		private DateTime fromDateField;

		// Token: 0x04001203 RID: 4611
		private bool fromDateFieldSpecified;

		// Token: 0x04001204 RID: 4612
		private DateTime toDateField;

		// Token: 0x04001205 RID: 4613
		private bool toDateFieldSpecified;

		// Token: 0x04001206 RID: 4614
		private SearchItemKindType[] messageTypesField;

		// Token: 0x04001207 RID: 4615
		private bool searchDumpsterField;

		// Token: 0x04001208 RID: 4616
		private bool searchDumpsterFieldSpecified;

		// Token: 0x04001209 RID: 4617
		private bool includePersonalArchiveField;

		// Token: 0x0400120A RID: 4618
		private bool includePersonalArchiveFieldSpecified;

		// Token: 0x0400120B RID: 4619
		private bool includeUnsearchableItemsField;

		// Token: 0x0400120C RID: 4620
		private bool includeUnsearchableItemsFieldSpecified;
	}
}
