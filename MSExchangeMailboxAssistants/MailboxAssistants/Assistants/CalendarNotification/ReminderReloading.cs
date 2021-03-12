using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000D7 RID: 215
	internal class ReminderReloading : CalendarNotificationAction
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x0003D10C File Offset: 0x0003B30C
		public ReminderReloading(ExDateTime expectedTime, MailboxData mailboxData) : base(expectedTime, mailboxData)
		{
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0003D118 File Offset: 0x0003B318
		protected override void OnPerforming(long cookie)
		{
			using (base.Context.CreateReadLock())
			{
				if (!base.ShouldContinue(cookie))
				{
					ExTraceGlobals.AssistantTracer.TraceDebug((long)this.GetHashCode(), "Didn't perform reminder reloading because of cookie");
				}
				else if (!NotificationFactories.Instance.IsReminderEnabled(base.Context.Settings))
				{
					ExTraceGlobals.AssistantTracer.TraceDebug((long)this.GetHashCode(), "Didn't perform reminder reloading because user hasn't enabled reminder");
				}
				else
				{
					ExDateTime expectedTime = base.ExpectedTime;
					ExDateTime exDateTime = expectedTime + TimeSpan.FromDays(1.0);
					ExDateTime expectedTime2 = exDateTime;
					string name = base.GetType().Name;
					try
					{
						ExTraceGlobals.AssistantTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Start Reminder Reloading at {0}, {1}", expectedTime.ToShortDateString(), expectedTime.ToLongTimeString());
						ExchangePrincipal mailboxOwner = ExchangePrincipal.FromLocalServerMailboxGuid(base.Context.Settings.GetADSettings(), base.Context.DatabaseGuid, base.Context.MailboxGuid);
						using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.InvariantCulture, "Client=TBA;Action=ReloadReminders"))
						{
							if (!base.ShouldContinue(cookie))
							{
								ExTraceGlobals.AssistantTracer.TraceDebug((long)this.GetHashCode(), "Didn't perform reminder reloading because of cookie");
							}
							else
							{
								foreach (CalendarInfo calendarInfo in ReminderLoader.Load(base.ExpectedTime, base.Context.Settings.TimeZone.ExTimeZone, mailboxSession, base.Context.DefaultCalendarFolderId, null, expectedTime, exDateTime))
								{
									if (!base.ShouldContinue(cookie))
									{
										ExTraceGlobals.AssistantTracer.TraceDebug((long)this.GetHashCode(), "Didn't perform reminder reloading because of cookie");
										break;
									}
									expectedTime2 = calendarInfo.ReminderTime + TimeSpan.FromTicks(1L);
									ReminderEmitting action;
									if (NotificationFactories.Instance.TryCreateReminderEmitting(calendarInfo, base.Context, out action))
									{
										CalendarNotificationInitiator.ScheduleAction(action, name);
									}
								}
							}
						}
					}
					finally
					{
						if (base.ShouldContinue(cookie))
						{
							ExTraceGlobals.AssistantTracer.TraceDebug<string, string>((long)this.GetHashCode(), "The next reminder reloading is scheduled at {0}, {1}", expectedTime2.ToShortDateString(), expectedTime2.ToLongTimeString());
							CalendarNotificationInitiator.ScheduleAction(new ReminderReloading(expectedTime2, base.Context), name);
						}
					}
				}
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0003D3AC File Offset: 0x0003B5AC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ReminderReloading>(this);
		}

		// Token: 0x04000627 RID: 1575
		public const int ReloadingPeriodInDays = 1;
	}
}
