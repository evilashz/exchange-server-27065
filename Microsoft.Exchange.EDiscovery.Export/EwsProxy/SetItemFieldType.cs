using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002F8 RID: 760
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SetItemFieldType : ItemChangeDescriptionType
	{
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x00028527 File Offset: 0x00026727
		// (set) Token: 0x0600194C RID: 6476 RVA: 0x0002852F File Offset: 0x0002672F
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("CalendarItem", typeof(CalendarItemType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("Message", typeof(MessageType))]
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

		// Token: 0x0400111B RID: 4379
		private ItemType item1Field;
	}
}
