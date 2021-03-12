using System;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OnlineMeeting.Probes
{
	// Token: 0x02000258 RID: 600
	internal class OnlineMeetingCreateLocalProbe : OnlineMeetingCreateProbe
	{
		// Token: 0x060010D7 RID: 4311 RVA: 0x0006FFB4 File Offset: 0x0006E1B4
		protected override void DoWork(CancellationToken cancellationToken)
		{
			ExchangeServerRoleEndpoint exchangeServerRoleEndpoint = LocalEndpointManager.Instance.ExchangeServerRoleEndpoint;
			if (exchangeServerRoleEndpoint.IsCafeRoleInstalled)
			{
				base.DoWork(cancellationToken);
			}
			if (exchangeServerRoleEndpoint.IsMailboxRoleInstalled && base.Definition.Attributes.ContainsKey("DatabaseGuid"))
			{
				string text = base.Definition.Attributes["DatabaseGuid"];
				if (!string.IsNullOrEmpty(text) && DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(new Guid(text)))
				{
					base.DoWork(cancellationToken);
				}
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00070034 File Offset: 0x0006E234
		protected override HttpWebRequest GetEwsRequest(NetworkCredential credential, string calendarItemId, string userSipUri)
		{
			string ewsUrl = base.GetEwsUrl(this.traceListener, base.Definition.Endpoint);
			base.Result.StateAttribute1 = ewsUrl;
			bool flag = ewsUrl.Contains("444");
			string str = flag ? (OnlineMeetingCreateDiscovery.ProbeEndPoint.TrimEnd(new char[]
			{
				'/'
			}) + ":444") : OnlineMeetingCreateDiscovery.ProbeEndPoint.TrimEnd(new char[]
			{
				'/'
			});
			string text = str + string.Format("{0}?sipUri={1},itemId={2}", base.Definition.SecondaryEndpoint, userSipUri, calendarItemId);
			base.Result.StateAttribute11 = text;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text);
			httpWebRequest.KeepAlive = false;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.Headers.Add(CertificateValidationManager.ComponentIdHeaderName, base.Definition.ServiceName);
			httpWebRequest.ContentType = "text/xml";
			httpWebRequest.Method = "POST";
			if (flag)
			{
				httpWebRequest.UseDefaultCredentials = true;
				string text2 = string.Format("{0}@{1}", credential.UserName, credential.Domain);
				CommonAccessToken commonAccessToken;
				if (LocalEndpointManager.IsDataCenter)
				{
					commonAccessToken = CommonAccessTokenHelper.CreateLiveIdBasic(text2);
					base.Result.StateAttribute15 = "Mailbox Probe running in Datacenter using Cafe Auth";
				}
				else
				{
					commonAccessToken = CommonAccessTokenHelper.CreateWindows(text2, credential.Password);
					base.Result.StateAttribute15 = "Mailbox Probe running in Enterprise using Cafe Auth";
				}
				httpWebRequest.Headers.Add("X-CommonAccessToken", commonAccessToken.Serialize());
				httpWebRequest.Headers.Add("X-IsFromCafe", "1");
			}
			return httpWebRequest;
		}
	}
}
