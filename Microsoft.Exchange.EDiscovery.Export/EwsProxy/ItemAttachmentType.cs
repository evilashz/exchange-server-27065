using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000129 RID: 297
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ItemAttachmentType : AttachmentType
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00021FE8 File Offset: 0x000201E8
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x00021FF0 File Offset: 0x000201F0
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("CalendarItem", typeof(CalendarItemType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		public ItemType Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x04000934 RID: 2356
		private ItemType itemField;
	}
}
