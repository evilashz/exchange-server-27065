using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200088D RID: 2189
	[DataContract(Name = "SetItemField", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "SetItemFieldType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class SetItemPropertyUpdate : SetPropertyUpdate
	{
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x000D9279 File Offset: 0x000D7479
		// (set) Token: 0x06003EB3 RID: 16051 RVA: 0x000D9281 File Offset: 0x000D7481
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[DataMember(Name = "Item", IsRequired = true)]
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("CalendarItem", typeof(EwsCalendarItemType))]
		public ItemType Item { get; set; }

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x000D928A File Offset: 0x000D748A
		internal override ServiceObject ServiceObject
		{
			get
			{
				return this.Item;
			}
		}
	}
}
