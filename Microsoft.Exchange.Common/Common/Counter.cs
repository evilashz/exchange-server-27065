using System;
using System.Threading;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000003 RID: 3
	internal sealed class Counter
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002121 File Offset: 0x00000321
		internal Counter()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002129 File Offset: 0x00000329
		internal Counter(int value)
		{
			this.counter = value;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002138 File Offset: 0x00000338
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002140 File Offset: 0x00000340
		internal int Value
		{
			get
			{
				return this.counter;
			}
			set
			{
				Interlocked.Exchange(ref this.counter, value);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002150 File Offset: 0x00000350
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216B File Offset: 0x0000036B
		internal void Increment()
		{
			Interlocked.Increment(ref this.counter);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002179 File Offset: 0x00000379
		internal void Decrement()
		{
			Interlocked.Decrement(ref this.counter);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002187 File Offset: 0x00000387
		internal void IncrementBy(int value)
		{
			Interlocked.Add(ref this.counter, value);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002196 File Offset: 0x00000396
		internal void DecrementBy(int value)
		{
			Interlocked.Add(ref this.counter, -value);
		}

		// Token: 0x04000003 RID: 3
		private int counter;
	}
}
