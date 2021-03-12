using System;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy.ProxyAssistant
{
	// Token: 0x02000004 RID: 4
	internal class ProxyAssistantModule : IHttpModule
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002105 File Offset: 0x00000305
		public ProxyAssistantModule() : this(HttpProxySettings.ProxyAssistantEnabled.Value, null)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002118 File Offset: 0x00000318
		internal ProxyAssistantModule(bool isEnabled, IProxyAssistantDiagnostics diagnostics)
		{
			this.isEnabled = isEnabled;
			this.diagnostics = diagnostics;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000215E File Offset: 0x0000035E
		public void Init(HttpApplication application)
		{
			application.PostAuthorizeRequest += delegate(object sender, EventArgs args)
			{
				this.OnPostAuthorizeRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
			};
			application.PreSendRequestHeaders += delegate(object sender, EventArgs args)
			{
				this.OnPreSendRequestHeaders(new HttpContextWrapper(((HttpApplication)sender).Context));
			};
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002184 File Offset: 0x00000384
		public void Dispose()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021BC File Offset: 0x000003BC
		internal void OnPostAuthorizeRequest(HttpContextBase context)
		{
			if (!this.isEnabled)
			{
				return;
			}
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				if (NativeProxyHelper.CanNativeProxyHandleRequest(context.ApplicationInstance.Context))
				{
					NativeProxyHelper.UpdateRequestHeaders(context.ApplicationInstance.Context);
				}
			}, new Diagnostics.LastChanceExceptionHandler(this.LastChanceExceptionHandler));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002250 File Offset: 0x00000450
		internal void OnPreSendRequestHeaders(HttpContextBase context)
		{
			if (!this.isEnabled)
			{
				return;
			}
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				if (NativeProxyHelper.CanNativeProxyHandleRequest(context.ApplicationInstance.Context) && context.Response.StatusCode == 200)
				{
					this.AddCookiesToClientResponse(context);
				}
			}, new Diagnostics.LastChanceExceptionHandler(this.LastChanceExceptionHandler));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002298 File Offset: 0x00000498
		private IProxyAssistantDiagnostics GetDiagnostics(HttpContextBase context)
		{
			IProxyAssistantDiagnostics result;
			if (this.diagnostics == null)
			{
				result = new ProxyAssistantDiagnostics(context);
			}
			else
			{
				result = this.diagnostics;
			}
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022C0 File Offset: 0x000004C0
		private void LastChanceExceptionHandler(Exception ex)
		{
			IProxyAssistantDiagnostics proxyAssistantDiagnostics = this.GetDiagnostics(new HttpContextWrapper(HttpContext.Current));
			if (proxyAssistantDiagnostics != null)
			{
				proxyAssistantDiagnostics.LogUnhandledException(ex);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022E8 File Offset: 0x000004E8
		private void AddCookiesToClientResponse(HttpContextBase context)
		{
			if (HttpProxyGlobals.ProtocolType == ProtocolType.Ews)
			{
				HttpRequestBase request = context.Request;
				HttpResponseBase response = context.Response;
				string value = response.Headers["X-FromBackend-ServerAffinity"];
				if (!string.IsNullOrEmpty(value) && request.Cookies["X-BackEndOverrideCookie"] == null)
				{
					string text = request.Headers["X-ProxyTargetServer"];
					string s = request.Headers["X-ProxyTargetServerVersion"];
					int version = 0;
					if (!string.IsNullOrWhiteSpace(text) && int.TryParse(s, out version))
					{
						BackEndServer backEndServer = new BackEndServer(text, version);
						HttpCookie httpCookie = new HttpCookie("X-BackEndOverrideCookie", backEndServer.ToString());
						httpCookie.HttpOnly = true;
						httpCookie.Secure = request.IsSecureConnection;
						response.Cookies.Add(httpCookie);
					}
				}
			}
		}

		// Token: 0x04000002 RID: 2
		private readonly bool isEnabled;

		// Token: 0x04000003 RID: 3
		private readonly IProxyAssistantDiagnostics diagnostics;
	}
}
