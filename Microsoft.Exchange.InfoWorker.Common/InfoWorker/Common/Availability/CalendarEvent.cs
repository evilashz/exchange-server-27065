using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000EC RID: 236
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarEvent
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001B4DC File Offset: 0x000196DC
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0001B4E4 File Offset: 0x000196E4
		[XmlElement]
		[IgnoreDataMember]
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001B4ED File Offset: 0x000196ED
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001B4F5 File Offset: 0x000196F5
		[XmlElement]
		[IgnoreDataMember]
		public DateTime EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001B4FE File Offset: 0x000196FE
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0001B50B File Offset: 0x0001970B
		[DataMember(Name = "StartTime", IsRequired = true)]
		[XmlIgnore]
		public string StartTimeString
		{
			get
			{
				return this.StartTime.ToIso8061();
			}
			set
			{
				this.StartTime = DateTime.Parse(value);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001B519 File Offset: 0x00019719
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001B526 File Offset: 0x00019726
		[XmlIgnore]
		[DataMember(Name = "EndTime", IsRequired = true)]
		public string EndTimeString
		{
			get
			{
				return this.EndTime.ToIso8061();
			}
			set
			{
				this.EndTime = DateTime.Parse(value);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001B534 File Offset: 0x00019734
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0001B53C File Offset: 0x0001973C
		[IgnoreDataMember]
		[XmlElement]
		public BusyType BusyType
		{
			get
			{
				return this.busyType;
			}
			set
			{
				this.busyType = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001B545 File Offset: 0x00019745
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001B552 File Offset: 0x00019752
		[DataMember(Name = "BusyType")]
		[XmlIgnore]
		public string BusyTypeString
		{
			get
			{
				return EnumUtil.ToString<BusyType>(this.BusyType);
			}
			set
			{
				this.BusyType = EnumUtil.Parse<BusyType>(value);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001B560 File Offset: 0x00019760
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0001B568 File Offset: 0x00019768
		[DataMember]
		[XmlElement(IsNullable = false)]
		public CalendarEventDetails CalendarEventDetails
		{
			get
			{
				return this.calendarEventDetails;
			}
			set
			{
				this.calendarEventDetails = value;
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001B574 File Offset: 0x00019774
		public override string ToString()
		{
			return string.Format("<StartTime = {0}, EndTime = {1}, BusyType = {2}, Subject = {3}>", new object[]
			{
				this.startTime,
				this.EndTime,
				this.busyType,
				(this.calendarEventDetails != null) ? this.calendarEventDetails.Subject : "<null>"
			});
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001B5DA File Offset: 0x000197DA
		[XmlIgnore]
		internal byte[] GlobalObjectId
		{
			get
			{
				return this.globalObjectId;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001B5E2 File Offset: 0x000197E2
		[XmlIgnore]
		internal FreeBusyViewType FreeBusyViewType
		{
			get
			{
				return this.viewType;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001B5EC File Offset: 0x000197EC
		internal static CalendarEvent CreateFromQueryData(EmailAddress mailbox, object[] properties, FreeBusyViewType allowedView, bool isCallerMailboxOwner, ExchangeVersionType exchangeVersion)
		{
			CalendarEvent calendarEvent = new CalendarEvent();
			calendarEvent.viewType = allowedView;
			calendarEvent.globalObjectId = CalendarEvent.GetPropertyValue<byte[]>(properties, QueryPropertyIndexes.GlobalObjectId);
			calendarEvent.StartTime = DateTime.SpecifyKind((DateTime)CalendarEvent.GetPropertyValue<ExDateTime>(properties, QueryPropertyIndexes.StartTime, ExDateTime.MinValue), DateTimeKind.Unspecified);
			calendarEvent.EndTime = DateTime.SpecifyKind((DateTime)CalendarEvent.GetPropertyValue<ExDateTime>(properties, QueryPropertyIndexes.EndTime, ExDateTime.MinValue), DateTimeKind.Unspecified);
			BusyType busyType = CalendarEvent.GetPropertyValue<BusyType>(properties, QueryPropertyIndexes.BusyStatus, BusyType.Busy);
			if (busyType < BusyType.Free || busyType > BusyType.NoData)
			{
				CalendarEvent.CalendarViewTracer.TraceError((long)calendarEvent.GetHashCode(), "{0}: Calendar event with start time {1} and end time {2} in mailbox {3} has invalid busy type: {4}. This is being returned as BusyType.Tentative", new object[]
				{
					TraceContext.Get(),
					calendarEvent.StartTime,
					calendarEvent.EndTime,
					mailbox,
					busyType
				});
				calendarEvent.BusyType = BusyType.Tentative;
			}
			else
			{
				if (exchangeVersion < ExchangeVersionType.Exchange2012 && busyType == BusyType.WorkingElsewhere)
				{
					busyType = BusyType.Free;
				}
				calendarEvent.BusyType = busyType;
			}
			Sensitivity propertyValue = CalendarEvent.GetPropertyValue<Sensitivity>(properties, QueryPropertyIndexes.Sensitivity, Sensitivity.Normal);
			if (propertyValue < Sensitivity.Normal || propertyValue > Sensitivity.CompanyConfidential)
			{
				CalendarEvent.CalendarViewTracer.TraceError((long)calendarEvent.GetHashCode(), "{0}: Calendar event with start time {1} and end time {2} in mailbox {3} has invalid sensitivity value: {4}.", new object[]
				{
					TraceContext.Get(),
					calendarEvent.StartTime,
					calendarEvent.EndTime,
					mailbox,
					propertyValue
				});
			}
			VersionedId propertyValue2 = CalendarEvent.GetPropertyValue<VersionedId>(properties, QueryPropertyIndexes.EntryId);
			ByteArray byteArray = new ByteArray(propertyValue2.ObjectId.ProviderLevelItemId);
			if (allowedView != FreeBusyViewType.Detailed && allowedView != FreeBusyViewType.DetailedMerged)
			{
				return calendarEvent;
			}
			calendarEvent.CalendarEventDetails = new CalendarEventDetails();
			CalendarEventDetails calendarEventDetails = calendarEvent.CalendarEventDetails;
			calendarEventDetails.IsPrivate = (propertyValue != Sensitivity.Normal);
			if (calendarEventDetails.IsPrivate && !isCallerMailboxOwner)
			{
				CalendarEvent.CalendarViewTracer.TraceError((long)calendarEvent.GetHashCode(), "{0}: Calendar event with start time {1} and end time {2} in mailbox {3} is a private item. Detail data will not be included.", new object[]
				{
					TraceContext.Get(),
					calendarEvent.StartTime,
					calendarEvent.EndTime,
					mailbox
				});
				return calendarEvent;
			}
			calendarEventDetails.ID = byteArray.ToString();
			calendarEventDetails.Subject = CalendarEvent.GetPropertyValue<string>(properties, QueryPropertyIndexes.Subject);
			calendarEventDetails.Location = CalendarEvent.GetPropertyValue<string>(properties, QueryPropertyIndexes.Location);
			calendarEventDetails.IsReminderSet = CalendarEvent.GetPropertyValue<bool>(properties, QueryPropertyIndexes.IsReminderSet, false);
			AppointmentStateFlags propertyValue3 = CalendarEvent.GetPropertyValue<AppointmentStateFlags>(properties, QueryPropertyIndexes.AppointmentState, AppointmentStateFlags.None);
			calendarEventDetails.IsMeeting = ((propertyValue3 & AppointmentStateFlags.Meeting) > AppointmentStateFlags.None);
			CalendarItemType propertyValue4 = CalendarEvent.GetPropertyValue<CalendarItemType>(properties, QueryPropertyIndexes.CalendarItemType, CalendarItemType.Single);
			if (propertyValue4 == CalendarItemType.Occurrence)
			{
				calendarEventDetails.IsRecurring = true;
			}
			if (propertyValue4 == CalendarItemType.Exception)
			{
				calendarEventDetails.IsException = true;
				calendarEventDetails.IsRecurring = true;
			}
			return calendarEvent;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001B858 File Offset: 0x00019A58
		private static T GetPropertyValue<T>(object[] properties, QueryPropertyIndexes index, T defaultValue)
		{
			object obj = properties[(int)index];
			if (obj == null || obj is PropertyError)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001B87C File Offset: 0x00019A7C
		private static T GetPropertyValue<T>(object[] properties, QueryPropertyIndexes index) where T : class
		{
			object obj = properties[(int)index];
			if (obj == null || obj is PropertyError)
			{
				return default(T);
			}
			return (T)((object)obj);
		}

		// Token: 0x04000393 RID: 915
		private FreeBusyViewType viewType = FreeBusyViewType.FreeBusy;

		// Token: 0x04000394 RID: 916
		private DateTime startTime = DateTime.MinValue;

		// Token: 0x04000395 RID: 917
		private DateTime endTime = DateTime.MinValue;

		// Token: 0x04000396 RID: 918
		private BusyType busyType = BusyType.NoData;

		// Token: 0x04000397 RID: 919
		private CalendarEventDetails calendarEventDetails;

		// Token: 0x04000398 RID: 920
		private byte[] globalObjectId;

		// Token: 0x04000399 RID: 921
		private static readonly Trace CalendarViewTracer = ExTraceGlobals.CalendarViewTracer;
	}
}
