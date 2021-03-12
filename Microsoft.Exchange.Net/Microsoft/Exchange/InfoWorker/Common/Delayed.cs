using System;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000BE3 RID: 3043
	internal sealed class Delayed<T>
	{
		// Token: 0x0600427D RID: 17021 RVA: 0x000B0EE7 File Offset: 0x000AF0E7
		public Delayed(Func<T> initializer)
		{
			this.initializer = initializer;
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x000B0F01 File Offset: 0x000AF101
		public T Value
		{
			get
			{
				this.InitializeIfNeeded();
				return this.value;
			}
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000B0F0F File Offset: 0x000AF10F
		public static implicit operator T(Delayed<T> t)
		{
			return t.Value;
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x000B0F18 File Offset: 0x000AF118
		private void InitializeIfNeeded()
		{
			if (!this.initialized)
			{
				lock (this.locker)
				{
					if (!this.initialized)
					{
						this.value = this.initializer();
						this.initialized = true;
					}
				}
			}
		}

		// Token: 0x040038D8 RID: 14552
		private T value;

		// Token: 0x040038D9 RID: 14553
		private Func<T> initializer;

		// Token: 0x040038DA RID: 14554
		private bool initialized;

		// Token: 0x040038DB RID: 14555
		private object locker = new object();
	}
}
