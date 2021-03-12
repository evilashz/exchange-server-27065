using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003D3 RID: 979
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class AppendToItemFieldType : ItemChangeDescriptionType
	{
		// Token: 0x0400156A RID: 5482
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("CalendarItem", typeof(CalendarItemType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		public ItemType Item1;
	}
}
