using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000565 RID: 1381
	[XmlType("SyncConversationResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncConversationResponseMessage : ResponseMessage
	{
		// Token: 0x0600269E RID: 9886 RVA: 0x000A6802 File Offset: 0x000A4A02
		public SyncConversationResponseMessage()
		{
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000A680A File Offset: 0x000A4A0A
		internal SyncConversationResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000A6814 File Offset: 0x000A4A14
		public override ResponseType GetResponseType()
		{
			return ResponseType.SyncConversationResponseMessage;
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x000A6818 File Offset: 0x000A4A18
		// (set) Token: 0x060026A2 RID: 9890 RVA: 0x000A6820 File Offset: 0x000A4A20
		[DataMember(Name = "SyncState", IsRequired = true, Order = 1)]
		[XmlElement("SyncState", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string SyncState { get; set; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x000A6829 File Offset: 0x000A4A29
		// (set) Token: 0x060026A4 RID: 9892 RVA: 0x000A6831 File Offset: 0x000A4A31
		[DataMember(Name = "IncludesLastItemInRange", IsRequired = true, EmitDefaultValue = true, Order = 2)]
		[XmlElement("IncludesLastItemInRange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public bool IncludesLastItemInRange { get; set; }

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x000A683A File Offset: 0x000A4A3A
		// (set) Token: 0x060026A6 RID: 9894 RVA: 0x000A6842 File Offset: 0x000A4A42
		[DataMember(Order = 3)]
		[XmlArray(ElementName = "Conversations")]
		[XmlArrayItem(ElementName = "Conversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public ConversationType[] Conversations { get; set; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x000A684B File Offset: 0x000A4A4B
		// (set) Token: 0x060026A8 RID: 9896 RVA: 0x000A6853 File Offset: 0x000A4A53
		[XmlArrayItem(ElementName = "DeletedConversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArray(ElementName = "DeletedConversations")]
		[DataMember(Order = 4)]
		public DeletedConversationType[] DeletedConversations { get; set; }

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060026A9 RID: 9897 RVA: 0x000A685C File Offset: 0x000A4A5C
		// (set) Token: 0x060026AA RID: 9898 RVA: 0x000A6864 File Offset: 0x000A4A64
		[DataMember(Order = 5)]
		[XmlArray(ElementName = "ConversationViewDataList")]
		[XmlArrayItem(ElementName = "ConversationViewData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public ConversationViewDataType[] ConversationViewDataList { get; set; }
	}
}
