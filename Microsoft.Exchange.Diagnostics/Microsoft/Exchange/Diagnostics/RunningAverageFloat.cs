using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001D5 RID: 469
	public class RunningAverageFloat
	{
		// Token: 0x06000D1C RID: 3356 RVA: 0x000370A7 File Offset: 0x000352A7
		public RunningAverageFloat(ushort numberOfSamples)
		{
			if (numberOfSamples < 2)
			{
				throw new ArgumentException("numberOfSamples must be greater than 1", "numberOfSamples");
			}
			this.averageMultiplier = 1f / (float)numberOfSamples;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x000370DC File Offset: 0x000352DC
		public float Update(float newValue)
		{
			float result;
			lock (this.lockObject)
			{
				if (this.initialized)
				{
					this.cachedValue = (1f - this.averageMultiplier) * this.cachedValue + this.averageMultiplier * newValue;
				}
				else
				{
					this.cachedValue = newValue;
					this.initialized = true;
				}
				result = this.cachedValue;
			}
			return result;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00037160 File Offset: 0x00035360
		public void Reset(float valueToResetTo)
		{
			lock (this.lockObject)
			{
				this.cachedValue = valueToResetTo;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x000371A4 File Offset: 0x000353A4
		public float Value
		{
			get
			{
				return this.cachedValue;
			}
		}

		// Token: 0x040009B0 RID: 2480
		private volatile float cachedValue;

		// Token: 0x040009B1 RID: 2481
		private object lockObject = new object();

		// Token: 0x040009B2 RID: 2482
		private readonly float averageMultiplier;

		// Token: 0x040009B3 RID: 2483
		private bool initialized;
	}
}
