using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000460 RID: 1120
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ItemAttachment")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ItemAttachmentType : AttachmentType
	{
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x000A2109 File Offset: 0x000A0309
		// (set) Token: 0x060020FC RID: 8444 RVA: 0x000A2111 File Offset: 0x000A0311
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[DataMember(Name = "Item", IsRequired = true)]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("CalendarItem", typeof(EwsCalendarItemType))]
		public ItemType Item { get; set; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x000A211A File Offset: 0x000A031A
		// (set) Token: 0x060020FE RID: 8446 RVA: 0x000A2122 File Offset: 0x000A0322
		[XmlIgnore]
		[DataMember(Name = "EmbeddedItemClass", IsRequired = false)]
		public string EmbeddedItemClass { get; set; }
	}
}
