using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000418 RID: 1048
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetConversationItemsType : BaseRequestType
	{
		// Token: 0x0400160C RID: 5644
		public ItemResponseShapeType ItemShape;

		// Token: 0x0400160D RID: 5645
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderIdType[] FoldersToIgnore;

		// Token: 0x0400160E RID: 5646
		public int MaxItemsToReturn;

		// Token: 0x0400160F RID: 5647
		[XmlIgnore]
		public bool MaxItemsToReturnSpecified;

		// Token: 0x04001610 RID: 5648
		public ConversationNodeSortOrder SortOrder;

		// Token: 0x04001611 RID: 5649
		[XmlIgnore]
		public bool SortOrderSpecified;

		// Token: 0x04001612 RID: 5650
		public MailboxSearchLocationType MailboxScope;

		// Token: 0x04001613 RID: 5651
		[XmlIgnore]
		public bool MailboxScopeSpecified;

		// Token: 0x04001614 RID: 5652
		[XmlArrayItem("Conversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ConversationRequestType[] Conversations;
	}
}
