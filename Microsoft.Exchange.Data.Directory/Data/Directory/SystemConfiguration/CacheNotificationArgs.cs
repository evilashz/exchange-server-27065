using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000631 RID: 1585
	internal class CacheNotificationArgs
	{
		// Token: 0x06004B18 RID: 19224 RVA: 0x00115099 File Offset: 0x00113299
		public CacheNotificationArgs(CacheNotificationHandler cacheNotificationHandler, OrganizationId organizationId)
		{
			this.cacheNotificationHandler = cacheNotificationHandler;
			this.organizationId = organizationId;
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x001150AF File Offset: 0x001132AF
		public CacheNotificationHandler CacheNotificationHandler
		{
			get
			{
				return this.cacheNotificationHandler;
			}
		}

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x06004B1A RID: 19226 RVA: 0x001150B7 File Offset: 0x001132B7
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x0400339E RID: 13214
		private CacheNotificationHandler cacheNotificationHandler;

		// Token: 0x0400339F RID: 13215
		private OrganizationId organizationId;
	}
}
