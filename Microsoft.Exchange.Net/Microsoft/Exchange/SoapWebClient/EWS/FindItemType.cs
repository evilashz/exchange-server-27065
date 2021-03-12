using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200046A RID: 1130
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FindItemType : BaseRequestType
	{
		// Token: 0x0400174B RID: 5963
		public ItemResponseShapeType ItemShape;

		// Token: 0x0400174C RID: 5964
		[XmlElement("CalendarView", typeof(CalendarViewType))]
		[XmlElement("IndexedPageItemView", typeof(IndexedPageViewType))]
		[XmlElement("ContactsView", typeof(ContactsViewType))]
		[XmlElement("SeekToConditionPageItemView", typeof(SeekToConditionPageViewType))]
		[XmlElement("FractionalPageItemView", typeof(FractionalPageViewType))]
		public BasePagingType Item;

		// Token: 0x0400174D RID: 5965
		[XmlElement("GroupBy", typeof(GroupByType))]
		[XmlElement("DistinguishedGroupBy", typeof(DistinguishedGroupByType))]
		public BaseGroupByType Item1;

		// Token: 0x0400174E RID: 5966
		public RestrictionType Restriction;

		// Token: 0x0400174F RID: 5967
		[XmlArrayItem("FieldOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FieldOrderType[] SortOrder;

		// Token: 0x04001750 RID: 5968
		[XmlArrayItem("FolderId", typeof(FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderIdType[] ParentFolderIds;

		// Token: 0x04001751 RID: 5969
		public QueryStringType QueryString;

		// Token: 0x04001752 RID: 5970
		[XmlAttribute]
		public ItemQueryTraversalType Traversal;
	}
}
