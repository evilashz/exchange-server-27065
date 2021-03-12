using System;
using System.Threading;
using System.Web;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000474 RID: 1140
	public class Default : DefaultPageBase
	{
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x0008BD9C File Offset: 0x00089F9C
		public override string TenantId
		{
			get
			{
				UserContext userContext = UserContextManager.GetUserContext(this.Context);
				if (userContext != null && userContext.ExchangePrincipal != null && userContext.ExchangePrincipal.MailboxInfo.OrganizationId != null)
				{
					return userContext.ExchangePrincipal.MailboxInfo.OrganizationId.ToExternalDirectoryOrganizationId();
				}
				Guid guid = new Guid(new byte[16]);
				return guid.ToString();
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x0008BE0C File Offset: 0x0008A00C
		public override string MdbGuid
		{
			get
			{
				UserContext userContext = UserContextManager.GetUserContext(this.Context);
				if (userContext != null && userContext.ExchangePrincipal != null && userContext.ExchangePrincipal.MailboxInfo.MailboxDatabase != null)
				{
					return userContext.ExchangePrincipal.MailboxInfo.GetDatabaseGuid().ToString();
				}
				Guid guid = new Guid(new byte[16]);
				return guid.ToString();
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x0008BE7C File Offset: 0x0008A07C
		// (set) Token: 0x0600269D RID: 9885 RVA: 0x0008BEAF File Offset: 0x0008A0AF
		public override string VersionString
		{
			get
			{
				if (this.buildVersion == null)
				{
					UserContext userContext = UserContextManager.GetUserContext(this.Context);
					this.buildVersion = userContext.CurrentOwaVersion;
				}
				return this.buildVersion;
			}
			set
			{
				this.buildVersion = value;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x0008BEB8 File Offset: 0x0008A0B8
		// (set) Token: 0x0600269F RID: 9887 RVA: 0x0008BEDB File Offset: 0x0008A0DB
		public static string MapControlUrl
		{
			get
			{
				if (Default.mapControlUrlForTesting != null)
				{
					return Default.mapControlUrlForTesting;
				}
				return PlacesConfigurationCache.GetMapControlUrl(Thread.CurrentThread.CurrentUICulture.Name);
			}
			set
			{
				Default.mapControlUrlForTesting = value;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x0008BEE3 File Offset: 0x0008A0E3
		public string OwaTitle
		{
			get
			{
				return AntiXssEncoder.HtmlEncode(Strings.OwaTitle, false);
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x0008BEF0 File Offset: 0x0008A0F0
		public override string SlabsManifest
		{
			get
			{
				SlabManifestType slabManifestType = DefaultPageBase.IsPalEnabled(this.Context) ? SlabManifestType.Pal : SlabManifestType.Standard;
				UserContext userContext = UserContextManager.GetUserContext(HttpContext.Current, true);
				string[] features = (userContext == null) ? new string[0] : userContext.FeaturesManager.GetClientEnabledFeatures();
				return SlabManifestCollectionFactory.GetInstance(this.VersionString).GetSlabsJson(slabManifestType, features, this.UserAgent.Layout);
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x0008BF58 File Offset: 0x0008A158
		public override string IsChangeLayoutFeatureEnabled
		{
			get
			{
				if (this.isChangeLayoutFeatureEnabled == null)
				{
					this.isChangeLayoutFeatureEnabled = new bool?(UserContextManager.GetUserContext(HttpContext.Current).FeaturesManager.ClientServerSettings.ChangeLayout.Enabled);
				}
				return this.isChangeLayoutFeatureEnabled.Value.ToString().ToLowerInvariant();
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x0008BFB8 File Offset: 0x0008A1B8
		public bool IsEdgeModeEnabled
		{
			get
			{
				if (this.isEdgeModeEnabled == null)
				{
					this.isEdgeModeEnabled = new bool?(UserContextManager.GetUserContext(HttpContext.Current).FeaturesManager.ServerSettings.OWAEdgeMode.Enabled);
				}
				return this.isEdgeModeEnabled.Value;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x0008C00C File Offset: 0x0008A20C
		public bool IsO365HeaderEnabled
		{
			get
			{
				if (this.isO365HeaderEnabled == null)
				{
					this.isO365HeaderEnabled = new bool?(UserContextManager.GetUserContext(HttpContext.Current).FeaturesManager.ClientServerSettings.O365Header.Enabled);
				}
				return this.isO365HeaderEnabled.Value;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x0008C060 File Offset: 0x0008A260
		public bool IsO365G2HeaderEnabled
		{
			get
			{
				if (this.isO365G2HeaderEnabled == null)
				{
					this.isO365G2HeaderEnabled = new bool?(UserContextManager.GetUserContext(HttpContext.Current).FeaturesManager.ClientServerSettings.O365G2Header.Enabled);
				}
				return this.isO365G2HeaderEnabled.Value;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x0008C0B1 File Offset: 0x0008A2B1
		public bool ShouldEmitHeaderImageClass
		{
			get
			{
				if (this.shouldEmitHeaderImageClass == null)
				{
					this.shouldEmitHeaderImageClass = new bool?(ThemeManagerFactory.GetInstance(this.VersionString).ShouldSkipThemeFolder);
				}
				return this.shouldEmitHeaderImageClass.Value;
			}
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x0008C0E8 File Offset: 0x0008A2E8
		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);
			string text = base.IsAppCacheEnabledClient ? "1" : "0";
			base.Response.AppendToLog("&appcache=" + text);
			RequestDetailsLogger getRequestDetailsLogger = OwaApplication.GetRequestDetailsLogger;
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(getRequestDetailsLogger, OwaServerLogger.LoggerData.AppCache, text);
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x0008C13B File Offset: 0x0008A33B
		protected bool ForcePLTOnVersionChange
		{
			get
			{
				return !this.IsDatacenterNonDedicated || !this.CdnEnabled;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060026A9 RID: 9897 RVA: 0x0008C150 File Offset: 0x0008A350
		protected bool CheckCdnVersionAvailability
		{
			get
			{
				return base.IsAppCacheEnabledClient && this.IsDatacenterNonDedicated && this.CdnEnabled && !base.IsClientInOfflineMode;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x0008C178 File Offset: 0x0008A378
		protected virtual bool IsDatacenterNonDedicated
		{
			get
			{
				return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060026AB RID: 9899 RVA: 0x0008C1A3 File Offset: 0x0008A3A3
		protected virtual bool CdnEnabled
		{
			get
			{
				if (Default.cdnEnabled == null)
				{
					Default.cdnEnabled = new bool?(!string.IsNullOrEmpty(Globals.ContentDeliveryNetworkEndpoint));
				}
				return Default.cdnEnabled.Value;
			}
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0008C1D2 File Offset: 0x0008A3D2
		public override string FormatURIForCDN(string relativeUri, bool isBootResourceUri)
		{
			if (this.IsURIAppcacheable(isBootResourceUri) || this.UserAgent.IsBrowserIE8())
			{
				return relativeUri;
			}
			return Globals.FormatURIForCDN(relativeUri);
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x0008C1F2 File Offset: 0x0008A3F2
		public override string GetCdnEndpointForResources(bool bootResources)
		{
			if (this.IsURIAppcacheable(bootResources) || this.UserAgent.IsBrowserIE8())
			{
				return string.Empty;
			}
			return Globals.ContentDeliveryNetworkEndpoint ?? string.Empty;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0008C21E File Offset: 0x0008A41E
		protected bool IsURIAppcacheable(bool isBootResourceFolder)
		{
			return (base.IsAppCacheEnabledClient && isBootResourceFolder) || base.IsOfflineAppCacheEnabledClient;
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0008C236 File Offset: 0x0008A436
		protected override string GetThemeFolder()
		{
			return ThemeManagerFactory.GetInstance(this.VersionString).GetThemeFolderName(this.UserAgent, this.Context);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x0008C254 File Offset: 0x0008A454
		protected override bool ShouldSkipThemeFolder()
		{
			return ThemeManagerFactory.GetInstance(this.VersionString).ShouldSkipThemeFolder;
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x0008C268 File Offset: 0x0008A468
		protected override string GetFeaturesSupportedJsonArray(FlightedFeatureScope scope)
		{
			UserContext userContext = UserContextManager.GetUserContext(HttpContext.Current);
			return UserResourcesFinder.GetEnabledFlightedFeaturesJsonArray(this.ManifestType, userContext, scope);
		}

		// Token: 0x04001688 RID: 5768
		private const string IsAppCacheInstallationRequestLogKey = "&appcache=";

		// Token: 0x04001689 RID: 5769
		private static bool? cdnEnabled = null;

		// Token: 0x0400168A RID: 5770
		private static string mapControlUrlForTesting = null;

		// Token: 0x0400168B RID: 5771
		private string buildVersion;

		// Token: 0x0400168C RID: 5772
		private bool? isChangeLayoutFeatureEnabled;

		// Token: 0x0400168D RID: 5773
		private bool? isEdgeModeEnabled;

		// Token: 0x0400168E RID: 5774
		private bool? isO365HeaderEnabled;

		// Token: 0x0400168F RID: 5775
		private bool? isO365G2HeaderEnabled;

		// Token: 0x04001690 RID: 5776
		private bool? shouldEmitHeaderImageClass;
	}
}
