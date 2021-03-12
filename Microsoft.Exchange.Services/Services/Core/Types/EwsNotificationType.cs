using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005D2 RID: 1490
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EwsNotificationType
	{
		// Token: 0x06002CD9 RID: 11481 RVA: 0x000B19BF File Offset: 0x000AFBBF
		public EwsNotificationType()
		{
			this.eventList = new List<BaseNotificationEventType>();
			this.eventTypeList = new List<NotificationTypeEnum>();
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x000B19DD File Offset: 0x000AFBDD
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x000B19E5 File Offset: 0x000AFBE5
		[DataMember(EmitDefaultValue = false)]
		public string SubscriptionId { get; set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000B19EE File Offset: 0x000AFBEE
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x000B19F6 File Offset: 0x000AFBF6
		[DataMember(EmitDefaultValue = false)]
		public string PreviousWatermark { get; set; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000B19FF File Offset: 0x000AFBFF
		// (set) Token: 0x06002CDF RID: 11487 RVA: 0x000B1A07 File Offset: 0x000AFC07
		[DataMember(EmitDefaultValue = false)]
		public bool MoreEvents
		{
			get
			{
				return this.moreEvents;
			}
			set
			{
				this.MoreEventsSpecified = true;
				this.moreEvents = value;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000B1A17 File Offset: 0x000AFC17
		// (set) Token: 0x06002CE1 RID: 11489 RVA: 0x000B1A1F File Offset: 0x000AFC1F
		[XmlIgnore]
		[IgnoreDataMember]
		public bool MoreEventsSpecified { get; set; }

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x000B1A28 File Offset: 0x000AFC28
		// (set) Token: 0x06002CE3 RID: 11491 RVA: 0x000B1A35 File Offset: 0x000AFC35
		[XmlElement("CreatedEvent", typeof(BaseObjectChangedEventType))]
		[XmlElement("FreeBusyChangedEvent", typeof(BaseObjectChangedEventType))]
		[XmlElement("ModifiedEvent", typeof(ModifiedEventType))]
		[XmlElement("MovedEvent", typeof(MovedCopiedEventType))]
		[XmlElement("NewMailEvent", typeof(BaseObjectChangedEventType))]
		[XmlElement("StatusEvent", typeof(BaseNotificationEventType))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[DataMember(EmitDefaultValue = false)]
		[XmlElement("CopiedEvent", typeof(MovedCopiedEventType))]
		[XmlElement("DeletedEvent", typeof(BaseObjectChangedEventType))]
		public BaseNotificationEventType[] Events
		{
			get
			{
				return this.eventList.ToArray();
			}
			set
			{
				this.eventList = new List<BaseNotificationEventType>(value);
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x000B1A43 File Offset: 0x000AFC43
		// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x000B1A50 File Offset: 0x000AFC50
		[XmlIgnore]
		[XmlElement("ItemsElementName")]
		[IgnoreDataMember]
		public NotificationTypeEnum[] ItemsElementName
		{
			get
			{
				return this.eventTypeList.ToArray();
			}
			set
			{
				this.eventTypeList = new List<NotificationTypeEnum>(value);
			}
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000B1A5E File Offset: 0x000AFC5E
		public void AddNotificationEvent(BaseNotificationEventType notificationEvent)
		{
			this.eventList.Add(notificationEvent);
			this.eventTypeList.Add(notificationEvent.NotificationType);
		}

		// Token: 0x04001AF8 RID: 6904
		private bool moreEvents;

		// Token: 0x04001AF9 RID: 6905
		private List<BaseNotificationEventType> eventList;

		// Token: 0x04001AFA RID: 6906
		private List<NotificationTypeEnum> eventTypeList;
	}
}
