using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200014F RID: 335
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RetryTimer
	{
		// Token: 0x06000D0F RID: 3343 RVA: 0x00039A14 File Offset: 0x00037C14
		public RetryTimer(TimeSpan maxWait)
		{
			this.m_expiryTime = DateTime.UtcNow.Add(maxWait);
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00039A3B File Offset: 0x00037C3B
		public bool IsExpired
		{
			get
			{
				return DateTime.UtcNow >= this.m_expiryTime;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00039A4D File Offset: 0x00037C4D
		public virtual TimeSpan SleepTime
		{
			get
			{
				return new TimeSpan(0, 0, 3);
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00039A57 File Offset: 0x00037C57
		public void Sleep()
		{
			Thread.Sleep(this.SleepTime);
		}

		// Token: 0x0400058C RID: 1420
		private DateTime m_expiryTime;
	}
}
