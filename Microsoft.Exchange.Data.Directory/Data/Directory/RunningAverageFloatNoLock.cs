using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009C7 RID: 2503
	internal class RunningAverageFloatNoLock
	{
		// Token: 0x0600740E RID: 29710 RVA: 0x0017EFCE File Offset: 0x0017D1CE
		public RunningAverageFloatNoLock(ushort numberOfSamples)
		{
			if (numberOfSamples < 2)
			{
				throw new ArgumentException("numberOfSamples must be greater than 1", "numberOfSamples");
			}
			this.averageMultiplier = 1f / (float)numberOfSamples;
		}

		// Token: 0x0600740F RID: 29711 RVA: 0x0017EFF8 File Offset: 0x0017D1F8
		public float Update(float newValue)
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
			return this.cachedValue;
		}

		// Token: 0x06007410 RID: 29712 RVA: 0x0017F04D File Offset: 0x0017D24D
		public void Reset(float valueToResetTo)
		{
			this.cachedValue = valueToResetTo;
		}

		// Token: 0x1700295D RID: 10589
		// (get) Token: 0x06007411 RID: 29713 RVA: 0x0017F058 File Offset: 0x0017D258
		public float Value
		{
			get
			{
				return this.cachedValue;
			}
		}

		// Token: 0x04004AF0 RID: 19184
		private volatile float cachedValue;

		// Token: 0x04004AF1 RID: 19185
		private float averageMultiplier;

		// Token: 0x04004AF2 RID: 19186
		private bool initialized;
	}
}
