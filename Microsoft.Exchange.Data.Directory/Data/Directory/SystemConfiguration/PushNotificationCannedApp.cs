using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200054E RID: 1358
	internal class PushNotificationCannedApp
	{
		// Token: 0x04002950 RID: 10576
		public static readonly PushNotificationApp ApnsMowaDogfood = new PushNotificationApp
		{
			Name = "com.microsoft.mowadogfood",
			DisplayName = "MOWA Dogfood app for APNS",
			Platform = PushNotificationPlatform.APNS,
			AuthenticationKey = "1fed2f9d78ffd3881f88a85647d466f2760eb465",
			AuthenticationKeyFallback = "bed91e10743d26846b8dbb92a2b6a14d0fbf2ca2"
		};

		// Token: 0x04002951 RID: 10577
		public static readonly PushNotificationApp AzureMowaDogfood = new PushNotificationApp
		{
			Name = "com.microsoft.mowadogfood",
			DisplayName = "Azure MOWA Dogfood App",
			Platform = PushNotificationPlatform.Azure,
			AuthenticationId = "DatacenterManagementKey",
			RegistrationTemplate = "<AppleTemplateRegistrationDescription xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.microsoft.com/netservices/2010/10/servicebus/connect\"><Tags>{0}</Tags><DeviceToken>{1}</DeviceToken><BodyTemplate><![CDATA[{{\"aps\":{{\"alert\":\"$(message)\",\"badge\":\"#(unseen)\",\"content-available\":\"#(background)\"}},\"o\":\"$(storeId)\",\"t\":\"$(syncType)\",\"n\":\"$(id)\"}}]]></BodyTemplate></AppleTemplateRegistrationDescription>"
		};

		// Token: 0x04002952 RID: 10578
		public static readonly PushNotificationApp AzureOutlookWindowsImmersive = new PushNotificationApp
		{
			Name = "Microsoft.Office.Outlook",
			DisplayName = "Outlook Immersive Windows App",
			Platform = PushNotificationPlatform.Azure,
			AuthenticationId = "DatacenterManagementKey",
			RegistrationTemplate = "<WindowsTemplateRegistrationDescription xmlns=\"http://schemas.microsoft.com/netservices/2010/10/servicebus/connect\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Tags>{0}</Tags><ChannelUri>{1}</ChannelUri><BodyTemplate><![CDATA[{{\"data\":\"$(storeId)\"}}]]></BodyTemplate><WnsHeaders><WnsHeader><Header>X-WNS-Type</Header><Value>wns/raw</Value></WnsHeader></WnsHeaders></WindowsTemplateRegistrationDescription>"
		};

		// Token: 0x04002953 RID: 10579
		public static readonly PushNotificationApp ApnsMowaOfficialIPhone = new PushNotificationApp
		{
			Name = "com.microsoft.exchange.iphone",
			DisplayName = "MOWA iPhone app",
			Platform = PushNotificationPlatform.APNS,
			AuthenticationKey = "fd700c374cb324a21cfb5766c5e2631fcec53f41",
			AuthenticationKeyFallback = "9d130d73509c6a5f06c861c0949aacf1b325552e"
		};

		// Token: 0x04002954 RID: 10580
		public static readonly PushNotificationApp ApnsMowaOfficialIPad = new PushNotificationApp
		{
			Name = "com.microsoft.exchange.ipad",
			DisplayName = "MOWA iPad app",
			Platform = PushNotificationPlatform.APNS,
			AuthenticationKey = "1656d6b99c2d3393009c7ae5976230b622e17437",
			AuthenticationKeyFallback = "0f6fee26ac96ad14cab0110d5cf896f77c4b7c4f"
		};

		// Token: 0x04002955 RID: 10581
		public static readonly PushNotificationApp GcmMowaOfficial = new PushNotificationApp
		{
			Name = "com.microsoft.exchange.mowa",
			DisplayName = "MOWA app for GCM",
			Platform = PushNotificationPlatform.GCM,
			AuthenticationId = "909480690528"
		};

		// Token: 0x04002956 RID: 10582
		public static readonly PushNotificationApp AzureGcmMowaOfficial = new PushNotificationApp
		{
			Name = "com.microsoft.exchange.mowa",
			DisplayName = "MOWA app for GCM",
			Platform = PushNotificationPlatform.Azure,
			AuthenticationId = "DatacenterManagementKey",
			RegistrationTemplate = "<GcmTemplateRegistrationDescription xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.microsoft.com/netservices/2010/10/servicebus/connect\"><Tags>{0}</Tags><GcmRegistrationId>{1}</GcmRegistrationId><BodyTemplate><![CDATA[ {{\"data\":{{\"UnseenEmailCount\":\"#(unseen)\",\"Message\":\"$(message)\",\"BackgroundSyncType\":\"$(syncType)\",\"ServerNotificationId\":\"$(id)\"}}}}]]></BodyTemplate></GcmTemplateRegistrationDescription>"
		};

		// Token: 0x04002957 RID: 10583
		public static readonly PushNotificationApp WnsOutlookMailOfficialWindowsImmersive = new PushNotificationApp
		{
			Name = "Microsoft.Office.Mail",
			DisplayName = "Outlook Immersive Mail Windows app",
			Platform = PushNotificationPlatform.WNS,
			AuthenticationId = "ms-app://s-1-15-2-1190338913-2827757641-1409753268-749094866-3305885335-2697392884-2267884757"
		};

		// Token: 0x04002958 RID: 10584
		public static readonly PushNotificationApp WnsOutlookCalendarOfficialWindowsImmersive = new PushNotificationApp
		{
			Name = "Microsoft.Office.Calendar",
			DisplayName = "Outlook Immersive Calendar Windows app",
			Platform = PushNotificationPlatform.WNS,
			AuthenticationId = "ms-app://s-1-15-2-47894201-1569050309-2739407442-631412252-43010294-3300590618-1731436133"
		};

		// Token: 0x04002959 RID: 10585
		public static readonly PushNotificationApp OnPremProxy = new PushNotificationApp
		{
			Name = "OnPrem-Proxy",
			DisplayName = "On Premises Proxy app",
			Platform = PushNotificationPlatform.Proxy,
			Url = "https://outlook.office365.com/PushNotifications"
		};

		// Token: 0x0400295A RID: 10586
		public static readonly PushNotificationApp AzureHubCreation = new PushNotificationApp
		{
			Name = "azure.hubcreation",
			DisplayName = "Azure Hub Creation app",
			Platform = PushNotificationPlatform.AzureHubCreation
		};

		// Token: 0x0400295B RID: 10587
		public static readonly PushNotificationApp AzureChallengeRequest = new PushNotificationApp
		{
			Name = "azure.challengerequest",
			DisplayName = "Azure Issue Registration Challenge app",
			Platform = PushNotificationPlatform.AzureChallengeRequest
		};

		// Token: 0x0400295C RID: 10588
		public static readonly PushNotificationApp AzureDeviceRegistration = new PushNotificationApp
		{
			Name = "azure.deviceregistration",
			DisplayName = "Azure Device Registration app",
			Platform = PushNotificationPlatform.AzureDeviceRegistration
		};
	}
}
