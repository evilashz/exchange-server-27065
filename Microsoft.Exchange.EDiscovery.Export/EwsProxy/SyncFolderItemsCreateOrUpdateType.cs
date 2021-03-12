using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001FC RID: 508
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SyncFolderItemsCreateOrUpdateType
	{
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00025BD7 File Offset: 0x00023DD7
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x00025BDF File Offset: 0x00023DDF
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("Task", typeof(TaskType))]
		[XmlElement("CalendarItem", typeof(CalendarItemType))]
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
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

		// Token: 0x04000E0D RID: 3597
		private ItemType itemField;
	}
}
