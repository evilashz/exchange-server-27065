using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200047C RID: 1148
	[DataContract]
	public class SetCalendarNotificationSettings : SetObjectProperties
	{
		// Token: 0x170022C9 RID: 8905
		// (get) Token: 0x060039AC RID: 14764 RVA: 0x000AF372 File Offset: 0x000AD572
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-CalendarNotification";
			}
		}

		// Token: 0x170022CA RID: 8906
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000AF379 File Offset: 0x000AD579
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x170022CB RID: 8907
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x000AF380 File Offset: 0x000AD580
		// (set) Token: 0x060039AF RID: 14767 RVA: 0x000AF39C File Offset: 0x000AD59C
		[DataMember]
		public bool DailyAgendaNotification
		{
			get
			{
				return (bool)(base[CalendarNotificationSchema.DailyAgendaNotification] ?? false);
			}
			set
			{
				base[CalendarNotificationSchema.DailyAgendaNotification] = value;
			}
		}

		// Token: 0x170022CC RID: 8908
		// (get) Token: 0x060039B0 RID: 14768 RVA: 0x000AF3B0 File Offset: 0x000AD5B0
		// (set) Token: 0x060039B1 RID: 14769 RVA: 0x000AF3E4 File Offset: 0x000AD5E4
		[DataMember]
		public int DailyAgendaNotificationSendTime
		{
			get
			{
				return (int)((TimeSpan)(base[CalendarNotificationSchema.DailyAgendaNotificationSendTime] ?? TimeSpan.Zero)).TotalMinutes;
			}
			set
			{
				base[CalendarNotificationSchema.DailyAgendaNotificationSendTime] = TimeSpan.FromMinutes((double)value);
			}
		}

		// Token: 0x170022CD RID: 8909
		// (get) Token: 0x060039B2 RID: 14770 RVA: 0x000AF3FD File Offset: 0x000AD5FD
		// (set) Token: 0x060039B3 RID: 14771 RVA: 0x000AF419 File Offset: 0x000AD619
		[DataMember]
		public bool CalendarUpdateNotification
		{
			get
			{
				return (bool)(base[CalendarNotificationSchema.CalendarUpdateNotification] ?? false);
			}
			set
			{
				base[CalendarNotificationSchema.CalendarUpdateNotification] = value;
			}
		}

		// Token: 0x170022CE RID: 8910
		// (get) Token: 0x060039B4 RID: 14772 RVA: 0x000AF42C File Offset: 0x000AD62C
		// (set) Token: 0x060039B5 RID: 14773 RVA: 0x000AF448 File Offset: 0x000AD648
		[DataMember]
		public bool CalendarUpdateSendDuringWorkHour
		{
			get
			{
				return (bool)(base[CalendarNotificationSchema.CalendarUpdateSendDuringWorkHour] ?? false);
			}
			set
			{
				base[CalendarNotificationSchema.CalendarUpdateSendDuringWorkHour] = value;
			}
		}

		// Token: 0x170022CF RID: 8911
		// (get) Token: 0x060039B6 RID: 14774 RVA: 0x000AF45B File Offset: 0x000AD65B
		// (set) Token: 0x060039B7 RID: 14775 RVA: 0x000AF477 File Offset: 0x000AD677
		[DataMember]
		public int NextDays
		{
			get
			{
				return (int)(base[CalendarNotificationSchema.NextDays] ?? 1);
			}
			set
			{
				base[CalendarNotificationSchema.NextDays] = value;
			}
		}

		// Token: 0x170022D0 RID: 8912
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x000AF48A File Offset: 0x000AD68A
		// (set) Token: 0x060039B9 RID: 14777 RVA: 0x000AF4A6 File Offset: 0x000AD6A6
		[DataMember]
		public bool MeetingReminderNotification
		{
			get
			{
				return (bool)(base[CalendarNotificationSchema.MeetingReminderNotification] ?? false);
			}
			set
			{
				base[CalendarNotificationSchema.MeetingReminderNotification] = value;
			}
		}

		// Token: 0x170022D1 RID: 8913
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x000AF4B9 File Offset: 0x000AD6B9
		// (set) Token: 0x060039BB RID: 14779 RVA: 0x000AF4D5 File Offset: 0x000AD6D5
		[DataMember]
		public bool MeetingReminderSendDuringWorkHour
		{
			get
			{
				return (bool)(base[CalendarNotificationSchema.MeetingReminderSendDuringWorkHour] ?? false);
			}
			set
			{
				base[CalendarNotificationSchema.MeetingReminderSendDuringWorkHour] = value;
			}
		}
	}
}
