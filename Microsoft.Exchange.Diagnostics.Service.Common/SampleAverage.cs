using System;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000018 RID: 24
	internal class SampleAverage
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000053B4 File Offset: 0x000035B4
		public SampleAverage(int numSamples)
		{
			this.numSamples = numSamples;
			this.samples = new int[numSamples];
			this.lastIndex = -1;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000053D6 File Offset: 0x000035D6
		public double AverageValue
		{
			get
			{
				return this.averageValue;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000053E0 File Offset: 0x000035E0
		public int? LastValue
		{
			get
			{
				if (this.lastIndex != -1)
				{
					return new int?(this.samples[this.lastIndex]);
				}
				return null;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00005414 File Offset: 0x00003614
		public double AddNewSample(double newValue)
		{
			this.lastIndex = (this.lastIndex + 1) % this.numSamples;
			if (this.sampleCount < this.numSamples)
			{
				this.averageValue = ((this.sampleCount == 0) ? newValue : ((this.averageValue * (double)this.sampleCount + newValue) / (double)(this.sampleCount + 1)));
				this.sampleCount++;
			}
			else
			{
				this.averageValue += (newValue - (double)this.samples[this.lastIndex]) / (double)this.numSamples;
			}
			this.samples[this.lastIndex] = (int)newValue;
			return this.averageValue;
		}

		// Token: 0x040002DF RID: 735
		private readonly int numSamples;

		// Token: 0x040002E0 RID: 736
		private readonly int[] samples;

		// Token: 0x040002E1 RID: 737
		private int lastIndex;

		// Token: 0x040002E2 RID: 738
		private int sampleCount;

		// Token: 0x040002E3 RID: 739
		private double averageValue;
	}
}
