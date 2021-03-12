using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000A68 RID: 2664
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarNotificationOptions : OptionsPropertyChangeTracker
	{
		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06004B8E RID: 19342 RVA: 0x00105A72 File Offset: 0x00103C72
		// (set) Token: 0x06004B8F RID: 19343 RVA: 0x00105A7A File Offset: 0x00103C7A
		[DataMember]
		public bool CalendarUpdateNotification
		{
			get
			{
				return this.calendarUpdateNotification;
			}
			set
			{
				this.calendarUpdateNotification = value;
				base.TrackPropertyChanged("CalendarUpdateNotification");
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06004B90 RID: 19344 RVA: 0x00105A8E File Offset: 0x00103C8E
		// (set) Token: 0x06004B91 RID: 19345 RVA: 0x00105A96 File Offset: 0x00103C96
		[DataMember]
		public int NextDays
		{
			get
			{
				return this.nextDays;
			}
			set
			{
				this.nextDays = value;
				base.TrackPropertyChanged("NextDays");
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06004B92 RID: 19346 RVA: 0x00105AAA File Offset: 0x00103CAA
		// (set) Token: 0x06004B93 RID: 19347 RVA: 0x00105AB2 File Offset: 0x00103CB2
		[DataMember]
		public bool CalendarUpdateSendDuringWorkHour
		{
			get
			{
				return this.calendarUpdateSendDuringWorkHour;
			}
			set
			{
				this.calendarUpdateSendDuringWorkHour = value;
				base.TrackPropertyChanged("CalendarUpdateSendDuringWorkHour");
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06004B94 RID: 19348 RVA: 0x00105AC6 File Offset: 0x00103CC6
		// (set) Token: 0x06004B95 RID: 19349 RVA: 0x00105ACE File Offset: 0x00103CCE
		[DataMember]
		public bool MeetingReminderNotification
		{
			get
			{
				return this.meetingReminderNotification;
			}
			set
			{
				this.meetingReminderNotification = value;
				base.TrackPropertyChanged("MeetingReminderNotification");
			}
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06004B96 RID: 19350 RVA: 0x00105AE2 File Offset: 0x00103CE2
		// (set) Token: 0x06004B97 RID: 19351 RVA: 0x00105AEA File Offset: 0x00103CEA
		[DataMember]
		public bool MeetingReminderSendDuringWorkHour
		{
			get
			{
				return this.meetingReminderSendDuringWorkHour;
			}
			set
			{
				this.meetingReminderSendDuringWorkHour = value;
				base.TrackPropertyChanged("MeetingReminderSendDuringWorkHour");
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06004B98 RID: 19352 RVA: 0x00105AFE File Offset: 0x00103CFE
		// (set) Token: 0x06004B99 RID: 19353 RVA: 0x00105B06 File Offset: 0x00103D06
		[DataMember]
		public bool DailyAgendaNotification
		{
			get
			{
				return this.dailyAgendaNotification;
			}
			set
			{
				this.dailyAgendaNotification = value;
				base.TrackPropertyChanged("DailyAgendaNotification");
			}
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06004B9A RID: 19354 RVA: 0x00105B1A File Offset: 0x00103D1A
		// (set) Token: 0x06004B9B RID: 19355 RVA: 0x00105B22 File Offset: 0x00103D22
		[DataMember]
		public int DailyAgendaNotificationSendTime
		{
			get
			{
				return this.dailyAgendaNotificationSendTime;
			}
			set
			{
				this.dailyAgendaNotificationSendTime = value;
				base.TrackPropertyChanged("DailyAgendaNotificationSendTime");
			}
		}

		// Token: 0x04002AF0 RID: 10992
		private bool calendarUpdateNotification;

		// Token: 0x04002AF1 RID: 10993
		private int nextDays;

		// Token: 0x04002AF2 RID: 10994
		private bool calendarUpdateSendDuringWorkHour;

		// Token: 0x04002AF3 RID: 10995
		private bool meetingReminderNotification;

		// Token: 0x04002AF4 RID: 10996
		private bool meetingReminderSendDuringWorkHour;

		// Token: 0x04002AF5 RID: 10997
		private bool dailyAgendaNotification;

		// Token: 0x04002AF6 RID: 10998
		private int dailyAgendaNotificationSendTime;
	}
}
