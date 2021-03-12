using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000421 RID: 1057
	[XmlType(TypeName = "FindItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(SeekToConditionPageView))]
	[KnownType(typeof(GroupByType))]
	[KnownType(typeof(DistinguishedGroupByType))]
	[KnownType(typeof(CalendarPageView))]
	[KnownType(typeof(ContactsPageView))]
	[KnownType(typeof(IndexedPageView))]
	[KnownType(typeof(FractionalPageView))]
	[Serializable]
	public class FindItemRequest : BaseRequest
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x000A043E File Offset: 0x0009E63E
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x000A0446 File Offset: 0x0009E646
		[DataMember(Name = "ItemShape", IsRequired = true, Order = 0)]
		public ItemResponseShape ItemShape { get; set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x000A044F File Offset: 0x0009E64F
		// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x000A0457 File Offset: 0x0009E657
		[XmlIgnore]
		[DataMember(Name = "ShapeName", IsRequired = false)]
		public string ShapeName { get; set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x000A0460 File Offset: 0x0009E660
		// (set) Token: 0x06001EB2 RID: 7858 RVA: 0x000A0468 File Offset: 0x0009E668
		[XmlElement("SeekToConditionPageItemView", typeof(SeekToConditionPageView))]
		[XmlElement("CalendarView", typeof(CalendarPageView))]
		[DataMember(Name = "Paging", IsRequired = false, Order = 1)]
		[XmlElement("IndexedPageItemView", typeof(IndexedPageView))]
		[XmlElement("FractionalPageItemView", typeof(FractionalPageView))]
		[XmlElement("ContactsView", typeof(ContactsPageView))]
		public BasePagingType Paging { get; set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x000A0471 File Offset: 0x0009E671
		// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x000A048C File Offset: 0x0009E68C
		[DataMember(Name = "Grouping", IsRequired = false, Order = 2)]
		[XmlElement("DistinguishedGroupBy", typeof(DistinguishedGroupByType))]
		[XmlElement("GroupBy", typeof(GroupByType))]
		public BaseGroupByType Grouping
		{
			get
			{
				if (this.grouping == null)
				{
					this.grouping = new NoGrouping();
				}
				return this.grouping;
			}
			set
			{
				this.grouping = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x000A0495 File Offset: 0x0009E695
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x000A049D File Offset: 0x0009E69D
		[XmlElement("Restriction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "Restriction", IsRequired = false, Order = 3)]
		public RestrictionType Restriction { get; set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x000A04A6 File Offset: 0x0009E6A6
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x000A04AE File Offset: 0x0009E6AE
		[XmlArrayItem("FieldOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(Name = "SortOrder", IsRequired = false, Order = 4)]
		public SortResults[] SortOrder { get; set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x000A04B7 File Offset: 0x0009E6B7
		// (set) Token: 0x06001EBA RID: 7866 RVA: 0x000A04BF File Offset: 0x0009E6BF
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray("ParentFolderIds")]
		[XmlArrayItem("FolderId", typeof(FolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "ParentFolderIds", IsRequired = true, Order = 5)]
		public BaseFolderId[] ParentFolderIds { get; set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x000A04C8 File Offset: 0x0009E6C8
		// (set) Token: 0x06001EBC RID: 7868 RVA: 0x000A04D0 File Offset: 0x0009E6D0
		[DataMember(Name = "QueryString", IsRequired = false, Order = 6)]
		public QueryStringType QueryString { get; set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x000A04D9 File Offset: 0x0009E6D9
		// (set) Token: 0x06001EBE RID: 7870 RVA: 0x000A04E1 File Offset: 0x0009E6E1
		[IgnoreDataMember]
		[XmlAttribute]
		public ItemQueryTraversal Traversal { get; set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x000A04EA File Offset: 0x0009E6EA
		// (set) Token: 0x06001EC0 RID: 7872 RVA: 0x000A04F7 File Offset: 0x0009E6F7
		[XmlIgnore]
		[DataMember(Name = "Traversal", IsRequired = true, Order = 7)]
		public string TraversalString
		{
			get
			{
				return EnumUtilities.ToString<ItemQueryTraversal>(this.Traversal);
			}
			set
			{
				this.Traversal = EnumUtilities.Parse<ItemQueryTraversal>(value);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x000A0505 File Offset: 0x0009E705
		// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x000A050D File Offset: 0x0009E70D
		[IgnoreDataMember]
		[XmlIgnore]
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

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x000A0516 File Offset: 0x0009E716
		// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x000A0523 File Offset: 0x0009E723
		[XmlIgnore]
		[DataMember(Name = "ViewFilter", IsRequired = false, Order = 8)]
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

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x000A0531 File Offset: 0x0009E731
		// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x000A0539 File Offset: 0x0009E739
		[XmlIgnore]
		[DataMember(Name = "SearchFolderIdentity", IsRequired = false)]
		public string SearchFolderIdentity { get; set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x000A0542 File Offset: 0x0009E742
		// (set) Token: 0x06001EC8 RID: 7880 RVA: 0x000A054A File Offset: 0x0009E74A
		[XmlIgnore]
		[DataMember(Name = "SearchFolderId", IsRequired = false)]
		public BaseFolderId SearchFolderId { get; set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x000A0553 File Offset: 0x0009E753
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x000A055B File Offset: 0x0009E75B
		[DataMember(Name = "RefinerRestriction", IsRequired = false)]
		[XmlIgnore]
		public RestrictionType RefinerRestriction { get; set; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x000A0564 File Offset: 0x0009E764
		// (set) Token: 0x06001ECC RID: 7884 RVA: 0x000A056C File Offset: 0x0009E76C
		[DataMember(Name = "IsWarmUpSearch", IsRequired = false)]
		[XmlIgnore]
		public bool IsWarmUpSearch { get; set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x000A0575 File Offset: 0x0009E775
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x000A057D File Offset: 0x0009E77D
		[DataMember(Name = "FromFilter", IsRequired = false)]
		[XmlIgnore]
		public string FromFilter { get; set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x000A0586 File Offset: 0x0009E786
		// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x000A058E File Offset: 0x0009E78E
		[IgnoreDataMember]
		[XmlIgnore]
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

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x000A0597 File Offset: 0x0009E797
		// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x000A05A4 File Offset: 0x0009E7A4
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

		// Token: 0x06001ED3 RID: 7891 RVA: 0x000A05B2 File Offset: 0x0009E7B2
		protected override List<ServiceObjectId> GetAllIds()
		{
			return new List<ServiceObjectId>(this.ParentFolderIds);
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000A05BF File Offset: 0x0009E7BF
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new FindItem(callContext, this);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000A05C8 File Offset: 0x0009E7C8
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.ParentFolderIds == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderIdList(callContext, this.ParentFolderIds);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000A05E0 File Offset: 0x0009E7E0
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.ParentFolderIds == null || this.ParentFolderIds.Length < taskStep)
			{
				return null;
			}
			return base.GetResourceKeysForFolderId(false, callContext, this.ParentFolderIds[taskStep]);
		}

		// Token: 0x04001393 RID: 5011
		internal const string ParentFolderIdsElementName = "ParentFolderIds";

		// Token: 0x04001394 RID: 5012
		private BaseGroupByType grouping;

		// Token: 0x04001395 RID: 5013
		private ViewFilter viewFilter;

		// Token: 0x04001396 RID: 5014
		private ClutterFilter clutterFilter;
	}
}
