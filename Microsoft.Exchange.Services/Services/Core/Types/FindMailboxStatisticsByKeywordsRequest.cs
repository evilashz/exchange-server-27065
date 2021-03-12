using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000422 RID: 1058
	[XmlType(TypeName = "FindMailboxStatisticsByKeywordsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FindMailboxStatisticsByKeywordsRequest : BaseRequest
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x000A060F File Offset: 0x0009E80F
		// (set) Token: 0x06001ED9 RID: 7897 RVA: 0x000A0617 File Offset: 0x0009E817
		[XmlArray(ElementName = "Mailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem(ElementName = "UserMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(UserMailbox))]
		public UserMailbox[] Mailboxes
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

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x000A0620 File Offset: 0x0009E820
		// (set) Token: 0x06001EDB RID: 7899 RVA: 0x000A0628 File Offset: 0x0009E828
		[XmlArray(ElementName = "Keywords", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem(ElementName = "String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
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

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x000A0631 File Offset: 0x0009E831
		// (set) Token: 0x06001EDD RID: 7901 RVA: 0x000A0639 File Offset: 0x0009E839
		[XmlElement("Language")]
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

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x000A0642 File Offset: 0x0009E842
		// (set) Token: 0x06001EDF RID: 7903 RVA: 0x000A064A File Offset: 0x0009E84A
		[XmlArrayItem(ElementName = "SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[XmlArray(ElementName = "Senders", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
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

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x000A0653 File Offset: 0x0009E853
		// (set) Token: 0x06001EE1 RID: 7905 RVA: 0x000A065B File Offset: 0x0009E85B
		[XmlArray(ElementName = "Recipients", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem(ElementName = "SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
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

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x000A0664 File Offset: 0x0009E864
		// (set) Token: 0x06001EE3 RID: 7907 RVA: 0x000A066C File Offset: 0x0009E86C
		[XmlElement("FromDate")]
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

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x000A0675 File Offset: 0x0009E875
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x000A067D File Offset: 0x0009E87D
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

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x000A0686 File Offset: 0x0009E886
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x000A068E File Offset: 0x0009E88E
		[XmlElement("ToDate")]
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

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x000A0697 File Offset: 0x0009E897
		// (set) Token: 0x06001EE9 RID: 7913 RVA: 0x000A069F File Offset: 0x0009E89F
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

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x000A06A8 File Offset: 0x0009E8A8
		// (set) Token: 0x06001EEB RID: 7915 RVA: 0x000A06B0 File Offset: 0x0009E8B0
		[XmlArrayItem(ElementName = "SearchItemKind", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(SearchItemKind))]
		[XmlArray(ElementName = "MessageTypes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SearchItemKind[] MessageTypes
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

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x000A06B9 File Offset: 0x0009E8B9
		// (set) Token: 0x06001EED RID: 7917 RVA: 0x000A06C1 File Offset: 0x0009E8C1
		[XmlElement("SearchDumpster")]
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

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x000A06CA File Offset: 0x0009E8CA
		// (set) Token: 0x06001EEF RID: 7919 RVA: 0x000A06D2 File Offset: 0x0009E8D2
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

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x000A06DB File Offset: 0x0009E8DB
		// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x000A06E3 File Offset: 0x0009E8E3
		[XmlElement("IncludePersonalArchive")]
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

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x000A06EC File Offset: 0x0009E8EC
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x000A06F4 File Offset: 0x0009E8F4
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

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x000A06FD File Offset: 0x0009E8FD
		// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x000A0705 File Offset: 0x0009E905
		[XmlElement("IncludeUnsearchableItems")]
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

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x000A070E File Offset: 0x0009E90E
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x000A0716 File Offset: 0x0009E916
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

		// Token: 0x06001EF8 RID: 7928 RVA: 0x000A071F File Offset: 0x0009E91F
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new FindMailboxStatisticsByKeywords(callContext, this);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000A0728 File Offset: 0x0009E928
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.Mailboxes == null || this.Mailboxes.Length == 0)
			{
				return null;
			}
			string id = this.Mailboxes[0].Id;
			MailboxId mailboxId;
			if (this.Mailboxes[0].IsArchive)
			{
				mailboxId = new MailboxId(new Guid(id), true);
			}
			else
			{
				mailboxId = new MailboxId(id);
			}
			return MailboxIdServerInfo.Create(mailboxId);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x000A0782 File Offset: 0x0009E982
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x040013A4 RID: 5028
		private UserMailbox[] mailboxesField;

		// Token: 0x040013A5 RID: 5029
		private string[] keywordsField;

		// Token: 0x040013A6 RID: 5030
		private string languageField;

		// Token: 0x040013A7 RID: 5031
		private string[] sendersField;

		// Token: 0x040013A8 RID: 5032
		private string[] recipientsField;

		// Token: 0x040013A9 RID: 5033
		private DateTime fromDateField;

		// Token: 0x040013AA RID: 5034
		private bool fromDateFieldSpecified;

		// Token: 0x040013AB RID: 5035
		private DateTime toDateField;

		// Token: 0x040013AC RID: 5036
		private bool toDateFieldSpecified;

		// Token: 0x040013AD RID: 5037
		private SearchItemKind[] messageTypesField;

		// Token: 0x040013AE RID: 5038
		private bool searchDumpsterField;

		// Token: 0x040013AF RID: 5039
		private bool searchDumpsterFieldSpecified;

		// Token: 0x040013B0 RID: 5040
		private bool includePersonalArchiveField;

		// Token: 0x040013B1 RID: 5041
		private bool includePersonalArchiveFieldSpecified;

		// Token: 0x040013B2 RID: 5042
		private bool includeUnsearchableItemsField;

		// Token: 0x040013B3 RID: 5043
		private bool includeUnsearchableItemsFieldSpecified;
	}
}
