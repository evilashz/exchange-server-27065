using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200047B RID: 1147
	[DataContract]
	public class CalendarNotificationSettings : BaseRow
	{
		// Token: 0x06003997 RID: 14743 RVA: 0x000AF28C File Offset: 0x000AD48C
		public CalendarNotificationSettings(CalendarNotification calendarNotification) : base(calendarNotification)
		{
			this.CalendarNotification = calendarNotification;
		}

		// Token: 0x170022BF RID: 8895
		// (get) Token: 0x06003998 RID: 14744 RVA: 0x000AF29C File Offset: 0x000AD49C
		// (set) Token: 0x06003999 RID: 14745 RVA: 0x000AF2A4 File Offset: 0x000AD4A4
		private CalendarNotification CalendarNotification { get; set; }

		// Token: 0x170022C0 RID: 8896
		// (get) Token: 0x0600399A RID: 14746 RVA: 0x000AF2AD File Offset: 0x000AD4AD
		// (set) Token: 0x0600399B RID: 14747 RVA: 0x000AF2BA File Offset: 0x000AD4BA
		[DataMember]
		public bool DailyAgendaNotification
		{
			get
			{
				return this.CalendarNotification.DailyAgendaNotification;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022C1 RID: 8897
		// (get) Token: 0x0600399C RID: 14748 RVA: 0x000AF2C4 File Offset: 0x000AD4C4
		// (set) Token: 0x0600399D RID: 14749 RVA: 0x000AF2E5 File Offset: 0x000AD4E5
		[DataMember]
		public int DailyAgendaNotificationSendTime
		{
			get
			{
				return (int)this.CalendarNotification.DailyAgendaNotificationSendTime.TotalMinutes;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022C2 RID: 8898
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000AF2EC File Offset: 0x000AD4EC
		// (set) Token: 0x0600399F RID: 14751 RVA: 0x000AF2F9 File Offset: 0x000AD4F9
		[DataMember]
		public bool CalendarUpdateNotification
		{
			get
			{
				return this.CalendarNotification.CalendarUpdateNotification;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022C3 RID: 8899
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x000AF300 File Offset: 0x000AD500
		// (set) Token: 0x060039A1 RID: 14753 RVA: 0x000AF30D File Offset: 0x000AD50D
		[DataMember]
		public bool CalendarUpdateSendDuringWorkHour
		{
			get
			{
				return this.CalendarNotification.CalendarUpdateSendDuringWorkHour;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022C4 RID: 8900
		// (get) Token: 0x060039A2 RID: 14754 RVA: 0x000AF314 File Offset: 0x000AD514
		// (set) Token: 0x060039A3 RID: 14755 RVA: 0x000AF321 File Offset: 0x000AD521
		[DataMember]
		public int NextDays
		{
			get
			{
				return this.CalendarNotification.NextDays;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022C5 RID: 8901
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x000AF328 File Offset: 0x000AD528
		// (set) Token: 0x060039A5 RID: 14757 RVA: 0x000AF335 File Offset: 0x000AD535
		[DataMember]
		public bool MeetingReminderNotification
		{
			get
			{
				return this.CalendarNotification.MeetingReminderNotification;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022C6 RID: 8902
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x000AF33C File Offset: 0x000AD53C
		// (set) Token: 0x060039A7 RID: 14759 RVA: 0x000AF349 File Offset: 0x000AD549
		[DataMember]
		public bool MeetingReminderSendDuringWorkHour
		{
			get
			{
				return this.CalendarNotification.MeetingReminderSendDuringWorkHour;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022C7 RID: 8903
		// (get) Token: 0x060039A8 RID: 14760 RVA: 0x000AF350 File Offset: 0x000AD550
		// (set) Token: 0x060039A9 RID: 14761 RVA: 0x000AF358 File Offset: 0x000AD558
		[DataMember]
		public bool NotificationEnabled { get; set; }

		// Token: 0x170022C8 RID: 8904
		// (get) Token: 0x060039AA RID: 14762 RVA: 0x000AF361 File Offset: 0x000AD561
		// (set) Token: 0x060039AB RID: 14763 RVA: 0x000AF369 File Offset: 0x000AD569
		[DataMember]
		public bool EasEnabled { get; set; }
	}
}
