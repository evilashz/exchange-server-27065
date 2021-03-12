using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000026 RID: 38
	internal class MemoryCounter
	{
		// Token: 0x0600015A RID: 346 RVA: 0x000076EC File Offset: 0x000058EC
		internal MemoryCounter(string counterName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("counterName", counterName);
			this.counterName = counterName;
			this.counter = 0L;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000770E File Offset: 0x0000590E
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00007716 File Offset: 0x00005916
		internal long RawValue
		{
			get
			{
				return this.counter;
			}
			set
			{
				this.counter = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000771F File Offset: 0x0000591F
		internal string CounterName
		{
			get
			{
				return this.counterName;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007727 File Offset: 0x00005927
		internal long Increment()
		{
			return Interlocked.Increment(ref this.counter);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007734 File Offset: 0x00005934
		internal long IncrementBy(long val)
		{
			return Interlocked.Add(ref this.counter, val);
		}

		// Token: 0x040000C9 RID: 201
		private readonly string counterName;

		// Token: 0x040000CA RID: 202
		private long counter;
	}
}
