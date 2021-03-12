using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002F2 RID: 754
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AppendToItemFieldType : ItemChangeDescriptionType
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x000284C4 File Offset: 0x000266C4
		// (set) Token: 0x06001940 RID: 6464 RVA: 0x000284CC File Offset: 0x000266CC
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("CalendarItem", typeof(CalendarItemType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		public ItemType Item1
		{
			get
			{
				return this.item1Field;
			}
			set
			{
				this.item1Field = value;
			}
		}

		// Token: 0x04001118 RID: 4376
		private ItemType item1Field;
	}
}
