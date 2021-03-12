using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000057 RID: 87
	public class AutodiscoverBasicAuthenticationHttpModule : IHttpModule
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00011B38 File Offset: 0x0000FD38
		public void Dispose()
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00011B3C File Offset: 0x0000FD3C
		public void Init(HttpApplication application)
		{
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.Autodiscover.NoAuthenticationTokenToNego.Enabled && Common.AutodiscoverBindingAuthenticationScheme == AuthenticationSchemes.Negotiate)
			{
				application.PostAuthenticateRequest += AutodiscoverBasicAuthenticationHttpModule.OnPostAuthenticateRequestHandler;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00011B78 File Offset: 0x0000FD78
		private static void OnPostAuth(object source, EventArgs args)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			HttpRequest request = context.Request;
			if (request.IsAuthenticated && request.LogonUserIdentity.AuthenticationType == "Basic" && request.LogonUserIdentity.IsAuthenticated)
			{
				WindowsIdentity windowsIdentity = new WindowsIdentity(context.Request.LogonUserIdentity.Token, "Negotiate", WindowsAccountType.Normal);
				try
				{
					FaultInjection.GenerateFault((FaultInjection.LIDs)4154862909U);
					context.User = new WindowsPrincipal(windowsIdentity);
				}
				catch (SystemException ex)
				{
					string safeName = context.Request.LogonUserIdentity.GetSafeName(true);
					ExTraceGlobals.FrameworkTracer.TraceError<string, SystemException>(0L, "Exception thrown constructing WindowsPrincipal for user {0}. Exception {1}", safeName, ex);
					Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_ErrWebException, Common.PeriodicKey, new object[]
					{
						ex.Message,
						ex.StackTrace
					});
					windowsIdentity.Dispose();
				}
			}
		}

		// Token: 0x040002A0 RID: 672
		private static readonly EventHandler OnPostAuthenticateRequestHandler = new EventHandler(AutodiscoverBasicAuthenticationHttpModule.OnPostAuth);
	}
}
