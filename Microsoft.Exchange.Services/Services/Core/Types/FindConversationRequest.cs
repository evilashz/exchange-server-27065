using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200041F RID: 1055
	[XmlType(TypeName = "FindConversationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[KnownType(typeof(IndexedPageView))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(SeekToConditionPageView))]
	[Serializable]
	public class FindConversationRequest : BaseRequest
	{
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0009FFA0 File Offset: 0x0009E1A0
		// (set) Token: 0x06001E59 RID: 7769 RVA: 0x0009FFA8 File Offset: 0x0009E1A8
		[XmlElement("SeekToConditionPageItemView", typeof(SeekToConditionPageView))]
		[DataMember(Name = "Paging", IsRequired = false)]
		[XmlElement("IndexedPageItemView", typeof(IndexedPageView))]
		public BasePagingType Paging { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0009FFB1 File Offset: 0x0009E1B1
		// (set) Token: 0x06001E5B RID: 7771 RVA: 0x0009FFB9 File Offset: 0x0009E1B9
		[DataMember(Name = "SortOrder", IsRequired = false)]
		[XmlArrayItem("FieldOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public SortResults[] SortOrder { get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0009FFC2 File Offset: 0x0009E1C2
		// (set) Token: 0x06001E5D RID: 7773 RVA: 0x0009FFCA File Offset: 0x0009E1CA
		[DataMember(Name = "ParentFolderId", IsRequired = true)]
		[XmlElement("ParentFolderId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public TargetFolderId ParentFolderId { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x0009FFD3 File Offset: 0x0009E1D3
		// (set) Token: 0x06001E5F RID: 7775 RVA: 0x0009FFDB File Offset: 0x0009E1DB
		[XmlIgnore]
		[IgnoreDataMember]
		public bool MailboxScopeSpecified { get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x0009FFE4 File Offset: 0x0009E1E4
		// (set) Token: 0x06001E61 RID: 7777 RVA: 0x0009FFEC File Offset: 0x0009E1EC
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

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0009FFFC File Offset: 0x0009E1FC
		// (set) Token: 0x06001E63 RID: 7779 RVA: 0x000A0013 File Offset: 0x0009E213
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x000A0021 File Offset: 0x0009E221
		// (set) Token: 0x06001E65 RID: 7781 RVA: 0x000A0029 File Offset: 0x0009E229
		[IgnoreDataMember]
		[XmlAttribute]
		public ConversationQueryTraversal Traversal
		{
			get
			{
				return this.traversal;
			}
			set
			{
				this.traversal = value;
				this.TraversalSpecified = true;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x000A0039 File Offset: 0x0009E239
		// (set) Token: 0x06001E67 RID: 7783 RVA: 0x000A0041 File Offset: 0x0009E241
		[IgnoreDataMember]
		[XmlIgnore]
		public bool TraversalSpecified { get; set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x000A004A File Offset: 0x0009E24A
		// (set) Token: 0x06001E69 RID: 7785 RVA: 0x000A0061 File Offset: 0x0009E261
		[DataMember(Name = "Traversal", IsRequired = false, EmitDefaultValue = false)]
		[XmlIgnore]
		public string TraversalString
		{
			get
			{
				if (!this.TraversalSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<ConversationQueryTraversal>(this.Traversal);
			}
			set
			{
				this.Traversal = EnumUtilities.Parse<ConversationQueryTraversal>(value);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x000A006F File Offset: 0x0009E26F
		// (set) Token: 0x06001E6B RID: 7787 RVA: 0x000A0077 File Offset: 0x0009E277
		[IgnoreDataMember]
		[XmlAttribute]
		public ViewFilter ViewFilter
		{
			get
			{
				return this.viewFilter;
			}
			set
			{
				this.viewFilter = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x000A0080 File Offset: 0x0009E280
		// (set) Token: 0x06001E6D RID: 7789 RVA: 0x000A008D File Offset: 0x0009E28D
		[DataMember(Name = "ViewFilter", IsRequired = false)]
		[XmlIgnore]
		public string ViewFilterString
		{
			get
			{
				return EnumUtilities.ToString<ViewFilter>(this.ViewFilter);
			}
			set
			{
				this.ViewFilter = EnumUtilities.Parse<ViewFilter>(value);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x000A009B File Offset: 0x0009E29B
		// (set) Token: 0x06001E6F RID: 7791 RVA: 0x000A00A3 File Offset: 0x0009E2A3
		[DataMember(Name = "QueryString", IsRequired = false)]
		[XmlElement("QueryString", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public QueryStringType QueryString { get; set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x000A00AC File Offset: 0x0009E2AC
		// (set) Token: 0x06001E71 RID: 7793 RVA: 0x000A00B4 File Offset: 0x0009E2B4
		[XmlElement]
		[DataMember(Name = "ConversationShape", IsRequired = false)]
		public ConversationResponseShape ConversationShape { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000A00BD File Offset: 0x0009E2BD
		// (set) Token: 0x06001E73 RID: 7795 RVA: 0x000A00C5 File Offset: 0x0009E2C5
		[DataMember(Name = "ShapeName", IsRequired = false)]
		[XmlIgnore]
		public string ShapeName { get; set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x000A00CE File Offset: 0x0009E2CE
		// (set) Token: 0x06001E75 RID: 7797 RVA: 0x000A00D6 File Offset: 0x0009E2D6
		[XmlIgnore]
		[DataMember(Name = "SearchFolderIdentity", IsRequired = false)]
		public string SearchFolderIdentity { get; set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x000A00DF File Offset: 0x0009E2DF
		// (set) Token: 0x06001E77 RID: 7799 RVA: 0x000A00E7 File Offset: 0x0009E2E7
		[XmlIgnore]
		[DataMember(Name = "SearchFolderId", IsRequired = false)]
		public BaseFolderId SearchFolderId { get; set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x000A00F0 File Offset: 0x0009E2F0
		// (set) Token: 0x06001E79 RID: 7801 RVA: 0x000A00F8 File Offset: 0x0009E2F8
		[DataMember(Name = "RefinerRestriction", IsRequired = false)]
		[XmlIgnore]
		public RestrictionType RefinerRestriction { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x000A0101 File Offset: 0x0009E301
		// (set) Token: 0x06001E7B RID: 7803 RVA: 0x000A0109 File Offset: 0x0009E309
		[DataMember(Name = "FromFilter", IsRequired = false)]
		[XmlIgnore]
		public string FromFilter { get; set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x000A0112 File Offset: 0x0009E312
		// (set) Token: 0x06001E7D RID: 7805 RVA: 0x000A011A File Offset: 0x0009E31A
		[XmlIgnore]
		[IgnoreDataMember]
		public ClutterFilter ClutterFilter
		{
			get
			{
				return this.clutterFilter;
			}
			set
			{
				this.clutterFilter = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x000A0123 File Offset: 0x0009E323
		// (set) Token: 0x06001E7F RID: 7807 RVA: 0x000A0130 File Offset: 0x0009E330
		[XmlIgnore]
		[DataMember(Name = "ClutterFilter", IsRequired = false)]
		public string ClutterFilterString
		{
			get
			{
				return EnumUtilities.ToString<ClutterFilter>(this.ClutterFilter);
			}
			set
			{
				this.ClutterFilter = EnumUtilities.Parse<ClutterFilter>(value);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x000A013E File Offset: 0x0009E33E
		// (set) Token: 0x06001E81 RID: 7809 RVA: 0x000A0146 File Offset: 0x0009E346
		internal Guid[] MailboxGuids { get; set; }

		// Token: 0x06001E82 RID: 7810 RVA: 0x000A014F File Offset: 0x0009E34F
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new FindConversation(callContext, this);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x000A0158 File Offset: 0x0009E358
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForFolderId(callContext, this.ParentFolderId.BaseFolderId);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x000A016B File Offset: 0x0009E36B
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x04001376 RID: 4982
		private ConversationQueryTraversal traversal;

		// Token: 0x04001377 RID: 4983
		private ViewFilter viewFilter;

		// Token: 0x04001378 RID: 4984
		private ClutterFilter clutterFilter;

		// Token: 0x04001379 RID: 4985
		private MailboxSearchLocation mailboxScope;
	}
}
