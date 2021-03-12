using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x020001AB RID: 427
	internal class QueueDataAvailableEventSource<T>
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x00043943 File Offset: 0x00041B43
		public QueueDataAvailableEventSource(ThreadSafeQueue<T> queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			this.Queue = queue;
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x00043960 File Offset: 0x00041B60
		// (set) Token: 0x0600106B RID: 4203 RVA: 0x00043968 File Offset: 0x00041B68
		public ThreadSafeQueue<T> Queue { get; private set; }
	}
}
