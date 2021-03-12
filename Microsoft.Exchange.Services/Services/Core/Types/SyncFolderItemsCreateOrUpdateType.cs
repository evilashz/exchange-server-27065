using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200066D RID: 1645
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SyncFolderItemsCreateOrUpdateType : SyncFolderItemsChangeTypeBase
	{
		// Token: 0x06003251 RID: 12881 RVA: 0x000B7A40 File Offset: 0x000B5C40
		public SyncFolderItemsCreateOrUpdateType()
		{
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000B7A48 File Offset: 0x000B5C48
		public SyncFolderItemsCreateOrUpdateType(ItemType item, bool isUpdate)
		{
			this.Item = item;
			this.isUpdate = isUpdate;
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x000B7A5E File Offset: 0x000B5C5E
		// (set) Token: 0x06003254 RID: 12884 RVA: 0x000B7A66 File Offset: 0x000B5C66
		[XmlElement("Contact", typeof(ContactItemType))]
		[XmlElement("PostItem", typeof(PostItemType))]
		[XmlElement("Task", typeof(TaskType))]
		[DataMember(EmitDefaultValue = false)]
		[XmlElement("Item", typeof(ItemType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("CalendarItem", typeof(EwsCalendarItemType))]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		[XmlElement("DistributionList", typeof(DistributionListType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		public ItemType Item { get; set; }

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x000B7A6F File Offset: 0x000B5C6F
		public override SyncFolderItemsChangesEnum ChangeType
		{
			get
			{
				if (!this.isUpdate)
				{
					return SyncFolderItemsChangesEnum.Create;
				}
				return SyncFolderItemsChangesEnum.Update;
			}
		}

		// Token: 0x04001CB1 RID: 7345
		private bool isUpdate;
	}
}
