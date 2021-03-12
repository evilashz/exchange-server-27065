using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000007 RID: 7
	internal class CountedEntityWrapper<TEntityType, TCountType> : ICountedEntityWrapper<TEntityType, TCountType> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002FC2 File Offset: 0x000011C2
		public CountedEntityWrapper(ICountedEntity<TEntityType> entity, ICountTracker<TEntityType, TCountType> tracker)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("tracker", tracker);
			this.Entity = entity;
			this.tracker = tracker;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002FEE File Offset: 0x000011EE
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002FF6 File Offset: 0x000011F6
		public ICountedEntity<TEntityType> Entity { get; private set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002FFF File Offset: 0x000011FF
		public ICountedConfig GetConfig(TCountType measure)
		{
			return this.tracker.GetConfig(this.Entity, measure);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003013 File Offset: 0x00001213
		public void AddUsage(TCountType measure, ICountedConfig countConfig, int increment)
		{
			this.tracker.AddUsage(this.Entity, measure, countConfig, (long)increment);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000302A File Offset: 0x0000122A
		public Task AddUsageAsync(TCountType measure, ICountedConfig countConfig, int increment)
		{
			return this.tracker.AddUsageAsync(this.Entity, measure, countConfig, (long)increment);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003041 File Offset: 0x00001241
		public bool TrySetUsage(TCountType measure, int value)
		{
			return this.tracker.TrySetUsage(this.Entity, measure, (long)value);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003057 File Offset: 0x00001257
		public Task<bool> SetUsageAsync(TCountType measure, int value)
		{
			return this.tracker.SetUsageAsync(this.Entity, measure, (long)value);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000306D File Offset: 0x0000126D
		public ICount<TEntityType, TCountType> GetUsage(TCountType measure)
		{
			return this.tracker.GetUsage(this.Entity, measure);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003081 File Offset: 0x00001281
		public Task<ICount<TEntityType, TCountType>> GetUsageAsync(TCountType measure)
		{
			return this.tracker.GetUsageAsync(this.Entity, measure);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003095 File Offset: 0x00001295
		public IDictionary<TCountType, ICount<TEntityType, TCountType>> GetUsage(TCountType[] measures)
		{
			return this.tracker.GetUsage(this.Entity, measures);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000030A9 File Offset: 0x000012A9
		public Task<IDictionary<TCountType, ICount<TEntityType, TCountType>>> GetUsageAsync(TCountType[] measures)
		{
			return this.tracker.GetUsageAsync(this.Entity, measures);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000030BD File Offset: 0x000012BD
		public IEnumerable<ICount<TEntityType, TCountType>> GetAllUsages()
		{
			return this.tracker.GetAllUsages(this.Entity);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000030D0 File Offset: 0x000012D0
		public Task<IEnumerable<ICount<TEntityType, TCountType>>> GetAllUsagesAsync()
		{
			return this.tracker.GetAllUsagesAsync(this.Entity);
		}

		// Token: 0x04000021 RID: 33
		private readonly ICountTracker<TEntityType, TCountType> tracker;
	}
}
