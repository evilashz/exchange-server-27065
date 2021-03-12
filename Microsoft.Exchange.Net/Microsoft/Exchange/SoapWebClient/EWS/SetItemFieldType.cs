using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003D9 RID: 985
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class SetItemFieldType : ItemChangeDescriptionType
	{
		// Token: 0x0400156D RID: 5485
		[XmlElement("DistributionList", typeof(DistributionListType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("CalendarItem", typeof(CalendarItemType))]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		public ItemType Item1;
	}
}
