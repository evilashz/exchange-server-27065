using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x0200017F RID: 383
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskPerformanceData
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00028400 File Offset: 0x00026600
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x00028408 File Offset: 0x00026608
		public PerformanceData Start
		{
			get
			{
				return this.start;
			}
			set
			{
				if (this.haveStartValue)
				{
					throw new InvalidOperationException("Start is already set.");
				}
				this.start = value;
				this.haveStartValue = true;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0002842B File Offset: 0x0002662B
		// (set) Token: 0x06000AFF RID: 2815 RVA: 0x00028434 File Offset: 0x00026634
		public PerformanceData End
		{
			get
			{
				return this.end;
			}
			set
			{
				if (!this.haveStartValue)
				{
					throw new InvalidOperationException("May not set End before Start.");
				}
				if (this.haveEndValue)
				{
					throw new InvalidOperationException("End is already set.");
				}
				this.end = value;
				bool flag = false;
				if (this.start.Latency <= this.end.Latency)
				{
					flag = (this.start.Count <= this.end.Count);
				}
				this.difference = (flag ? (value - this.start) : PerformanceData.Unknown);
				this.haveEndValue = true;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x000284CC File Offset: 0x000266CC
		public PerformanceData Difference
		{
			get
			{
				return this.difference;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x000284D4 File Offset: 0x000266D4
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x000284DC File Offset: 0x000266DC
		public string Operations { get; internal set; }

		// Token: 0x06000B03 RID: 2819 RVA: 0x000284E8 File Offset: 0x000266E8
		internal void InvalidateIfAsynchronous()
		{
			if (this.Start.ThreadId != this.End.ThreadId)
			{
				this.difference = PerformanceData.Unknown;
			}
		}

		// Token: 0x04000781 RID: 1921
		private PerformanceData start;

		// Token: 0x04000782 RID: 1922
		private PerformanceData end;

		// Token: 0x04000783 RID: 1923
		private PerformanceData difference;

		// Token: 0x04000784 RID: 1924
		private bool haveStartValue;

		// Token: 0x04000785 RID: 1925
		private bool haveEndValue;
	}
}
