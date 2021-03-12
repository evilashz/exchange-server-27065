using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000047 RID: 71
	internal static class PushNotificationsMonitoring
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x00005D88 File Offset: 0x00003F88
		static PushNotificationsMonitoring()
		{
			HashSet<PushNotificationApp> hashSet = new HashSet<PushNotificationApp>();
			foreach (PushNotificationSetupConfig pushNotificationSetupConfig in PushNotificationCannedSet.PushNotificationSetupEnvironmentConfig.Values)
			{
				hashSet.UnionWith(pushNotificationSetupConfig.InstallableBySetup);
			}
			PushNotificationsMonitoring.CannedAppPlatformSet = new Dictionary<string, PushNotificationPlatform>();
			foreach (PushNotificationApp pushNotificationApp in hashSet)
			{
				if (!PushNotificationsMonitoring.CannedAppPlatformSet.ContainsKey(pushNotificationApp.Name))
				{
					PushNotificationsMonitoring.CannedAppPlatformSet.Add(pushNotificationApp.Name, pushNotificationApp.Platform);
				}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00005E54 File Offset: 0x00004054
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00005E5B File Offset: 0x0000405B
		internal static Dictionary<string, PushNotificationPlatform> CannedAppPlatformSet { get; private set; }

		// Token: 0x060001CA RID: 458 RVA: 0x00005E64 File Offset: 0x00004064
		internal static void PublishSuccessNotification(string notificationEvent, string targetResource = "")
		{
			EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.PushNotificationsProtocol.Name, notificationEvent, targetResource, string.Empty, ResultSeverityLevel.Informational);
			eventNotificationItem.Publish(false);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005E90 File Offset: 0x00004090
		internal static void PublishFailureNotification(string notificationEvent, string targetResource = "", string failureReason = "")
		{
			new EventNotificationItem(ExchangeComponent.PushNotificationsProtocol.Name, notificationEvent, targetResource, string.Empty, ResultSeverityLevel.Error)
			{
				StateAttribute2 = failureReason
			}.Publish(false);
		}

		// Token: 0x040000A0 RID: 160
		internal const string ApnsChannelConnect = "ApnsChannelConnect";

		// Token: 0x040000A1 RID: 161
		internal const string ApnsChannelAuthenticate = "ApnsChannelAuthenticate";

		// Token: 0x040000A2 RID: 162
		internal const string ApnsCertPresent = "ApnsCertPresent";

		// Token: 0x040000A3 RID: 163
		internal const string ApnsCertPrivateKey = "ApnsCertPrivateKey";

		// Token: 0x040000A4 RID: 164
		internal const string ApnsCertValidation = "ApnsCertValidation";

		// Token: 0x040000A5 RID: 165
		internal const string ApnsCertLoaded = "ApnsCertLoaded";

		// Token: 0x040000A6 RID: 166
		internal const string WnsChannelBackOff = "WnsChannelBackOff";

		// Token: 0x040000A7 RID: 167
		internal const string GcmChannelBackOff = "GcmChannelBackOff";

		// Token: 0x040000A8 RID: 168
		internal const string WebAppChannelBackOff = "WebAppChannelBackOff";

		// Token: 0x040000A9 RID: 169
		internal const string AzureChannelBackOff = "AzureChannelBackOff";

		// Token: 0x040000AA RID: 170
		internal const string AzureHubCreationChannelBackOff = "AzureHubCreationChannelBackOff";

		// Token: 0x040000AB RID: 171
		internal const string AzureDeviceRegistrationChannelBackOff = "AzureDeviceRegistrationChannelBackOff";

		// Token: 0x040000AC RID: 172
		internal const string AzureChallengeRequestChannelBackOff = "AzureChallengeRequestChannelBackOff";

		// Token: 0x040000AD RID: 173
		internal const string SendPublishNotification = "SendPublishNotification";

		// Token: 0x040000AE RID: 174
		internal const string NotificationProcessed = "NotificationProcessed";

		// Token: 0x040000AF RID: 175
		internal const string HubCreationProcessed = "HubCreationProcessed";

		// Token: 0x040000B0 RID: 176
		internal const string DeviceRegistrationProcessed = "DeviceRegistrationProcessed";

		// Token: 0x040000B1 RID: 177
		internal const string ChallengeRequestProcessed = "ChallengeRequestProcessed";

		// Token: 0x040000B2 RID: 178
		internal const string EnterpriseNotificationProcessed = "EnterpriseNotificationProcessed";
	}
}
