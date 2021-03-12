using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000467 RID: 1127
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class FindConversationType : BaseRequestType
	{
		// Token: 0x04001732 RID: 5938
		[XmlElement("SeekToConditionPageItemView", typeof(SeekToConditionPageViewType))]
		[XmlElement("IndexedPageItemView", typeof(IndexedPageViewType))]
		public BasePagingType Item;

		// Token: 0x04001733 RID: 5939
		[XmlArrayItem("FieldOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FieldOrderType[] SortOrder;

		// Token: 0x04001734 RID: 5940
		public TargetFolderIdType ParentFolderId;

		// Token: 0x04001735 RID: 5941
		public MailboxSearchLocationType MailboxScope;

		// Token: 0x04001736 RID: 5942
		[XmlIgnore]
		public bool MailboxScopeSpecified;

		// Token: 0x04001737 RID: 5943
		public QueryStringType QueryString;

		// Token: 0x04001738 RID: 5944
		public ConversationResponseShapeType ConversationShape;

		// Token: 0x04001739 RID: 5945
		[XmlAttribute]
		public ConversationQueryTraversalType Traversal;

		// Token: 0x0400173A RID: 5946
		[XmlIgnore]
		public bool TraversalSpecified;

		// Token: 0x0400173B RID: 5947
		[XmlAttribute]
		public ViewFilterType ViewFilter;

		// Token: 0x0400173C RID: 5948
		[XmlIgnore]
		public bool ViewFilterSpecified;
	}
}
