using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Exchange.PushNotifications.Extensions;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PushNotifications
{
	// Token: 0x02000416 RID: 1046
	public class CafeOnPremProbe : ProbeWorkItem
	{
		// Token: 0x06001ABD RID: 6845 RVA: 0x0009335C File Offset: 0x0009155C
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			MailboxDatabaseInfo mailboxDatabaseInfo = LocalEndpointManager.Instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe.FirstOrDefault((MailboxDatabaseInfo db) => !string.IsNullOrWhiteSpace(db.MonitoringAccountPassword));
			if (mailboxDatabaseInfo != null)
			{
				definition.Attributes["AccountDomain"] = mailboxDatabaseInfo.MonitoringAccountDomain;
				return;
			}
			throw new ApplicationException("No monitoring account could be found for this server.");
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000933B8 File Offset: 0x000915B8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			using (HttpClient httpClient = new HttpClient())
			{
				Uri url = new Uri(new Uri("https://localhost"), "PushNotifications/service.svc/PublishOnPremNotifications");
				string text = base.Definition.Attributes["AccountDomain"];
				OAuthCredentials oauthCredentialsForAppToken = OAuthCredentials.GetOAuthCredentialsForAppToken(OrganizationId.FromAcceptedDomain(text), text);
				PushNotificationProxyRequest pushNotificationProxyRequest = PushNotificationProxyRequest.CreateMonitoringRequest(oauthCredentialsForAppToken);
				pushNotificationProxyRequest.Timeout = 30000;
				string componentId = "PushNotificationsCafeProbe";
				CertificateValidationManager.SetComponentId(pushNotificationProxyRequest.Headers, componentId);
				CertificateValidationManager.RegisterCallback(componentId, (object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors) => true);
				DownloadResult downloadResult;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					pushNotificationProxyRequest.RequestStream = memoryStream;
					downloadResult = httpClient.EndDownload(httpClient.BeginDownload(url, pushNotificationProxyRequest, null, null));
				}
				string body = string.Empty;
				if (downloadResult.ResponseStream != null)
				{
					using (downloadResult.ResponseStream)
					{
						using (StreamReader streamReader = new StreamReader(downloadResult.ResponseStream))
						{
							body = streamReader.ReadToEnd();
						}
					}
				}
				if (downloadResult.StatusCode != HttpStatusCode.OK)
				{
					throw new PushNotificationCafeUnexpectedResponse(downloadResult.StatusCode.ToString(), pushNotificationProxyRequest.Headers.ToTraceString(null), downloadResult.ResponseHeaders.ToTraceString(null), body, downloadResult.Exception);
				}
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00093588 File Offset: 0x00091788
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}
	}
}
