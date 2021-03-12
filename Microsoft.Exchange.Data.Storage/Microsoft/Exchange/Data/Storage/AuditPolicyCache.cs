using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F25 RID: 3877
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AuditPolicyCache
	{
		// Token: 0x06008551 RID: 34129 RVA: 0x002475C7 File Offset: 0x002457C7
		public AuditPolicyCache(int maxCount, double entryExpirationInSeconds, double timeForCacheEntryUpdateInSeconds)
		{
			this.EntryExpirationInSeconds = ((entryExpirationInSeconds < 1.0) ? 1.0 : entryExpirationInSeconds);
			this.Cache = new UpdatableCache<OrganizationId, AuditPolicyCacheEntry>(maxCount, timeForCacheEntryUpdateInSeconds);
		}

		// Token: 0x06008552 RID: 34130 RVA: 0x002475FC File Offset: 0x002457FC
		public bool GetAuditPolicy(OrganizationId orgId, out AuditPolicyCacheEntry cacheEntry, out bool expired, DateTime? currentUtcTime = null)
		{
			return this.Cache.GetCacheEntry(orgId, out cacheEntry, out expired, currentUtcTime ?? DateTime.UtcNow);
		}

		// Token: 0x06008553 RID: 34131 RVA: 0x00247634 File Offset: 0x00245834
		internal bool UpdateAuditPolicy(OrganizationId orgId, ref AuditPolicyCacheEntry cacheEntry, DateTime? expirationUtc = null)
		{
			return this.Cache.UpdateCacheEntry(orgId, ref cacheEntry, expirationUtc ?? DateTime.UtcNow.AddSeconds(this.EntryExpirationInSeconds));
		}

		// Token: 0x04005952 RID: 22866
		private const double CacheEntryExpirationInSeconds = 900.0;

		// Token: 0x04005953 RID: 22867
		private const double TimeForCacheEntryUpdateInSeconds = 60.0;

		// Token: 0x04005954 RID: 22868
		private readonly UpdatableCache<OrganizationId, AuditPolicyCacheEntry> Cache;

		// Token: 0x04005955 RID: 22869
		private readonly double EntryExpirationInSeconds;

		// Token: 0x04005956 RID: 22870
		public static readonly AuditPolicyCache Instance = new AuditPolicyCache(10007, 900.0, 60.0);
	}
}
