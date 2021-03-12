using System;
using System.Text;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000055 RID: 85
	public struct StorePerformanceCounters
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000B68F File Offset: 0x0000988F
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000B697 File Offset: 0x00009897
		public long ElapsedMilliseconds { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000B6A0 File Offset: 0x000098A0
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000B6A8 File Offset: 0x000098A8
		public double Cpu { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000B6B1 File Offset: 0x000098B1
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000B6B9 File Offset: 0x000098B9
		public double RpcLatency { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000B6C2 File Offset: 0x000098C2
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000B6CA File Offset: 0x000098CA
		public int RpcCount { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000B6D3 File Offset: 0x000098D3
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000B6DB File Offset: 0x000098DB
		public double RpcLatencyOnStore { get; set; }

		// Token: 0x06000217 RID: 535 RVA: 0x0000B6E4 File Offset: 0x000098E4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.AppendFormat("elapsed={0}ms\n", this.ElapsedMilliseconds);
			stringBuilder.AppendFormat("cpu={0}ms\n", this.Cpu);
			stringBuilder.AppendFormat("rpcLatency={0}ms\n", this.RpcLatency);
			stringBuilder.AppendFormat("rpcCount={0}\n", this.RpcCount);
			stringBuilder.AppendFormat("rpcLatencyOnStore={0}ms\n", this.RpcLatencyOnStore);
			return stringBuilder.ToString();
		}
	}
}
