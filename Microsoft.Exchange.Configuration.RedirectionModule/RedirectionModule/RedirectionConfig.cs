using System;
using System.Configuration;

namespace Microsoft.Exchange.Configuration.RedirectionModule
{
	// Token: 0x02000007 RID: 7
	internal static class RedirectionConfig
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00003324 File Offset: 0x00001524
		static RedirectionConfig()
		{
			RedirectionConfig.podRedirectTemplate = ConfigurationManager.AppSettings["PodRedirectTemplate"];
			int.TryParse(ConfigurationManager.AppSettings["PodSiteStartRange"], out RedirectionConfig.podSiteStartRange);
			int.TryParse(ConfigurationManager.AppSettings["PodSiteEndRange"], out RedirectionConfig.podSiteEndRange);
			RedirectionConfig.siteRedirectTemplate = ConfigurationManager.AppSettings["SiteRedirectTemplate"];
			if (!int.TryParse(ConfigurationManager.AppSettings["RedirectionTenantSiteCacheExpirationInHours"], out RedirectionConfig.currentSiteTenantsCacheExpirationInHours))
			{
				RedirectionConfig.currentSiteTenantsCacheExpirationInHours = 6;
			}
			if (ConfigurationManager.AppSettings["SessionKeyCreation"] != null)
			{
				try
				{
					RedirectionConfig.sessionKeyCreationStatus = (RedirectionConfig.SessionKeyCreation)Enum.Parse(typeof(RedirectionConfig.SessionKeyCreation), ConfigurationManager.AppSettings["SessionKeyCreation"], true);
				}
				catch (ArgumentException)
				{
					RedirectionConfig.sessionKeyCreationStatus = RedirectionConfig.SessionKeyCreation.Partner;
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000343C File Offset: 0x0000163C
		internal static string[] RedirectionUriFilterProperties
		{
			get
			{
				return RedirectionConfig.redirectionUriFilterProperties;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00003443 File Offset: 0x00001643
		internal static int PodSiteStartRange
		{
			get
			{
				return RedirectionConfig.podSiteStartRange;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000344A File Offset: 0x0000164A
		internal static int PodSiteEndRange
		{
			get
			{
				return RedirectionConfig.podSiteEndRange;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00003451 File Offset: 0x00001651
		internal static string PodRedirectTemplate
		{
			get
			{
				return RedirectionConfig.podRedirectTemplate;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003458 File Offset: 0x00001658
		internal static string SiteRedirectTemplate
		{
			get
			{
				return RedirectionConfig.siteRedirectTemplate;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000345F File Offset: 0x0000165F
		internal static int CurrentSiteTenantsCacheExpirationInHours
		{
			get
			{
				return RedirectionConfig.currentSiteTenantsCacheExpirationInHours;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003466 File Offset: 0x00001666
		internal static RedirectionConfig.SessionKeyCreation SessionKeyCreationStatus
		{
			get
			{
				return RedirectionConfig.sessionKeyCreationStatus;
			}
		}

		// Token: 0x0400001D RID: 29
		internal const int DefaultCurrentSiteTenantCacheExpirationInHours = 6;

		// Token: 0x0400001E RID: 30
		internal const int CurrentSiteTenantsCacheMaximumSize = 10000;

		// Token: 0x0400001F RID: 31
		private static string[] redirectionUriFilterProperties = new string[]
		{
			"sessionId"
		};

		// Token: 0x04000020 RID: 32
		private static int podSiteStartRange = 0;

		// Token: 0x04000021 RID: 33
		private static int podSiteEndRange = 0;

		// Token: 0x04000022 RID: 34
		private static string podRedirectTemplate = null;

		// Token: 0x04000023 RID: 35
		private static string siteRedirectTemplate = null;

		// Token: 0x04000024 RID: 36
		private static int currentSiteTenantsCacheExpirationInHours = 0;

		// Token: 0x04000025 RID: 37
		private static RedirectionConfig.SessionKeyCreation sessionKeyCreationStatus = RedirectionConfig.SessionKeyCreation.Partner;

		// Token: 0x02000008 RID: 8
		public enum SessionKeyCreation
		{
			// Token: 0x04000027 RID: 39
			Disable,
			// Token: 0x04000028 RID: 40
			Partner,
			// Token: 0x04000029 RID: 41
			Enable
		}
	}
}
