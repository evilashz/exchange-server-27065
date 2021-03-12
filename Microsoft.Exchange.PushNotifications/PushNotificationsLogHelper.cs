using System;
using System.Net;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000046 RID: 70
	internal static class PushNotificationsLogHelper
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00005D28 File Offset: 0x00003F28
		public static void LogServerVersion()
		{
			PushNotificationsCrimsonEvents.ServerVersion.Log<string>("15.00.1497.010");
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005D39 File Offset: 0x00003F39
		public static void LogOnPremPublishingResponse(WebHeaderCollection headers)
		{
			if (PushNotificationsCrimsonEvents.OnPremPublisherServiceProxyHeaders.IsEnabled(PushNotificationsCrimsonEvent.Provider))
			{
				PushNotificationsCrimsonEvents.OnPremPublisherServiceProxyHeaders.Log<string>(headers.ToTraceString(null));
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005D5D File Offset: 0x00003F5D
		public static void LogOnPremPublishingError(Exception error, WebHeaderCollection headers)
		{
			if (error == null)
			{
				return;
			}
			PushNotificationsCrimsonEvents.OnPremPublisherServiceProxyError.LogPeriodic<string, string>(error.ToString(), CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, headers.ToTraceString(null), error.ToTraceString());
		}
	}
}
