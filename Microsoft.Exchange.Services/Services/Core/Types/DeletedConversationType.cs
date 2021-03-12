using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000566 RID: 1382
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "DeletedConversation")]
	[Serializable]
	public class DeletedConversationType
	{
		// Token: 0x060026AB RID: 9899 RVA: 0x000A686D File Offset: 0x000A4A6D
		public DeletedConversationType()
		{
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000A6875 File Offset: 0x000A4A75
		public DeletedConversationType(ItemId convItemId, FolderId folderId)
		{
			this.ConversationId = convItemId;
			this.FolderId = folderId;
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x000A688B File Offset: 0x000A4A8B
		// (set) Token: 0x060026AE RID: 9902 RVA: 0x000A6893 File Offset: 0x000A4A93
		[DataMember(IsRequired = true, Order = 1)]
		[XmlElement("ConversationId")]
		public ItemId ConversationId { get; set; }

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x000A689C File Offset: 0x000A4A9C
		// (set) Token: 0x060026B0 RID: 9904 RVA: 0x000A68A4 File Offset: 0x000A4AA4
		[DataMember(IsRequired = true, Order = 2)]
		[XmlElement("FolderId")]
		public BaseFolderId FolderId { get; set; }
	}
}
