using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Exchange.PushNotifications.Publishers;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PushNotifications
{
	// Token: 0x02000417 RID: 1047
	public class DatacenterOnPremBackendEndpointProbe : ProbeWorkItem
	{
		// Token: 0x06001AC3 RID: 6851 RVA: 0x000935A4 File Offset: 0x000917A4
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000935AC File Offset: 0x000917AC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			MonitoringMailboxNotificationFactory monitoringMailboxNotificationFactory = new MonitoringMailboxNotificationFactory();
			MailboxNotificationBatch notifications = monitoringMailboxNotificationFactory.CreateExternalMonitoringNotificationBatch();
			string text = "PushNotificationsDatacenterOnPremBackendEndpointProbe";
			using (OnPremPublisherServiceProxy onPremPublisherServiceProxy = OnPremPublisherServiceProxy.CreateMonitoringProxy(new Uri(string.Format("https://{0}:444", Dns.GetHostEntry("localhost").HostName)), CredentialCache.DefaultCredentials, text))
			{
				CertificateValidationManager.RegisterCallback(text, (object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors) => true);
				onPremPublisherServiceProxy.EndPublishOnPremNotifications(onPremPublisherServiceProxy.BeginPublishOnPremNotifications(notifications, null, null));
			}
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00093644 File Offset: 0x00091844
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}
	}
}
