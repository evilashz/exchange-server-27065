using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Office.Story.V1.ImageAnalysis
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	internal class CountStatistics
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000022AC File Offset: 0x000004AC
		public CountStatistics()
		{
			this.Maximum = double.NegativeInfinity;
			this.Minimum = double.PositiveInfinity;
			this.Average = 0.0;
			this.Count = 0.0;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022FB File Offset: 0x000004FB
		public CountStatistics(IEnumerable<double> sequence) : this()
		{
			if (sequence == null)
			{
				throw new ArgumentNullException("sequence");
			}
			this.Add(sequence);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002318 File Offset: 0x00000518
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002320 File Offset: 0x00000520
		public double Average { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002329 File Offset: 0x00000529
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002331 File Offset: 0x00000531
		public double Minimum { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000233A File Offset: 0x0000053A
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002342 File Offset: 0x00000542
		public double Maximum { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002353 File Offset: 0x00000553
		public double Count { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x0000235C File Offset: 0x0000055C
		public void Add(IEnumerable<double> sequence)
		{
			if (sequence == null)
			{
				throw new ArgumentNullException("sequence");
			}
			foreach (double num in sequence)
			{
				double value = num;
				this.Add(value);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000023B4 File Offset: 0x000005B4
		public void Add(double value)
		{
			this.Average = this.Average * this.Count + value;
			this.Count += 1.0;
			this.Average /= this.Count;
			if (this.Minimum > value)
			{
				this.Minimum = value;
			}
			if (this.Maximum < value)
			{
				this.Maximum = value;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002420 File Offset: 0x00000620
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Minimum: {0:G} Average: {1:G} Maximum: {2:G} Count: {3:G}", new object[]
			{
				this.Minimum,
				this.Average,
				this.Maximum,
				this.Count
			});
		}
	}
}
