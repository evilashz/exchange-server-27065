using System;
using System.Web;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200055C RID: 1372
	public static class NavigationUtil
	{
		// Token: 0x06004027 RID: 16423 RVA: 0x000C35E4 File Offset: 0x000C17E4
		internal static bool ShouldRenderOwaLink(RbacPrincipal rbacPrincipal, bool showAdminFeature)
		{
			return !showAdminFeature && !NavigationUtil.LaunchedFromOutlook && rbacPrincipal.IsInRole("Mailbox+OWA+MailboxFullAccess");
		}

		// Token: 0x170024E0 RID: 9440
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x000C3600 File Offset: 0x000C1800
		private static bool LaunchedFromOutlook
		{
			get
			{
				bool result = false;
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					string value = httpContext.Request.QueryString["rfr"];
					result = "olk".Equals(value, StringComparison.OrdinalIgnoreCase);
				}
				return result;
			}
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x000C363C File Offset: 0x000C183C
		internal static bool ShouldRenderLogoutLink(RbacPrincipal rbacPrincipal)
		{
			return rbacPrincipal.IsInRole("MailboxFullAccess+!DelegatedAdmin+!ByoidAdmin");
		}
	}
}
