using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200059F RID: 1439
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ArrayOfRealItemsType
	{
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x000AC8BD File Offset: 0x000AAABD
		// (set) Token: 0x060028BF RID: 10431 RVA: 0x000AC8C5 File Offset: 0x000AAAC5
		[XmlElement("CalendarItem", typeof(EwsCalendarItemType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[DataMember(EmitDefaultValue = false)]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("Task", typeof(TaskType))]
		public ItemType[] Items { get; set; }
	}
}
