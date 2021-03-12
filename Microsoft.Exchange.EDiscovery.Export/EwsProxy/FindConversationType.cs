using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000386 RID: 902
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class FindConversationType : BaseRequestType
	{
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0002A034 File Offset: 0x00028234
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x0002A03C File Offset: 0x0002823C
		[XmlElement("SeekToConditionPageItemView", typeof(SeekToConditionPageViewType))]
		[XmlElement("IndexedPageItemView", typeof(IndexedPageViewType))]
		public BasePagingType Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0002A045 File Offset: 0x00028245
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x0002A04D File Offset: 0x0002824D
		[XmlArrayItem("FieldOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FieldOrderType[] SortOrder
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

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x0002A056 File Offset: 0x00028256
		// (set) Token: 0x06001C85 RID: 7301 RVA: 0x0002A05E File Offset: 0x0002825E
		public TargetFolderIdType ParentFolderId
		{
			get
			{
				return this.parentFolderIdField;
			}
			set
			{
				this.parentFolderIdField = value;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x0002A067 File Offset: 0x00028267
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x0002A06F File Offset: 0x0002826F
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

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x0002A078 File Offset: 0x00028278
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x0002A080 File Offset: 0x00028280
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

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0002A089 File Offset: 0x00028289
		// (set) Token: 0x06001C8B RID: 7307 RVA: 0x0002A091 File Offset: 0x00028291
		public QueryStringType QueryString
		{
			get
			{
				return this.queryStringField;
			}
			set
			{
				this.queryStringField = value;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x0002A09A File Offset: 0x0002829A
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x0002A0A2 File Offset: 0x000282A2
		public ConversationResponseShapeType ConversationShape
		{
			get
			{
				return this.conversationShapeField;
			}
			set
			{
				this.conversationShapeField = value;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x0002A0AB File Offset: 0x000282AB
		// (set) Token: 0x06001C8F RID: 7311 RVA: 0x0002A0B3 File Offset: 0x000282B3
		[XmlAttribute]
		public ConversationQueryTraversalType Traversal
		{
			get
			{
				return this.traversalField;
			}
			set
			{
				this.traversalField = value;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x0002A0BC File Offset: 0x000282BC
		// (set) Token: 0x06001C91 RID: 7313 RVA: 0x0002A0C4 File Offset: 0x000282C4
		[XmlIgnore]
		public bool TraversalSpecified
		{
			get
			{
				return this.traversalFieldSpecified;
			}
			set
			{
				this.traversalFieldSpecified = value;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x0002A0CD File Offset: 0x000282CD
		// (set) Token: 0x06001C93 RID: 7315 RVA: 0x0002A0D5 File Offset: 0x000282D5
		[XmlAttribute]
		public ViewFilterType ViewFilter
		{
			get
			{
				return this.viewFilterField;
			}
			set
			{
				this.viewFilterField = value;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0002A0DE File Offset: 0x000282DE
		// (set) Token: 0x06001C95 RID: 7317 RVA: 0x0002A0E6 File Offset: 0x000282E6
		[XmlIgnore]
		public bool ViewFilterSpecified
		{
			get
			{
				return this.viewFilterFieldSpecified;
			}
			set
			{
				this.viewFilterFieldSpecified = value;
			}
		}

		// Token: 0x040012E0 RID: 4832
		private BasePagingType itemField;

		// Token: 0x040012E1 RID: 4833
		private FieldOrderType[] sortOrderField;

		// Token: 0x040012E2 RID: 4834
		private TargetFolderIdType parentFolderIdField;

		// Token: 0x040012E3 RID: 4835
		private MailboxSearchLocationType mailboxScopeField;

		// Token: 0x040012E4 RID: 4836
		private bool mailboxScopeFieldSpecified;

		// Token: 0x040012E5 RID: 4837
		private QueryStringType queryStringField;

		// Token: 0x040012E6 RID: 4838
		private ConversationResponseShapeType conversationShapeField;

		// Token: 0x040012E7 RID: 4839
		private ConversationQueryTraversalType traversalField;

		// Token: 0x040012E8 RID: 4840
		private bool traversalFieldSpecified;

		// Token: 0x040012E9 RID: 4841
		private ViewFilterType viewFilterField;

		// Token: 0x040012EA RID: 4842
		private bool viewFilterFieldSpecified;
	}
}
