using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000115 RID: 277
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class NonEmptyArrayOfAllItemsType
	{
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00021584 File Offset: 0x0001F784
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x0002158C File Offset: 0x0001F78C
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("CalendarItem", typeof(CalendarItemType))]
		[XmlElement("CancelCalendarItem", typeof(CancelCalendarItemType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("DeclineItem", typeof(DeclineItemType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
		[XmlElement("ForwardItem", typeof(ForwardItemType))]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("PostReplyItem", typeof(PostReplyItemType))]
		[XmlElement("RemoveItem", typeof(RemoveItemType))]
		[XmlElement("ReplyAllToItem", typeof(ReplyAllToItemType))]
		[XmlElement("ReplyToItem", typeof(ReplyToItemType))]
		[XmlElement("SuppressReadReceipt", typeof(SuppressReadReceiptType))]
		[XmlElement("AcceptItem", typeof(AcceptItemType))]
		[XmlElement("TentativelyAcceptItem", typeof(TentativelyAcceptItemType))]
		[XmlElement("AcceptSharingInvitation", typeof(AcceptSharingInvitationType))]
		public ItemType[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x04000897 RID: 2199
		private ItemType[] itemsField;
	}
}
