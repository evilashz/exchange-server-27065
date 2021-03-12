using System;
using System.Threading;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000161 RID: 353
	internal class ForkCount
	{
		// Token: 0x06000F87 RID: 3975 RVA: 0x0003F216 File Offset: 0x0003D416
		internal int Get()
		{
			return this.forkCount;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0003F21E File Offset: 0x0003D41E
		internal int Increment()
		{
			return Interlocked.Increment(ref this.forkCount);
		}

		// Token: 0x040007C8 RID: 1992
		private int forkCount;
	}
}
