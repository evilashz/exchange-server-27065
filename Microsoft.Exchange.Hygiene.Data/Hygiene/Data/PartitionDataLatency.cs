using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000099 RID: 153
	internal class PartitionDataLatency
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x000115B5 File Offset: 0x0000F7B5
		public PartitionDataLatency(int physicalInstanceId, DataLatencyDetailCollection[] latencyDetail)
		{
			if (latencyDetail == null)
			{
				throw new ArgumentNullException("latencyDetail");
			}
			this.PhysicalInstanceId = physicalInstanceId;
			this.LatencyDetail = latencyDetail;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x000115D9 File Offset: 0x0000F7D9
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x000115E1 File Offset: 0x0000F7E1
		public int PhysicalInstanceId { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000115EA File Offset: 0x0000F7EA
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x000115F2 File Offset: 0x0000F7F2
		public DataLatencyDetailCollection[] LatencyDetail { get; private set; }
	}
}
