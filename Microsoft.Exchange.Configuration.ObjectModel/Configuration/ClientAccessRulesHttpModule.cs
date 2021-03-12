using System;
using System.Web;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ClientAccessRules;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration
{
	// Token: 0x020000B1 RID: 177
	public class ClientAccessRulesHttpModule : IHttpModule
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x0001A93D File Offset: 0x00018B3D
		public void Init(HttpApplication context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("application");
			}
			context.PostAuthenticateRequest += this.OnPostAuthenticateRequest;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001A95F File Offset: 0x00018B5F
		public void Dispose()
		{
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001A9EC File Offset: 0x00018BEC
		private void OnPostAuthenticateRequest(object source, EventArgs args)
		{
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[ClientAccessRulesHttpModule::OnPostAuthenticateRequest] Entering");
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.RpsClientAccessRulesEnabled.Enabled && HttpRuntime.AppDomainAppVirtualPath.IndexOf("/PowerShell", StringComparison.OrdinalIgnoreCase) == 0)
			{
				IClientAccessRulesAuthorizer clientAccessRulesAuthorizer = new RemotePowershellClientAccessRulesAuthorizer();
				HttpContext httpContext = HttpContext.Current;
				if (httpContext.Request.IsAuthenticated)
				{
					ExTraceGlobals.HttpModuleTracer.TraceDebug((long)this.GetHashCode(), "[ClientAccessRulesHttpModule::OnPostAuthenticateRequest] Request is already authenticated.");
					OrganizationId userOrganization = clientAccessRulesAuthorizer.GetUserOrganization();
					if (userOrganization == null)
					{
						ExTraceGlobals.HttpModuleTracer.TraceDebug((long)this.GetHashCode(), "[ClientAccessRulesHttpModule::OnPostAuthenticateRequest] orgId = null.");
						return;
					}
					UserToken userToken = HttpContext.Current.CurrentUserToken();
					bool flag = ClientAccessRulesUtils.ShouldBlockConnection(userOrganization, ClientAccessRulesUtils.GetUsernameFromContext(httpContext), clientAccessRulesAuthorizer.Protocol, ClientAccessRulesUtils.GetRemoteEndPointFromContext(httpContext), ClientAccessRulesHttpModule.GetAuthenticationTypeFromContext(httpContext), userToken.Recipient, delegate(ClientAccessRulesEvaluationContext context)
					{
						clientAccessRulesAuthorizer.SafeAppendGenericInfo(httpContext, ClientAccessRulesConstants.ClientAccessRuleName, context.CurrentRule.Name);
					}, delegate(double latency)
					{
						if (latency > 50.0)
						{
							clientAccessRulesAuthorizer.SafeAppendGenericInfo(httpContext, ClientAccessRulesConstants.ClientAccessRulesLatency, latency.ToString());
							ExTraceGlobals.HttpModuleTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[ClientAccessRulesHttpModule::OnPostAuthenticateRequest] {0} = {1}.", ClientAccessRulesConstants.ClientAccessRulesLatency, latency.ToString());
						}
					});
					if (flag)
					{
						ExTraceGlobals.HttpModuleTracer.TraceDebug((long)this.GetHashCode(), "[ClientAccessRulesHttpModule::OnPostAuthenticateRequest] ClientAccessRules' evaluation for the organization blocks the connection");
						clientAccessRulesAuthorizer.ResponseToError(httpContext);
						httpContext.ApplicationInstance.CompleteRequest();
					}
				}
				ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[ClientAccessRulesHttpModule::OnPostAuthenticateRequest] Exit");
				return;
			}
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[ClientAccessRulesHttpModule::OnPostAuthenticateRequest] Exit");
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001ABA2 File Offset: 0x00018DA2
		private static ClientAccessAuthenticationMethod GetAuthenticationTypeFromContext(HttpContext httpContext)
		{
			if (httpContext.Items["AuthType"] != null && "BASIC".Equals(httpContext.Items["AuthType"].ToString(), StringComparison.InvariantCultureIgnoreCase))
			{
				return ClientAccessAuthenticationMethod.BasicAuthentication;
			}
			return ClientAccessAuthenticationMethod.NonBasicAuthentication;
		}

		// Token: 0x0400019C RID: 412
		private const string AuthenticationTypeItemName = "AuthType";

		// Token: 0x0400019D RID: 413
		private const string BasicAuthenticationType = "BASIC";

		// Token: 0x0400019E RID: 414
		private const string ModuleName = "ClientAccessRulesHttpModule";
	}
}
