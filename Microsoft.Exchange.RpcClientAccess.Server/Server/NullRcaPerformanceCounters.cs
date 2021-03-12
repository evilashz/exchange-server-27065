using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200002C RID: 44
	internal class NullRcaPerformanceCounters : IRcaPerformanceCounters
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00007376 File Offset: 0x00005576
		public IExPerformanceCounter ActiveUserCount
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000737D File Offset: 0x0000557D
		public IExPerformanceCounter AveragedLatency
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00007384 File Offset: 0x00005584
		public IExPerformanceCounter BytesRead
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000738B File Offset: 0x0000558B
		public IExPerformanceCounter BytesWritten
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00007392 File Offset: 0x00005592
		public IExPerformanceCounter ClientBackgroundCallsFailed
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00007399 File Offset: 0x00005599
		public IExPerformanceCounter ClientBackgroundCallsSucceeded
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000073A0 File Offset: 0x000055A0
		public IExPerformanceCounter ClientCallsAttempted
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000073A7 File Offset: 0x000055A7
		public IExPerformanceCounter ClientCallsFailed
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000073AE File Offset: 0x000055AE
		public IExPerformanceCounter ClientCallsSlow1
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000073B5 File Offset: 0x000055B5
		public IExPerformanceCounter ClientCallsSlow2
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000073BC File Offset: 0x000055BC
		public IExPerformanceCounter ClientCallsSlow3
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000073C3 File Offset: 0x000055C3
		public IExPerformanceCounter ClientCallsSucceeded
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000073CA File Offset: 0x000055CA
		public IExPerformanceCounter ClientForegroundCallsFailed
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000073D1 File Offset: 0x000055D1
		public IExPerformanceCounter ClientForegroundCallsSucceeded
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000073D8 File Offset: 0x000055D8
		public IExPerformanceCounter ConnectionCount
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000073DF File Offset: 0x000055DF
		public IExPerformanceCounter DispatchTaskActiveThreads
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000073E6 File Offset: 0x000055E6
		public IExPerformanceCounter DispatchTaskOperationsRate
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000073ED File Offset: 0x000055ED
		public IExPerformanceCounter DispatchTaskQueueLength
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000073F4 File Offset: 0x000055F4
		public IExPerformanceCounter DispatchTaskThreads
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000073FB File Offset: 0x000055FB
		public IExPerformanceCounter OperationsRate
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00007402 File Offset: 0x00005602
		public IExPerformanceCounter PacketsRate
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00007409 File Offset: 0x00005609
		public IExPerformanceCounter Requests
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00007410 File Offset: 0x00005610
		public IExPerformanceCounter UncompressedBytesRead
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00007417 File Offset: 0x00005617
		public IExPerformanceCounter UncompressedBytesWritten
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000741E File Offset: 0x0000561E
		public IExPerformanceCounter UserCount
		{
			get
			{
				return NullPerformanceCounter.Instance;
			}
		}
	}
}
