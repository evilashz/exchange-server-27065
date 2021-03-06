using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002DD RID: 733
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class SyncFolderItemsCreateOrUpdateType
	{
		// Token: 0x0400125F RID: 4703
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("CalendarItem", typeof(CalendarItemType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
		public ItemType Item;
	}
}
