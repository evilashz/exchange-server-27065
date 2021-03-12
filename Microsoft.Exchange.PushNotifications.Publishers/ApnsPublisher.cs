using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000037 RID: 55
	internal class ApnsPublisher : PushNotificationPublisher<ApnsNotification, ApnsChannel>
	{
		// Token: 0x0600021E RID: 542 RVA: 0x000083A4 File Offset: 0x000065A4
		public ApnsPublisher(ApnsPublisherSettings publisherSettings, IApnsFeedbackProvider feedbackProvider, IThrottlingManager throttlingManager, List<IPushNotificationMapping<ApnsNotification>> mappings = null) : this(publisherSettings, feedbackProvider, throttlingManager, ExTraceGlobals.ApnsPublisherTracer, mappings)
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000083B6 File Offset: 0x000065B6
		protected ApnsPublisher(ApnsPublisherSettings publisherSettings, IApnsFeedbackProvider feedbackProvider, IThrottlingManager throttlingManager, ITracer tracer, List<IPushNotificationMapping<ApnsNotification>> mappings = null) : base(publisherSettings, tracer, throttlingManager, mappings, null, null)
		{
			ArgumentValidator.ThrowIfNull("feedbackProvider", feedbackProvider);
			this.channelSettings = publisherSettings.ChannelSettings;
			this.feedbackProvider = feedbackProvider;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000083E4 File Offset: 0x000065E4
		protected override ApnsChannel CreateNotificationChannel()
		{
			return new ApnsChannel(this.channelSettings, base.Tracer);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000083F8 File Offset: 0x000065F8
		protected override bool TryPreprocess(ApnsNotification notification)
		{
			if (!base.TryPreprocess(notification))
			{
				return false;
			}
			ApnsFeedbackValidationResult apnsFeedbackValidationResult = this.feedbackProvider.ValidateNotification(notification);
			base.Tracer.TraceDebug<ApnsNotification, string>((long)this.GetHashCode(), "[TryPreprocess] FeedbackValidationResult for '{0}': '{1}'.", notification, apnsFeedbackValidationResult.ToString());
			if (apnsFeedbackValidationResult == ApnsFeedbackValidationResult.Uncertain && (ExDateTime)notification.LastSubscriptionUpdate < ExDateTime.UtcNow.Subtract(TimeSpan.FromDays(3.0)))
			{
				base.Tracer.TraceDebug<ApnsNotification, DateTime>((long)this.GetHashCode(), "[TryPreprocess] Expiring '{0}' because the last subscription update was too long ago: '{1}'.", notification, notification.LastSubscriptionUpdate);
				apnsFeedbackValidationResult = ApnsFeedbackValidationResult.Expired;
			}
			if (apnsFeedbackValidationResult == ApnsFeedbackValidationResult.Expired)
			{
				ExTraceGlobals.PublisherManagerTracer.TraceError<string>((long)this.GetHashCode(), "[TryPreprocess] APNs notification dropped by feedback: '{0}'.", notification.ToFullString());
				PushNotificationTracker.ReportDropped(notification, "ApnsTokenExpired");
				if (PushNotificationsCrimsonEvents.ApnsPublisherFeedbackBlock.IsEnabled(PushNotificationsCrimsonEvent.Provider))
				{
					PushNotificationsCrimsonEvents.ApnsPublisherFeedbackBlock.Log<string, string>(notification.AppId, notification.ToFullString());
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000084E5 File Offset: 0x000066E5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ApnsPublisher>(this);
		}

		// Token: 0x040000D4 RID: 212
		private ApnsChannelSettings channelSettings;

		// Token: 0x040000D5 RID: 213
		private IApnsFeedbackProvider feedbackProvider;
	}
}
