using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000389 RID: 905
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FindItemType : BaseRequestType
	{
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x0002A0F7 File Offset: 0x000282F7
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x0002A0FF File Offset: 0x000282FF
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

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x0002A108 File Offset: 0x00028308
		// (set) Token: 0x06001C9A RID: 7322 RVA: 0x0002A110 File Offset: 0x00028310
		[XmlElement("CalendarView", typeof(CalendarViewType))]
		[XmlElement("SeekToConditionPageItemView", typeof(SeekToConditionPageViewType))]
		[XmlElement("ContactsView", typeof(ContactsViewType))]
		[XmlElement("FractionalPageItemView", typeof(FractionalPageViewType))]
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

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x0002A119 File Offset: 0x00028319
		// (set) Token: 0x06001C9C RID: 7324 RVA: 0x0002A121 File Offset: 0x00028321
		[XmlElement("GroupBy", typeof(GroupByType))]
		[XmlElement("DistinguishedGroupBy", typeof(DistinguishedGroupByType))]
		public BaseGroupByType Item1
		{
			get
			{
				return this.item1Field;
			}
			set
			{
				this.item1Field = value;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x0002A12A File Offset: 0x0002832A
		// (set) Token: 0x06001C9E RID: 7326 RVA: 0x0002A132 File Offset: 0x00028332
		public RestrictionType Restriction
		{
			get
			{
				return this.restrictionField;
			}
			set
			{
				this.restrictionField = value;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0002A13B File Offset: 0x0002833B
		// (set) Token: 0x06001CA0 RID: 7328 RVA: 0x0002A143 File Offset: 0x00028343
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

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x0002A14C File Offset: 0x0002834C
		// (set) Token: 0x06001CA2 RID: 7330 RVA: 0x0002A154 File Offset: 0x00028354
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderIdType[] ParentFolderIds
		{
			get
			{
				return this.parentFolderIdsField;
			}
			set
			{
				this.parentFolderIdsField = value;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x0002A15D File Offset: 0x0002835D
		// (set) Token: 0x06001CA4 RID: 7332 RVA: 0x0002A165 File Offset: 0x00028365
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

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x0002A16E File Offset: 0x0002836E
		// (set) Token: 0x06001CA6 RID: 7334 RVA: 0x0002A176 File Offset: 0x00028376
		[XmlAttribute]
		public ItemQueryTraversalType Traversal
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

		// Token: 0x040012F9 RID: 4857
		private ItemResponseShapeType itemShapeField;

		// Token: 0x040012FA RID: 4858
		private BasePagingType itemField;

		// Token: 0x040012FB RID: 4859
		private BaseGroupByType item1Field;

		// Token: 0x040012FC RID: 4860
		private RestrictionType restrictionField;

		// Token: 0x040012FD RID: 4861
		private FieldOrderType[] sortOrderField;

		// Token: 0x040012FE RID: 4862
		private BaseFolderIdType[] parentFolderIdsField;

		// Token: 0x040012FF RID: 4863
		private QueryStringType queryStringField;

		// Token: 0x04001300 RID: 4864
		private ItemQueryTraversalType traversalField;
	}
}
