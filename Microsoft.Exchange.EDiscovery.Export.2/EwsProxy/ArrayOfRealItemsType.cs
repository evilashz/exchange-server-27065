using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000259 RID: 601
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ArrayOfRealItemsType
	{
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x00026CDB File Offset: 0x00024EDB
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x00026CE3 File Offset: 0x00024EE3
		[XmlElement("CalendarItem", typeof(CalendarItemType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("Message", typeof(MessageType))]
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

		// Token: 0x04000F53 RID: 3923
		private ItemType[] itemsField;
	}
}
