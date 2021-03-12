using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200000A RID: 10
	internal class ConcurrentQueueWrapper : ISchedulerQueue
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002994 File Offset: 0x00000B94
		public bool IsEmpty
		{
			get
			{
				return this.queue.IsEmpty;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000029A1 File Offset: 0x00000BA1
		public long Count
		{
			get
			{
				return (long)this.queue.Count;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029AF File Offset: 0x00000BAF
		public void Enqueue(ISchedulableMessage message)
		{
			ArgumentValidator.ThrowIfNull("message", message);
			this.queue.Enqueue(message);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000029C8 File Offset: 0x00000BC8
		public bool TryDequeue(out ISchedulableMessage message)
		{
			return this.queue.TryDequeue(out message);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029D6 File Offset: 0x00000BD6
		public bool TryPeek(out ISchedulableMessage message)
		{
			return this.queue.TryPeek(out message);
		}

		// Token: 0x04000023 RID: 35
		private readonly ConcurrentQueue<ISchedulableMessage> queue = new ConcurrentQueue<ISchedulableMessage>();
	}
}
