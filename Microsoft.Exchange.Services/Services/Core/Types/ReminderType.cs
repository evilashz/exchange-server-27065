using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200053A RID: 1338
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Reminder")]
	[XmlType("Reminder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ReminderType
	{
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x000A6217 File Offset: 0x000A4417
		// (set) Token: 0x06002605 RID: 9733 RVA: 0x000A621F File Offset: 0x000A441F
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 1)]
		public string Subject { get; set; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x000A6228 File Offset: 0x000A4428
		// (set) Token: 0x06002607 RID: 9735 RVA: 0x000A6230 File Offset: 0x000A4430
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 2)]
		public string Location { get; set; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x000A6239 File Offset: 0x000A4439
		// (set) Token: 0x06002609 RID: 9737 RVA: 0x000A6241 File Offset: 0x000A4441
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 3)]
		[DateTimeString]
		public string ReminderTime { get; set; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x000A624A File Offset: 0x000A444A
		// (set) Token: 0x0600260B RID: 9739 RVA: 0x000A6252 File Offset: 0x000A4452
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 4)]
		public string StartDate { get; set; }

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x000A625B File Offset: 0x000A445B
		// (set) Token: 0x0600260D RID: 9741 RVA: 0x000A6263 File Offset: 0x000A4463
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 5)]
		public string EndDate { get; set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x000A626C File Offset: 0x000A446C
		// (set) Token: 0x0600260F RID: 9743 RVA: 0x000A6274 File Offset: 0x000A4474
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 6)]
		public ItemId ItemId { get; set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x000A627D File Offset: 0x000A447D
		// (set) Token: 0x06002611 RID: 9745 RVA: 0x000A6285 File Offset: 0x000A4485
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 7)]
		public ItemId RecurringMasterItemId { get; set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x000A628E File Offset: 0x000A448E
		// (set) Token: 0x06002613 RID: 9747 RVA: 0x000A6296 File Offset: 0x000A4496
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 8)]
		public ReminderGroupType ReminderGroup { get; set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000A629F File Offset: 0x000A449F
		// (set) Token: 0x06002615 RID: 9749 RVA: 0x000A62C5 File Offset: 0x000A44C5
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 9)]
		public string UID
		{
			get
			{
				if (this.uid != null)
				{
					return this.uid;
				}
				if (this.ItemId != null)
				{
					return this.ItemId.Id;
				}
				return null;
			}
			set
			{
				this.uid = value;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x000A62CE File Offset: 0x000A44CE
		// (set) Token: 0x06002617 RID: 9751 RVA: 0x000A62D6 File Offset: 0x000A44D6
		[IgnoreDataMember]
		[XmlIgnore]
		public ExDateTime ReminderDateTime
		{
			get
			{
				return this.reminderDateTime;
			}
			set
			{
				this.reminderDateTime = value;
				this.ReminderTime = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(value);
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x000A62EB File Offset: 0x000A44EB
		// (set) Token: 0x06002619 RID: 9753 RVA: 0x000A62F3 File Offset: 0x000A44F3
		[IgnoreDataMember]
		[XmlIgnore]
		public ExDateTime StartDateTime
		{
			get
			{
				return this.startDateTime;
			}
			set
			{
				this.startDateTime = value;
				this.StartDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(value);
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x000A6308 File Offset: 0x000A4508
		// (set) Token: 0x0600261B RID: 9755 RVA: 0x000A6310 File Offset: 0x000A4510
		[IgnoreDataMember]
		[XmlIgnore]
		public ExDateTime EndDateTime
		{
			get
			{
				return this.endDateTime;
			}
			set
			{
				this.endDateTime = value;
				this.EndDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(value);
			}
		}

		// Token: 0x040015E2 RID: 5602
		private ExDateTime reminderDateTime;

		// Token: 0x040015E3 RID: 5603
		private ExDateTime startDateTime;

		// Token: 0x040015E4 RID: 5604
		private ExDateTime endDateTime;

		// Token: 0x040015E5 RID: 5605
		private string uid;
	}
}
