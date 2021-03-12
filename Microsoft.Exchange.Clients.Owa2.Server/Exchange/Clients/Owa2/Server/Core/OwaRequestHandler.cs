using System;
using System.Globalization;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001FE RID: 510
	internal class OwaRequestHandler : RequestHandlerBase
	{
		// Token: 0x060011E4 RID: 4580 RVA: 0x00045100 File Offset: 0x00043300
		internal override void OnPostAuthorizeRequest(object sender, EventArgs e)
		{
			RequestDetailsLogger getRequestDetailsLogger = OwaApplication.GetRequestDetailsLogger;
			RequestDetailsLogger.LogEvent(getRequestDetailsLogger, OwaServerLogger.LoggerData.OnPostAuthorizeRequestBegin);
			OwaRequestHandler.InternalOnPostAuthorizeRequest(sender);
			RequestDetailsLogger.LogEvent(getRequestDetailsLogger, OwaServerLogger.LoggerData.OnPostAuthorizeRequestEnd);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00045134 File Offset: 0x00043334
		internal override void OnPreRequestHandlerExecute(object sender, EventArgs e)
		{
			RequestDetailsLogger getRequestDetailsLogger = OwaApplication.GetRequestDetailsLogger;
			if (getRequestDetailsLogger != null)
			{
				HttpApplication httpApplication = (HttpApplication)sender;
				httpApplication.Context.Items.Add(ServiceLatencyMetadata.HttpPipelineLatency, (int)getRequestDetailsLogger.ActivityScope.TotalMilliseconds);
				OwaServerLogger.LogUserContextData(httpApplication.Context, getRequestDetailsLogger);
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00045184 File Offset: 0x00043384
		internal override void OnEndRequest(object sender, EventArgs e)
		{
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00045188 File Offset: 0x00043388
		internal override void OnPreSendRequestHeaders(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			if (httpApplication != null)
			{
				HttpContext context = httpApplication.Context;
				if (context != null && context.Response != null && context.Response.Headers != null)
				{
					if (context.Request != null && context.Request.Headers != null)
					{
						foreach (string name in HttpUtilities.TransferrableHeaders)
						{
							if (!string.IsNullOrEmpty(context.Request.Headers[name]))
							{
								context.Response.Headers[name] = context.Request.Headers[name];
							}
						}
					}
					if (context.Response.StatusCode == 500)
					{
						context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
						context.Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
					}
					context.Response.Headers["X-BackEnd-End"] = ExDateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
				}
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000452A0 File Offset: 0x000434A0
		private static void InternalOnPostAuthorizeRequest(object sender)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "[OwaRequestHandler::InternalOnPostAuthorizeRequest] entry.");
			RequestDetailsLogger getRequestDetailsLogger = OwaApplication.GetRequestDetailsLogger;
			SubActivityScopeLogger subActivityScopeLogger = SubActivityScopeLogger.Create(getRequestDetailsLogger, OwaServerLogger.LoggerData.OnPostAuthorizeRequestLatencyDetails);
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			if (!context.Request.IsAuthenticated && (context.Request.Url.LocalPath.EndsWith("service.svc", StringComparison.OrdinalIgnoreCase) || context.Request.Url.LocalPath.EndsWith("Speech.reco", StringComparison.OrdinalIgnoreCase)))
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>(0L, "[OwaRequestHandler::InternalOnPostAuthorizeRequest] unauthorized request. Request URL={0}.", context.Request.Url.OriginalString);
				context.Response.StatusCode = 401;
				httpApplication.CompleteRequest();
				return;
			}
			RequestContext requestContext = RequestContext.Get(httpApplication.Context);
			subActivityScopeLogger.LogNext("a");
			RequestDispatcher.DispatchRequest(requestContext);
			subActivityScopeLogger.LogNext("b");
			OwaRequestType requestType = requestContext.RequestType;
			RequestDispatcherUtilities.SetXFrameOptionsHeader(RequestContext.Current, requestType);
			subActivityScopeLogger.LogNext("c");
			if (context.User != null && context.User.Identity != null)
			{
				if (context.User.Identity is ClientSecurityContextIdentity)
				{
					IMailboxContext mailboxContext = UserContextManager.GetMailboxContext(context, null, false);
					subActivityScopeLogger.LogNext("d");
					if (OwaRequestHandler.IsProxyLogonRequest(requestType))
					{
						ExTraceGlobals.CoreCallTracer.TraceDebug<OwaRequestType>(0L, "[OwaRequestHandler::InternalOnPostAuthorizeRequest] proxy logon request. RequestType={0}", requestType);
						return;
					}
					RequestDetailsLogger.LogEvent(getRequestDetailsLogger, OwaServerLogger.LoggerData.CanaryValidationBegin);
					bool flag = OwaRequestHandler.IsRequestWithCanary(context.Request, requestType, context.Request.IsAuthenticated);
					bool flag2 = OwaRequestHandler.IsAfterLogonRequest(context.Request);
					string originalIdentitySid = OwaRequestHandler.GetOriginalIdentitySid(context);
					CanaryLogEvent.CanaryStatus canaryStatus = CanaryLogEvent.CanaryStatus.None;
					bool flag3 = !flag || flag2;
					if (!flag3)
					{
						Canary15Cookie.CanaryValidationResult canaryValidationResult;
						flag3 = Canary15Cookie.ValidateCanaryInHeaders(context, originalIdentitySid, Canary15Profile.Owa, out canaryValidationResult);
						canaryStatus |= (CanaryLogEvent.CanaryStatus)canaryValidationResult;
					}
					OwaRequestHandler.UpdateCanaryStatus(ref canaryStatus, flag, CanaryLogEvent.CanaryStatus.IsCanaryNeeded);
					OwaRequestHandler.UpdateCanaryStatus(ref canaryStatus, flag3, CanaryLogEvent.CanaryStatus.IsCanaryValid);
					OwaRequestHandler.UpdateCanaryStatus(ref canaryStatus, flag2, CanaryLogEvent.CanaryStatus.IsCanaryAfterLogonRequest);
					Canary15Cookie canary15Cookie = Canary15Cookie.TryCreateFromHttpContext(context, originalIdentitySid, Canary15Profile.Owa);
					OwaRequestHandler.UpdateCanaryStatus(ref canaryStatus, canary15Cookie.IsAboutToExpire, CanaryLogEvent.CanaryStatus.IsCanaryAboutToExpire);
					OwaRequestHandler.UpdateCanaryStatus(ref canaryStatus, canary15Cookie.IsRenewed, CanaryLogEvent.CanaryStatus.IsCanaryRenewed);
					subActivityScopeLogger.LogNext("e");
					bool flag4 = flag || canary15Cookie.IsAboutToExpire;
					if (flag4)
					{
						canary15Cookie = new Canary15Cookie(originalIdentitySid, Canary15Profile.Owa);
					}
					if (canary15Cookie.IsRenewed || flag4)
					{
						context.Response.SetCookie(canary15Cookie.HttpCookie);
						CanaryLogEvent logEvent = new CanaryLogEvent(context, mailboxContext, canaryStatus, canary15Cookie.CreationTime, canary15Cookie.LogData);
						OwaServerLogger.AppendToLog(logEvent);
						subActivityScopeLogger.LogNext("f");
					}
					if (flag3)
					{
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(getRequestDetailsLogger, OwaServerLogger.LoggerData.CanaryCreationTime, canary15Cookie.CreationTime);
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(getRequestDetailsLogger, OwaServerLogger.LoggerData.CanaryLogData, canary15Cookie.LogData);
						subActivityScopeLogger.LogNext("g");
					}
					else
					{
						if (RequestDispatcherUtilities.IsDownLevelClient(context, false))
						{
							throw new OwaCanaryException(Canary15Profile.Owa.Name, canary15Cookie.Value);
						}
						context.Response.StatusCode = 449;
						context.Response.End();
					}
					RequestDetailsLoggerBase<RequestDetailsLogger> requestDetailsLogger = getRequestDetailsLogger;
					Enum key = OwaServerLogger.LoggerData.CanaryStatus;
					int num = (int)canaryStatus;
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(requestDetailsLogger, key, num.ToString("X"));
					RequestDetailsLogger.LogEvent(getRequestDetailsLogger, OwaServerLogger.LoggerData.CanaryValidationEnd);
					subActivityScopeLogger.LogEnd();
					return;
				}
			}
			else
			{
				ExTraceGlobals.CoreCallTracer.TraceError(0L, "[OwaRequestHandler::InternalOnPostAuthorizeRequest] httpContext.User or httpContext.User.Identity is <NULL>.");
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000455FC File Offset: 0x000437FC
		public static string GetOriginalIdentitySid(HttpContext httpContext)
		{
			CompositeIdentity compositeIdentity = httpContext.User.Identity as CompositeIdentity;
			SecurityIdentifier securityIdentifier;
			if (compositeIdentity != null)
			{
				securityIdentifier = compositeIdentity.CanarySid;
			}
			else
			{
				securityIdentifier = httpContext.User.Identity.GetSecurityIdentifier();
			}
			return securityIdentifier.ToString();
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00045640 File Offset: 0x00043840
		private static bool IsAfterLogonRequest(HttpRequest request)
		{
			Uri uri;
			return request != null && request.TryParseUrlReferrer(out uri) && string.CompareOrdinal(request.HttpMethod, "GET") == 0 && string.Compare(uri.AbsolutePath, OwaUrl.LogonFBA.ImplicitUrl, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0004568C File Offset: 0x0004388C
		private static bool IsProxyLogonRequest(OwaRequestType requestType)
		{
			return requestType == OwaRequestType.ProxyLogon || requestType == OwaRequestType.ProxyPing;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00045698 File Offset: 0x00043898
		private static void UpdateCanaryStatus(ref CanaryLogEvent.CanaryStatus canaryStatus, bool condition, CanaryLogEvent.CanaryStatus flag)
		{
			if (condition)
			{
				canaryStatus |= flag;
			}
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000456A4 File Offset: 0x000438A4
		private static bool IsRequestWithCanary(HttpRequest request, OwaRequestType requestType, bool isAuthenticated)
		{
			if (!isAuthenticated)
			{
				return false;
			}
			if (requestType != OwaRequestType.EsoRequest)
			{
				switch (requestType)
				{
				case OwaRequestType.ProxyPing:
				case OwaRequestType.KeepAlive:
				case OwaRequestType.Resource:
				case OwaRequestType.PublishedCalendarView:
				case OwaRequestType.ICalHttpHandler:
				case OwaRequestType.HealthPing:
				case OwaRequestType.SpeechReco:
					return false;
				}
				if (string.Compare(request.Path, OwaUrl.Default14Page.ImplicitUrl, true, CultureInfo.InvariantCulture) == 0)
				{
					return false;
				}
				if (string.CompareOrdinal(request.HttpMethod, "POST") == 0)
				{
					return string.Compare(request.Path, OwaUrl.SessionDataPage.ImplicitUrl, true, CultureInfo.InvariantCulture) != 0 && string.Compare(request.Path, OwaUrl.PreloadSessionDataPage.ImplicitUrl, true, CultureInfo.InvariantCulture) != 0 && string.Compare(request.Path, OwaUrl.PLT1Page.ImplicitUrl, true, CultureInfo.InvariantCulture) != 0 && !UrlUtilities.IsRemoteNotificationRequest(request);
				}
				return string.CompareOrdinal(request.HttpMethod, "GET") != 0 || requestType == OwaRequestType.Oeh || (requestType == OwaRequestType.ServiceRequest && OwaRequestHandler.IsGetRequestWithCanary(request));
			}
			return false;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000457B7 File Offset: 0x000439B7
		private static bool IsGetRequestWithCanary(HttpRequest request)
		{
			return request.Url.LocalPath.EndsWith("/service.svc/s/GetFileAttachment", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000AA7 RID: 2727
		private const int HttpStatusCodeRetryWith = 449;
	}
}
