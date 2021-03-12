using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200002B RID: 43
	internal class NullPerformanceCounter : IExPerformanceCounter
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000734E File Offset: 0x0000554E
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00007352 File Offset: 0x00005552
		public long RawValue
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007354 File Offset: 0x00005554
		public long Decrement()
		{
			return 0L;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007358 File Offset: 0x00005558
		public long Increment()
		{
			return 0L;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000735C File Offset: 0x0000555C
		public long IncrementBy(long incrementValue)
		{
			return 0L;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007360 File Offset: 0x00005560
		public void Reset()
		{
		}

		// Token: 0x040000A1 RID: 161
		public static readonly NullPerformanceCounter Instance = new NullPerformanceCounter();
	}
}
