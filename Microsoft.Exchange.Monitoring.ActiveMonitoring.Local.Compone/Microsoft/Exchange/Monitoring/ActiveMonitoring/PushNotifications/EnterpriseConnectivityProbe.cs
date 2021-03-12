using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Exchange.PushNotifications.Publishers;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PushNotifications
{
	// Token: 0x02000419 RID: 1049
	public class EnterpriseConnectivityProbe : ExternalProbe
	{
		// Token: 0x06001ACD RID: 6861 RVA: 0x000936D4 File Offset: 0x000918D4
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000936D6 File Offset: 0x000918D6
		internal override OnPremPublisherServiceProxy CreateServiceProxy(Uri uri, ICredentials credentials)
		{
			return OnPremPublisherServiceProxy.CreateMonitoringProxy(uri, credentials, null);
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000936E0 File Offset: 0x000918E0
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000936F4 File Offset: 0x000918F4
		protected override void PopulateEnterpriseAppSettings(out string tenantDomain, out Uri targetUri)
		{
			PushNotificationPublisherConfiguration pushNotificationPublisherConfiguration = new PushNotificationPublisherConfiguration(false, null);
			ProxyPublisherSettings proxyPublisherSettings = pushNotificationPublisherConfiguration.ProxyPublisherSettings;
			if (proxyPublisherSettings == null)
			{
				throw new Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.PushNotificationConfigurationException(Strings.PushNotificationEnterpriseNotConfigured);
			}
			if (string.IsNullOrEmpty(proxyPublisherSettings.ChannelSettings.Organization))
			{
				throw new Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.PushNotificationConfigurationException(Strings.PushNotificationEnterpriseEmptyDomain);
			}
			if (proxyPublisherSettings.ChannelSettings.ServiceUri == null)
			{
				throw new Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.PushNotificationConfigurationException(Strings.PushNotificationEnterpriseEmptyServiceUri);
			}
			tenantDomain = proxyPublisherSettings.ChannelSettings.Organization;
			targetUri = proxyPublisherSettings.ChannelSettings.ServiceUri;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00093778 File Offset: 0x00091978
		protected override void ReportFailure(Exception ex)
		{
			if (ex is Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.PushNotificationConfigurationException)
			{
				base.Result.StateAttribute1 = Components.PushNotifications.ToString();
				base.Result.StateAttribute2 = ex.Message;
			}
			else if (ex is PushNotificationTransientException && ex.ContainsInnerException<SocketException>())
			{
				base.Result.StateAttribute1 = Components.Networking.ToString();
				base.Result.StateAttribute2 = Strings.PushNotificationEnterpriseNetworkingError;
			}
			else if (ex is PushNotificationPermanentException && ex.ContainsInnerException<AuthenticationException>())
			{
				base.Result.StateAttribute1 = Components.Authentication.ToString();
				base.Result.StateAttribute2 = Strings.PushNotificationEnterpriseAuthError;
			}
			else
			{
				base.Result.StateAttribute1 = Components.Unknown.ToString();
				base.Result.StateAttribute2 = Strings.PushNotificationEnterpriseUnknownError;
			}
			throw ex;
		}
	}
}
