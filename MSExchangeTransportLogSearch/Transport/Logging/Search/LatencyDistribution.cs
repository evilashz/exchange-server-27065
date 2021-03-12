using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200003C RID: 60
	internal class LatencyDistribution
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00009199 File Offset: 0x00007399
		internal LatencyDistributionRange[] LatencyDistributionRanges
		{
			get
			{
				return this.latencyDistributionRanges;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000091A4 File Offset: 0x000073A4
		internal LatencyDistribution(int[] distributionBoundaries, int operationCount)
		{
			if (distributionBoundaries.Length < 2)
			{
				throw new ArgumentException("Need at least an upper and lower bound for the distribution boundaries", "distributionBoundaries");
			}
			this.distributionBoundaries = distributionBoundaries;
			this.latencyDistributionRanges = new LatencyDistributionRange[this.distributionBoundaries.Length - 1];
			for (int i = 1; i < this.distributionBoundaries.Length; i++)
			{
				int latencyLowRange = this.distributionBoundaries[i - 1];
				int latencyHighRange = this.distributionBoundaries[i];
				this.latencyDistributionRanges[i - 1] = new LatencyDistributionRange(latencyLowRange, latencyHighRange, operationCount);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00009224 File Offset: 0x00007424
		internal void AddSample(int totalLatency, int totalResultSize, int[] operationTimings, int[] operationCounts, int[] operationResultSize)
		{
			if (totalLatency < this.distributionBoundaries[0])
			{
				totalLatency = this.distributionBoundaries[0];
			}
			if (totalLatency >= this.distributionBoundaries[this.distributionBoundaries.Length - 1])
			{
				totalLatency = this.distributionBoundaries[this.distributionBoundaries.Length - 1] - 1;
			}
			int num = Array.BinarySearch<int>(this.distributionBoundaries, totalLatency);
			if (num < 0)
			{
				num = ~num - 1;
			}
			LatencyDistributionRange latencyDistributionRange = this.latencyDistributionRanges[num];
			latencyDistributionRange.AverageTotalLatency = LatencyDistribution.Summation(latencyDistributionRange.AverageTotalLatency, latencyDistributionRange.Frequency, totalLatency);
			latencyDistributionRange.AverageResultSize = LatencyDistribution.Summation(latencyDistributionRange.AverageResultSize, latencyDistributionRange.Frequency, totalResultSize);
			for (int i = 0; i < latencyDistributionRange.OperationTimings.Length; i++)
			{
				latencyDistributionRange.OperationTimings[i] = LatencyDistribution.Summation(latencyDistributionRange.OperationTimings[i], latencyDistributionRange.Frequency, operationTimings[i]);
				latencyDistributionRange.OperationCounts[i] = LatencyDistribution.Summation(latencyDistributionRange.OperationCounts[i], latencyDistributionRange.Frequency, operationCounts[i]);
				latencyDistributionRange.OperationResultSizes[i] = LatencyDistribution.Summation(latencyDistributionRange.OperationResultSizes[i], latencyDistributionRange.Frequency, operationResultSize[i]);
			}
			latencyDistributionRange.Frequency++;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000933C File Offset: 0x0000753C
		private static int Summation(int currentAverage, int currentCount, int newSample)
		{
			double num = (double)currentAverage;
			double num2 = (double)currentCount;
			double num3 = (double)newSample;
			return (int)(num * (num2 / (num2 + 1.0)) + num3 / (num2 + 1.0));
		}

		// Token: 0x040000E6 RID: 230
		private int[] distributionBoundaries;

		// Token: 0x040000E7 RID: 231
		private LatencyDistributionRange[] latencyDistributionRanges;

		// Token: 0x0200003D RID: 61
		internal enum Properties
		{
			// Token: 0x040000E9 RID: 233
			Name,
			// Token: 0x040000EA RID: 234
			Low,
			// Token: 0x040000EB RID: 235
			High,
			// Token: 0x040000EC RID: 236
			Frequency,
			// Token: 0x040000ED RID: 237
			TotalLatency,
			// Token: 0x040000EE RID: 238
			ResultSize
		}
	}
}
