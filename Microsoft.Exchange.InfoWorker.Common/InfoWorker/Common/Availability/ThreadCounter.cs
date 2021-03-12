using System;
using System.Threading;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000050 RID: 80
	internal sealed class ThreadCounter
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00009756 File Offset: 0x00007956
		internal ThreadCounter()
		{
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000975E File Offset: 0x0000795E
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009766 File Offset: 0x00007966
		public void Increment()
		{
			Interlocked.Increment(ref this.count);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009774 File Offset: 0x00007974
		public void Decrement()
		{
			Interlocked.Decrement(ref this.count);
		}

		// Token: 0x0400012A RID: 298
		private int count;
	}
}
