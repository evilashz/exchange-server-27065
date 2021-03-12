using System;

namespace Microsoft.Exchange.Transport.Scheduler.Contracts
{
	// Token: 0x0200000B RID: 11
	internal class SchedulerDiagnosticsInfo
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000020D8 File Offset: 0x000002D8
		public long Received { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000020E1 File Offset: 0x000002E1
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000020E9 File Offset: 0x000002E9
		public long Dispatched { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000020F2 File Offset: 0x000002F2
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000020FA File Offset: 0x000002FA
		public long Throttled { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002103 File Offset: 0x00000303
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000210B File Offset: 0x0000030B
		public long Processed { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002114 File Offset: 0x00000314
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000211C File Offset: 0x0000031C
		public long ReceiveRate { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002125 File Offset: 0x00000325
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000212D File Offset: 0x0000032D
		public long DispatchRate { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002136 File Offset: 0x00000336
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000213E File Offset: 0x0000033E
		public long ThrottleRate { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002147 File Offset: 0x00000347
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000214F File Offset: 0x0000034F
		public long ProcessRate { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002158 File Offset: 0x00000358
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002160 File Offset: 0x00000360
		public long TotalScopedQueues { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002169 File Offset: 0x00000369
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002171 File Offset: 0x00000371
		public long ScopedQueuesCreatedRate { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000217A File Offset: 0x0000037A
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002182 File Offset: 0x00000382
		public long ScopedQueuesDestroyedRate { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000218B File Offset: 0x0000038B
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002193 File Offset: 0x00000393
		public DateTime OldestScopedQueueCreateTime { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000219C File Offset: 0x0000039C
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000021A4 File Offset: 0x000003A4
		public DateTime OldestLockTimeStamp { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000021AD File Offset: 0x000003AD
		public double DispatchingVelocity
		{
			get
			{
				return (double)(this.DispatchRate - this.ReceiveRate) * 1.0 / 60.0;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000021D1 File Offset: 0x000003D1
		public double ProcessingVelocity
		{
			get
			{
				return (double)(this.ProcessRate - this.ReceiveRate) * 1.0 / 60.0;
			}
		}
	}
}
