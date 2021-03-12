using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.TextMessaging.MobileDriver;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000E1 RID: 225
	internal class CalendarNotificationInitiator
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x0003F71C File Offset: 0x0003D91C
		static CalendarNotificationInitiator()
		{
			CalendarNotificationInitiator.scheduler.Start();
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0003F780 File Offset: 0x0003D980
		public static void CompleteAction(ScheduledActionBase action, string callerHint)
		{
			CalendarNotificationInitiator.actionSchedulingQueue.Enqueue(new CalendarNotificationInitiator.SchedulingInfo(action, null, null, callerHint));
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0003F795 File Offset: 0x0003D995
		public static void UpdateAction(ScheduledActionBase oldAction, ScheduledActionBase action, string callerHint)
		{
			CalendarNotificationInitiator.actionSchedulingQueue.Enqueue(new CalendarNotificationInitiator.SchedulingInfo(oldAction, action, null, callerHint));
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0003F7AA File Offset: 0x0003D9AA
		public static void ScheduleAction(ScheduledActionBase action, string callerHint)
		{
			CalendarNotificationInitiator.actionSchedulingQueue.Enqueue(new CalendarNotificationInitiator.SchedulingInfo(null, action, null, callerHint));
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0003F7BF File Offset: 0x0003D9BF
		public static void PerformCustomizedOperationOnActionsList(Action customizedOperation, string callerHint)
		{
			CalendarNotificationInitiator.actionSchedulingQueue.Enqueue(new CalendarNotificationInitiator.SchedulingInfo(null, null, customizedOperation, callerHint));
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0003F7D4 File Offset: 0x0003D9D4
		public static void CountInMailbox(Guid databaseGuid, Guid mailboxGuid)
		{
			lock (CalendarNotificationInitiator.mailboxGroups)
			{
				if (!CalendarNotificationInitiator.mailboxGroups.ContainsKey(databaseGuid))
				{
					CalendarNotificationInitiator.mailboxGroups[databaseGuid] = new HashSet<Guid>();
				}
				CalendarNotificationInitiator.mailboxGroups[databaseGuid].TryAdd(mailboxGuid);
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0003F83C File Offset: 0x0003DA3C
		public static void ExcludeDatabase(Guid databaseGuid)
		{
			HashSet<Guid> hashSet = null;
			lock (CalendarNotificationInitiator.mailboxGroups)
			{
				if (CalendarNotificationInitiator.mailboxGroups.ContainsKey(databaseGuid))
				{
					hashSet = CalendarNotificationInitiator.mailboxGroups[databaseGuid];
					CalendarNotificationInitiator.mailboxGroups.Remove(databaseGuid);
				}
			}
			if (hashSet == null)
			{
				return;
			}
			foreach (Guid mailboxGuid in hashSet)
			{
				MailboxData fromCache = MailboxData.GetFromCache(mailboxGuid);
				if (fromCache != null)
				{
					using (fromCache.CreateWriteLock())
					{
						fromCache.Settings.Text = null;
						fromCache.Settings.Voice = null;
						using (fromCache.Actions.SyncObj.CreateWriteLock())
						{
							CalendarNotificationInitiator.StopAll(fromCache);
						}
					}
				}
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0003F950 File Offset: 0x0003DB50
		public static void InitiateEmittingReminder(ExDateTime now, MailboxData mailboxData)
		{
			if (NotificationFactories.Instance.IsReminderEnabled(mailboxData.Settings))
			{
				CalendarNotificationInitiator.ScheduleAction(new ReminderReloading(now, mailboxData), "InitiateEmittingReminder");
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0003F978 File Offset: 0x0003DB78
		public static void InitiateEmittingSummary(ExDateTime now, MailboxData mailboxData)
		{
			if (!NotificationFactories.Instance.IsSummaryEnabled(mailboxData.Settings))
			{
				return;
			}
			ExDateTime exDateTime = now.Date + mailboxData.Settings.Text.TextNotification.DailyAgendaNotificationSendTime;
			ExDateTime startTime = now;
			if (exDateTime < now)
			{
				startTime = exDateTime;
				exDateTime += TimeSpan.FromDays((double)mailboxData.Settings.Text.TextNotification.NextDays);
			}
			CalendarNotificationInitiator.ScheduleAction(new SummaryGenerating(startTime, exDateTime, mailboxData), "InitiateEmittingSummary");
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0003FA78 File Offset: 0x0003DC78
		public static void StopEmittingReminder(MailboxData mailboxData)
		{
			if (!NotificationFactories.Instance.IsReminderEnabled(mailboxData.Settings))
			{
				return;
			}
			CalendarNotificationInitiator.PerformCustomizedOperationOnActionsList(delegate
			{
				mailboxData.Actions.ForEach(delegate(CalendarNotificationAction action)
				{
					Type type = action.GetType();
					if (typeof(ReminderEmitting) != type && typeof(ReminderReloading) != type)
					{
						return;
					}
					CalendarNotificationInitiator.CompleteAction(action, "StopEmittingReminder");
				});
			}, "StopEmittingReminder");
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0003FB1C File Offset: 0x0003DD1C
		public static void StopEmittingSummary(MailboxData mailboxData)
		{
			if (!NotificationFactories.Instance.IsSummaryEnabled(mailboxData.Settings))
			{
				return;
			}
			CalendarNotificationInitiator.PerformCustomizedOperationOnActionsList(delegate
			{
				mailboxData.Actions.ForEach(delegate(CalendarNotificationAction action)
				{
					if (typeof(SummaryGenerating) == action.GetType())
					{
						CalendarNotificationInitiator.CompleteAction(action, "StopEmittingSummary");
					}
				});
			}, "StopEmittingSummary");
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0003FBC0 File Offset: 0x0003DDC0
		public static void StopEmittingUpdate(MailboxData mailboxData)
		{
			CalendarNotificationInitiator.PerformCustomizedOperationOnActionsList(delegate
			{
				mailboxData.Actions.ForEach(delegate(CalendarNotificationAction action)
				{
					if (typeof(UpdateEmitting) == action.GetType())
					{
						CalendarNotificationInitiator.CompleteAction(action, "StopEmittingUpdate");
					}
				});
			}, "StopEmittingUpdate");
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0003FC34 File Offset: 0x0003DE34
		public static void StopAll(MailboxData mailboxData)
		{
			CalendarNotificationInitiator.PerformCustomizedOperationOnActionsList(delegate
			{
				mailboxData.Actions.ForEach(delegate(CalendarNotificationAction action)
				{
					CalendarNotificationInitiator.CompleteAction(action, "StopAll");
				});
			}, "StopAll");
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0003FC64 File Offset: 0x0003DE64
		public static void ResetEmittingReminder(ExDateTime now, MailboxData mailboxData)
		{
			CalendarNotificationInitiator.StopEmittingReminder(mailboxData);
			CalendarNotificationInitiator.InitiateEmittingReminder(now, mailboxData);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0003FC73 File Offset: 0x0003DE73
		public static void ResetEmittingSummary(ExDateTime now, MailboxData mailboxData)
		{
			CalendarNotificationInitiator.StopEmittingSummary(mailboxData);
			CalendarNotificationInitiator.InitiateEmittingSummary(now, mailboxData);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0003FC84 File Offset: 0x0003DE84
		public void Initiate(DatabaseInfo databaseInfo)
		{
			if (CalendarNotificationInitiator.stateOfSettingsChangeListener == 0 && Interlocked.CompareExchange(ref CalendarNotificationInitiator.stateOfSettingsChangeListener, 1, 0) == 0)
			{
				SettingsChangeListener.Instance.UserSettingsChanged += CalendarNotificationInitiator.SettingsChangedEventHandler;
			}
			bool flag = false;
			lock (CalendarNotificationInitiator.initiatedDatabaseGuids)
			{
				if (!CalendarNotificationInitiator.initiatedDatabaseGuids.ContainsKey(databaseInfo.Guid))
				{
					CalendarNotificationInitiator.initiatedDatabaseGuids[databaseInfo.Guid] = databaseInfo;
					flag = true;
				}
			}
			if (flag)
			{
				CalendarNotificationInitiator.ScheduleAction(new Initiating(ExDateTime.Now, databaseInfo), "initiating database");
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0003FD5C File Offset: 0x0003DF5C
		public void Stop(DatabaseInfo databaseInfo)
		{
			lock (CalendarNotificationInitiator.initiatedDatabaseGuids)
			{
				if (CalendarNotificationInitiator.initiatedDatabaseGuids.ContainsKey(databaseInfo.Guid))
				{
					CalendarNotificationInitiator.initiatedDatabaseGuids.Remove(databaseInfo.Guid);
				}
			}
			CalendarNotificationInitiator.pendingInitiatings.ForEach(delegate(Initiating action)
			{
				if (action.Context.Guid == databaseInfo.Guid)
				{
					CalendarNotificationInitiator.CompleteAction(action, "stoping database");
				}
			});
			CalendarNotificationInitiator.ExcludeDatabase(databaseInfo.Guid);
			SystemMailbox.RemoveInstance(databaseInfo);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0003FE00 File Offset: 0x0003E000
		private static void Schedule(QueueDataAvailableEventSource<CalendarNotificationInitiator.SchedulingInfo> src, QueueDataAvailableEventArgs<CalendarNotificationInitiator.SchedulingInfo> e)
		{
			e.Item.Perform();
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0003FE0D File Offset: 0x0003E00D
		private static void SettingsChangedEventHandler(object sender, UserSettings settings, InfoFromUserMailboxSession info)
		{
			if (NotificationFactories.Instance.IsNotificationEnabled(settings))
			{
				CalendarNotificationInitiator.NotificationStillEnabled(settings, info);
				return;
			}
			CalendarNotificationInitiator.DisableUser(settings, info.MailboxGuid);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0003FE34 File Offset: 0x0003E034
		private static void NotificationStillEnabled(UserSettings settings, InfoFromUserMailboxSession info)
		{
			try
			{
				ExDateTime userNow = settings.TimeZone.ExTimeZone.ConvertDateTime(ExDateTime.Now);
				MailboxData mailboxData = MailboxData.CreateFromUserSettings(settings, info);
				if (mailboxData != null)
				{
					try
					{
						MailboxData fromCache = MailboxData.GetFromCache(mailboxData.MailboxGuid);
						if (fromCache == null)
						{
							CalendarNotificationInitiator.EnableUser(ref mailboxData, userNow);
							return;
						}
						if (Utils.AreInterestedFieldsEqual(mailboxData.Settings, fromCache.Settings))
						{
							return;
						}
						using (fromCache.CreateReadLock())
						{
							using (fromCache.Actions.SyncObj.CreateWriteLock())
							{
								CalendarNotificationInitiator.StopEmittingReminder(fromCache);
								CalendarNotificationInitiator.StopEmittingSummary(fromCache);
							}
						}
						CalendarNotificationInitiator.UpdateCacheAndInitiateEmittings(ref mailboxData, userNow);
					}
					finally
					{
						if (mailboxData != null)
						{
							mailboxData.Dispose();
							mailboxData = null;
						}
					}
				}
				ExTraceGlobals.AssistantTracer.TraceDebug<string>((long)typeof(CalendarNotificationInitiator).GetHashCode(), "notif changed, user: {0}", settings.LegacyDN);
			}
			catch (Exception ex)
			{
				if (!CalendarNotificationAssistant.TryHandleException((long)typeof(CalendarNotificationInitiator).GetHashCode(), "handling changing", settings.LegacyDN, ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0003FF74 File Offset: 0x0003E174
		private static void UpdateCacheAndInitiateEmittings(ref MailboxData mailboxData, ExDateTime userNow)
		{
			MailboxData mailboxData2 = MailboxData.UpdateCache(ref mailboxData);
			using (mailboxData2.CreateReadLock())
			{
				using (mailboxData2.Actions.SyncObj.CreateWriteLock())
				{
					CalendarNotificationInitiator.InitiateEmittingReminder(userNow, mailboxData2);
					CalendarNotificationInitiator.InitiateEmittingSummary(userNow, mailboxData2);
				}
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0003FFE4 File Offset: 0x0003E1E4
		private static void DisableUser(UserSettings settings, Guid mailboxGuid)
		{
			try
			{
				MailboxData mailboxData = MailboxData.CreateFromUserSettings(settings);
				if (mailboxData != null)
				{
					try
					{
						MailboxData mailboxData2 = MailboxData.UpdateCache(ref mailboxData);
						using (mailboxData2.CreateReadLock())
						{
							using (mailboxData2.Actions.SyncObj.CreateWriteLock())
							{
								CalendarNotificationInitiator.StopAll(mailboxData2);
							}
						}
					}
					finally
					{
						if (mailboxData != null)
						{
							mailboxData.Dispose();
							mailboxData = null;
						}
					}
				}
				ExTraceGlobals.AssistantTracer.TraceDebug<string>((long)typeof(CalendarNotificationInitiator).GetHashCode(), "notif disabled, user: {0}", settings.LegacyDN);
			}
			catch (Exception ex)
			{
				if (!CalendarNotificationAssistant.TryHandleException((long)typeof(CalendarNotificationInitiator).GetHashCode(), "handling diabling", settings.LegacyDN, ex))
				{
					throw;
				}
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000400D0 File Offset: 0x0003E2D0
		private static void EnableUser(ref MailboxData mailboxData, ExDateTime userNow)
		{
			string legacyDN = mailboxData.Settings.LegacyDN;
			try
			{
				CalendarNotificationInitiator.CountInMailbox(mailboxData.DatabaseGuid, mailboxData.MailboxGuid);
				CalendarNotificationInitiator.UpdateCacheAndInitiateEmittings(ref mailboxData, userNow);
				ExTraceGlobals.AssistantTracer.TraceDebug<string>((long)typeof(CalendarNotificationInitiator).GetHashCode(), "notif enabled, user: {0}", legacyDN);
			}
			catch (Exception ex)
			{
				if (!CalendarNotificationAssistant.TryHandleException((long)typeof(CalendarNotificationInitiator).GetHashCode(), "handling enabling", legacyDN, ex))
				{
					throw;
				}
			}
		}

		// Token: 0x04000659 RID: 1625
		private const int DefaultMailboxGroupsCapacity = 10;

		// Token: 0x0400065A RID: 1626
		private const int DefaultPendingInitiatingCollectionCapacity = 10;

		// Token: 0x0400065B RID: 1627
		private static int stateOfSettingsChangeListener;

		// Token: 0x0400065C RID: 1628
		private static Dictionary<Guid, DatabaseInfo> initiatedDatabaseGuids = new Dictionary<Guid, DatabaseInfo>(10);

		// Token: 0x0400065D RID: 1629
		private static Dictionary<Guid, HashSet<Guid>> mailboxGroups = new Dictionary<Guid, HashSet<Guid>>(10);

		// Token: 0x0400065E RID: 1630
		private static ThreadSafeQueue<CalendarNotificationInitiator.SchedulingInfo> actionSchedulingQueue = new ThreadSafeQueue<CalendarNotificationInitiator.SchedulingInfo>(true);

		// Token: 0x0400065F RID: 1631
		private static QueuePorter<CalendarNotificationInitiator.SchedulingInfo> scheduler = new QueuePorter<CalendarNotificationInitiator.SchedulingInfo>(CalendarNotificationInitiator.actionSchedulingQueue, new QueueDataAvailableEventHandler<CalendarNotificationInitiator.SchedulingInfo>(CalendarNotificationInitiator.Schedule), true);

		// Token: 0x04000660 RID: 1632
		private static ThreadSafeCollection<Initiating> pendingInitiatings = new ThreadSafeCollection<Initiating>(10, null, null);

		// Token: 0x020000E2 RID: 226
		private class SchedulingInfo
		{
			// Token: 0x0600098C RID: 2444 RVA: 0x00040164 File Offset: 0x0003E364
			public SchedulingInfo(ScheduledActionBase oldScheduledAction, ScheduledActionBase scheduledAction, Action customizedSteps, string contextInfo)
			{
				if (oldScheduledAction != null && scheduledAction != null && oldScheduledAction.GetType() != scheduledAction.GetType())
				{
					throw new ArgumentOutOfRangeException("scheduledAction");
				}
				if (customizedSteps != null && (oldScheduledAction != null || scheduledAction != null))
				{
					throw new ArgumentOutOfRangeException("customizedSteps");
				}
				if (customizedSteps == null && oldScheduledAction == null && scheduledAction == null)
				{
					throw new ArgumentNullException("customizedSteps");
				}
				this.OldScheduledAction = oldScheduledAction;
				this.ScheduledAction = scheduledAction;
				this.CustomizedSteps = customizedSteps;
				this.ContextInfo = contextInfo;
			}

			// Token: 0x17000258 RID: 600
			// (get) Token: 0x0600098D RID: 2445 RVA: 0x000401E0 File Offset: 0x0003E3E0
			// (set) Token: 0x0600098E RID: 2446 RVA: 0x000401E8 File Offset: 0x0003E3E8
			public ScheduledActionBase OldScheduledAction { get; private set; }

			// Token: 0x17000259 RID: 601
			// (get) Token: 0x0600098F RID: 2447 RVA: 0x000401F1 File Offset: 0x0003E3F1
			// (set) Token: 0x06000990 RID: 2448 RVA: 0x000401F9 File Offset: 0x0003E3F9
			public ScheduledActionBase ScheduledAction { get; private set; }

			// Token: 0x1700025A RID: 602
			// (get) Token: 0x06000991 RID: 2449 RVA: 0x00040202 File Offset: 0x0003E402
			// (set) Token: 0x06000992 RID: 2450 RVA: 0x0004020A File Offset: 0x0003E40A
			public Action CustomizedSteps { get; private set; }

			// Token: 0x1700025B RID: 603
			// (get) Token: 0x06000993 RID: 2451 RVA: 0x00040213 File Offset: 0x0003E413
			// (set) Token: 0x06000994 RID: 2452 RVA: 0x0004021B File Offset: 0x0003E41B
			public string ContextInfo { get; private set; }

			// Token: 0x06000995 RID: 2453 RVA: 0x00040224 File Offset: 0x0003E424
			public void Perform()
			{
				if (this.CustomizedSteps != null)
				{
					this.CustomizedSteps();
					return;
				}
				CalendarNotificationAction calendarNotificationAction = this.OldScheduledAction as CalendarNotificationAction;
				CalendarNotificationAction calendarNotificationAction2 = this.ScheduledAction as CalendarNotificationAction;
				Initiating initiating = this.OldScheduledAction as Initiating;
				Initiating initiating2 = this.ScheduledAction as Initiating;
				ReminderEmitting reminderEmitting = this.OldScheduledAction as ReminderEmitting;
				ReminderEmitting reminderEmitting2 = this.ScheduledAction as ReminderEmitting;
				if (this.OldScheduledAction != null && this.ScheduledAction != null && reminderEmitting != null && reminderEmitting.Dismiss())
				{
					reminderEmitting.CalendarInfo.CopyFrom(reminderEmitting2.CalendarInfo);
					if (reminderEmitting.Reschedule(reminderEmitting2.ExpectedTime))
					{
						ExTraceGlobals.AssistantTracer.TraceDebug((long)this.GetHashCode(), "rmd>>|<< caller: {0}, SUBJ: {1}, usr: {2}, calItemId: {3}, calItemOccId: {4}, event_t: {5}, rmd_t: {6}, s_t: {7}, e_t: {8}, subj: {9},EVENT_T: {10} RMD_T: {11}, S_T: {12}, E_T: {13}", new object[]
						{
							this.ContextInfo,
							reminderEmitting2.CalendarInfo.NormalizedSubject,
							reminderEmitting2.Context,
							reminderEmitting2.CalendarInfo.CalendarItemIdentity,
							reminderEmitting2.CalendarInfo.CalendarItemOccurrenceIdentity,
							reminderEmitting.CalendarInfo.CreationRequestTime,
							reminderEmitting.CalendarInfo.ReminderTime,
							reminderEmitting.CalendarInfo.StartTime,
							reminderEmitting.CalendarInfo.EndTime,
							reminderEmitting.CalendarInfo.NormalizedSubject,
							reminderEmitting2.CalendarInfo.CreationRequestTime,
							reminderEmitting2.CalendarInfo.ReminderTime,
							reminderEmitting2.CalendarInfo.StartTime,
							reminderEmitting2.CalendarInfo.EndTime
						});
						reminderEmitting2.Dispose();
						return;
					}
				}
				if (this.OldScheduledAction != null && !this.OldScheduledAction.IsDisposed)
				{
					if (calendarNotificationAction != null)
					{
						if (reminderEmitting != null)
						{
							ExTraceGlobals.AssistantTracer.TraceDebug((long)this.GetHashCode(), "rmd----- caller: {0}, subj: {1}, usr: {2}, calItemId: {3}, calItemOccId: {4}, event_t: {5}, rmd_t: {6}, s_t: {7}, e_t: {8}", new object[]
							{
								this.ContextInfo,
								reminderEmitting.CalendarInfo.NormalizedSubject,
								reminderEmitting.Context,
								reminderEmitting.CalendarInfo.CalendarItemIdentity,
								reminderEmitting.CalendarInfo.CalendarItemOccurrenceIdentity,
								reminderEmitting.CalendarInfo.CreationRequestTime,
								reminderEmitting.CalendarInfo.ReminderTime,
								reminderEmitting.CalendarInfo.StartTime,
								reminderEmitting.CalendarInfo.EndTime
							});
						}
						calendarNotificationAction.Dispose();
						if (calendarNotificationAction.Context != null && !calendarNotificationAction.Context.IsDisposed && !calendarNotificationAction.Context.Actions.IsDisposed)
						{
							calendarNotificationAction.Context.Actions.Remove(calendarNotificationAction);
						}
					}
					else if (initiating != null)
					{
						initiating.Dispose();
						CalendarNotificationInitiator.pendingInitiatings.Remove(initiating);
					}
				}
				if (this.ScheduledAction != null && !this.ScheduledAction.IsDisposed)
				{
					if (calendarNotificationAction2 != null)
					{
						if (reminderEmitting2 != null)
						{
							ExTraceGlobals.AssistantTracer.TraceDebug((long)this.GetHashCode(), "rmd+++++ caller: {0}, subj: {1}, usr: {2}, calItemId: {3}, calItemOccId: {4}, event_t: {5}, rmd_t: {6}, s_t: {7}, e_t: {8}", new object[]
							{
								this.ContextInfo,
								reminderEmitting2.CalendarInfo.NormalizedSubject,
								reminderEmitting2.Context,
								reminderEmitting2.CalendarInfo.CalendarItemIdentity,
								reminderEmitting2.CalendarInfo.CalendarItemOccurrenceIdentity,
								reminderEmitting2.CalendarInfo.CreationRequestTime,
								reminderEmitting2.CalendarInfo.ReminderTime,
								reminderEmitting2.CalendarInfo.StartTime,
								reminderEmitting2.CalendarInfo.EndTime
							});
						}
						if (calendarNotificationAction2.Context != null && !calendarNotificationAction2.IsDisposed && !calendarNotificationAction2.Context.Actions.IsDisposed)
						{
							calendarNotificationAction2.Context.Actions.Add(calendarNotificationAction2);
						}
						calendarNotificationAction2.Schedule();
						return;
					}
					if (initiating2 != null)
					{
						CalendarNotificationInitiator.pendingInitiatings.Add(initiating2);
						initiating2.Schedule();
					}
				}
			}
		}
	}
}
