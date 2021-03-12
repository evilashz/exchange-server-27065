using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200003D RID: 61
	internal class MovingAveragePerfCounter
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x00011666 File Offset: 0x0000F866
		public MovingAveragePerfCounter(IExPerformanceCounter perfCounter, int sampleSize)
		{
			this.perfCounter = perfCounter;
			this.samples = new long[sampleSize];
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00011684 File Offset: 0x0000F884
		public void AddSample(long sample)
		{
			lock (this.samples)
			{
				this.total -= this.samples[this.nextSample];
				this.total += sample;
				this.samples[this.nextSample++] = sample;
				if (this.nextSample == this.samples.Length)
				{
					this.nextSample = 0;
				}
				if (this.samplesFilled != this.samples.Length)
				{
					this.samplesFilled++;
				}
				this.perfCounter.RawValue = this.total / (long)this.samplesFilled;
			}
		}

		// Token: 0x04000181 RID: 385
		private readonly IExPerformanceCounter perfCounter;

		// Token: 0x04000182 RID: 386
		private readonly long[] samples;

		// Token: 0x04000183 RID: 387
		private int samplesFilled;

		// Token: 0x04000184 RID: 388
		private int nextSample;

		// Token: 0x04000185 RID: 389
		private long total;
	}
}
