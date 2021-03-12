using System;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa2.Server.Core;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x020004A2 RID: 1186
	internal class UserSpecificResourceInjector : UserSpecificResourceInjectorBase
	{
		// Token: 0x0600287F RID: 10367 RVA: 0x00095FB6 File Offset: 0x000941B6
		protected override IPageContext GetPageContext(HttpContext context, string owaVersion)
		{
			return new UserSpecificResourceInjector.PageContext(context, owaVersion);
		}

		// Token: 0x020004A3 RID: 1187
		public class PageContext : IPageContext
		{
			// Token: 0x06002881 RID: 10369 RVA: 0x00095FC8 File Offset: 0x000941C8
			public PageContext(HttpContext context, string owaVersion)
			{
				this.UserAgent = OwaUserAgentUtilities.CreateUserAgentWithLayoutOverride(context);
				this.IsAppCacheEnabledClient = (context == null || context.Request.QueryString["appcacheclient"] == "1");
				this.ManifestType = (DefaultPageBase.IsPalEnabled(context, this.UserAgent.RawString) ? SlabManifestType.Pal : SlabManifestType.Standard);
				this.Theme = ThemeManagerFactory.GetInstance(owaVersion).GetThemeFolderName(this.UserAgent, context);
			}

			// Token: 0x17000AB4 RID: 2740
			// (get) Token: 0x06002882 RID: 10370 RVA: 0x0009604F File Offset: 0x0009424F
			// (set) Token: 0x06002883 RID: 10371 RVA: 0x00096057 File Offset: 0x00094257
			public UserAgent UserAgent { get; private set; }

			// Token: 0x17000AB5 RID: 2741
			// (get) Token: 0x06002884 RID: 10372 RVA: 0x00096060 File Offset: 0x00094260
			// (set) Token: 0x06002885 RID: 10373 RVA: 0x00096068 File Offset: 0x00094268
			public string Theme { get; private set; }

			// Token: 0x17000AB6 RID: 2742
			// (get) Token: 0x06002886 RID: 10374 RVA: 0x00096071 File Offset: 0x00094271
			// (set) Token: 0x06002887 RID: 10375 RVA: 0x00096079 File Offset: 0x00094279
			public bool IsAppCacheEnabledClient { get; private set; }

			// Token: 0x17000AB7 RID: 2743
			// (get) Token: 0x06002888 RID: 10376 RVA: 0x00096082 File Offset: 0x00094282
			// (set) Token: 0x06002889 RID: 10377 RVA: 0x0009608A File Offset: 0x0009428A
			public SlabManifestType ManifestType { get; private set; }

			// Token: 0x0600288A RID: 10378 RVA: 0x00096093 File Offset: 0x00094293
			public string FormatURIForCDN(string relativeUri, bool isBootResourceUri)
			{
				if (!this.IsAppCacheEnabledClient)
				{
					return Globals.FormatURIForCDN(relativeUri);
				}
				return relativeUri;
			}
		}
	}
}
