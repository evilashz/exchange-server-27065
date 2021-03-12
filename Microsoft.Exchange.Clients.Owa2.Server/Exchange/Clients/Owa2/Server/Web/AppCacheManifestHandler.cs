using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Clients.Security;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000470 RID: 1136
	internal class AppCacheManifestHandler : AppCacheManifestHandlerBase
	{
		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x0008B2A0 File Offset: 0x000894A0
		// (set) Token: 0x0600266F RID: 9839 RVA: 0x0008B2D3 File Offset: 0x000894D3
		public override string VersionString
		{
			get
			{
				if (this.buildVersion == null)
				{
					UserContext userContext = UserContextManager.GetUserContext(base.Context);
					this.buildVersion = userContext.CurrentOwaVersion;
				}
				return this.buildVersion;
			}
			set
			{
				this.buildVersion = value;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x0008B2DC File Offset: 0x000894DC
		protected override bool IsDatacenterNonDedicated
		{
			get
			{
				return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x0008B307 File Offset: 0x00089507
		protected override string ResourceDirectory
		{
			get
			{
				return "../prem/" + this.VersionString;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x0008B31C File Offset: 0x0008951C
		protected override bool UseRootDirForAppCacheVdir
		{
			get
			{
				return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.UseRootDirForAppCacheVdir.Enabled;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x0008B348 File Offset: 0x00089548
		protected override bool IsHostNameSwitchFlightEnabled
		{
			get
			{
				UserContext userContext = UserContextManager.GetUserContext(base.Context);
				return userContext != null && userContext.FeaturesManager != null && userContext.FeaturesManager.ServerSettings.OwaHostNameSwitch.Enabled;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x0008B386 File Offset: 0x00089586
		public override HostNameController HostNameController
		{
			get
			{
				return RequestDispatcher.HostNameController;
			}
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0008B390 File Offset: 0x00089590
		protected override string[] GetEnabledFeatures()
		{
			FeaturesManager featuresManager = UserContextManager.GetUserContext(base.Context).FeaturesManager;
			return featuresManager.GetClientEnabledFeatures();
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x0008B3B4 File Offset: 0x000895B4
		internal static string GetUserContextId(HttpContext context)
		{
			UserContextCookie userContextCookie = UserContextCookie.GetUserContextCookie(context);
			if (userContextCookie == null || userContextCookie.CookieValue == null)
			{
				return string.Empty;
			}
			return userContextCookie.CookieValue;
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x0008B3DF File Offset: 0x000895DF
		protected override bool IsRealmRewrittenFromPathToQuery(HttpContext context)
		{
			return UrlUtilities.IsRealmRewrittenFromPathToQuery(context);
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x0008B3E7 File Offset: 0x000895E7
		protected override string GetThemeFolder()
		{
			return ThemeManagerFactory.GetInstance(this.VersionString).GetThemeFolderName(this.UserAgent, base.Context);
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0008B405 File Offset: 0x00089605
		protected override bool ShouldSkipThemeFolder()
		{
			return ThemeManagerFactory.GetInstance(this.VersionString).ShouldSkipThemeFolder;
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0008B418 File Offset: 0x00089618
		protected override string GetUserUPN()
		{
			string result = string.Empty;
			UserContext userContext = UserContextManager.GetUserContext(base.Context);
			if (userContext != null && userContext.ExchangePrincipal != null)
			{
				result = userContext.UserPrincipalName;
			}
			return result;
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x0008B44A File Offset: 0x0008964A
		protected override List<CultureInfo> GetSupportedCultures()
		{
			return ClientCultures.SupportedCultureInfos;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x0008B454 File Offset: 0x00089654
		protected override bool ShouldAddDefaultMasterPageEntiresWithFlightDisabled()
		{
			if (this.IsMowaClient)
			{
				HttpCookie httpCookie = base.Context.Request.Cookies.Get("flights");
				if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value) && httpCookie.Value.Equals("none", StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400167E RID: 5758
		private string buildVersion;
	}
}
