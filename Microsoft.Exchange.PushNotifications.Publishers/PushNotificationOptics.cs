using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200000F RID: 15
	internal class PushNotificationOptics : IPushNotificationOptics
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002BC0 File Offset: 0x00000DC0
		static PushNotificationOptics()
		{
			foreach (ExPerformanceCounter exPerformanceCounter in PublisherManagerCounters.AllCounters)
			{
				exPerformanceCounter.Reset();
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002BF5 File Offset: 0x00000DF5
		public void ReportReceived(MulticastNotification notification, PushNotificationPublishingContext context)
		{
			PublisherManagerCounters.TotalMulticastNotificationRequests.Increment();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002C0A File Offset: 0x00000E0A
		public void ReportDiscardedByValidation(MulticastNotification notification)
		{
			PublisherManagerCounters.TotalInvalidMulticastNotifications.Increment();
			PushNotificationsCrimsonEvents.InvalidMulticastNotification.Log<string>(notification.ToNullableString((MulticastNotification x) => x.ToFullString()));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002C44 File Offset: 0x00000E44
		public void ReportReceived(Notification notification, PushNotificationPublishingContext context)
		{
			PublisherManagerCounters.TotalNotificationRequests.Increment();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002C5C File Offset: 0x00000E5C
		public void ReportDiscardedByValidation(Notification notification)
		{
			PublisherManagerCounters.TotalDiscardedNotifications.Increment();
			PublisherManagerCounters.TotalInvalidNotifications.Increment();
			PushNotificationsCrimsonEvents.InvalidNotification.Log<string, string>(notification.ToNullableString((Notification x) => x.ToFullString()), notification.ValidationErrors.ToNullableString(null));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public void ReportDiscardedByUnsuitablePublisher(Notification notification)
		{
			PublisherManagerCounters.TotalDiscardedNotifications.Increment();
			if (PushNotificationsCrimsonEvents.PushNotificationUnsuitableAppId.IsEnabled(PushNotificationsCrimsonEvent.Provider))
			{
				PushNotificationsCrimsonEvents.PushNotificationUnsuitableAppId.LogPeriodic<string, string, string>(notification.AppId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, notification.AppId, notification.Identifier, notification.ToFullString());
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D09 File Offset: 0x00000F09
		public void ReportDiscardedByUnknownPublisher(Notification notification)
		{
			PublisherManagerCounters.TotalDiscardedNotifications.Increment();
			PushNotificationsCrimsonEvents.PushNotificationUnknownAppId.LogPeriodic<string, string, string>(notification.AppId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, notification.AppId, notification.Identifier, notification.ToFullString());
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002D3E File Offset: 0x00000F3E
		public void ReportDiscardedByUnknownMapping(Notification notification)
		{
			PublisherManagerCounters.TotalDiscardedNotifications.Increment();
			PushNotificationsCrimsonEvents.NotificationMappingNotFound.LogPeriodic<string, string>(notification.AppId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, notification.AppId, notification.ToFullString());
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D6D File Offset: 0x00000F6D
		public void ReportDiscardedByFailedMapping(Notification notification)
		{
			PublisherManagerCounters.TotalDiscardedNotifications.Increment();
			PushNotificationsCrimsonEvents.NotificationMappingFailed.LogPeriodic<string, string>(notification.AppId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, notification.AppId, notification.ToFullString());
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D9C File Offset: 0x00000F9C
		public void ReportDiscardedByDisposedPublisher(Notification notification)
		{
			PublisherManagerCounters.TotalDiscardedNotifications.Increment();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002DA9 File Offset: 0x00000FA9
		public void ReportProcessed(Notification notification, PushNotification pushNotification, PushNotificationPublishingContext context)
		{
			NotificationTracker.ReportReceived(notification, context.Source);
			PushNotificationTracker.ReportCreated(pushNotification, context.ReceivedTime, notification);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public void ReportDiscardedByMissmatchingType(PushNotification notification)
		{
			PublisherManagerCounters.TotalDiscardedPushNotifications.Increment();
			PushNotificationsCrimsonEvents.InvalidPushNotificationType.Log<string, string>(notification.AppId, notification.ToFullString());
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public void ReportDiscardedByValidation(PushNotification notification, Exception ex)
		{
			PublisherManagerCounters.TotalInvalidNotifications.Increment();
			PublisherManagerCounters.TotalDiscardedPushNotifications.Increment();
			string nullable = (notification != null) ? notification.AppId : null;
			PushNotificationsCrimsonEvents.InvalidPushNotification.Log<string, string, string>(nullable.ToNullableString(), notification.ToNullableString((PushNotification x) => x.ToFullString()), ex.ToNullableString((Exception x) => x.ToTraceString()));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002E80 File Offset: 0x00001080
		public void ReportDiscardedByUnsuitablePublisher(PushNotification notification)
		{
			PublisherManagerCounters.TotalDiscardedPushNotifications.Increment();
			if (PushNotificationsCrimsonEvents.PushNotificationUnsuitableAppId.IsEnabled(PushNotificationsCrimsonEvent.Provider))
			{
				PushNotificationsCrimsonEvents.PushNotificationUnsuitableAppId.LogPeriodic<string, string, string>(notification.AppId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, notification.AppId, notification.Identifier, notification.ToFullString());
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002ED1 File Offset: 0x000010D1
		public void ReportDiscardedByUnknownPublisher(PushNotification notification)
		{
			PublisherManagerCounters.TotalDiscardedPushNotifications.Increment();
			PushNotificationsCrimsonEvents.PushNotificationUnknownAppId.LogPeriodic<string, string, string>(notification.AppId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, notification.AppId, notification.Identifier, notification.ToFullString());
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002F10 File Offset: 0x00001110
		public void ReportDiscardedByThrottling(PushNotification notification, OverBudgetException obe)
		{
			PublisherManagerCounters.TotalDiscardedPushNotifications.Increment();
			string text = obe.ToNullableString((OverBudgetException x) => x.ToTraceString());
			PushNotificationTracker.ReportDropped(notification, text);
			PushNotificationsCrimsonEvents.DeviceOverBudget.LogPeriodic<string, string, string>(notification.RecipientId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, notification.AppId, notification.ToFullString(), text);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002F76 File Offset: 0x00001176
		public void ReportDiscardedByDisposedPublisher(PushNotification notification)
		{
			PublisherManagerCounters.TotalDiscardedPushNotifications.Increment();
		}

		// Token: 0x0400001E RID: 30
		public static readonly PushNotificationOptics Default = new PushNotificationOptics();
	}
}
