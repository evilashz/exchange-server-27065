using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200042F RID: 1071
	[KnownType(typeof(GetConversationItemsDiagnosticsRequest))]
	[XmlType(TypeName = "GetConversationItemsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(GetThreadedConversationItemsRequest))]
	[Serializable]
	public class GetConversationItemsRequest : BaseRequest
	{
		// Token: 0x06001F5D RID: 8029 RVA: 0x000A0B8C File Offset: 0x0009ED8C
		public GetConversationItemsRequest()
		{
			this.Init();
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x000A0B9A File Offset: 0x0009ED9A
		private void Init()
		{
			this.ItemShape = new ItemResponseShape();
			this.maxItemsToReturn = 100;
			this.returnSubmittedItems = false;
			this.returnModernConversationItems = false;
			this.SortOrder = ConversationNodeSortOrder.DateOrderDescending;
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x000A0BC4 File Offset: 0x0009EDC4
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x000A0BCC File Offset: 0x0009EDCC
		[DataMember(Name = "ItemShape", IsRequired = false)]
		[XmlElement]
		public ItemResponseShape ItemShape { get; set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x000A0BD5 File Offset: 0x0009EDD5
		// (set) Token: 0x06001F62 RID: 8034 RVA: 0x000A0BDD File Offset: 0x0009EDDD
		[XmlIgnore]
		[DataMember(Name = "ShapeName", IsRequired = false)]
		public string ShapeName { get; set; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x000A0BE6 File Offset: 0x0009EDE6
		// (set) Token: 0x06001F64 RID: 8036 RVA: 0x000A0BEE File Offset: 0x0009EDEE
		[DataMember(Name = "FoldersToIgnore", IsRequired = false)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderId[] FoldersToIgnore { get; set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x000A0BF7 File Offset: 0x0009EDF7
		// (set) Token: 0x06001F66 RID: 8038 RVA: 0x000A0BFF File Offset: 0x0009EDFF
		[XmlElement]
		[IgnoreDataMember]
		public ConversationNodeSortOrder SortOrder { get; set; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x000A0C08 File Offset: 0x0009EE08
		// (set) Token: 0x06001F68 RID: 8040 RVA: 0x000A0C15 File Offset: 0x0009EE15
		[XmlIgnore]
		[DataMember(Name = "SortOrder", IsRequired = false)]
		public string SortOrderString
		{
			get
			{
				return EnumUtilities.ToString<ConversationNodeSortOrder>(this.SortOrder);
			}
			set
			{
				this.SortOrder = EnumUtilities.Parse<ConversationNodeSortOrder>(value);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x000A0C23 File Offset: 0x0009EE23
		// (set) Token: 0x06001F6A RID: 8042 RVA: 0x000A0C2B File Offset: 0x0009EE2B
		[XmlIgnore]
		[DataMember(Name = "ReturnSubmittedItems", IsRequired = false)]
		public bool ReturnSubmittedItems
		{
			get
			{
				return this.returnSubmittedItems;
			}
			set
			{
				this.returnSubmittedItems = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x000A0C34 File Offset: 0x0009EE34
		// (set) Token: 0x06001F6C RID: 8044 RVA: 0x000A0C3C File Offset: 0x0009EE3C
		[DataMember(Name = "MaxItemsToReturn", IsRequired = false)]
		[XmlElement]
		public int MaxItemsToReturn
		{
			get
			{
				return this.maxItemsToReturn;
			}
			set
			{
				this.maxItemsToReturn = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x000A0C45 File Offset: 0x0009EE45
		// (set) Token: 0x06001F6E RID: 8046 RVA: 0x000A0C4D File Offset: 0x0009EE4D
		[IgnoreDataMember]
		[XmlIgnore]
		public bool MailboxScopeSpecified { get; set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x000A0C56 File Offset: 0x0009EE56
		// (set) Token: 0x06001F70 RID: 8048 RVA: 0x000A0C5E File Offset: 0x0009EE5E
		[IgnoreDataMember]
		[XmlElement]
		public MailboxSearchLocation MailboxScope
		{
			get
			{
				return this.mailboxScope;
			}
			set
			{
				this.mailboxScope = value;
				this.MailboxScopeSpecified = true;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x000A0C6E File Offset: 0x0009EE6E
		// (set) Token: 0x06001F72 RID: 8050 RVA: 0x000A0C85 File Offset: 0x0009EE85
		[DataMember(Name = "MailboxScope", IsRequired = false)]
		[XmlIgnore]
		public string MailboxScopeString
		{
			get
			{
				if (!this.MailboxScopeSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<MailboxSearchLocation>(this.mailboxScope);
			}
			set
			{
				this.MailboxScope = EnumUtilities.Parse<MailboxSearchLocation>(value);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x000A0C93 File Offset: 0x0009EE93
		// (set) Token: 0x06001F74 RID: 8052 RVA: 0x000A0C9B File Offset: 0x0009EE9B
		[XmlArrayItem("Conversation", typeof(ConversationRequestType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "Conversations", IsRequired = true)]
		public ConversationRequestType[] Conversations { get; set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x000A0CA4 File Offset: 0x0009EEA4
		// (set) Token: 0x06001F76 RID: 8054 RVA: 0x000A0CAC File Offset: 0x0009EEAC
		[DataMember(Name = "ReturnModernConversationItems", IsRequired = false)]
		[XmlIgnore]
		public bool ReturnModernConversationItems
		{
			get
			{
				return this.returnModernConversationItems;
			}
			set
			{
				this.returnModernConversationItems = value;
			}
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x000A0CB5 File Offset: 0x0009EEB5
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			if (this.ReturnModernConversationItems)
			{
				return new GetModernConversationItems(callContext, this);
			}
			return new GetConversationItems(callContext, this);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000A0CCE File Offset: 0x0009EECE
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemId(callContext, this.Conversations[0].ConversationId);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x000A0CE3 File Offset: 0x0009EEE3
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.Conversations == null || taskStep < 0 || taskStep >= this.Conversations.Length)
			{
				return null;
			}
			return base.GetResourceKeysForItemId(false, callContext, this.Conversations[taskStep].ConversationId);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000A0D14 File Offset: 0x0009EF14
		internal override void Validate()
		{
			base.Validate();
			if (this.FoldersToIgnore != null)
			{
				BaseFolderId[] foldersToIgnore = this.FoldersToIgnore;
				for (int i = 0; i < foldersToIgnore.Length; i++)
				{
					if (foldersToIgnore[i] == null)
					{
						throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidFolderId);
					}
				}
			}
			if (this.ReturnModernConversationItems && this.SortOrder != ConversationNodeSortOrder.DateOrderAscending && this.SortOrder != ConversationNodeSortOrder.DateOrderDescending)
			{
				throw new ServiceInvalidOperationException(CoreResources.ErrorInvalidParameter(this.SortOrder.ToString()), new ArgumentException("Only chronological sort orders are valid."));
			}
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000A0D9A File Offset: 0x0009EF9A
		[OnDeserializing]
		private void Init(StreamingContext context)
		{
			this.Init();
		}

		// Token: 0x040013DC RID: 5084
		private const int DefaultMaxItemsToReturn = 100;

		// Token: 0x040013DD RID: 5085
		private int maxItemsToReturn;

		// Token: 0x040013DE RID: 5086
		private bool returnSubmittedItems;

		// Token: 0x040013DF RID: 5087
		private bool returnModernConversationItems;

		// Token: 0x040013E0 RID: 5088
		private MailboxSearchLocation mailboxScope;
	}
}
