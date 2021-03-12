using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000D8 RID: 216
	internal class SummaryGenerating : CalendarNotificationAction
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0003D3B4 File Offset: 0x0003B5B4
		public SummaryGenerating(ExDateTime startTime, ExDateTime expectedTime, MailboxData mailboxData) : base(expectedTime, mailboxData)
		{
			base.ExpectedTime = SummaryGenerating.GetDayLightTransitionAdjustedTime(startTime, expectedTime, mailboxData.Settings.TimeZone.ExTimeZone);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0003D3DC File Offset: 0x0003B5DC
		protected override void OnPerforming(long cookie)
		{
			using (base.Context.CreateReadLock())
			{
				if (base.ShouldContinue(cookie))
				{
					if (NotificationFactories.Instance.IsSummaryEnabled(base.Context.Settings))
					{
						ExDateTime exDateTime = base.ExpectedTime.Date + base.Context.Settings.Text.TextNotification.CalendarNotificationSettings.SummarySettings.NotifyingTimeInDay.TimeOfDay;
						ExDateTime exDateTime2 = exDateTime + TimeSpan.FromDays(base.Context.Settings.Text.TextNotification.CalendarNotificationSettings.SummarySettings.Duration.Interval);
						try
						{
							using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(ExchangePrincipal.FromLocalServerMailboxGuid(base.Context.Settings.GetADSettings(), base.Context.DatabaseGuid, base.Context.MailboxGuid), CultureInfo.InvariantCulture, "Client=TBA;Action=ReloadReminders"))
							{
								if (base.ShouldContinue(cookie))
								{
									IList<CalendarInfo> list = SummaryLoader.Load(base.ExpectedTime, base.Context.Settings.TimeZone.ExTimeZone, mailboxSession, base.Context.DefaultCalendarFolderId, null, exDateTime, exDateTime2);
									if (0 < list.Count && base.ShouldContinue(cookie))
									{
										new TextNotificationFactory.TextMessagingEmitter(base.Context).Emit(mailboxSession, CalendarNotificationType.Summary, list);
									}
								}
							}
						}
						finally
						{
							if (base.ShouldContinue(cookie))
							{
								CalendarNotificationInitiator.ScheduleAction(new SummaryGenerating(exDateTime, exDateTime2, base.Context), base.GetType().Name);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0003D5C8 File Offset: 0x0003B7C8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SummaryGenerating>(this);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0003D5D0 File Offset: 0x0003B7D0
		public static ExDateTime GetDayLightTransitionAdjustedTime(ExDateTime startTime, ExDateTime endTime, ExTimeZone userConfigTimeZone)
		{
			bool flag = userConfigTimeZone.IsDaylightSavingTime(startTime);
			bool flag2 = userConfigTimeZone.IsDaylightSavingTime(endTime);
			if (flag && !flag2)
			{
				return endTime + new TimeSpan(1, 0, 0);
			}
			if (!flag && flag2)
			{
				return endTime - new TimeSpan(1, 0, 0);
			}
			return endTime;
		}
	}
}
