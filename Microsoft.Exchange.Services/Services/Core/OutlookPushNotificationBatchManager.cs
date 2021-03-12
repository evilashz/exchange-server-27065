using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Exchange.PushNotifications.Extensions;
using Microsoft.Exchange.PushNotifications.Utils;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.ExchangeService;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000F72 RID: 3954
	internal class OutlookPushNotificationBatchManager : BaseNotificationBatchManager<string, PendingOutlookPushNotification>
	{
		// Token: 0x06006420 RID: 25632 RVA: 0x001388A8 File Offset: 0x00136AA8
		private OutlookPushNotificationBatchManager(ExchangeServiceTraceDelegate traceDelegate, IOutlookPushNotificationSubscriptionCache subscriptionCache, IOutlookPushNotificationSerializer notificationSerializer) : this(traceDelegate, subscriptionCache, notificationSerializer, new OutlookPublisherServiceProxy(null), 10U, 1000U)
		{
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x001388C0 File Offset: 0x00136AC0
		internal OutlookPushNotificationBatchManager(ExchangeServiceTraceDelegate traceDelegate, IOutlookPushNotificationSubscriptionCache subscriptionCache, IOutlookPushNotificationSerializer notificationSerializer, IOutlookPublisherServiceContract publisherService, uint batchTimerInSeconds, uint batchSize) : base(batchTimerInSeconds, batchSize)
		{
			this.traceDelegate = traceDelegate;
			this.subscriptionCache = subscriptionCache;
			this.publisherService = publisherService;
			this.notificationSerializer = notificationSerializer;
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x001388EC File Offset: 0x00136AEC
		internal static OutlookPushNotificationBatchManager GetInstance(ExchangeServiceTraceDelegate traceDelegate, IOutlookPushNotificationSubscriptionCache subscriptionCache, IOutlookPushNotificationSerializer notificationSerializer)
		{
			if (OutlookPushNotificationBatchManager.instance == null)
			{
				lock (OutlookPushNotificationBatchManager.instanceLock)
				{
					if (OutlookPushNotificationBatchManager.instance == null)
					{
						OutlookPushNotificationBatchManager.instance = new OutlookPushNotificationBatchManager(traceDelegate, subscriptionCache, notificationSerializer);
					}
				}
			}
			if (OutlookPushNotificationBatchManager.instance.subscriptionCache != subscriptionCache)
			{
				throw new ArgumentException("Changing subscriptionCache is not supported");
			}
			return OutlookPushNotificationBatchManager.instance;
		}

		// Token: 0x06006423 RID: 25635 RVA: 0x00138960 File Offset: 0x00136B60
		protected override void Merge(PendingOutlookPushNotification notificationDst, PendingOutlookPushNotification notificationSrc)
		{
			notificationDst.Merge(notificationSrc);
		}

		// Token: 0x06006424 RID: 25636 RVA: 0x00138969 File Offset: 0x00136B69
		protected override void ReportDrainNotificationsException(AggregateException error)
		{
			this.traceDelegate("OutlookPushNotificationBatchManager.ReportDrainNotificationsException", "Error: Caught exception " + error.ToString());
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x00138A20 File Offset: 0x00136C20
		protected override void DrainNotifications(ConcurrentDictionary<string, PendingOutlookPushNotification> pendingNotifications)
		{
			this.traceDelegate("OutlookPushNotificationBatchManager.DrainNotifications", "Processing " + pendingNotifications.Count + " notifications");
			foreach (string text in pendingNotifications.Keys)
			{
				OutlookNotificationBatch batch = new OutlookNotificationBatch();
				if (base.CheckCancellation())
				{
					break;
				}
				this.AddPendingNotificationToBatch(batch, text, pendingNotifications[text]);
				if (!batch.IsEmpty)
				{
					if (base.CheckCancellation())
					{
						break;
					}
					this.publisherService.BeginPublishOutlookNotifications(batch, delegate(IAsyncResult asyncResult)
					{
						try
						{
							this.publisherService.EndPublishOutlookNotifications(asyncResult);
							this.traceDelegate("OutlookPushNotificationBatchManager.DrainNotifications", "EndPublishOutlookNotifications succeeded with " + batch.Count + " notifications");
						}
						catch (Exception ex)
						{
							this.traceDelegate("OutlookPushNotificationBatchManager.DrainNotifications", "Error: IOutlookPublisherServiceContract.BeginPublishOutlookNotifications encountered exception " + ex.ToString());
						}
					}, null);
				}
			}
		}

		// Token: 0x06006426 RID: 25638 RVA: 0x00138B08 File Offset: 0x00136D08
		private void AddPendingNotificationToBatch(OutlookNotificationBatch batch, string notificationContext, PendingOutlookPushNotification pendingNotification)
		{
			string tenantId;
			List<OutlookServiceNotificationSubscription> activeSubscriptions;
			if (!this.subscriptionCache.QueryMailboxSubscriptions(notificationContext, out tenantId, out activeSubscriptions))
			{
				this.traceDelegate("OutlookPushNotificationBatchManager.AddPendingNotificationToBatch", "Warning: Ignored attempt to publish notifications for an uncached mailbox " + notificationContext.ToNullableString());
				return;
			}
			this.AddPayloadToBatchForAppId(batch, this.notificationSerializer.ConvertToPayloadForHxCalendar(pendingNotification), tenantId, activeSubscriptions, OutlookServiceNotificationSubscription.AppId_HxCalendar);
			this.AddPayloadToBatchForAppId(batch, this.notificationSerializer.ConvertToPayloadForHxMail(pendingNotification), tenantId, activeSubscriptions, OutlookServiceNotificationSubscription.AppId_HxMail);
		}

		// Token: 0x06006427 RID: 25639 RVA: 0x00138B7C File Offset: 0x00136D7C
		private void AddPayloadToBatchForAppId(OutlookNotificationBatch batch, byte[] payload, string tenantId, List<OutlookServiceNotificationSubscription> activeSubscriptions, string appId)
		{
			if (payload != null)
			{
				List<OutlookNotificationRecipient> list = new List<OutlookNotificationRecipient>();
				foreach (OutlookServiceNotificationSubscription outlookServiceNotificationSubscription in activeSubscriptions)
				{
					if (outlookServiceNotificationSubscription.AppId == appId)
					{
						list.Add(new OutlookNotificationRecipient(outlookServiceNotificationSubscription.PackageId, (outlookServiceNotificationSubscription.PackageId == outlookServiceNotificationSubscription.AppId) ? outlookServiceNotificationSubscription.DeviceNotificationId : outlookServiceNotificationSubscription.SubscriptionId));
					}
				}
				if (list.Count > 0)
				{
					OutlookNotification outlookNotification = new OutlookNotification(new OutlookNotificationPayload(tenantId, payload), list);
					if (outlookNotification.IsValid)
					{
						this.traceDelegate("OutlookPushNotificationBatchManager.AddPayloadToBatchForAppId", "Adding notification: " + outlookNotification.ToFullString());
						batch.Add(outlookNotification);
						return;
					}
					this.traceDelegate("OutlookPushNotificationBatchManager.AddPayloadToBatchForAppId", "Warning: Ignored invalid notification" + outlookNotification.ToNullableString(null));
				}
			}
		}

		// Token: 0x0400353D RID: 13629
		private static OutlookPushNotificationBatchManager instance = null;

		// Token: 0x0400353E RID: 13630
		private static readonly object instanceLock = new object();

		// Token: 0x0400353F RID: 13631
		private readonly ExchangeServiceTraceDelegate traceDelegate;

		// Token: 0x04003540 RID: 13632
		private readonly IOutlookPushNotificationSubscriptionCache subscriptionCache;

		// Token: 0x04003541 RID: 13633
		private readonly IOutlookPushNotificationSerializer notificationSerializer;

		// Token: 0x04003542 RID: 13634
		private readonly IOutlookPublisherServiceContract publisherService;
	}
}
