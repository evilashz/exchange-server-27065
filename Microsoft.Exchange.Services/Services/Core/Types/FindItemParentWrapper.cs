using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000787 RID: 1927
	[XmlType(TypeName = "FindItemParentType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class FindItemParentWrapper : FindParentWrapperBase
	{
		// Token: 0x0600399A RID: 14746 RVA: 0x000CB774 File Offset: 0x000C9974
		public FindItemParentWrapper()
		{
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000CB77C File Offset: 0x000C997C
		internal FindItemParentWrapper(ItemType[] items, BasePageResult paging) : base(paging)
		{
			this.Items = items;
			this.Groups = null;
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000CB793 File Offset: 0x000C9993
		internal FindItemParentWrapper(GroupType[] groups, BasePageResult paging) : base(paging)
		{
			this.Items = null;
			this.Groups = groups;
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x000CB7AA File Offset: 0x000C99AA
		// (set) Token: 0x0600399E RID: 14750 RVA: 0x000CB7B2 File Offset: 0x000C99B2
		[XmlArrayItem("Item", typeof(ItemType))]
		[XmlArrayItem("Contact", typeof(ContactItemType))]
		[XmlArrayItem("DistributionList", typeof(DistributionListType))]
		[XmlArrayItem("Message", typeof(MessageType))]
		[XmlArrayItem("CalendarItem", typeof(EwsCalendarItemType))]
		[XmlArrayItem("MeetingMessage", typeof(MeetingMessageType))]
		[XmlArrayItem("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlArrayItem("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlArrayItem("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlArrayItem("PostItem", typeof(PostItemType))]
		[XmlArrayItem("Task", typeof(TaskType))]
		[XmlArray("Items")]
		[DataMember(Name = "Items")]
		public ItemType[] Items { get; set; }

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x000CB7BB File Offset: 0x000C99BB
		// (set) Token: 0x060039A0 RID: 14752 RVA: 0x000CB7C3 File Offset: 0x000C99C3
		[XmlArray("Groups")]
		[XmlArrayItem("GroupedItems")]
		[DataMember(Name = "Groups")]
		public GroupType[] Groups { get; set; }
	}
}
