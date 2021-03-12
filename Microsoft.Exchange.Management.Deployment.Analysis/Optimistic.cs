using System;
using System.Threading;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000038 RID: 56
	internal sealed class Optimistic<T> where T : class
	{
		// Token: 0x0600019F RID: 415 RVA: 0x000079C4 File Offset: 0x00005BC4
		public Optimistic(T initialValue, Resolver<T> resolver)
		{
			this.value = initialValue;
			this.resolver = resolver;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000079DA File Offset: 0x00005BDA
		public T UnsafeValue
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000079E4 File Offset: 0x00005BE4
		public T SafeValue
		{
			get
			{
				T result = this.value;
				Thread.MemoryBarrier();
				return result;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000079FE File Offset: 0x00005BFE
		public T Update(T originalValue, T updatedValue)
		{
			return this.Update(originalValue, updatedValue, this.resolver);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007A10 File Offset: 0x00005C10
		public T Update(T originalValue, T updatedValue, Resolver<T> resolver)
		{
			if (object.ReferenceEquals(originalValue, updatedValue))
			{
				return updatedValue;
			}
			SpinWait spinWait = default(SpinWait);
			T t = originalValue;
			for (;;)
			{
				t = Interlocked.CompareExchange<T>(ref this.value, updatedValue, t);
				if (object.ReferenceEquals(t, originalValue))
				{
					break;
				}
				updatedValue = resolver(originalValue, t, updatedValue);
				spinWait.SpinOnce();
			}
			return updatedValue;
		}

		// Token: 0x04000087 RID: 135
		private T value;

		// Token: 0x04000088 RID: 136
		private Resolver<T> resolver;
	}
}
