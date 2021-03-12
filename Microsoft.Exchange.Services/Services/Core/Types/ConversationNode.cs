using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005B2 RID: 1458
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ConversationNode")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ConversationNode
	{
		// Token: 0x06002B9F RID: 11167 RVA: 0x000AFD01 File Offset: 0x000ADF01
		internal void AddItem(ItemType item)
		{
			if (this.itemsList == null)
			{
				this.itemsList = new List<ItemType>();
			}
			this.itemsList.Add(item);
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x000AFD22 File Offset: 0x000ADF22
		internal int ItemCount
		{
			get
			{
				if (this.itemsList == null)
				{
					return 0;
				}
				return this.itemsList.Count;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x000AFD39 File Offset: 0x000ADF39
		// (set) Token: 0x06002BA2 RID: 11170 RVA: 0x000AFD41 File Offset: 0x000ADF41
		[XmlElement("InternetMessageId", IsNullable = false)]
		[DataMember(Name = "InternetMessageId", IsRequired = true, Order = 1)]
		public string InternetMessageId { get; set; }

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x000AFD4A File Offset: 0x000ADF4A
		// (set) Token: 0x06002BA4 RID: 11172 RVA: 0x000AFD52 File Offset: 0x000ADF52
		[XmlElement("ParentInternetMessageId", IsNullable = false)]
		[DataMember(Name = "ParentInternetMessageId", EmitDefaultValue = false, IsRequired = false, Order = 2)]
		public string ParentInternetMessageId { get; set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x000AFD5B File Offset: 0x000ADF5B
		// (set) Token: 0x06002BA6 RID: 11174 RVA: 0x000AFD72 File Offset: 0x000ADF72
		[XmlArrayItem("Message", typeof(MessageType))]
		[XmlArrayItem("PostItem", typeof(PostItemType))]
		[XmlArrayItem("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlArrayItem("MeetingMessage", typeof(MeetingMessageType))]
		[XmlArrayItem("CalendarItem", typeof(EwsCalendarItemType))]
		[XmlArrayItem("Contact", typeof(ContactItemType))]
		[XmlArrayItem("DistributionList", typeof(DistributionListType))]
		[XmlArrayItem("Item", typeof(ItemType))]
		[XmlArrayItem("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlArrayItem("Task", typeof(TaskType))]
		[XmlArray("Items")]
		[XmlArrayItem("MeetingResponse", typeof(MeetingResponseMessageType))]
		[DataMember(Name = "Items", IsRequired = true, Order = 3)]
		public ItemType[] Items
		{
			get
			{
				if (this.itemsList == null)
				{
					return null;
				}
				return this.itemsList.ToArray();
			}
			set
			{
				this.itemsList = ((value != null) ? new List<ItemType>(value) : null);
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x000AFD86 File Offset: 0x000ADF86
		// (set) Token: 0x06002BA8 RID: 11176 RVA: 0x000AFDAB File Offset: 0x000ADFAB
		[XmlIgnore]
		[DataMember(Name = "NewParticipants", EmitDefaultValue = false, Order = 4)]
		public EmailAddressWrapper[] NewParticipants
		{
			get
			{
				if (this.newParticipants != null && this.newParticipants.Count > 0)
				{
					return this.newParticipants.ToArray();
				}
				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.newParticipants = null;
					return;
				}
				this.newParticipants = new List<EmailAddressWrapper>(value);
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x000AFDC9 File Offset: 0x000ADFC9
		// (set) Token: 0x06002BAA RID: 11178 RVA: 0x000AFDD1 File Offset: 0x000ADFD1
		[XmlIgnore]
		[DataMember(Name = "InReplyToItem", EmitDefaultValue = false, Order = 5)]
		public InReplyToAdapterType InReplyToItem { get; set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x000AFDDA File Offset: 0x000ADFDA
		// (set) Token: 0x06002BAC RID: 11180 RVA: 0x000AFDE2 File Offset: 0x000ADFE2
		[XmlIgnore]
		[DataMember(Name = "IsRootNode", Order = 6)]
		public bool IsRootNode { get; set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x000AFDEB File Offset: 0x000ADFEB
		// (set) Token: 0x06002BAE RID: 11182 RVA: 0x000AFDF3 File Offset: 0x000ADFF3
		[DataMember(Name = "ForwardMessages", EmitDefaultValue = false, Order = 7)]
		[XmlIgnore]
		public BreadcrumbAdapterType[] ForwardMessages { get; set; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000AFDFC File Offset: 0x000ADFFC
		// (set) Token: 0x06002BB0 RID: 11184 RVA: 0x000AFE04 File Offset: 0x000AE004
		[DataMember(Name = "BackwardMessage", EmitDefaultValue = false, Order = 8)]
		[XmlIgnore]
		public BreadcrumbAdapterType BackwardMessage { get; set; }

		// Token: 0x04001A4D RID: 6733
		private List<ItemType> itemsList;

		// Token: 0x04001A4E RID: 6734
		private List<EmailAddressWrapper> newParticipants;
	}
}
