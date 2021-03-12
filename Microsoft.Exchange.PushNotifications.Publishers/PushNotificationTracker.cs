using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000018 RID: 24
	internal static class PushNotificationTracker
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00004040 File Offset: 0x00002240
		public static void ReportCreated(PushNotification pushNotification, ExDateTime timestamp, Notification notification)
		{
			PushNotificationTracker.LogPushNotificationState(pushNotification, "NotificationCreated", new ExDateTime?(timestamp), notification.Identifier, PushNotificationPlatform.None);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000405C File Offset: 0x0000225C
		public static void ReportSent(PushNotification pushNotification, PushNotificationPlatform platform = PushNotificationPlatform.None)
		{
			PushNotificationTracker.LogPushNotificationState(pushNotification, "NotificationSent", null, null, platform);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000407F File Offset: 0x0000227F
		public static void ReportDropped(PushNotification pushNotification, string traces = "")
		{
			PushNotificationsCrimsonEvents.NotificationDropped.Log<string, string, string, string>(pushNotification.RecipientId, pushNotification.AppId, pushNotification.Identifier, traces);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000040A0 File Offset: 0x000022A0
		private static void LogPushNotificationState(PushNotification pushNotification, string state, ExDateTime? timestamp = null, string notificationId = null, PushNotificationPlatform platform = PushNotificationPlatform.None)
		{
			string text = (timestamp != null) ? timestamp.Value.ToString("o") : string.Empty;
			PushNotificationsCrimsonEvents.NotificationTiming.Log<string, string, string, string, string, string, string, bool>(pushNotification.AppId, pushNotification.Identifier, state, (platform == PushNotificationPlatform.None) ? string.Empty : platform.ToString(), text, pushNotification.RecipientId, notificationId ?? string.Empty, pushNotification.IsBackgroundSyncAvailable);
		}

		// Token: 0x0400003B RID: 59
		public const string NotificationSentLabel = "NotificationSent";

		// Token: 0x0400003C RID: 60
		public const string NotificationCreatedLabel = "NotificationCreated";
	}
}
