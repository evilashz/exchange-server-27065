using System;
using System.Text;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000177 RID: 375
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PerformanceDataProvider : IPerformanceDataProvider
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002784F File Offset: 0x00025A4F
		public PerformanceDataProvider(string name) : this(name, true)
		{
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00027859 File Offset: 0x00025A59
		public PerformanceDataProvider(string name, bool threadLocal)
		{
			this.Name = name;
			this.ThreadLocal = threadLocal;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0002786F File Offset: 0x00025A6F
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x00027877 File Offset: 0x00025A77
		public string Name { get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00027880 File Offset: 0x00025A80
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00027888 File Offset: 0x00025A88
		public bool ThreadLocal { get; private set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x00027891 File Offset: 0x00025A91
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x00027899 File Offset: 0x00025A99
		public uint RequestCount { get; protected set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000278A2 File Offset: 0x00025AA2
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x000278AA File Offset: 0x00025AAA
		public TimeSpan Latency { get; protected set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x000278B3 File Offset: 0x00025AB3
		public string Operations
		{
			get
			{
				if (this.operationsBuilder != null)
				{
					return this.operationsBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000278CE File Offset: 0x00025ACE
		public bool IsSnapshotInProgress
		{
			get
			{
				return this.snapshotsInProgress > 0U;
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000278D9 File Offset: 0x00025AD9
		public virtual PerformanceData TakeSnapshot(bool begin)
		{
			this.UpdateSnapshotInProgress(begin);
			return new PerformanceData(this.Latency, this.RequestCount);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000278F3 File Offset: 0x00025AF3
		public void ResetOperations()
		{
			this.operationsBuilder = null;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000278FC File Offset: 0x00025AFC
		public void AppendToOperations(string append)
		{
			if (this.IsSnapshotInProgress)
			{
				if (this.operationsBuilder == null)
				{
					this.operationsBuilder = new StringBuilder();
				}
				if (this.operationsBuilder.Length + 1 + append.Length <= 32767)
				{
					if (this.operationsBuilder.Length > 0)
					{
						this.operationsBuilder.Append('/');
					}
					this.operationsBuilder.Append(append);
				}
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00027968 File Offset: 0x00025B68
		public IDisposable StartRequestTimer()
		{
			this.RequestCount += 1U;
			return new PerformanceDataProvider.PerformanceTimer(this);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002797E File Offset: 0x00025B7E
		public IDisposable StartOperationTimer()
		{
			return new PerformanceDataProvider.PerformanceTimer(this);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00027986 File Offset: 0x00025B86
		private void UpdateSnapshotInProgress(bool begin)
		{
			if (begin)
			{
				if (PerformanceReportingOptions.Instance.LatencyDetectionEnabled)
				{
					this.snapshotsInProgress += 1U;
					return;
				}
			}
			else if (this.snapshotsInProgress > 0U)
			{
				this.snapshotsInProgress -= 1U;
			}
		}

		// Token: 0x04000744 RID: 1860
		public const int MaximumOperationsBufferLength = 32767;

		// Token: 0x04000745 RID: 1861
		private StringBuilder operationsBuilder;

		// Token: 0x04000746 RID: 1862
		private uint snapshotsInProgress;

		// Token: 0x02000178 RID: 376
		private sealed class PerformanceTimer : IDisposable
		{
			// Token: 0x06000AB3 RID: 2739 RVA: 0x000279BD File Offset: 0x00025BBD
			public PerformanceTimer(PerformanceDataProvider perfDataProvider)
			{
				this.perfDataProvider = perfDataProvider;
			}

			// Token: 0x06000AB4 RID: 2740 RVA: 0x000279D7 File Offset: 0x00025BD7
			public void Dispose()
			{
				this.perfDataProvider.Latency += this.stopwatch.Elapsed;
			}

			// Token: 0x0400074B RID: 1867
			private MyStopwatch stopwatch = MyStopwatch.StartNew();

			// Token: 0x0400074C RID: 1868
			private PerformanceDataProvider perfDataProvider;
		}
	}
}
