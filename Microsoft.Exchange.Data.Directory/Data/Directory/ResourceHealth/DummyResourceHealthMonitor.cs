using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009F5 RID: 2549
	internal class DummyResourceHealthMonitor : CacheableResourceHealthMonitor
	{
		// Token: 0x0600764D RID: 30285 RVA: 0x001858A5 File Offset: 0x00183AA5
		internal DummyResourceHealthMonitor(ResourceKey key) : base(key)
		{
		}

		// Token: 0x17002A5E RID: 10846
		// (get) Token: 0x0600764E RID: 30286 RVA: 0x001858AE File Offset: 0x00183AAE
		// (set) Token: 0x0600764F RID: 30287 RVA: 0x001858B5 File Offset: 0x00183AB5
		public override DateTime LastUpdateUtc
		{
			get
			{
				return TimeProvider.UtcNow;
			}
			protected internal set
			{
			}
		}

		// Token: 0x06007650 RID: 30288 RVA: 0x001858B7 File Offset: 0x00183AB7
		public override ResourceHealthMonitorWrapper CreateWrapper()
		{
			return new DummyResourceHealthMonitorWrapper(this);
		}

		// Token: 0x17002A5F RID: 10847
		// (get) Token: 0x06007651 RID: 30289 RVA: 0x001858BF File Offset: 0x00183ABF
		protected override int InternalMetricValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06007652 RID: 30290 RVA: 0x001858C2 File Offset: 0x00183AC2
		public override ResourceLoad GetResourceLoad(WorkloadClassification classification, bool raw = false, object optionalData = null)
		{
			return ResourceLoad.Zero;
		}
	}
}
