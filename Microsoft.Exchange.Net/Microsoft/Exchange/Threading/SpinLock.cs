using System;
using System.Threading;

namespace Microsoft.Exchange.Threading
{
	// Token: 0x02000B0B RID: 2827
	internal struct SpinLock
	{
		// Token: 0x06003CD4 RID: 15572 RVA: 0x0009E77E File Offset: 0x0009C97E
		public void Enter()
		{
			if (Interlocked.CompareExchange(ref this.lockHeld, 1, 0) != 0)
			{
				this.EnterSpin();
			}
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x0009E798 File Offset: 0x0009C998
		private void EnterSpin()
		{
			int num = 0;
			while (this.lockHeld != 0 || Interlocked.CompareExchange(ref this.lockHeld, 1, 0) != 0)
			{
				if (num < 20 && SpinLock.processorCount > 1)
				{
					Thread.SpinWait(100);
				}
				else if (num < 25)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Sleep(1);
				}
				num++;
			}
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x0009E7EF File Offset: 0x0009C9EF
		public void Exit()
		{
			this.lockHeld = 0;
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x0009E7FA File Offset: 0x0009C9FA
		public bool IsHeld
		{
			get
			{
				return this.lockHeld != 0;
			}
		}

		// Token: 0x0400355E RID: 13662
		private volatile int lockHeld;

		// Token: 0x0400355F RID: 13663
		private static readonly int processorCount = Environment.ProcessorCount;
	}
}
