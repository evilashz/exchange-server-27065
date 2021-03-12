using System;
using System.Configuration;
using System.Web.Configuration;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.PowerShell.RbacHostingTools.Asp.Net
{
	// Token: 0x02000009 RID: 9
	internal class RbacSection : ConfigurationSection
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003286 File Offset: 0x00001486
		public static RbacSection Instance
		{
			get
			{
				return RbacSection.instance;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003290 File Offset: 0x00001490
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000032C1 File Offset: 0x000014C1
		[ConfigurationProperty("rbacPrincipalMaximumAge", IsRequired = true, DefaultValue = "00:15:00")]
		public TimeSpan RbacPrincipalMaximumAge
		{
			get
			{
				TimeSpan maximumAgeLimit = ExchangeExpiringRunspaceConfiguration.GetMaximumAgeLimit(ExpirationLimit.RunspaceRefresh);
				TimeSpan timeSpan = (TimeSpan)base["rbacPrincipalMaximumAge"];
				if (!(timeSpan < maximumAgeLimit))
				{
					return maximumAgeLimit;
				}
				return timeSpan;
			}
			set
			{
				base["rbacPrincipalMaximumAge"] = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000032D4 File Offset: 0x000014D4
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000032E6 File Offset: 0x000014E6
		[ConfigurationProperty("rbacRunspaceSlidingExpiration", IsRequired = true, DefaultValue = "00:05:00")]
		public TimeSpan RbacRunspaceSlidingExpiration
		{
			get
			{
				return (TimeSpan)base["rbacRunspaceSlidingExpiration"];
			}
			set
			{
				base["rbacRunspaceSlidingExpiration"] = value;
			}
		}

		// Token: 0x0400001C RID: 28
		private static RbacSection instance = ((RbacSection)WebConfigurationManager.GetSection("rbacConfig")) ?? new RbacSection();
	}
}
