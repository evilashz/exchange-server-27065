using System;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Data.Directory.IsMemberOfProvider
{
	// Token: 0x020001CA RID: 458
	internal class IsMemberOfResolverPerformanceCounters
	{
		// Token: 0x0600129E RID: 4766 RVA: 0x000599D7 File Offset: 0x00057BD7
		public IsMemberOfResolverPerformanceCounters(string componentName)
		{
			this.perfCountersInstance = IsMemberOfResolverPerfCounters.GetInstance(componentName);
			this.Reset();
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x000599F1 File Offset: 0x00057BF1
		protected IsMemberOfResolverPerformanceCounters()
		{
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000599F9 File Offset: 0x00057BF9
		public virtual ICachePerformanceCounters GetResolvedGroupsCacheCounters(long maxCacheSizeInBytes)
		{
			return new CachePerformanceCounters(this.perfCountersInstance.ResolvedGroupsHitCount, this.perfCountersInstance.ResolvedGroupsMissCount, this.perfCountersInstance.ResolvedGroupsCacheSize, this.perfCountersInstance.ResolvedGroupsCacheSizePercentage, maxCacheSizeInBytes);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00059A2D File Offset: 0x00057C2D
		public virtual ICachePerformanceCounters GetExpandedGroupsCacheCounters(long maxCacheSizeInBytes)
		{
			return new CachePerformanceCounters(this.perfCountersInstance.ExpandedGroupsHitCount, this.perfCountersInstance.ExpandedGroupsMissCount, this.perfCountersInstance.ExpandedGroupsCacheSize, this.perfCountersInstance.ExpandedGroupsCacheSizePercentage, maxCacheSizeInBytes);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00059A64 File Offset: 0x00057C64
		public virtual void Reset()
		{
			this.perfCountersInstance.ResolvedGroupsHitCount.RawValue = 0L;
			this.perfCountersInstance.ResolvedGroupsMissCount.RawValue = 0L;
			this.perfCountersInstance.ResolvedGroupsCacheSize.RawValue = 0L;
			this.perfCountersInstance.ResolvedGroupsCacheSizePercentage.RawValue = 0L;
			this.perfCountersInstance.ExpandedGroupsHitCount.RawValue = 0L;
			this.perfCountersInstance.ExpandedGroupsMissCount.RawValue = 0L;
			this.perfCountersInstance.ExpandedGroupsCacheSize.RawValue = 0L;
			this.perfCountersInstance.ExpandedGroupsCacheSizePercentage.RawValue = 0L;
			this.perfCountersInstance.LdapQueries.RawValue = 0L;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00059B13 File Offset: 0x00057D13
		public virtual void IncrementLDAPQueryCount(int count)
		{
			this.perfCountersInstance.LdapQueries.IncrementBy((long)count);
		}

		// Token: 0x04000AB7 RID: 2743
		private IsMemberOfResolverPerfCountersInstance perfCountersInstance;
	}
}
