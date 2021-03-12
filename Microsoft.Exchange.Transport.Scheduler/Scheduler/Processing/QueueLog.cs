using System;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000028 RID: 40
	internal class QueueLog
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004606 File Offset: 0x00002806
		public void RecordEnqueue()
		{
			this.enqueues += 1L;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004617 File Offset: 0x00002817
		public void RecordDequeue()
		{
			this.dequeues += 1L;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004628 File Offset: 0x00002828
		public void Flush(DateTime timestamp, QueueLogInfo info)
		{
			info.Dequeues = this.dequeues;
			info.Enqueues = this.enqueues;
			this.FlushInternal(timestamp, info);
			this.Reset(timestamp);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004651 File Offset: 0x00002851
		protected virtual void FlushInternal(DateTime timestamp, QueueLogInfo info)
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004653 File Offset: 0x00002853
		protected virtual void ResetInternal(DateTime timestamp)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004655 File Offset: 0x00002855
		private void Reset(DateTime timestamp)
		{
			this.enqueues = 0L;
			this.dequeues = 0L;
			this.ResetInternal(timestamp);
		}

		// Token: 0x0400006F RID: 111
		private long enqueues;

		// Token: 0x04000070 RID: 112
		private long dequeues;
	}
}
