using System;
using System.Web;
using Microsoft.Exchange.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpUtilities
{
	// Token: 0x02000002 RID: 2
	public class AnonymousRequestFilterModule : IHttpModule
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020DE File Offset: 0x000002DE
		public void Init(HttpApplication application)
		{
			application.PostAuthenticateRequest += delegate(object sender, EventArgs args)
			{
				this.OnPostAuthenticateRequest((HttpApplication)sender);
			};
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F2 File Offset: 0x000002F2
		public void Dispose()
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000212C File Offset: 0x0000032C
		private void OnPostAuthenticateRequest(HttpApplication httpApplication)
		{
			if (!HttpProxySettings.AnonymousRequestFilterEnabled)
			{
				return;
			}
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				this.OnPostAuthenticateInternal(httpApplication);
			}, delegate(Exception exception)
			{
				HttpContext.Current.Items[AnonymousRequestFilterModule.AnonymousRequestFilterModuleLoggingKey] = exception.ToString();
			});
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002184 File Offset: 0x00000384
		private void OnPostAuthenticateInternal(HttpApplication httpApplication)
		{
			HttpContext context = httpApplication.Context;
			if (!context.Request.IsAuthenticated)
			{
				bool flag = false;
				bool flag2 = false;
				string value = string.Empty;
				string httpMethod = context.Request.HttpMethod;
				string absolutePath = context.Request.Url.AbsolutePath;
				if (httpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase) || httpMethod.Equals("HEAD", StringComparison.OrdinalIgnoreCase))
				{
					if (HttpProxyGlobals.ProtocolType != ProtocolType.Autodiscover || (!ProtocolHelper.IsOAuthMetadataRequest(absolutePath) && !ProtocolHelper.IsAutodiscoverV2Request(absolutePath)))
					{
						flag2 = true;
						flag = true;
						value = "AutodiscoverEwsDiscovery";
					}
				}
				else if (!ProtocolHelper.IsAnyWsSecurityRequest(context.Request.Url.AbsolutePath) && HttpProxyGlobals.ProtocolType != ProtocolType.Autodiscover)
				{
					flag2 = true;
					value = "AnonymousRequestDisallowed";
				}
				if (!string.IsNullOrEmpty(value))
				{
					context.Items[AnonymousRequestFilterModule.AnonymousRequestFilterModuleLoggingKey] = value;
				}
				if (flag)
				{
					AutodiscoverEwsDiscoveryResponseHelper.AddEndpointEnabledHeaders(context.Response);
				}
				if (flag2)
				{
					context.Response.StatusCode = 401;
					context.Response.StatusDescription = "Anonymous Request Disallowed";
					httpApplication.CompleteRequest();
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly string AnonymousRequestFilterModuleLoggingKey = "AnonymousRequestFilterModule";
	}
}
