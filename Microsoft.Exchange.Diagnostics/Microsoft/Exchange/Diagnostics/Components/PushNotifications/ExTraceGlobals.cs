using System;

namespace Microsoft.Exchange.Diagnostics.Components.PushNotifications
{
	// Token: 0x020003EF RID: 1007
	public static class ExTraceGlobals
	{
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x0005B906 File Offset: 0x00059B06
		public static Trace ExchangeToOwaTracer
		{
			get
			{
				if (ExTraceGlobals.exchangeToOwaTracer == null)
				{
					ExTraceGlobals.exchangeToOwaTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.exchangeToOwaTracer;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0005B924 File Offset: 0x00059B24
		public static Trace NotificationFormatTracer
		{
			get
			{
				if (ExTraceGlobals.notificationFormatTracer == null)
				{
					ExTraceGlobals.notificationFormatTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.notificationFormatTracer;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0005B942 File Offset: 0x00059B42
		public static Trace PublisherManagerTracer
		{
			get
			{
				if (ExTraceGlobals.publisherManagerTracer == null)
				{
					ExTraceGlobals.publisherManagerTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.publisherManagerTracer;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x0005B960 File Offset: 0x00059B60
		public static Trace StorageNotificationSubscriptionTracer
		{
			get
			{
				if (ExTraceGlobals.storageNotificationSubscriptionTracer == null)
				{
					ExTraceGlobals.storageNotificationSubscriptionTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.storageNotificationSubscriptionTracer;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0005B97E File Offset: 0x00059B7E
		public static Trace PushNotificationAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.pushNotificationAssistantTracer == null)
				{
					ExTraceGlobals.pushNotificationAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.pushNotificationAssistantTracer;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x0005B99C File Offset: 0x00059B9C
		public static Trace ApnsPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.apnsPublisherTracer == null)
				{
					ExTraceGlobals.apnsPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.apnsPublisherTracer;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0005B9BA File Offset: 0x00059BBA
		public static Trace WnsPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.wnsPublisherTracer == null)
				{
					ExTraceGlobals.wnsPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.wnsPublisherTracer;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x0005B9D8 File Offset: 0x00059BD8
		public static Trace GcmPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.gcmPublisherTracer == null)
				{
					ExTraceGlobals.gcmPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.gcmPublisherTracer;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x0005B9F6 File Offset: 0x00059BF6
		public static Trace ProxyPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.proxyPublisherTracer == null)
				{
					ExTraceGlobals.proxyPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.proxyPublisherTracer;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0005BA14 File Offset: 0x00059C14
		public static Trace PendingGetPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.pendingGetPublisherTracer == null)
				{
					ExTraceGlobals.pendingGetPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.pendingGetPublisherTracer;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0005BA33 File Offset: 0x00059C33
		public static Trace PushNotificationServiceTracer
		{
			get
			{
				if (ExTraceGlobals.pushNotificationServiceTracer == null)
				{
					ExTraceGlobals.pushNotificationServiceTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.pushNotificationServiceTracer;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x0005BA52 File Offset: 0x00059C52
		public static Trace PushNotificationClientTracer
		{
			get
			{
				if (ExTraceGlobals.pushNotificationClientTracer == null)
				{
					ExTraceGlobals.pushNotificationClientTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.pushNotificationClientTracer;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x0005BA71 File Offset: 0x00059C71
		public static Trace WebAppPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.webAppPublisherTracer == null)
				{
					ExTraceGlobals.webAppPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.webAppPublisherTracer;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0005BA90 File Offset: 0x00059C90
		public static Trace AzurePublisherTracer
		{
			get
			{
				if (ExTraceGlobals.azurePublisherTracer == null)
				{
					ExTraceGlobals.azurePublisherTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.azurePublisherTracer;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x0005BAAF File Offset: 0x00059CAF
		public static Trace AzureHubCreationPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.azureHubCreationPublisherTracer == null)
				{
					ExTraceGlobals.azureHubCreationPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.azureHubCreationPublisherTracer;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x0005BACE File Offset: 0x00059CCE
		public static Trace AzureChallengeRequestPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.azureChallengeRequestPublisherTracer == null)
				{
					ExTraceGlobals.azureChallengeRequestPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.azureChallengeRequestPublisherTracer;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x0005BAED File Offset: 0x00059CED
		public static Trace AzureDeviceRegistrationPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.azureDeviceRegistrationPublisherTracer == null)
				{
					ExTraceGlobals.azureDeviceRegistrationPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.azureDeviceRegistrationPublisherTracer;
			}
		}

		// Token: 0x04001CDF RID: 7391
		private static Guid componentGuid = new Guid("5af2f275-ee7b-466b-8ba6-b317da1f7800");

		// Token: 0x04001CE0 RID: 7392
		private static Trace exchangeToOwaTracer = null;

		// Token: 0x04001CE1 RID: 7393
		private static Trace notificationFormatTracer = null;

		// Token: 0x04001CE2 RID: 7394
		private static Trace publisherManagerTracer = null;

		// Token: 0x04001CE3 RID: 7395
		private static Trace storageNotificationSubscriptionTracer = null;

		// Token: 0x04001CE4 RID: 7396
		private static Trace pushNotificationAssistantTracer = null;

		// Token: 0x04001CE5 RID: 7397
		private static Trace apnsPublisherTracer = null;

		// Token: 0x04001CE6 RID: 7398
		private static Trace wnsPublisherTracer = null;

		// Token: 0x04001CE7 RID: 7399
		private static Trace gcmPublisherTracer = null;

		// Token: 0x04001CE8 RID: 7400
		private static Trace proxyPublisherTracer = null;

		// Token: 0x04001CE9 RID: 7401
		private static Trace pendingGetPublisherTracer = null;

		// Token: 0x04001CEA RID: 7402
		private static Trace pushNotificationServiceTracer = null;

		// Token: 0x04001CEB RID: 7403
		private static Trace pushNotificationClientTracer = null;

		// Token: 0x04001CEC RID: 7404
		private static Trace webAppPublisherTracer = null;

		// Token: 0x04001CED RID: 7405
		private static Trace azurePublisherTracer = null;

		// Token: 0x04001CEE RID: 7406
		private static Trace azureHubCreationPublisherTracer = null;

		// Token: 0x04001CEF RID: 7407
		private static Trace azureChallengeRequestPublisherTracer = null;

		// Token: 0x04001CF0 RID: 7408
		private static Trace azureDeviceRegistrationPublisherTracer = null;
	}
}
