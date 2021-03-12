using System;
using System.Threading;
using System.Web;
using Microsoft.Exchange.CommonHelpProvider;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005EE RID: 1518
	internal static class HelpUtil
	{
		// Token: 0x0600442C RID: 17452 RVA: 0x000CE1A8 File Offset: 0x000CC3A8
		private static bool IsEACHelpId(string helpId)
		{
			bool result = false;
			if (Enum.IsDefined(typeof(EACHelpId), helpId))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x000CE1CC File Offset: 0x000CC3CC
		private static string ConstructOwaOptionsHelpUrl(string helpId)
		{
			OrganizationProperties organizationProperties = null;
			OrganizationPropertyCache.TryGetOrganizationProperties(RbacPrincipal.Current.RbacConfiguration.OrganizationId, out organizationProperties);
			return HelpProvider.ConstructHelpRenderingUrl(Thread.CurrentThread.CurrentUICulture.LCID, HelpProvider.OwaHelpExperience.Options, helpId, HelpProvider.RenderingMode.Mouse, null, organizationProperties).ToString();
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x000CE210 File Offset: 0x000CC410
		private static string BuildEhcHref(string helpId, bool isEACHelpId)
		{
			string result;
			if (isEACHelpId)
			{
				RbacPrincipal rbacPrincipal = HttpContext.Current.User as RbacPrincipal;
				result = ((rbacPrincipal != null) ? HelpProvider.ConstructHelpRenderingUrl(helpId, rbacPrincipal.RbacConfiguration).ToString() : HelpProvider.ConstructHelpRenderingUrl(helpId).ToString());
			}
			else
			{
				result = HelpUtil.ConstructOwaOptionsHelpUrl(helpId);
			}
			return result;
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x000CE25E File Offset: 0x000CC45E
		public static string BuildEhcHref(string helpId)
		{
			HelpUtil.EnsureInit();
			return HelpUtil.BuildEhcHref(helpId, HelpUtil.IsEACHelpId(helpId));
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x000CE271 File Offset: 0x000CC471
		public static string BuildEhcHref(EACHelpId helpId)
		{
			HelpUtil.EnsureInit();
			return HelpUtil.BuildEhcHref(helpId.ToString(), true);
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x000CE289 File Offset: 0x000CC489
		public static string BuildEhcHref(OptionsHelpId helpId)
		{
			HelpUtil.EnsureInit();
			return HelpUtil.BuildEhcHref(helpId.ToString(), false);
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x000CE2A1 File Offset: 0x000CC4A1
		public static string BuildFVAEhcHref(string helpId, string controlId)
		{
			HelpUtil.EnsureInit();
			return HelpUtil.BuildEhcHref(controlId, HelpUtil.IsEACHelpId(helpId));
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x000CE2B4 File Offset: 0x000CC4B4
		public static string BuildErrorAssistanceUrl(LocalizedException locEx)
		{
			HelpUtil.EnsureInit();
			Uri uri = null;
			HelpProvider.TryGetErrorAssistanceUrl(locEx, out uri);
			if (!(uri != null))
			{
				return null;
			}
			return uri.ToEscapedString();
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x000CE2E2 File Offset: 0x000CC4E2
		public static string BuildPrivacyStatmentHref()
		{
			HelpUtil.EnsureInit();
			if (Util.IsDataCenter)
			{
				return HelpUtil.AppendLCID(HelpProvider.GetMSOnlinePrivacyStatementUrl().ToString());
			}
			return HelpUtil.AppendLCID(HelpProvider.GetExchange2013PrivacyStatementUrl().ToString());
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x000CE310 File Offset: 0x000CC510
		public static string BuildCommunitySiteHref()
		{
			Uri uri;
			if (HelpProvider.TryGetCommunityUrl(RbacPrincipal.Current.RbacConfiguration.OrganizationId, out uri))
			{
				return HelpUtil.AppendLCID(uri.ToEscapedString());
			}
			return string.Empty;
		}

		// Token: 0x06004436 RID: 17462 RVA: 0x000CE346 File Offset: 0x000CC546
		private static string AppendLCID(string url)
		{
			if (!string.IsNullOrEmpty(url))
			{
				return string.Format("{0}&clcid={1}", url, Util.GetLCID());
			}
			return url;
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x000CE362 File Offset: 0x000CC562
		private static void EnsureInit()
		{
			if (!HelpUtil.initialized)
			{
				HelpProvider.Initialize(HelpProvider.HelpAppName.Ecp);
				HelpUtil.initialized = true;
			}
		}

		// Token: 0x04002DE0 RID: 11744
		private static bool initialized;
	}
}
