using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F22 RID: 3874
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpdatableCacheEntry<T> where T : IUpdatableItem
	{
		// Token: 0x17002345 RID: 9029
		// (get) Token: 0x0600853A RID: 34106 RVA: 0x002471CC File Offset: 0x002453CC
		// (set) Token: 0x0600853B RID: 34107 RVA: 0x002471D4 File Offset: 0x002453D4
		public DateTime UtcOfExpiration { get; private set; }

		// Token: 0x17002346 RID: 9030
		// (get) Token: 0x0600853C RID: 34108 RVA: 0x002471DD File Offset: 0x002453DD
		// (set) Token: 0x0600853D RID: 34109 RVA: 0x002471E5 File Offset: 0x002453E5
		public DateTime UtcOfUpdaterFinish { get; private set; }

		// Token: 0x17002347 RID: 9031
		// (get) Token: 0x0600853E RID: 34110 RVA: 0x002471EE File Offset: 0x002453EE
		// (set) Token: 0x0600853F RID: 34111 RVA: 0x002471F6 File Offset: 0x002453F6
		public double TimeForUpdateInSeconds { get; private set; }

		// Token: 0x06008540 RID: 34112 RVA: 0x002471FF File Offset: 0x002453FF
		public UpdatableCacheEntry(T itemToCache, DateTime expirationUtc, double timeForUpdateInSeconds)
		{
			this.CachedItem = itemToCache;
			this.UtcOfExpiration = expirationUtc;
			this.UtcOfUpdaterFinish = DateTime.MinValue;
			this.TimeForUpdateInSeconds = ((timeForUpdateInSeconds < 1.0) ? 1.0 : timeForUpdateInSeconds);
		}

		// Token: 0x06008541 RID: 34113 RVA: 0x00247240 File Offset: 0x00245440
		public bool GetCachedItem(out T cachedItem, DateTime currentUtcTime)
		{
			cachedItem = this.CachedItem;
			if (currentUtcTime < this.UtcOfExpiration)
			{
				return false;
			}
			if (this.UtcOfUpdaterFinish == DateTime.MinValue || currentUtcTime >= this.UtcOfUpdaterFinish)
			{
				this.UtcOfUpdaterFinish = currentUtcTime.AddSeconds(this.TimeForUpdateInSeconds);
				return true;
			}
			return false;
		}

		// Token: 0x06008542 RID: 34114 RVA: 0x0024729F File Offset: 0x0024549F
		public bool UpdateCachedItem(T newItem, DateTime expirationUtc)
		{
			this.UtcOfExpiration = expirationUtc;
			this.UtcOfUpdaterFinish = DateTime.MinValue;
			return this.CachedItem.UpdateWith(newItem);
		}

		// Token: 0x04005946 RID: 22854
		protected T CachedItem;
	}
}
