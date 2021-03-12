using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200003E RID: 62
	internal class LatencyDistributionRange
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00009370 File Offset: 0x00007570
		internal string Name
		{
			get
			{
				return string.Format("[{0}-{1}]", this.latencyLowRange, this.latencyHighRange);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00009392 File Offset: 0x00007592
		internal int LatencyLowRange
		{
			get
			{
				return this.latencyLowRange;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000939A File Offset: 0x0000759A
		internal int LatencyHighRange
		{
			get
			{
				return this.latencyHighRange;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000093A2 File Offset: 0x000075A2
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000093AA File Offset: 0x000075AA
		internal int Frequency
		{
			get
			{
				return this.frequency;
			}
			set
			{
				this.frequency = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000093B3 File Offset: 0x000075B3
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000093BB File Offset: 0x000075BB
		internal int AverageTotalLatency
		{
			get
			{
				return this.averageTotalLatency;
			}
			set
			{
				this.averageTotalLatency = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000093C4 File Offset: 0x000075C4
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000093CC File Offset: 0x000075CC
		internal int AverageResultSize
		{
			get
			{
				return this.averageResultSize;
			}
			set
			{
				this.averageResultSize = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000093D5 File Offset: 0x000075D5
		internal int[] OperationTimings
		{
			get
			{
				return this.operationTimings;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000093DD File Offset: 0x000075DD
		internal int[] OperationCounts
		{
			get
			{
				return this.operationCounts;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000093E5 File Offset: 0x000075E5
		internal int[] OperationResultSizes
		{
			get
			{
				return this.operationResultSizes;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000093F0 File Offset: 0x000075F0
		internal LatencyDistributionRange(int latencyLowRange, int latencyHighRange, int operationsCount)
		{
			this.latencyLowRange = latencyLowRange;
			this.latencyHighRange = latencyHighRange;
			this.operationTimings = new int[operationsCount];
			this.operationCounts = new int[operationsCount];
			this.operationResultSizes = new int[operationsCount];
			this.frequency = 0;
			this.averageTotalLatency = 0;
			this.averageResultSize = 0;
		}

		// Token: 0x040000EF RID: 239
		private int latencyLowRange;

		// Token: 0x040000F0 RID: 240
		private int latencyHighRange;

		// Token: 0x040000F1 RID: 241
		private int frequency;

		// Token: 0x040000F2 RID: 242
		private int averageTotalLatency;

		// Token: 0x040000F3 RID: 243
		private int averageResultSize;

		// Token: 0x040000F4 RID: 244
		private int[] operationTimings;

		// Token: 0x040000F5 RID: 245
		private int[] operationCounts;

		// Token: 0x040000F6 RID: 246
		private int[] operationResultSizes;
	}
}
