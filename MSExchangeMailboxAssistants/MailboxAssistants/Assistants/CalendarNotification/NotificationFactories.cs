using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000E9 RID: 233
	internal class NotificationFactories
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x00040DBC File Offset: 0x0003EFBC
		private NotificationFactories()
		{
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00040DED File Offset: 0x0003EFED
		internal static NotificationFactories Instance
		{
			get
			{
				return NotificationFactories.instance;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00040DF4 File Offset: 0x0003EFF4
		internal bool IsNotificationEnabled(UserSettings settings)
		{
			foreach (NotificationFactoryBase notificationFactoryBase in this.factories)
			{
				if (notificationFactoryBase.IsFeatureEnabled(settings))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00040E2C File Offset: 0x0003F02C
		internal bool IsReminderEnabled(UserSettings settings)
		{
			foreach (NotificationFactoryBase notificationFactoryBase in this.factories)
			{
				if (notificationFactoryBase.IsReminderEnabled(settings))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00040E64 File Offset: 0x0003F064
		internal bool IsSummaryEnabled(UserSettings settings)
		{
			foreach (NotificationFactoryBase notificationFactoryBase in this.factories)
			{
				if (notificationFactoryBase.IsSummaryEnabled(settings))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00040E9C File Offset: 0x0003F09C
		internal bool IsInterestedInCalendarChangeEvent(UserSettings settings)
		{
			foreach (NotificationFactoryBase notificationFactoryBase in this.factories)
			{
				if (notificationFactoryBase.IsInterestedInCalendarChangeEvent(settings))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00040ED4 File Offset: 0x0003F0D4
		internal bool IsInterestedInCalendarMeetingEvent(UserSettings settings)
		{
			foreach (NotificationFactoryBase notificationFactoryBase in this.factories)
			{
				if (notificationFactoryBase.IsInterestedInCalendarMeetingEvent(settings))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00040F0C File Offset: 0x0003F10C
		internal UserSettings LoadUserSettingsFromMailboxSession(MailboxSession session)
		{
			UserSettings userSettings = new UserSettings(session);
			foreach (NotificationFactoryBase notificationFactoryBase in this.factories)
			{
				notificationFactoryBase.LoadUserSettingsFromMailboxSession(session, userSettings);
			}
			return userSettings;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00040F44 File Offset: 0x0003F144
		internal void UpdateSettingUnderSystemMailbox(UserSettings settings, SystemMailbox systemMailbox)
		{
			foreach (NotificationFactoryBase notificationFactoryBase in this.factories)
			{
				notificationFactoryBase.UpdateSettingUnderSystemMailbox(settings, systemMailbox);
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00040F74 File Offset: 0x0003F174
		internal void GetAllUsersSettingsFromSystemMailbox(Dictionary<string, UserSettings> settingsDictionary, SystemMailbox systemMailbox, MailboxSession systemMailboxSession)
		{
			foreach (NotificationFactoryBase notificationFactoryBase in this.factories)
			{
				notificationFactoryBase.GetAllUsersSettingsFromSystemMailbox(settingsDictionary, systemMailbox, systemMailboxSession);
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00040FA4 File Offset: 0x0003F1A4
		internal bool TryCreateReminderEmitting(CalendarInfo CalendarInfo, MailboxData mailboxData, out ReminderEmitting reminderEmitting)
		{
			reminderEmitting = null;
			List<ICalendarNotificationEmitter> list = new List<ICalendarNotificationEmitter>();
			foreach (NotificationFactoryBase notificationFactoryBase in this.factories)
			{
				ICalendarNotificationEmitter item;
				if (notificationFactoryBase.TryCreateEmitter(CalendarInfo, mailboxData, out item))
				{
					list.Add(item);
				}
			}
			if (list.Count > 0)
			{
				reminderEmitting = new ReminderEmitting(CalendarInfo.ReminderTime, mailboxData, CalendarInfo, list);
			}
			return reminderEmitting != null;
		}

		// Token: 0x0400067E RID: 1662
		private static readonly NotificationFactories instance = new NotificationFactories();

		// Token: 0x0400067F RID: 1663
		private readonly NotificationFactoryBase[] factories = new NotificationFactoryBase[]
		{
			new TextNotificationFactory(),
			new VoiceNotificationFactory()
		};
	}
}
