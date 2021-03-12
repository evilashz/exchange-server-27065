using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009F2 RID: 2546
	internal sealed class ADResourceKey : ResourceKey
	{
		// Token: 0x06007625 RID: 30245 RVA: 0x0018566A File Offset: 0x0018386A
		private ADResourceKey() : base(ResourceMetricType.ActiveDirectoryReplicationLatency, null)
		{
		}

		// Token: 0x17002A4F RID: 10831
		// (get) Token: 0x06007626 RID: 30246 RVA: 0x00185674 File Offset: 0x00183874
		public static ADResourceKey Key
		{
			get
			{
				return ADResourceKey.key;
			}
		}

		// Token: 0x06007627 RID: 30247 RVA: 0x0018567B File Offset: 0x0018387B
		protected internal override CacheableResourceHealthMonitor CreateMonitor()
		{
			return new ADResourceHealthMonitor(this);
		}

		// Token: 0x04004BAD RID: 19373
		private static ADResourceKey key = new ADResourceKey();
	}
}
