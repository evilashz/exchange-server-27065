using System;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x0200001C RID: 28
	internal class RunningPercentage
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000053C7 File Offset: 0x000035C7
		public RunningPercentage(ushort numberOfSamples)
		{
			if (numberOfSamples < 2)
			{
				throw new ArgumentException("numberOfSamples must be greater than 1", "numberOfSamples");
			}
			this.numberOfSamples = numberOfSamples;
			this.cachedNumerators = new bool[(int)this.numberOfSamples];
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005406 File Offset: 0x00003606
		public long Value
		{
			get
			{
				return this.cachedValue;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005410 File Offset: 0x00003610
		public long Update()
		{
			lock (this.lockObject)
			{
				if (!this.cachedNumerators[this.movingIndex])
				{
					this.totalSetCachedNumerators += 1L;
				}
				this.cachedNumerators[this.movingIndex] = true;
				this.UpdateCachedValue();
			}
			return this.cachedValue;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005484 File Offset: 0x00003684
		public long IncrementBase()
		{
			lock (this.lockObject)
			{
				this.IncrementNumeratorIndex();
				if (this.cachedNumerators[this.movingIndex])
				{
					this.totalSetCachedNumerators -= 1L;
				}
				this.cachedNumerators[this.movingIndex] = false;
				if (this.cachedDenominator < (long)((ulong)this.numberOfSamples))
				{
					this.cachedDenominator += 1L;
				}
				this.UpdateCachedValue();
			}
			return this.cachedValue;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000551C File Offset: 0x0000371C
		private void IncrementNumeratorIndex()
		{
			this.movingIndex++;
			if (this.movingIndex >= (int)this.numberOfSamples)
			{
				this.movingIndex = 0;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005541 File Offset: 0x00003741
		private void UpdateCachedValue()
		{
			if (this.cachedDenominator > 0L)
			{
				this.cachedValue = this.totalSetCachedNumerators * 100L / this.cachedDenominator;
			}
		}

		// Token: 0x040000D1 RID: 209
		private readonly ushort numberOfSamples;

		// Token: 0x040000D2 RID: 210
		private bool[] cachedNumerators;

		// Token: 0x040000D3 RID: 211
		private long totalSetCachedNumerators;

		// Token: 0x040000D4 RID: 212
		private long cachedDenominator;

		// Token: 0x040000D5 RID: 213
		private long cachedValue;

		// Token: 0x040000D6 RID: 214
		private int movingIndex;

		// Token: 0x040000D7 RID: 215
		private object lockObject = new object();
	}
}
