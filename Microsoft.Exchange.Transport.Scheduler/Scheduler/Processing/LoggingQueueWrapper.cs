using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200001B RID: 27
	internal class LoggingQueueWrapper : ISchedulerQueue
	{
		// Token: 0x06000075 RID: 117 RVA: 0x0000367F File Offset: 0x0000187F
		public LoggingQueueWrapper(ISchedulerQueue queue)
		{
			ArgumentValidator.ThrowIfNull("queue", queue);
			this.queue = queue;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000036A4 File Offset: 0x000018A4
		public bool IsEmpty
		{
			get
			{
				return this.queue.IsEmpty;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000036B1 File Offset: 0x000018B1
		public long Count
		{
			get
			{
				return this.queue.Count;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000036BE File Offset: 0x000018BE
		public void Enqueue(ISchedulableMessage message)
		{
			this.queue.Enqueue(message);
			this.queueLog.RecordEnqueue();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000036D7 File Offset: 0x000018D7
		public bool TryDequeue(out ISchedulableMessage message)
		{
			if (this.queue.TryDequeue(out message))
			{
				this.queueLog.RecordDequeue();
				return true;
			}
			return false;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000036F5 File Offset: 0x000018F5
		public bool TryPeek(out ISchedulableMessage message)
		{
			return this.queue.TryPeek(out message);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003703 File Offset: 0x00001903
		public void Flush(DateTime timestamp, QueueLogInfo info)
		{
			info.Count = this.Count;
			this.queueLog.Flush(timestamp, info);
		}

		// Token: 0x04000047 RID: 71
		private readonly ISchedulerQueue queue;

		// Token: 0x04000048 RID: 72
		private readonly QueueLog queueLog = new QueueLog();
	}
}
