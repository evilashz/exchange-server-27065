using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006B5 RID: 1717
	[DataContract(Name = "AppendToItemField", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "AppendToItemFieldType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class AppendItemPropertyUpdate : AppendPropertyUpdate
	{
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x000BE320 File Offset: 0x000BC520
		// (set) Token: 0x060034E3 RID: 13539 RVA: 0x000BE328 File Offset: 0x000BC528
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("Task", typeof(TaskType))]
		[DataMember(Name = "Item", IsRequired = true)]
		[XmlElement("CalendarItem", typeof(EwsCalendarItemType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("Item", typeof(ItemType))]
		public ItemType Item { get; set; }

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x000BE331 File Offset: 0x000BC531
		internal override ServiceObject ServiceObject
		{
			get
			{
				return this.Item;
			}
		}
	}
}
