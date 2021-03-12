using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000014 RID: 20
	internal class PushNotificationPublisherManagerBuilder
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public PushNotificationPublisherManagerBuilder(List<PushNotificationPlatform> platforms)
		{
			ArgumentValidator.ThrowIfNull("platforms", platforms);
			this.Platforms = platforms;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003BFA File Offset: 0x00001DFA
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003C02 File Offset: 0x00001E02
		private List<PushNotificationPlatform> Platforms { get; set; }

		// Token: 0x060000AE RID: 174 RVA: 0x00003C0C File Offset: 0x00001E0C
		public PushNotificationPublisherManager Build(PushNotificationPublisherConfiguration configuration, IThrottlingManager throttlingManager, AzureHubEventHandler hubEventHandler = null)
		{
			Dictionary<PushNotificationPlatform, PushNotificationPublisherFactory> dictionary = new Dictionary<PushNotificationPlatform, PushNotificationPublisherFactory>();
			foreach (PushNotificationPlatform pushNotificationPlatform in this.Platforms)
			{
				PushNotificationPublisherFactory pushNotificationPublisherFactory;
				switch (pushNotificationPlatform)
				{
				case PushNotificationPlatform.APNS:
					pushNotificationPublisherFactory = this.CreateApnsFactory(configuration, throttlingManager);
					break;
				case PushNotificationPlatform.PendingGet:
					pushNotificationPublisherFactory = this.CreatePendingGetFactory(configuration, throttlingManager);
					break;
				case PushNotificationPlatform.WNS:
					pushNotificationPublisherFactory = this.CreateWnsFactory(configuration, throttlingManager);
					break;
				case PushNotificationPlatform.Proxy:
					pushNotificationPublisherFactory = this.CreateProxyFactory(configuration, throttlingManager);
					break;
				case PushNotificationPlatform.GCM:
					pushNotificationPublisherFactory = this.CreateGcmFactory(configuration, throttlingManager);
					break;
				case PushNotificationPlatform.WebApp:
					pushNotificationPublisherFactory = this.CreateWebAppFactory(configuration, throttlingManager);
					break;
				case PushNotificationPlatform.Azure:
					pushNotificationPublisherFactory = this.CreateAzureFactory(configuration, throttlingManager, hubEventHandler);
					break;
				case PushNotificationPlatform.AzureHubCreation:
					pushNotificationPublisherFactory = this.CreateAzureHubFactory(configuration, throttlingManager, hubEventHandler);
					break;
				case PushNotificationPlatform.AzureChallengeRequest:
					pushNotificationPublisherFactory = this.CreateAzureChallengeRequestFactory(configuration, throttlingManager, hubEventHandler);
					break;
				case PushNotificationPlatform.AzureDeviceRegistration:
					pushNotificationPublisherFactory = this.CreateAzureDeviceRegistrationFactory(configuration, throttlingManager, hubEventHandler);
					break;
				default:
					throw new InvalidOperationException("Unrecognized platform: " + pushNotificationPlatform.ToString());
				}
				dictionary.Add(pushNotificationPublisherFactory.Platform, pushNotificationPublisherFactory);
			}
			PushNotificationPublisherManager pushNotificationPublisherManager = configuration.CreatePublisherManager(dictionary);
			if (hubEventHandler != null)
			{
				hubEventHandler.PublisherManager = pushNotificationPublisherManager;
			}
			return pushNotificationPublisherManager;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003D50 File Offset: 0x00001F50
		private PushNotificationPublisherFactory CreateApnsFactory(PushNotificationPublisherConfiguration configuration, IThrottlingManager throttlingManager)
		{
			RegistrySession registrySession = new RegistrySession(true);
			ApnsFeedbackManagerSettings settings = registrySession.Read<ApnsFeedbackManagerSettings>();
			ApnsFeedbackManager feedbackProvider = new ApnsFeedbackManager(settings);
			ApnsNotificationFactory item = new ApnsNotificationFactory();
			List<IPushNotificationMapping<ApnsNotification>> mappings = new List<IPushNotificationMapping<ApnsNotification>>
			{
				item
			};
			return new ApnsPublisherFactory(feedbackProvider, throttlingManager, mappings);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003D94 File Offset: 0x00001F94
		private PushNotificationPublisherFactory CreatePendingGetFactory(PushNotificationPublisherConfiguration configuration, IThrottlingManager throttlingManager)
		{
			PendingGetNotificationFactory item = new PendingGetNotificationFactory();
			List<IPushNotificationMapping<PendingGetNotification>> mappings = new List<IPushNotificationMapping<PendingGetNotification>>
			{
				item
			};
			IPendingGetConnectionCache instance = PendingGetConnectionCache.Instance;
			return new PendingGetPublisherFactory(instance, mappings);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003DC4 File Offset: 0x00001FC4
		private PushNotificationPublisherFactory CreateWnsFactory(PushNotificationPublisherConfiguration configuration, IThrottlingManager throttlingManager)
		{
			WnsNotificationFactory item = new WnsNotificationFactory();
			WnsOutlookNotificationMapping item2 = new WnsOutlookNotificationMapping();
			List<IPushNotificationMapping<WnsNotification>> mappings = new List<IPushNotificationMapping<WnsNotification>>
			{
				item,
				item2
			};
			return new WnsPublisherFactory(throttlingManager, mappings);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003DFC File Offset: 0x00001FFC
		private PushNotificationPublisherFactory CreateGcmFactory(PushNotificationPublisherConfiguration configuration, IThrottlingManager throttlingManager)
		{
			GcmNotificationFactory item = new GcmNotificationFactory();
			List<IPushNotificationMapping<GcmNotification>> mappings = new List<IPushNotificationMapping<GcmNotification>>
			{
				item
			};
			return new GcmPublisherFactory(throttlingManager, mappings);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003E28 File Offset: 0x00002028
		private PushNotificationPublisherFactory CreateWebAppFactory(PushNotificationPublisherConfiguration configuration, IThrottlingManager throttlingManager)
		{
			WebAppNotificationFactory item = new WebAppNotificationFactory();
			List<IPushNotificationMapping<WebAppNotification>> mappings = new List<IPushNotificationMapping<WebAppNotification>>
			{
				item
			};
			return new WebAppPublisherFactory(mappings);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003E50 File Offset: 0x00002050
		private PushNotificationPublisherFactory CreateAzureFactory(PushNotificationPublisherConfiguration configuration, IThrottlingManager throttlingManager, AzureHubEventHandler hubEventHandler)
		{
			AzureNotificationFactory item = new AzureNotificationFactory();
			AzureOutlookNotificationMapping item2 = new AzureOutlookNotificationMapping();
			List<IPushNotificationMapping<AzureNotification>> mappings = new List<IPushNotificationMapping<AzureNotification>>
			{
				item,
				item2
			};
			return new AzurePublisherFactory(mappings);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003E88 File Offset: 0x00002088
		private PushNotificationPublisherFactory CreateAzureHubFactory(PushNotificationPublisherConfiguration configuration, IThrottlingManager throttlingManager, AzureHubEventHandler hubEventHandler)
		{
			AzureHubDefinitionMapping item = new AzureHubDefinitionMapping(configuration);
			List<IPushNotificationMapping<AzureHubCreationNotification>> mappings = new List<IPushNotificationMapping<AzureHubCreationNotification>>
			{
				item
			};
			return new AzureHubCreationPublisherFactory(mappings);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003EB4 File Offset: 0x000020B4
		private PushNotificationPublisherFactory CreateAzureChallengeRequestFactory(PushNotificationPublisherConfiguration configuration, IThrottlingManager throttlingManager, AzureHubEventHandler hubEventHandler)
		{
			AzureChallengeRequestInfoMapping item = new AzureChallengeRequestInfoMapping(configuration);
			List<IPushNotificationMapping<AzureChallengeRequestNotification>> mappings = new List<IPushNotificationMapping<AzureChallengeRequestNotification>>
			{
				item
			};
			return new AzureChallengeRequestPublisherFactory(mappings, hubEventHandler);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003EE0 File Offset: 0x000020E0
		private PushNotificationPublisherFactory CreateAzureDeviceRegistrationFactory(PushNotificationPublisherConfiguration configuration, IThrottlingManager throttlingManager, AzureHubEventHandler hubEventHandler)
		{
			AzureDeviceRegistrationInfoMapping item = new AzureDeviceRegistrationInfoMapping(configuration);
			List<IPushNotificationMapping<AzureDeviceRegistrationNotification>> mappings = new List<IPushNotificationMapping<AzureDeviceRegistrationNotification>>
			{
				item
			};
			return new AzureDeviceRegistrationPublisherFactory(mappings, hubEventHandler);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003F0A File Offset: 0x0000210A
		private PushNotificationPublisherFactory CreateProxyFactory(PushNotificationPublisherConfiguration configuration, IThrottlingManager throttlingManager)
		{
			return new ProxyPublisherFactory();
		}
	}
}
