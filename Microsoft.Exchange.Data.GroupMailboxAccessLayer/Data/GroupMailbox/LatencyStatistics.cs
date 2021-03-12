using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000038 RID: 56
	internal struct LatencyStatistics
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000C200 File Offset: 0x0000A400
		public AggregatedOperationStatistics? ADLatency { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000C209 File Offset: 0x0000A409
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000C211 File Offset: 0x0000A411
		public AggregatedOperationStatistics? RpcLatency { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000C21A File Offset: 0x0000A41A
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000C222 File Offset: 0x0000A422
		public TimeSpan ElapsedTime { get; set; }

		// Token: 0x060001A6 RID: 422 RVA: 0x0000C22C File Offset: 0x0000A42C
		public static LatencyStatistics operator -(LatencyStatistics s1, LatencyStatistics s2)
		{
			return new LatencyStatistics
			{
				ElapsedTime = s1.ElapsedTime - s2.ElapsedTime,
				ADLatency = s1.ADLatency - s2.ADLatency,
				RpcLatency = s1.RpcLatency - s2.RpcLatency
			};
		}
	}
}
