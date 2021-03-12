using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Data.PushNotifications;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.Extensions;
using Microsoft.Exchange.PushNotifications.Utils;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000208 RID: 520
	internal class PushNotificationBatchManager : BaseNotificationBatchManager<Guid, PushNotificationContext>
	{
		// Token: 0x060013F9 RID: 5113 RVA: 0x00073C50 File Offset: 0x00071E50
		internal PushNotificationBatchManager(PushNotificationBatchManagerConfig assistantConfig, IPublisherServiceContract publisherClient) : base((assistantConfig != null) ? assistantConfig.BatchTimerInSeconds : 0U, (assistantConfig != null) ? assistantConfig.BatchSize : 0U)
		{
			ArgumentValidator.ThrowIfNull("assistantConfig", assistantConfig);
			ArgumentValidator.ThrowIfNull("publisherClient", publisherClient);
			this.configuration = assistantConfig;
			this.publisherClient = publisherClient;
			PushNotificationHelper.LogBatchManager(this.configuration);
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x00073CAA File Offset: 0x00071EAA
		public PushNotificationBatchManagerConfig BatchManagerConfig
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00073CB4 File Offset: 0x00071EB4
		internal override void Add(Guid guid, PushNotificationContext notificationContext)
		{
			if (notificationContext.UnseenEmailCount == null)
			{
				return;
			}
			base.Add(guid, notificationContext);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00073CDA File Offset: 0x00071EDA
		protected override void Merge(PushNotificationContext notificationDst, PushNotificationContext notificationSrc)
		{
			notificationDst.Merge(notificationSrc);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00073CE3 File Offset: 0x00071EE3
		protected override void ReportDiscardedNotifications(int discarded)
		{
			PushNotificationBatchManager.discardedNotificationsPerBatchCounter.AddSample((long)discarded);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00073CF1 File Offset: 0x00071EF1
		protected override void ReportDrainNotificationsException(AggregateException error)
		{
			PushNotificationHelper.LogProcessNotificationBatchException(error);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00073CF9 File Offset: 0x00071EF9
		protected override void ReportBatchCancelled()
		{
			ExTraceGlobals.PushNotificationAssistantTracer.TraceWarning((long)this.GetHashCode(), "GenericNotificationBatchManager.CheckCancellation(): Processing a batch cancelled before sending it.");
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00073D14 File Offset: 0x00071F14
		protected override void DrainNotifications(ConcurrentDictionary<Guid, PushNotificationContext> notificationContexts)
		{
			MailboxNotificationBatch mailboxNotificationBatch = new MailboxNotificationBatch();
			foreach (Guid guid in notificationContexts.Keys)
			{
				if (base.CheckCancellation())
				{
					return;
				}
				PushNotificationContext pushNotificationContext = notificationContexts[guid];
				MailboxNotification notification = this.CreateMailboxNotification(guid, pushNotificationContext);
				mailboxNotificationBatch.Add(notification);
				NotificationTracker.ReportCreated(notification, pushNotificationContext.OriginalTime);
				PushNotificationHelper.LogNotificationBatchEntry(guid, notification);
			}
			if (!mailboxNotificationBatch.IsEmpty)
			{
				ExTraceGlobals.PushNotificationAssistantTracer.TraceDebug<int>((long)this.GetHashCode(), "PushNotificationBatchManager.DrainNotifications: Ready to send notification batch, size:'{0}'.", mailboxNotificationBatch.Count);
				if (base.CheckCancellation())
				{
					return;
				}
				PushNotificationBatchManager.notificationsPerBatchCounter.AddSample((long)mailboxNotificationBatch.Count);
				this.SendPublishNotificationRequest(mailboxNotificationBatch);
			}
			notificationContexts.Clear();
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00073E68 File Offset: 0x00072068
		private void SendPublishNotificationRequest(MailboxNotificationBatch batch)
		{
			AverageTimeCounter requestCounter = new AverageTimeCounter(PushNotificationsAssistantPerfCounters.AveragePublishingRequestProcessing, PushNotificationsAssistantPerfCounters.AveragePublishingRequestProcessingBase, true);
			this.publisherClient.BeginPublishNotifications(batch, delegate(IAsyncResult asyncResult)
			{
				object asyncState = asyncResult.AsyncState;
				try
				{
					this.publisherClient.EndPublishNotifications(asyncResult);
					PushNotificationsMonitoring.PublishSuccessNotification("SendPublishNotification", "");
				}
				catch (Exception exception)
				{
					PushNotificationsAssistantPerfCounters.PublishingRequestErrors.Increment();
					PushNotificationHelper.LogSendPublishNotificationException(exception, null);
				}
				finally
				{
					requestCounter.Stop();
				}
			}, requestCounter);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00073EB8 File Offset: 0x000720B8
		private MailboxNotification CreateMailboxNotification(Guid mailboxGuid, PushNotificationContext context)
		{
			MailboxNotificationPayload payload = new MailboxNotificationPayload(context.TenantId, context.UnseenEmailCount, context.BackgroundSyncType, null);
			List<MailboxNotificationRecipient> list = new List<MailboxNotificationRecipient>(context.Subscriptions.Count);
			foreach (PushNotificationServerSubscription pushNotificationServerSubscription in context.Subscriptions)
			{
				if (this.IsSubscriptionSuitable(pushNotificationServerSubscription))
				{
					list.Add(new MailboxNotificationRecipient(pushNotificationServerSubscription.AppId, pushNotificationServerSubscription.DeviceNotificationId, pushNotificationServerSubscription.LastSubscriptionUpdate, pushNotificationServerSubscription.InstallationId));
				}
				else
				{
					PushNotificationHelper.LogInvalidSubscription(mailboxGuid, pushNotificationServerSubscription);
				}
			}
			return new MailboxNotification(payload, list);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00073F6C File Offset: 0x0007216C
		private bool IsSubscriptionSuitable(PushNotificationSubscription subscription)
		{
			return subscription.Platform.SupportsSubscriptions() && !string.IsNullOrWhiteSpace(subscription.AppId) && !string.IsNullOrWhiteSpace(subscription.DeviceNotificationId);
		}

		// Token: 0x04000C1E RID: 3102
		private static AverageCounter notificationsPerBatchCounter = new AverageCounter(PushNotificationsAssistantPerfCounters.NotificationsPerBatch, PushNotificationsAssistantPerfCounters.NotificationsPerBatchBase);

		// Token: 0x04000C1F RID: 3103
		private static AverageCounter discardedNotificationsPerBatchCounter = new AverageCounter(PushNotificationsAssistantPerfCounters.DiscardedNotificationsPerBatch, PushNotificationsAssistantPerfCounters.DiscardedNotificationsPerBatchBase);

		// Token: 0x04000C20 RID: 3104
		private readonly PushNotificationBatchManagerConfig configuration;

		// Token: 0x04000C21 RID: 3105
		private readonly IPublisherServiceContract publisherClient;
	}
}
