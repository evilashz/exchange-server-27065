using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000337 RID: 823
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetConversationItemsType : BaseRequestType
	{
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x00028F56 File Offset: 0x00027156
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x00028F5E File Offset: 0x0002715E
		public ItemResponseShapeType ItemShape
		{
			get
			{
				return this.itemShapeField;
			}
			set
			{
				this.itemShapeField = value;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x00028F67 File Offset: 0x00027167
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x00028F6F File Offset: 0x0002716F
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderIdType[] FoldersToIgnore
		{
			get
			{
				return this.foldersToIgnoreField;
			}
			set
			{
				this.foldersToIgnoreField = value;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x00028F78 File Offset: 0x00027178
		// (set) Token: 0x06001A86 RID: 6790 RVA: 0x00028F80 File Offset: 0x00027180
		public int MaxItemsToReturn
		{
			get
			{
				return this.maxItemsToReturnField;
			}
			set
			{
				this.maxItemsToReturnField = value;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x00028F89 File Offset: 0x00027189
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x00028F91 File Offset: 0x00027191
		[XmlIgnore]
		public bool MaxItemsToReturnSpecified
		{
			get
			{
				return this.maxItemsToReturnFieldSpecified;
			}
			set
			{
				this.maxItemsToReturnFieldSpecified = value;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x00028F9A File Offset: 0x0002719A
		// (set) Token: 0x06001A8A RID: 6794 RVA: 0x00028FA2 File Offset: 0x000271A2
		public ConversationNodeSortOrder SortOrder
		{
			get
			{
				return this.sortOrderField;
			}
			set
			{
				this.sortOrderField = value;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x00028FAB File Offset: 0x000271AB
		// (set) Token: 0x06001A8C RID: 6796 RVA: 0x00028FB3 File Offset: 0x000271B3
		[XmlIgnore]
		public bool SortOrderSpecified
		{
			get
			{
				return this.sortOrderFieldSpecified;
			}
			set
			{
				this.sortOrderFieldSpecified = value;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x00028FBC File Offset: 0x000271BC
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x00028FC4 File Offset: 0x000271C4
		public MailboxSearchLocationType MailboxScope
		{
			get
			{
				return this.mailboxScopeField;
			}
			set
			{
				this.mailboxScopeField = value;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x00028FCD File Offset: 0x000271CD
		// (set) Token: 0x06001A90 RID: 6800 RVA: 0x00028FD5 File Offset: 0x000271D5
		[XmlIgnore]
		public bool MailboxScopeSpecified
		{
			get
			{
				return this.mailboxScopeFieldSpecified;
			}
			set
			{
				this.mailboxScopeFieldSpecified = value;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x00028FDE File Offset: 0x000271DE
		// (set) Token: 0x06001A92 RID: 6802 RVA: 0x00028FE6 File Offset: 0x000271E6
		[XmlArrayItem("Conversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ConversationRequestType[] Conversations
		{
			get
			{
				return this.conversationsField;
			}
			set
			{
				this.conversationsField = value;
			}
		}

		// Token: 0x040011BA RID: 4538
		private ItemResponseShapeType itemShapeField;

		// Token: 0x040011BB RID: 4539
		private BaseFolderIdType[] foldersToIgnoreField;

		// Token: 0x040011BC RID: 4540
		private int maxItemsToReturnField;

		// Token: 0x040011BD RID: 4541
		private bool maxItemsToReturnFieldSpecified;

		// Token: 0x040011BE RID: 4542
		private ConversationNodeSortOrder sortOrderField;

		// Token: 0x040011BF RID: 4543
		private bool sortOrderFieldSpecified;

		// Token: 0x040011C0 RID: 4544
		private MailboxSearchLocationType mailboxScopeField;

		// Token: 0x040011C1 RID: 4545
		private bool mailboxScopeFieldSpecified;

		// Token: 0x040011C2 RID: 4546
		private ConversationRequestType[] conversationsField;
	}
}
