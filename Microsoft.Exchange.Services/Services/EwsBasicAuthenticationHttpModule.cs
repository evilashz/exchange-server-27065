using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services
{
	// Token: 0x02000063 RID: 99
	public class EwsBasicAuthenticationHttpModule : IHttpModule
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000BDAA File Offset: 0x00009FAA
		public void Dispose()
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000BDAC File Offset: 0x00009FAC
		public void Init(HttpApplication application)
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.WindowsLiveID.Enabled)
			{
				application.PostAuthenticateRequest += EwsBasicAuthenticationHttpModule.OnPostAuthenticateRequestHandler;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000BDE0 File Offset: 0x00009FE0
		private static void OnPostAuth(object source, EventArgs args)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (context.Request.IsAuthenticated && context.User.Identity is WindowsIdentity)
			{
				WindowsIdentity windowsIdentity = context.User.Identity as WindowsIdentity;
				string memberName = context.GetMemberName();
				if (!string.IsNullOrEmpty(memberName))
				{
					ADSessionSettings adsessionSettings = Directory.SessionSettingsFromAddress(memberName);
					if (adsessionSettings.CurrentOrganizationId != OrganizationId.ForestWideOrgId)
					{
						context.User = new GenericPrincipal(new LiveIDIdentity(windowsIdentity.Name, windowsIdentity.User.ToString(), memberName, null, null, null)
						{
							UserOrganizationId = adsessionSettings.CurrentOrganizationId
						}, null);
					}
				}
			}
		}

		// Token: 0x0400054C RID: 1356
		private static readonly EventHandler OnPostAuthenticateRequestHandler = new EventHandler(EwsBasicAuthenticationHttpModule.OnPostAuth);
	}
}
