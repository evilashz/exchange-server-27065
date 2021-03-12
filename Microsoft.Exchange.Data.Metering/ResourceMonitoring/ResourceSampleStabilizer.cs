using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000022 RID: 34
	internal class ResourceSampleStabilizer
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00006724 File Offset: 0x00004924
		public ResourceSampleStabilizer(int maxSamples, ResourceSample initialSample)
		{
			ArgumentValidator.ThrowIfInvalidValue<int>("maxSamples", maxSamples, (int count) => count > 1);
			this.samples = new ResourceSample[maxSamples];
			for (int i = 0; i < maxSamples; i++)
			{
				this.samples[i] = new ResourceSample(initialSample.UseLevel, initialSample.Pressure);
			}
			this.useLevelSampleCounts[(int)initialSample.UseLevel] = maxSamples;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000067C6 File Offset: 0x000049C6
		public ResourceSample GetStabilizedSample(ResourceSample sample)
		{
			this.AddSample(sample);
			return this.CalculateStableSample();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000067D8 File Offset: 0x000049D8
		public void AddSample(ResourceSample sample)
		{
			this.currentIndex = (this.currentIndex + 1) % this.samples.Length;
			ResourceSample resourceSample = this.samples[this.currentIndex];
			this.useLevelSampleCounts[(int)resourceSample.UseLevel]--;
			this.samples[this.currentIndex] = sample;
			this.useLevelSampleCounts[(int)sample.UseLevel]++;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006868 File Offset: 0x00004A68
		public int GetUseLevelPercentage(UseLevel useLevel)
		{
			return this.useLevelSampleCounts[(int)useLevel] * 100 / this.samples.Length;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000068A0 File Offset: 0x00004AA0
		private ResourceSample CalculateStableSample()
		{
			UseLevel stableUseLevel = UseLevel.Low;
			for (int i = 0; i < this.useLevelSampleCounts.Length; i++)
			{
				if (this.useLevelSampleCounts[i] != 0)
				{
					stableUseLevel = (UseLevel)i;
					break;
				}
			}
			long pressure = (long)(from sample in this.samples
			where sample.UseLevel == stableUseLevel
			select sample).Average((ResourceSample sample) => sample.Pressure);
			return new ResourceSample(stableUseLevel, pressure);
		}

		// Token: 0x040000A0 RID: 160
		private readonly ResourceSample[] samples;

		// Token: 0x040000A1 RID: 161
		private readonly int[] useLevelSampleCounts = new int[Enum.GetNames(typeof(UseLevel)).Length];

		// Token: 0x040000A2 RID: 162
		private int currentIndex;
	}
}
