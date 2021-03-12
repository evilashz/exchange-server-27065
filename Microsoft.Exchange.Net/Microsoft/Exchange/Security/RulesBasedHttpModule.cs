using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A2A RID: 2602
	public class RulesBasedHttpModule : IHttpModule
	{
		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x060038AC RID: 14508 RVA: 0x000903D8 File Offset: 0x0008E5D8
		internal static ExEventLog EventLogger
		{
			get
			{
				return RulesBasedHttpModule.eventLogger;
			}
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000903E0 File Offset: 0x0008E5E0
		public void Init(HttpApplication context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (!RulesBasedHttpModuleConfiguration.Instance.TryLoad())
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError((long)this.GetHashCode(), "[RulesBasedHttpModule.Init] Failed to load DenyRules");
			}
			context.BeginRequest += this.OnBeginRequest;
			context.AuthenticateRequest += this.OnAuthenticate;
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x00090444 File Offset: 0x0008E644
		private void OnBeginRequest(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			HttpModuleAuthenticationDenyRulesCollection authenticationDenyRules = RulesBasedHttpModuleConfiguration.Instance.AuthenticationDenyRules;
			ExTraceGlobals.RulesBasedHttpModuleTracer.TraceDebug((long)this.GetHashCode(), "[RulesBasedHttpModule.OnBeginRequest] Enter");
			if (authenticationDenyRules != null && authenticationDenyRules.EvaluatePreAuthRules(context))
			{
				context.Response.StatusCode = 403;
				context.Response.StatusDescription = "Forbidden";
				context.ApplicationInstance.CompleteRequest();
			}
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000904B7 File Offset: 0x0008E6B7
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000904BC File Offset: 0x0008E6BC
		private void OnAuthenticate(object source, EventArgs args)
		{
			ExTraceGlobals.RulesBasedHttpModuleTracer.TraceDebug((long)this.GetHashCode(), "[RulesBasedHttpModule.OnAuthenticate] Enter");
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			HttpRequest request = context.Request;
			if (!request.IsAuthenticated)
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceDebug((long)this.GetHashCode(), "[RulesBasedHttpModule.OnAuthenticate] Request is not authenticated. Skip.");
				return;
			}
			HttpModuleAuthenticationDenyRulesCollection authenticationDenyRules = RulesBasedHttpModuleConfiguration.Instance.AuthenticationDenyRules;
			if (authenticationDenyRules != null && authenticationDenyRules.EvaluatePostAuthRules(context))
			{
				context.Response.StatusCode = 403;
				context.Response.StatusDescription = "Forbidden";
				context.ApplicationInstance.CompleteRequest();
			}
		}

		// Token: 0x040030B3 RID: 12467
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ConfigurationTracer.Category, "MSExchange Common");
	}
}
