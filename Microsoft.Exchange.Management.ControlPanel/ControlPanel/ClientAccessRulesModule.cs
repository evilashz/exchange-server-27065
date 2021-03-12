using System;
using System.Web;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Clients;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ClientAccessRules;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001F4 RID: 500
	internal sealed class ClientAccessRulesModule : IHttpModule
	{
		// Token: 0x06002656 RID: 9814 RVA: 0x00076459 File Offset: 0x00074659
		public void Init(HttpApplication application)
		{
			application.PostAuthenticateRequest += this.OnPostAuthenticateRequest;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x00076470 File Offset: 0x00074670
		private void OnPostAuthenticateRequest(object sender, EventArgs e)
		{
			HttpApplication applicationInstance = HttpContext.Current.ApplicationInstance;
			HttpContext httpContext = HttpContext.Current;
			HttpRequest request = httpContext.Request;
			if (request.IsAuthenticated && RbacPrincipal.GetCurrent(false) != null && ClientAccessRulesModule.ShouldBlockConnection(httpContext, RbacPrincipal.Current.RbacConfiguration))
			{
				if (httpContext.IsWebServiceRequest() || httpContext.IsUploadRequest())
				{
					ClientAccessRulesModule.SendAjaxErrorToClient(httpContext);
				}
				else
				{
					httpContext.Response.Redirect(string.Format("{0}logoff.owa?reason=6", EcpUrl.OwaVDir));
				}
				applicationInstance.CompleteRequest();
			}
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000764EF File Offset: 0x000746EF
		public void Dispose()
		{
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000764F4 File Offset: 0x000746F4
		private static void SendAjaxErrorToClient(HttpContext httpContext)
		{
			httpContext.ClearError();
			httpContext.Response.Clear();
			httpContext.Response.TrySkipIisCustomErrors = true;
			httpContext.Response.Cache.SetCacheability(HttpCacheability.Private);
			httpContext.Response.ContentType = (httpContext.IsWebServiceRequest() ? "application/json; charset=utf-8" : "text/html");
			httpContext.Response.Headers["jsonerror"] = "true";
			httpContext.Response.AddHeader("X-ECP-ERROR", "ClientAccessRulesBlock");
			httpContext.Response.Write(AntiXssEncoder.HtmlEncode(Strings.ClientAccessRulesEACBlockMessage, false));
			httpContext.Response.StatusCode = (httpContext.IsWebServiceRequest() ? 500 : 200);
			httpContext.Response.End();
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000765E4 File Offset: 0x000747E4
		private static bool ShouldBlockConnection(HttpContext httpContext, ExchangeRunspaceConfiguration exchangeRunspaceConfiguration)
		{
			if (exchangeRunspaceConfiguration == null || exchangeRunspaceConfiguration.ExecutingUser == null || !VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Eac.EACClientAccessRulesEnabled.Enabled)
			{
				return false;
			}
			double ruleLatency = 0.0;
			string ruleName = string.Empty;
			string usernameFromADRawEntry = ClientAccessRulesUtils.GetUsernameFromADRawEntry(exchangeRunspaceConfiguration.ExecutingUser);
			bool flag = ClientAccessRulesUtils.ShouldBlockConnection(exchangeRunspaceConfiguration.OrganizationId, usernameFromADRawEntry, ClientAccessProtocol.ExchangeAdminCenter, ClientAccessRulesUtils.GetRemoteEndPointFromContext(httpContext), httpContext.Request.IsAuthenticatedByAdfs() ? ClientAccessAuthenticationMethod.AdfsAuthentication : ClientAccessAuthenticationMethod.BasicAuthentication, exchangeRunspaceConfiguration.ExecutingUser, delegate(ClientAccessRulesEvaluationContext context)
			{
				ruleName = context.CurrentRule.Name;
			}, delegate(double latency)
			{
				ruleLatency = latency;
			});
			if (flag || ruleLatency > 50.0)
			{
				ActivityContextLogger.Instance.LogEvent(new ClientAccessRulesLogEvent(exchangeRunspaceConfiguration.OrganizationId, usernameFromADRawEntry, ClientAccessRulesUtils.GetRemoteEndPointFromContext(httpContext), httpContext.Request.IsAuthenticatedByAdfs() ? ClientAccessAuthenticationMethod.AdfsAuthentication : ClientAccessAuthenticationMethod.BasicAuthentication, ruleName, ruleLatency, flag));
			}
			return flag;
		}

		// Token: 0x04001F60 RID: 8032
		private const string EACBlockedByClientAccessRulesLogoff = "{0}logoff.owa?reason=6";

		// Token: 0x04001F61 RID: 8033
		private const string BlockedEcpErrorHeaderContent = "ClientAccessRulesBlock";
	}
}
