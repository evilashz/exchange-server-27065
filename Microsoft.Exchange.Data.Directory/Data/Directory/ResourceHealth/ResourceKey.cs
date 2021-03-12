using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009F1 RID: 2545
	internal abstract class ResourceKey
	{
		// Token: 0x0600761C RID: 30236 RVA: 0x001855A1 File Offset: 0x001837A1
		public ResourceKey(ResourceMetricType metric, string id)
		{
			this.MetricType = metric;
			this.Id = id;
		}

		// Token: 0x17002A4D RID: 10829
		// (get) Token: 0x0600761D RID: 30237 RVA: 0x001855B7 File Offset: 0x001837B7
		// (set) Token: 0x0600761E RID: 30238 RVA: 0x001855BF File Offset: 0x001837BF
		public ResourceMetricType MetricType { get; private set; }

		// Token: 0x17002A4E RID: 10830
		// (get) Token: 0x0600761F RID: 30239 RVA: 0x001855C8 File Offset: 0x001837C8
		// (set) Token: 0x06007620 RID: 30240 RVA: 0x001855D0 File Offset: 0x001837D0
		public string Id { get; private set; }

		// Token: 0x06007621 RID: 30241 RVA: 0x001855DC File Offset: 0x001837DC
		public override bool Equals(object obj)
		{
			ResourceKey resourceKey = obj as ResourceKey;
			return resourceKey != null && this.MetricType == resourceKey.MetricType && string.Equals(this.Id, resourceKey.Id, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06007622 RID: 30242 RVA: 0x00185615 File Offset: 0x00183815
		public override int GetHashCode()
		{
			return (int)(this.MetricType ^ (ResourceMetricType)((this.Id != null) ? this.Id.GetHashCode() : 0));
		}

		// Token: 0x06007623 RID: 30243 RVA: 0x00185634 File Offset: 0x00183834
		public override string ToString()
		{
			if (this.Id != null)
			{
				return string.Format("{0}({1})", this.MetricType, this.Id);
			}
			return this.MetricType.ToString();
		}

		// Token: 0x06007624 RID: 30244
		protected internal abstract CacheableResourceHealthMonitor CreateMonitor();
	}
}
