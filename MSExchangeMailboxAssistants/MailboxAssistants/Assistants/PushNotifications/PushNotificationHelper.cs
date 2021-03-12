using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.PushNotifications;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000210 RID: 528
	internal static class PushNotificationHelper
	{
		// Token: 0x06001432 RID: 5170 RVA: 0x00074864 File Offset: 0x00072A64
		internal static void LogHandleEventError(Exception ex, IDatabaseInfo databaseInfo, IMapiEvent mapiEvent)
		{
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ErrorProcessingHandleEvent, ex.ToString(), new object[]
			{
				ex.ToString(),
				(ex.InnerException != null) ? ex.InnerException.ToString() : string.Empty,
				databaseInfo.DatabaseName,
				mapiEvent.MailboxGuid
			});
			PushNotificationsCrimsonEvents.AssistantException.Log<string, Guid, string, string>(databaseInfo.DatabaseName, mapiEvent.MailboxGuid, ex.ToTraceString(), string.Empty);
			ExTraceGlobals.PushNotificationAssistantTracer.TraceError<string, Guid, string>(0L, "Notification for account {2}:{3} failed with error: {0}.", databaseInfo.DatabaseName, mapiEvent.MailboxGuid, ex.ToTraceString());
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00074910 File Offset: 0x00072B10
		internal static void LogProcessNotificationBatchException(AggregateException exceptions)
		{
			foreach (Exception ex in exceptions.InnerExceptions)
			{
				ExTraceGlobals.PushNotificationAssistantTracer.TraceError<string, string>(0L, "An error '{0}'-'{1}' was generated when processing a notification batch.", ex.ToString(), (ex.InnerException != null) ? ex.InnerException.ToString() : string.Empty);
				PushNotificationsCrimsonEvents.ProcessNotificationBatchException.LogPeriodic<string>(ex.Message, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, ex.ToTraceString());
			}
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x000749A4 File Offset: 0x00072BA4
		internal static void LogSendPublishNotificationException(Exception exception, MailboxNotificationBatch batch = null)
		{
			PushNotificationsMonitoring.PublishFailureNotification("SendPublishNotification", "", "");
			ExTraceGlobals.PushNotificationAssistantTracer.TraceError<string, string>(0L, "An error '{0}'-'{1}' was generated when publishing a notification batch.", exception.ToString(), (exception.InnerException != null) ? exception.InnerException.ToString() : string.Empty);
			PushNotificationsCrimsonEvents.SendPublishNotificationException.LogPeriodic<string>(exception.Message, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, exception.ToTraceString());
			if (batch != null && batch.Notifications != null)
			{
				foreach (MailboxNotification notification in batch.Notifications)
				{
					NotificationTracker.ReportDropped(notification, exception.ToTraceString());
				}
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00074A68 File Offset: 0x00072C68
		internal static void LogSubscriptionUpdated(IPushNotificationSubscriptionItem subscription, PushNotificationServerSubscription subscriptionContract, Guid mailboxGuid)
		{
			PushNotificationsCrimsonEvents.SubscriptionCreated.Log<string, string, Guid, string>(subscription.SubscriptionId, subscriptionContract.AppId, mailboxGuid, subscription.SerializedNotificationSubscription);
			ExTraceGlobals.PushNotificationAssistantTracer.TraceDebug<Guid, byte>(0L, "Mailbox Header Table updated for {0} with new Subscription type {1}.", mailboxGuid, (byte)subscriptionContract.GetSubscriptionOption());
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00074AA0 File Offset: 0x00072CA0
		internal static void LogAssistantStartup(string databaseName, int totalSubscriptions)
		{
			PushNotificationsCrimsonEvents.AssistantStartup.LogPeriodic<string, int>(databaseName, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, databaseName, totalSubscriptions);
			ExTraceGlobals.PushNotificationAssistantTracer.TraceDebug<string, int>(0L, "Assistant started for Database '{0}' with a total number of subscriptions: '{1}'. [{2},{3}]", databaseName, totalSubscriptions);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00074AC8 File Offset: 0x00072CC8
		internal static void LogBatchManager(PushNotificationBatchManagerConfig config)
		{
			PushNotificationsCrimsonEvents.AssistantBatchManager.Log<uint, uint>(config.BatchSize, config.BatchTimerInSeconds);
			ExTraceGlobals.PushNotificationAssistantTracer.TraceDebug<uint, uint>(0L, "Batch Manager created with the following settings: BatchSize='%1', BatchTimeout='%2'", config.BatchSize, config.BatchTimerInSeconds);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00074AFD File Offset: 0x00072CFD
		internal static void LogSubscriptionExpired(Guid mailboxGuid)
		{
			PushNotificationsCrimsonEvents.SubscriptionExpired.Log<Guid, string>(mailboxGuid, string.Empty);
			ExTraceGlobals.PushNotificationAssistantTracer.TraceDebug<Guid>(0L, "All active subscriptions are expired for mailbox {0}.", mailboxGuid);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00074B21 File Offset: 0x00072D21
		internal static void LogMailboxNotificationResolution(Guid mailboxGuid, PushNotificationContext notificationContext)
		{
			PushNotificationsCrimsonEvents.MailboxNotificationResolved.Log<Guid, string>(mailboxGuid, notificationContext.ToString());
			ExTraceGlobals.PushNotificationAssistantTracer.TraceDebug<Guid, string>(0L, "A PushNotificationContext for Mailbox '{0}' is resolved: '{1}'.", mailboxGuid, notificationContext.ToString());
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00074B4C File Offset: 0x00072D4C
		internal static void LogActiveSubscriptions(Guid mailboxGuid, List<PushNotificationServerSubscription> subscriptions)
		{
			bool flag = PushNotificationsCrimsonEvents.ActiveSubscriptions.IsEnabled(PushNotificationsCrimsonEvent.Provider);
			bool flag2 = ExTraceGlobals.PushNotificationAssistantTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (!flag && !flag2)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PushNotificationServerSubscription pushNotificationServerSubscription in subscriptions)
			{
				stringBuilder.AppendFormat("{0}\n", pushNotificationServerSubscription.ToFullString());
			}
			string text = stringBuilder.ToString();
			if (flag)
			{
				PushNotificationsCrimsonEvents.ActiveSubscriptions.LogPeriodic<string, string, Guid, string>(mailboxGuid, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, string.Empty, string.Empty, mailboxGuid, text);
			}
			if (flag2)
			{
				ExTraceGlobals.PushNotificationAssistantTracer.TraceDebug<Guid, string>(0L, "Active subscriptions resolved on the Assistant for user:'{0}' are:\n'{1}'.", mailboxGuid, text);
			}
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00074C14 File Offset: 0x00072E14
		internal static void LogNotificationBatchEntry(Guid mailboxGuid, MailboxNotification notification)
		{
			if (PushNotificationsCrimsonEvents.NotificationBatchEntry.IsEnabled(PushNotificationsCrimsonEvent.Provider))
			{
				PushNotificationsCrimsonEvents.NotificationBatchEntry.Log<Guid, string>(mailboxGuid, notification.ToFullString());
			}
			if (ExTraceGlobals.PushNotificationAssistantTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.PushNotificationAssistantTracer.TraceDebug<Guid, string>(0L, "MailboxNotification processed for Mailbox '{0}': {1}", mailboxGuid, notification.ToFullString());
			}
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00074C68 File Offset: 0x00072E68
		internal static void LogAssistantPublishingStatus(bool isEnabled)
		{
			PushNotificationsCrimsonEvents.AssistantPublishingStatus.Log<bool>(isEnabled);
			ExTraceGlobals.PushNotificationAssistantTracer.TraceDebug<bool>(0L, "Assistant Publishing is set to '{0}' through configuration.", isEnabled);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00074C88 File Offset: 0x00072E88
		internal static void CheckAndLogInvalidConfigurationSetting(RegistryObject instance)
		{
			if (instance == null)
			{
				return;
			}
			if (!instance.IsValid)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (ValidationError validationError in instance.Validate())
				{
					stringBuilder.AppendLine(string.Format("{0}: {1}", validationError.PropertyName, validationError.Description));
				}
				string text = (instance.Identity != null) ? instance.Identity.ToString() : string.Empty;
				PushNotificationsCrimsonEvents.InvalidConfiguration.Log<string, StringBuilder>(text, stringBuilder);
				ExTraceGlobals.PushNotificationAssistantTracer.TraceWarning<string, StringBuilder>(0L, "Invalid configuration settings were detected for '{0}':\n{1}", text, stringBuilder);
			}
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00074D24 File Offset: 0x00072F24
		internal static void LogInvalidSubscription(Guid mailboxGuid, PushNotificationSubscription subscription)
		{
			string text = subscription.ToFullString();
			PushNotificationsCrimsonEvents.InvalidSubscriptionInMailbox.LogPeriodic<Guid, string>(text, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, mailboxGuid, text);
			ExTraceGlobals.PushNotificationAssistantTracer.TraceWarning<string, Guid>(0L, "An invalid notification was detected in mailbox '{0}': '{1}'.", text, mailboxGuid);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00074D5E File Offset: 0x00072F5E
		internal static void LogSuppressedNotifications(Guid mailboxGuid, PushNotificationSubscription subscription)
		{
			PushNotificationsCrimsonEvents.SuppressedNotificationsWhileOof.Log<string, Guid, string>(subscription.AppId, mailboxGuid, subscription.DeviceNotificationId);
			ExTraceGlobals.PushNotificationAssistantTracer.TraceDebug<string, Guid, string>(0L, "A notification was skipped because user is OOF. AppId - '{0}', Mailbox - '{1}', DeviceId - '{2}'", subscription.AppId, mailboxGuid, subscription.DeviceNotificationId);
		}

		// Token: 0x04000C2F RID: 3119
		internal const string PushNotificationAssistantSubkey = "PushNotificationAssistant";
	}
}
