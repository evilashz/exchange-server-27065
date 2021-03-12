using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.PushNotifications.Client
{
	// Token: 0x02000028 RID: 40
	internal class PushNotificationsProxyPoolFactory
	{
		// Token: 0x0600011B RID: 283 RVA: 0x000047F0 File Offset: 0x000029F0
		public static PushNotificationsProxyPool<IPublisherServiceContract> CreatePublisherProxyPool()
		{
			ChannelFactory<IPublisherServiceContract> channelFactory = new ChannelFactory<IPublisherServiceContract>(PushNotificationsProxyPoolFactory.namedPipeBinding, new EndpointAddress("net.pipe://localhost/PushNotifications/service.svc"));
			return new PushNotificationsProxyPool<IPublisherServiceContract>("PushNotifications-Publishing", Environment.MachineName, channelFactory, true);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004824 File Offset: 0x00002A24
		public static PushNotificationsProxyPool<IOutlookPublisherServiceContract> CreateOutlookPublisherProxyPool()
		{
			ChannelFactory<IOutlookPublisherServiceContract> channelFactory = new ChannelFactory<IOutlookPublisherServiceContract>(PushNotificationsProxyPoolFactory.namedPipeBinding, new EndpointAddress("net.pipe://localhost/PushNotifications/service.svc"));
			return new PushNotificationsProxyPool<IOutlookPublisherServiceContract>("Outlook-PushNotifications-Publishing", Environment.MachineName, channelFactory, true);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004858 File Offset: 0x00002A58
		public static PushNotificationsProxyPool<IAzureHubCreationServiceContract> CreateAzureHubCreationProxyPool()
		{
			ChannelFactory<IAzureHubCreationServiceContract> channelFactory = new ChannelFactory<IAzureHubCreationServiceContract>(PushNotificationsProxyPoolFactory.namedPipeBinding, new EndpointAddress("net.pipe://localhost/PushNotifications/service.svc"));
			return new PushNotificationsProxyPool<IAzureHubCreationServiceContract>("Azure-Hub-Creation", Environment.MachineName, channelFactory, true);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000488C File Offset: 0x00002A8C
		public static PushNotificationsProxyPool<IAzureChallengeRequestServiceContract> CreateAzureChallengeRequestProxyPool()
		{
			ChannelFactory<IAzureChallengeRequestServiceContract> channelFactory = new ChannelFactory<IAzureChallengeRequestServiceContract>(PushNotificationsProxyPoolFactory.namedPipeBinding, new EndpointAddress("net.pipe://localhost/PushNotifications/service.svc"));
			return new PushNotificationsProxyPool<IAzureChallengeRequestServiceContract>("Azure-Challenge-Request", Environment.MachineName, channelFactory, true);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000048C0 File Offset: 0x00002AC0
		public static PushNotificationsProxyPool<IAzureDeviceRegistrationServiceContract> CreateAzureDeviceRegistrationProxyPool()
		{
			ChannelFactory<IAzureDeviceRegistrationServiceContract> channelFactory = new ChannelFactory<IAzureDeviceRegistrationServiceContract>(PushNotificationsProxyPoolFactory.namedPipeBinding, new EndpointAddress("net.pipe://localhost/PushNotifications/service.svc"));
			return new PushNotificationsProxyPool<IAzureDeviceRegistrationServiceContract>("Azure-Device-Registration", Environment.MachineName, channelFactory, true);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000048F4 File Offset: 0x00002AF4
		public static PushNotificationsProxyPool<ILocalUserNotificationPublisherServiceContract> CreateLocalUserNotificationPublisherProxyPool()
		{
			ChannelFactory<ILocalUserNotificationPublisherServiceContract> channelFactory = new ChannelFactory<ILocalUserNotificationPublisherServiceContract>(PushNotificationsProxyPoolFactory.namedPipeBinding, new EndpointAddress("net.pipe://localhost/PushNotifications/service.svc"));
			return new PushNotificationsProxyPool<ILocalUserNotificationPublisherServiceContract>("LocalUserNotification-Publishing", Environment.MachineName, channelFactory, true);
		}

		// Token: 0x0400005E RID: 94
		public const string PublishingEndpointName = "PushNotifications-Publishing";

		// Token: 0x0400005F RID: 95
		public const string OutlookPublishingEndpointName = "Outlook-PushNotifications-Publishing";

		// Token: 0x04000060 RID: 96
		public const string AzureHubCreationEndpointName = "Azure-Hub-Creation";

		// Token: 0x04000061 RID: 97
		public const string AzureChallengeRequestEndpointName = "Azure-Challenge-Request";

		// Token: 0x04000062 RID: 98
		public const string AzureDeviceRegistrationEndpointName = "Azure-Device-Registration";

		// Token: 0x04000063 RID: 99
		public const string LocalUserNotificationPublishingEndpointName = "LocalUserNotification-Publishing";

		// Token: 0x04000064 RID: 100
		private static Binding namedPipeBinding = new NetNamedPipeBinding();
	}
}
