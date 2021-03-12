using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000483 RID: 1155
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FindFolderType : BaseRequestType
	{
		// Token: 0x0400179F RID: 6047
		public FolderResponseShapeType FolderShape;

		// Token: 0x040017A0 RID: 6048
		[XmlElement("FractionalPageFolderView", typeof(FractionalPageViewType))]
		[XmlElement("IndexedPageFolderView", typeof(IndexedPageViewType))]
		public BasePagingType Item;

		// Token: 0x040017A1 RID: 6049
		public RestrictionType Restriction;

		// Token: 0x040017A2 RID: 6050
		[XmlArrayItem("FolderId", typeof(FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderIdType[] ParentFolderIds;

		// Token: 0x040017A3 RID: 6051
		[XmlAttribute]
		public FolderQueryTraversalType Traversal;
	}
}
