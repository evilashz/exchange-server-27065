using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003A2 RID: 930
	internal class ProxyWebSession : BaseProxyWebSession
	{
		// Token: 0x0600311D RID: 12573 RVA: 0x00095FF1 File Offset: 0x000941F1
		public ProxyWebSession(Uri serviceUrl) : base(serviceUrl)
		{
			base.AllowedRequestCookies.Add("TimeOffset");
			base.AllowedResponseCookies.Add("EcpUpdatedUserSettings");
			base.AllowedResponseCookies.Add("mkt");
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x0009604C File Offset: 0x0009424C
		public void SendProxyLogon(Uri baseUri, OutboundProxySession session, Action<HttpStatusCode> onProxyLogonSucceeded, Action<Exception> onProxyLogonFailure)
		{
			Uri requestUri = new Uri(baseUri, "proxyLogon.ecp");
			HttpWebRequest request = base.CreateRequest(requestUri, "POST");
			ProxyWebSession.AddSecurityContextHeader(request, session);
			base.Send<HttpStatusCode>(request, new SerializedAccessTokenBody(session.RbacConfiguration.SecurityAccessToken), (HttpWebResponse response) => response.StatusCode, delegate(HttpStatusCode statusCode)
			{
				onProxyLogonSucceeded(statusCode);
			}, onProxyLogonFailure);
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000960C8 File Offset: 0x000942C8
		protected override void UpdatePerformanceCounters(RequestPerformance requestPerformance)
		{
			EcpPerfCounters.OutboundProxyRequestBytes.IncrementBy(requestPerformance.BytesSent);
			EcpPerfCounters.OutboundProxyResponseBytes.IncrementBy(requestPerformance.BytesReceived);
			ProxyWebSession.outboundProxyRequestTime.AddSample(requestPerformance.ElapsedTicks);
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x00096100 File Offset: 0x00094300
		protected override void OnSendingProxyRequest(HttpContext context, HttpWebRequest request)
		{
			OutboundProxySession outboundProxySession = (OutboundProxySession)context.User;
			ExTraceGlobals.ProxyTracer.TraceInformation<string, Uri>(0, 0L, "Sending Proxy Request. User={0}, Url={1}", outboundProxySession.NameForEventLog, request.RequestUri);
			ProxyWebSession.AddSecurityContextHeader(request, outboundProxySession);
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x00096140 File Offset: 0x00094340
		protected override void OnProxyRequestSucceeded(HttpContext context, HttpWebRequest request)
		{
			OutboundProxySession outboundProxySession = (OutboundProxySession)context.User;
			ExTraceGlobals.ProxyTracer.TraceInformation<ADObjectId, Uri>(0, 0L, "Proxy Request Completed. User={0}, Url={1}", outboundProxySession.ExecutingUserId, request.RequestUri);
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x00096178 File Offset: 0x00094378
		protected override void OnProxyRequestFailed(HttpContext context, HttpWebRequest request, HttpWebResponse response, Exception exception)
		{
			OutboundProxySession outboundProxySession = (OutboundProxySession)context.User;
			ExTraceGlobals.ProxyTracer.TraceError<ADObjectId, Uri, Exception>(0, 0L, "Proxy Request Failed. User={0}, Url={1}, Exception={2}", outboundProxySession.ExecutingUserId, request.RequestUri, exception);
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000961B4 File Offset: 0x000943B4
		protected override void OnRequestException(HttpWebRequest request, WebException exception)
		{
			ExTraceGlobals.ProxyTracer.TraceError<Uri, WebException>(0, 0L, "Request Exception. Url={0}, Exception={1}", request.RequestUri, exception);
			base.OnRequestException(request, exception);
			if (!exception.IsProxyNeedIdentityError())
			{
				string machineName = Environment.MachineName;
				string host = request.RequestUri.Host;
				ExEventLog.EventTuple proxyEventLogTuple = exception.GetProxyEventLogTuple();
				if (proxyEventLogTuple.Period == ExEventLog.EventPeriod.LogAlways)
				{
					proxyEventLogTuple.LogEvent(new object[]
					{
						EcpEventLogExtensions.GetUserNameToLog(),
						machineName,
						host,
						request.RequestUri,
						exception
					});
					return;
				}
				proxyEventLogTuple.LogPeriodicEvent(host, new object[]
				{
					EcpEventLogExtensions.GetUserNameToLog(),
					machineName,
					host
				});
			}
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x0009625C File Offset: 0x0009445C
		protected override bool ShouldProcessFailedResponse(WebException exception)
		{
			return !exception.IsProxyNeedIdentityError() && base.ShouldProcessFailedResponse(exception);
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x0009626F File Offset: 0x0009446F
		protected override void ProcessProxyResponse(HttpContext context, HttpWebResponse proxyResponse)
		{
			ExTraceGlobals.ProxyTracer.TraceInformation<HttpStatusCode, Uri>(0, 0L, "ProcessProxyResponse. StatusCode: {0}. ResponseUri: {1}", proxyResponse.StatusCode, proxyResponse.ResponseUri);
			base.ProcessProxyResponse(context, proxyResponse);
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x00096298 File Offset: 0x00094498
		private static void AddSecurityContextHeader(HttpWebRequest request, OutboundProxySession session)
		{
			request.Headers.Add("msExchLogonAccount", session.RbacConfiguration.SecurityAccessToken.UserSid);
			request.Headers.Add("msExchLogonMailbox", session.RbacConfiguration.LogonUserSid.Value);
			SecurityIdentifier securityIdentifier;
			if (!session.RbacConfiguration.TryGetExecutingUserSid(out securityIdentifier))
			{
				throw new ExecutingUserPropertyNotFoundException("executingUserSid");
			}
			request.Headers.Add("msExchTargetMailbox", securityIdentifier.Value);
			request.Headers.Add("msExchProxyUri", HttpContext.Current.Request.Url.ToEscapedString());
		}

		// Token: 0x040023C6 RID: 9158
		private static AveragePerfCounter outboundProxyRequestTime = new AveragePerfCounter(EcpPerfCounters.AverageOutboundProxyRequestsResponseTime, EcpPerfCounters.AverageOutboundProxyRequestsResponseTimeBase);
	}
}
