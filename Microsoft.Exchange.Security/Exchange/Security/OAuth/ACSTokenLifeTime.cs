using System;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x0200009F RID: 159
	internal sealed class ACSTokenLifeTime
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0002B619 File Offset: 0x00029819
		public static ACSTokenLifeTime Instance
		{
			get
			{
				return ACSTokenLifeTime.instance;
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002B620 File Offset: 0x00029820
		public void SetValue(TimeSpan lifetime)
		{
			if (!this.initialized)
			{
				lock (this.lockObj)
				{
					if (!this.initialized)
					{
						this.acsTokenCacheSlidingExpiration = TimeSpan.FromMinutes(lifetime.TotalMinutes / 3.0);
						this.remaingLifetimeLimitToRefreshACSToken = TimeSpan.FromMinutes(lifetime.TotalMinutes * 2.0 / 3.0);
						this.initialized = true;
					}
				}
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0002B6B4 File Offset: 0x000298B4
		public TimeSpan RemaingLifetimeLimitToRefreshACSToken
		{
			get
			{
				if (!this.initialized)
				{
					return ACSTokenLifeTime.defaultRemaingLifetimeLimitToRefreshACSToken;
				}
				return this.remaingLifetimeLimitToRefreshACSToken;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0002B6CA File Offset: 0x000298CA
		public TimeSpan ACSTokenCacheSlidingExpiration
		{
			get
			{
				if (!this.initialized)
				{
					return ACSTokenLifeTime.defaultACSTokenCacheSlidingExpiration;
				}
				return this.acsTokenCacheSlidingExpiration;
			}
		}

		// Token: 0x04000595 RID: 1429
		private static readonly TimeSpan defaultRemaingLifetimeLimitToRefreshACSToken = TimeSpan.FromHours(16.0);

		// Token: 0x04000596 RID: 1430
		private static readonly TimeSpan defaultACSTokenCacheSlidingExpiration = TimeSpan.FromHours(8.0);

		// Token: 0x04000597 RID: 1431
		private static readonly ACSTokenLifeTime instance = new ACSTokenLifeTime();

		// Token: 0x04000598 RID: 1432
		private object lockObj = new object();

		// Token: 0x04000599 RID: 1433
		private bool initialized;

		// Token: 0x0400059A RID: 1434
		private TimeSpan remaingLifetimeLimitToRefreshACSToken;

		// Token: 0x0400059B RID: 1435
		private TimeSpan acsTokenCacheSlidingExpiration;
	}
}
