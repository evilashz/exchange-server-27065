using System;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Exchange.PushNotifications.Publishers;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PushNotifications
{
	// Token: 0x02000418 RID: 1048
	public abstract class ExternalProbe : ProbeWorkItem
	{
		// Token: 0x06001AC8 RID: 6856
		protected abstract void PopulateEnterpriseAppSettings(out string tenantDomain, out Uri targetUri);

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00093660 File Offset: 0x00091860
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				string userDomain;
				Uri uri;
				this.PopulateEnterpriseAppSettings(out userDomain, out uri);
				MonitoringMailboxNotificationFactory monitoringMailboxNotificationFactory = new MonitoringMailboxNotificationFactory();
				MailboxNotificationBatch notifications = monitoringMailboxNotificationFactory.CreateExternalMonitoringNotificationBatch();
				OAuthCredentials oauthCredentialsForAppToken = OAuthCredentials.GetOAuthCredentialsForAppToken(OrganizationId.ForestWideOrgId, userDomain);
				OnPremPublisherServiceProxy onPremPublisherServiceProxy = this.CreateServiceProxy(uri, oauthCredentialsForAppToken);
				onPremPublisherServiceProxy.EndPublishOnPremNotifications(onPremPublisherServiceProxy.BeginPublishOnPremNotifications(notifications, null, null));
			}
			catch (Exception ex)
			{
				this.ReportFailure(ex);
			}
		}

		// Token: 0x06001ACA RID: 6858
		internal abstract OnPremPublisherServiceProxy CreateServiceProxy(Uri uri, ICredentials credentials);

		// Token: 0x06001ACB RID: 6859
		protected abstract void ReportFailure(Exception ex);
	}
}
