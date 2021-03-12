using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005DE RID: 1502
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GroupType
	{
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x000B1EF1 File Offset: 0x000B00F1
		// (set) Token: 0x06002D3A RID: 11578 RVA: 0x000B1EF9 File Offset: 0x000B00F9
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public int GroupIndex { get; set; }

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000B1F02 File Offset: 0x000B0102
		// (set) Token: 0x06002D3C RID: 11580 RVA: 0x000B1F0A File Offset: 0x000B010A
		[XmlArray("Items")]
		[XmlArrayItem("Message", typeof(MessageType))]
		[XmlArrayItem("CalendarItem", typeof(EwsCalendarItemType))]
		[XmlArrayItem("Contact", typeof(ContactItemType))]
		[XmlArrayItem("DistributionList", typeof(DistributionListType))]
		[XmlArrayItem("Item", typeof(ItemType))]
		[XmlArrayItem("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlArrayItem("MeetingMessage", typeof(MeetingMessageType))]
		[XmlArrayItem("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlArrayItem("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlArrayItem("PostItem", typeof(PostItemType))]
		[XmlArrayItem("Task", typeof(TaskType))]
		[DataMember(Name = "Items", Order = 2)]
		public ItemType[] Items { get; set; }
	}
}
