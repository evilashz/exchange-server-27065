using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Publishers;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200035E RID: 862
	internal class RequestDeviceRegistrationChallenge : SingleStepServiceCommand<DeviceRegistrationChallengeRequest, ServiceResultNone>
	{
		// Token: 0x06001822 RID: 6178 RVA: 0x00081B4C File Offset: 0x0007FD4C
		static RequestDeviceRegistrationChallenge()
		{
			RequestDeviceRegistrationChallenge.ConfigWatcher.OnChangeEvent += RequestDeviceRegistrationChallenge.UpdateConfigurationData;
			PushNotificationPublisherConfiguration pushNotificationPublisherConfiguration = RequestDeviceRegistrationChallenge.ConfigWatcher.Start();
			RequestDeviceRegistrationChallenge.enabledApps = RequestDeviceRegistrationChallenge.ResolveAzureMultifactorEnabledApps(pushNotificationPublisherConfiguration.AzurePublisherSettings);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00081B9B File Offset: 0x0007FD9B
		public RequestDeviceRegistrationChallenge(CallContext callContext, DeviceRegistrationChallengeRequest request) : base(callContext, request)
		{
			this.ChallengeRequest = request;
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x00081BAC File Offset: 0x0007FDAC
		// (set) Token: 0x06001825 RID: 6181 RVA: 0x00081BB4 File Offset: 0x0007FDB4
		private DeviceRegistrationChallengeRequest ChallengeRequest { get; set; }

		// Token: 0x06001826 RID: 6182 RVA: 0x00081BBD File Offset: 0x0007FDBD
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new RequestDeviceRegistrationChallengeResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00081BDC File Offset: 0x0007FDDC
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			string appId = this.ChallengeRequest.AppId;
			bool? flag = RequestDeviceRegistrationChallenge.IsMultifactorRegistrationEnabledForApp(appId);
			if (flag == null || !flag.Value)
			{
				if (flag == null)
				{
					PushNotificationsCrimsonEvents.MultifactorRegistrationUnknownApp.LogPeriodic<string, string>(appId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, appId, this.ChallengeRequest.DeviceNotificationId);
				}
				else if (PushNotificationsCrimsonEvents.MultifactorRegistrationUnavailable.IsEnabled(PushNotificationsCrimsonEvent.Provider))
				{
					PushNotificationsCrimsonEvents.MultifactorRegistrationUnavailable.Log<string, string>(appId, this.ChallengeRequest.DeviceNotificationId);
				}
				ServiceError error = new ServiceError(CoreResources.MultifactorRegistrationUnavailable(appId), ResponseCodeType.ErrorMultifactorRegistrationUnavailable, 0, ExchangeVersion.Exchange2013);
				return new ServiceResult<ServiceResultNone>(error);
			}
			string hubName = null;
			OrganizationId organizationId = base.MailboxIdentityMailboxSession.OrganizationId;
			if (!OrganizationId.ForestWideOrgId.Equals(organizationId))
			{
				hubName = base.MailboxIdentityMailboxSession.OrganizationId.ToExternalDirectoryOrganizationId();
			}
			using (AzureChallengeRequestServiceProxy azureChallengeRequestServiceProxy = new AzureChallengeRequestServiceProxy(null))
			{
				string challenge = (!string.IsNullOrWhiteSpace(this.ChallengeRequest.ClientWatermark)) ? this.ChallengeRequest.ClientWatermark : appId;
				azureChallengeRequestServiceProxy.EndChallengeRequest(azureChallengeRequestServiceProxy.BeginChallengeRequest(new AzureChallengeRequestInfo(appId, this.ChallengeRequest.Platform, this.ChallengeRequest.DeviceNotificationId, challenge, hubName), null, null));
				if (PushNotificationsCrimsonEvents.DeviceRegistrationChallengeRequested.IsEnabled(PushNotificationsCrimsonEvent.Provider))
				{
					PushNotificationsCrimsonEvents.DeviceRegistrationChallengeRequested.Log<string, string>(this.ChallengeRequest.AppId, this.ChallengeRequest.DeviceNotificationId);
				}
			}
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00081D60 File Offset: 0x0007FF60
		private static bool? IsMultifactorRegistrationEnabledForApp(string appId)
		{
			Dictionary<string, bool> dictionary = Interlocked.CompareExchange<Dictionary<string, bool>>(ref RequestDeviceRegistrationChallenge.enabledApps, RequestDeviceRegistrationChallenge.enabledApps, null);
			if (!dictionary.ContainsKey(appId))
			{
				return null;
			}
			return new bool?(dictionary[appId]);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00081D9D File Offset: 0x0007FF9D
		private static void UpdateConfigurationData(object sender, ConfigurationChangedEventArgs configEventArgs)
		{
			Interlocked.Exchange<Dictionary<string, bool>>(ref RequestDeviceRegistrationChallenge.enabledApps, RequestDeviceRegistrationChallenge.ResolveAzureMultifactorEnabledApps(configEventArgs.UpdatedConfiguration.AzurePublisherSettings));
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00081DBC File Offset: 0x0007FFBC
		private static Dictionary<string, bool> ResolveAzureMultifactorEnabledApps(IEnumerable<AzurePublisherSettings> publisherSettings)
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			foreach (AzurePublisherSettings azurePublisherSettings in publisherSettings)
			{
				dictionary[azurePublisherSettings.AppId] = azurePublisherSettings.IsMultifactorRegistrationEnabled;
			}
			if (PushNotificationsCrimsonEvents.MultifactorRegistrationApps.IsEnabled(PushNotificationsCrimsonEvent.Provider))
			{
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (string text in dictionary.Keys)
				{
					if (dictionary[text])
					{
						stringBuilder.Append(text);
						stringBuilder.Append(",");
					}
					else
					{
						stringBuilder2.Append(text);
						stringBuilder2.Append(",");
					}
				}
				PushNotificationsCrimsonEvents.MultifactorRegistrationApps.Log<string>(string.Format("Enabled:[{0}]; Disabled:[{1}]", stringBuilder.ToString(), stringBuilder2.ToString()));
			}
			return dictionary;
		}

		// Token: 0x04001027 RID: 4135
		private const int DefaultConfigWatcherRefreshRateInMinutes = 15;

		// Token: 0x04001028 RID: 4136
		private const string DefaultServiceCommandAppPoolName = "msexchangeservicesapppool";

		// Token: 0x04001029 RID: 4137
		private static readonly PublisherConfigurationWatcher ConfigWatcher = new PublisherConfigurationWatcher("msexchangeservicesapppool", 15);

		// Token: 0x0400102A RID: 4138
		private static Dictionary<string, bool> enabledApps;
	}
}
