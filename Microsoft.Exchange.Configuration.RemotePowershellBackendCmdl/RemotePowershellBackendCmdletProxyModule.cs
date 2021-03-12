using System;
using System.Collections.Specialized;
using System.Web;
using Microsoft.Exchange.Configuration.RemotePowershellBackendCmdletProxy.EventLog;
using Microsoft.Exchange.Configuration.RemotePowershellBackendCmdletProxy.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.RemotePowershellBackendCmdletProxy;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Configuration.RemotePowershellBackendCmdletProxy
{
	// Token: 0x02000003 RID: 3
	public class RemotePowershellBackendCmdletProxyModule : IHttpModule
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021F5 File Offset: 0x000003F5
		public void Init(HttpApplication application)
		{
			application.AuthenticateRequest += this.OnAuthenticateRequest;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002209 File Offset: 0x00000409
		public void Dispose()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000220C File Offset: 0x0000040C
		private static CommonAccessToken CommonAccessTokenFromUrl(string user, Uri requestURI, out Exception ex)
		{
			if (requestURI == null)
			{
				throw new ArgumentNullException("requestURI");
			}
			ex = null;
			CommonAccessToken result = null;
			NameValueCollection nameValueCollectionFromUri = LiveIdBasicAuthModule.GetNameValueCollectionFromUri(requestURI);
			string text = nameValueCollectionFromUri.Get("X-Rps-CAT");
			if (!string.IsNullOrWhiteSpace(text))
			{
				try
				{
					result = CommonAccessToken.Deserialize(Uri.UnescapeDataString(text));
				}
				catch (Exception ex2)
				{
					Logger.TraceError(ExTraceGlobals.RemotePowershellBackendCmdletProxyModuleTracer, "Got exception when trying to parse CommonAccessToken: {0}.", new object[]
					{
						ex2.ToString()
					});
					Logger.LogEvent(TaskEventLogConstants.Tuple_LogInvalidCommonAccessTokenReceived, text, new object[]
					{
						user,
						requestURI.ToString(),
						Strings.ErrorWhenParsingCommonAccessToken(ex2.ToString())
					});
					ex = ex2;
				}
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022C8 File Offset: 0x000004C8
		private void OnAuthenticateRequest(object source, EventArgs args)
		{
			Logger.EnterFunction(ExTraceGlobals.RemotePowershellBackendCmdletProxyModuleTracer, "OnAuthenticateOrPostAuthenticateRequest");
			HttpContext httpContext = HttpContext.Current;
			if (httpContext.Request.IsAuthenticated)
			{
				Logger.TraceDebug(ExTraceGlobals.RemotePowershellBackendCmdletProxyModuleTracer, "[RemotePowershellBackendCmdletProxyModule::OnAuthenticateOrPostAuthenticateRequest] Current authenticated user is {0} of type {1}.", new object[]
				{
					httpContext.User.Identity,
					httpContext.User.Identity.GetType()
				});
				string value = httpContext.Request.Headers["X-CommonAccessToken"];
				if (string.IsNullOrEmpty(value))
				{
					Uri url = httpContext.Request.Url;
					Exception ex = null;
					CommonAccessToken commonAccessToken = RemotePowershellBackendCmdletProxyModule.CommonAccessTokenFromUrl(httpContext.User.Identity.ToString(), url, out ex);
					if (ex != null)
					{
						WinRMInfo.SetFailureCategoryInfo(httpContext.Response.Headers, FailureCategory.BackendCmdletProxy, ex.GetType().Name);
						httpContext.Response.StatusCode = 500;
						httpContext.ApplicationInstance.CompleteRequest();
					}
					else if (commonAccessToken != null)
					{
						httpContext.Request.Headers["X-CommonAccessToken"] = commonAccessToken.Serialize();
						Logger.TraceDebug(ExTraceGlobals.RemotePowershellBackendCmdletProxyModuleTracer, "[RemotePowershellBackendCmdletProxyModule::OnAuthenticateOrPostAuthenticateRequest] The CommonAccessToken has been successfully stampped in request HTTP header.", new object[0]);
					}
				}
			}
			Logger.ExitFunction(ExTraceGlobals.RemotePowershellBackendCmdletProxyModuleTracer, "OnAuthenticateOrPostAuthenticateRequest");
		}

		// Token: 0x04000006 RID: 6
		internal const string CommonAccessTokenHttpParam = "X-Rps-CAT";
	}
}
