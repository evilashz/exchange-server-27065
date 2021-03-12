using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000094 RID: 148
	internal class PushNotificationPublisherSettingsFactory
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x00010750 File Offset: 0x0000E950
		public PushNotificationPublisherSettings Create(IPushNotificationRawSettings rawSettings)
		{
			ArgumentValidator.ThrowIfNull("rawSettings", rawSettings);
			switch (rawSettings.Platform)
			{
			case PushNotificationPlatform.APNS:
				return this.CreateApnsPublisherSettings(rawSettings);
			case PushNotificationPlatform.PendingGet:
				return this.CreatePendingGetPublisherSettings(rawSettings);
			case PushNotificationPlatform.WNS:
				return this.CreateWnsPublisherSettings(rawSettings);
			case PushNotificationPlatform.Proxy:
				return this.CreateProxyPublisherSettings(rawSettings);
			case PushNotificationPlatform.GCM:
				return this.CreateGcmPublisherSettings(rawSettings);
			case PushNotificationPlatform.WebApp:
				return this.CreateWebAppPublisherSettings(rawSettings);
			case PushNotificationPlatform.Azure:
				return this.CreateAzurePublisherSettings(rawSettings);
			case PushNotificationPlatform.AzureHubCreation:
				return this.CreateAzureHubCreationPublisherSettings(rawSettings);
			case PushNotificationPlatform.AzureChallengeRequest:
				return this.CreateAzureChallengeRequestPublisherSettings(rawSettings);
			case PushNotificationPlatform.AzureDeviceRegistration:
				return this.CreateAzureDeviceRegistrationPublisherSettings(rawSettings);
			default:
				throw new NotSupportedException("Unsupported PushNotificationPlatform: " + rawSettings.Platform.ToString());
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00010810 File Offset: 0x0000EA10
		private ApnsPublisherSettings CreateApnsPublisherSettings(IPushNotificationRawSettings rawSettings)
		{
			ApnsEndPoint host = new ApnsEndPoint(rawSettings.Url ?? "gateway.push.apple.com", rawSettings.Port ?? 2195, rawSettings.SecondaryUrl ?? "feedback.push.apple.com", rawSettings.SecondaryPort ?? 2196);
			ApnsChannelSettings channelSettings = new ApnsChannelSettings(rawSettings.Name, rawSettings.AuthenticationKey, rawSettings.AuthenticationKeyFallback ?? null, host, rawSettings.ConnectStepTimeout ?? 500, rawSettings.ConnectTotalTimeout ?? 300000, rawSettings.ConnectRetryMax ?? 3, rawSettings.ConnectRetryDelay ?? 1500, rawSettings.AuthenticateRetryMax ?? 2, rawSettings.ReadTimeout ?? 5000, rawSettings.WriteTimeout ?? 5000, rawSettings.BackOffTimeInSeconds ?? 600, rawSettings.IgnoreCertificateErrors ?? false);
			return new ApnsPublisherSettings(rawSettings.Name, rawSettings.Enabled ?? true, rawSettings.ExchangeMinimumVersion ?? null, rawSettings.ExchangeMaximumVersion ?? null, rawSettings.QueueSize ?? 10000, rawSettings.NumberOfChannels ?? 1, rawSettings.AddTimeout ?? 15, channelSettings);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00010A3C File Offset: 0x0000EC3C
		private WnsPublisherSettings CreateWnsPublisherSettings(IPushNotificationRawSettings rawSettings)
		{
			WnsChannelSettings channelSettings = new WnsChannelSettings(rawSettings.Name, rawSettings.AuthenticationId, rawSettings.AuthenticationKey, rawSettings.IsAuthenticationKeyEncrypted ?? true, rawSettings.SecondaryUrl ?? "https://login.live.com/accesstoken.srf", rawSettings.ConnectTotalTimeout ?? 60000, rawSettings.ConnectStepTimeout ?? 500, rawSettings.ConnectRetryDelay ?? 1500, rawSettings.AuthenticateRetryMax ?? 2, rawSettings.BackOffTimeInSeconds ?? 600);
			return new WnsPublisherSettings(rawSettings.Name, rawSettings.Enabled ?? true, rawSettings.ExchangeMinimumVersion ?? null, rawSettings.ExchangeMaximumVersion ?? null, rawSettings.QueueSize ?? 10000, rawSettings.NumberOfChannels ?? 1, rawSettings.AddTimeout ?? 15, channelSettings);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		private GcmPublisherSettings CreateGcmPublisherSettings(IPushNotificationRawSettings rawSettings)
		{
			GcmChannelSettings channelSettings = new GcmChannelSettings(rawSettings.Name, rawSettings.AuthenticationId, rawSettings.AuthenticationKey, rawSettings.IsAuthenticationKeyEncrypted ?? true, rawSettings.Url ?? "https://android.googleapis.com/gcm/send", rawSettings.ConnectTotalTimeout ?? 60000, rawSettings.ConnectStepTimeout ?? 500, rawSettings.BackOffTimeInSeconds ?? 600);
			return new GcmPublisherSettings(rawSettings.Name, rawSettings.Enabled ?? true, rawSettings.ExchangeMinimumVersion ?? null, rawSettings.ExchangeMaximumVersion ?? null, rawSettings.QueueSize ?? 10000, rawSettings.NumberOfChannels ?? 1, rawSettings.AddTimeout ?? 15, channelSettings);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00010CF0 File Offset: 0x0000EEF0
		private WebAppPublisherSettings CreateWebAppPublisherSettings(IPushNotificationRawSettings rawSettings)
		{
			WebAppChannelSettings channelSettings = new WebAppChannelSettings(rawSettings.Name, rawSettings.ConnectTotalTimeout ?? 60000, rawSettings.ConnectStepTimeout ?? 500, rawSettings.BackOffTimeInSeconds ?? 300);
			return new WebAppPublisherSettings(rawSettings.Name, rawSettings.Enabled ?? true, rawSettings.ExchangeMinimumVersion ?? null, rawSettings.ExchangeMaximumVersion ?? null, rawSettings.QueueSize ?? 10000, rawSettings.NumberOfChannels ?? 1, rawSettings.AddTimeout ?? 15, channelSettings);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00010DF8 File Offset: 0x0000EFF8
		private AzurePublisherSettings CreateAzurePublisherSettings(IPushNotificationRawSettings rawSettings)
		{
			if (rawSettings.IsDefaultPartitionName != null && rawSettings.IsDefaultPartitionName.Value)
			{
				PushNotificationsCrimsonEvents.DefaultPartitionUsedForNamespace.LogPeriodic<string, string>(rawSettings.Name, TimeSpan.FromHours(12.0), rawSettings.Name, Environment.MachineName);
			}
			AzureChannelSettings channelSettings = new AzureChannelSettings(rawSettings.Name, rawSettings.UriTemplate ?? "https://{0}-{1}.servicebus.windows.net/exo/{2}/{3}", rawSettings.AuthenticationId, rawSettings.AuthenticationKey, rawSettings.IsAuthenticationKeyEncrypted ?? true, rawSettings.RegistrationTemplate, rawSettings.RegistrationEnabled ?? false, rawSettings.PartitionName, rawSettings.ConnectTotalTimeout ?? 60000, rawSettings.ConnectStepTimeout ?? 500, rawSettings.MaximumCacheSize ?? 10000, rawSettings.BackOffTimeInSeconds ?? 600);
			return new AzurePublisherSettings(rawSettings.Name, rawSettings.Enabled ?? true, rawSettings.ExchangeMinimumVersion ?? null, rawSettings.ExchangeMaximumVersion ?? null, rawSettings.QueueSize ?? 10000, rawSettings.NumberOfChannels ?? 1, rawSettings.AddTimeout ?? 15, rawSettings.MultifactorRegistrationEnabled ?? false, channelSettings);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00010FE4 File Offset: 0x0000F1E4
		private AzureHubCreationPublisherSettings CreateAzureHubCreationPublisherSettings(IPushNotificationRawSettings rawSettings)
		{
			AzureHubCreationChannelSettings channelSettings = new AzureHubCreationChannelSettings(rawSettings.Name, rawSettings.Url ?? "https://{0}-{1}-sb.accesscontrol.windows.net/", rawSettings.SecondaryUrl ?? "http://{0}-{1}.servicebus.windows.net/exo/", rawSettings.UriTemplate ?? "https://{0}-{1}.servicebus.windows.net/exo/{2}{3}", rawSettings.AuthenticationId, rawSettings.AuthenticationKey, rawSettings.IsAuthenticationKeyEncrypted ?? true, rawSettings.ConnectTotalTimeout ?? 60000, rawSettings.ConnectStepTimeout ?? 500, rawSettings.ConnectRetryDelay ?? 1500, rawSettings.MaximumCacheSize ?? 10000, rawSettings.BackOffTimeInSeconds ?? 600);
			return new AzureHubCreationPublisherSettings(rawSettings.Name, rawSettings.Enabled ?? true, rawSettings.ExchangeMinimumVersion ?? null, rawSettings.ExchangeMaximumVersion ?? null, rawSettings.QueueSize ?? 10000, rawSettings.NumberOfChannels ?? 1, rawSettings.AddTimeout ?? 15, channelSettings);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001117C File Offset: 0x0000F37C
		private AzureChallengeRequestPublisherSettings CreateAzureChallengeRequestPublisherSettings(IPushNotificationRawSettings rawSettings)
		{
			AzureChallengeRequestChannelSettings channelSettings = new AzureChallengeRequestChannelSettings(rawSettings.Name, rawSettings.ConnectTotalTimeout ?? 60000, rawSettings.ConnectStepTimeout ?? 500, rawSettings.BackOffTimeInSeconds ?? 600);
			return new AzureChallengeRequestPublisherSettings(rawSettings.Name, rawSettings.Enabled ?? true, rawSettings.ExchangeMinimumVersion ?? null, rawSettings.ExchangeMaximumVersion ?? null, rawSettings.QueueSize ?? 10000, rawSettings.NumberOfChannels ?? 1, rawSettings.AddTimeout ?? 15, channelSettings);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00011284 File Offset: 0x0000F484
		private AzureDeviceRegistrationPublisherSettings CreateAzureDeviceRegistrationPublisherSettings(IPushNotificationRawSettings rawSettings)
		{
			AzureDeviceRegistrationChannelSettings channelSettings = new AzureDeviceRegistrationChannelSettings(rawSettings.Name, rawSettings.ConnectTotalTimeout ?? 60000, rawSettings.ConnectStepTimeout ?? 500, rawSettings.MaximumCacheSize ?? 10000, rawSettings.BackOffTimeInSeconds ?? 600);
			return new AzureDeviceRegistrationPublisherSettings(rawSettings.Name, rawSettings.Enabled ?? true, rawSettings.ExchangeMinimumVersion ?? null, rawSettings.ExchangeMaximumVersion ?? null, rawSettings.QueueSize ?? 10000, rawSettings.NumberOfChannels ?? 1, rawSettings.AddTimeout ?? 15, channelSettings);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000113AC File Offset: 0x0000F5AC
		private PendingGetPublisherSettings CreatePendingGetPublisherSettings(IPushNotificationRawSettings rawSettings)
		{
			return new PendingGetPublisherSettings(rawSettings.Name, rawSettings.Enabled ?? true, rawSettings.ExchangeMinimumVersion ?? null, rawSettings.ExchangeMaximumVersion ?? null, rawSettings.QueueSize ?? 10000, rawSettings.NumberOfChannels ?? 1, rawSettings.AddTimeout ?? 15);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00011448 File Offset: 0x0000F648
		private ProxyPublisherSettings CreateProxyPublisherSettings(IPushNotificationRawSettings rawSettings)
		{
			PushNotificationApp pushNotificationApp = rawSettings as PushNotificationApp;
			ProxyChannelSettings channelSettings = new ProxyChannelSettings(rawSettings.Name, rawSettings.Url, rawSettings.AuthenticationKey, (pushNotificationApp != null) ? pushNotificationApp.LastUpdateTimeUtc : null, rawSettings.ConnectRetryMax ?? 3, rawSettings.ConnectRetryDelay ?? 1500, rawSettings.ConnectStepTimeout ?? 500, rawSettings.BackOffTimeInSeconds ?? 600);
			return new ProxyPublisherSettings(rawSettings.Name, rawSettings.Enabled ?? false, rawSettings.AuthenticationId, rawSettings.ExchangeMinimumVersion ?? null, rawSettings.ExchangeMaximumVersion ?? null, rawSettings.QueueSize ?? 10000, rawSettings.NumberOfChannels ?? 1, rawSettings.AddTimeout ?? 15, channelSettings);
		}
	}
}
