using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200033A RID: 826
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ArrayOfRealItemsType
	{
		// Token: 0x040013A5 RID: 5029
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("CalendarItem", typeof(CalendarItemType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		public ItemType[] Items;
	}
}
