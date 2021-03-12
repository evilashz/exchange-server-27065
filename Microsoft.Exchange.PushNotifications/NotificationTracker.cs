using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200000A RID: 10
	internal static class NotificationTracker
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002A10 File Offset: 0x00000C10
		public static void ReportCreated(MulticastNotification notification, ExDateTime timestamp)
		{
			foreach (Notification notification2 in notification.GetFragments())
			{
				NotificationTracker.LogNotificationState(notification2, "AssistantBatchProcessing", new ExDateTime?(timestamp), null);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A68 File Offset: 0x00000C68
		public static void ReportCreated(Notification notification, ExDateTime timestamp)
		{
			NotificationTracker.LogNotificationState(notification, "AssistantBatchProcessing", new ExDateTime?(timestamp), null);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A7C File Offset: 0x00000C7C
		public static void ReportReceived(MulticastNotification notification, string source)
		{
			foreach (Notification notification2 in notification.GetFragments())
			{
				NotificationTracker.LogNotificationState(notification2, "PublisherBatchProcessing", null, source);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public static void ReportReceived(Notification notification, string source)
		{
			NotificationTracker.LogNotificationState(notification, "PublisherBatchProcessing", null, source);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AFC File Offset: 0x00000CFC
		public static void ReportDropped(MulticastNotification notification, string traces = "")
		{
			foreach (Notification notification2 in notification.GetFragments())
			{
				NotificationTracker.ReportDropped(notification2, traces);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B4C File Offset: 0x00000D4C
		public static void ReportDropped(Notification notification, string traces = "")
		{
			PushNotificationsCrimsonEvents.NotificationDropped.Log<string, string, string, string>(notification.RecipientId, notification.AppId, notification.Identifier, traces);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B6C File Offset: 0x00000D6C
		private static void LogNotificationState(Notification notification, string state, ExDateTime? timestamp = null, string source = null)
		{
			string text = (timestamp != null) ? timestamp.Value.ToString("o") : string.Empty;
			PushNotificationsCrimsonEvents.NotificationTiming.Log<string, string, string, string, string, string, string, string>(notification.AppId, notification.Identifier, state, source ?? string.Empty, text, notification.RecipientId, string.Empty, string.Empty);
		}

		// Token: 0x04000018 RID: 24
		public const string NotificationCreatedLabel = "AssistantBatchProcessing";

		// Token: 0x04000019 RID: 25
		public const string NotificationReceivedLabel = "PublisherBatchProcessing";
	}
}
