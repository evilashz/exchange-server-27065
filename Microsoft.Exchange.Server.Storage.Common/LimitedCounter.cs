using System;
using System.Threading;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000049 RID: 73
	public class LimitedCounter
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		public LimitedCounter(uint limit)
		{
			this.limit = (int)limit;
			this.counter = 0;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		public bool IsIncrementedValueOverLimit()
		{
			bool result = false;
			int num = Interlocked.Increment(ref this.counter);
			if (num > this.limit)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000CDEB File Offset: 0x0000AFEB
		public void Decrement()
		{
			Interlocked.Decrement(ref this.counter);
		}

		// Token: 0x040004DF RID: 1247
		private int counter;

		// Token: 0x040004E0 RID: 1248
		private readonly int limit;
	}
}
