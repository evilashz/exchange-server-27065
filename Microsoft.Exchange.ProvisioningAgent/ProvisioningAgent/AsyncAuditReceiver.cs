using System;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200000E RID: 14
	internal class AsyncAuditReceiver : DisposeTrackableBase
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00004811 File Offset: 0x00002A11
		public AsyncAuditReceiver()
		{
			this.workerThread = new Thread(new ThreadStart(this.DoWork));
			this.workerThread.Name = "AsyncAuditReceiver";
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004840 File Offset: 0x00002A40
		public void Start()
		{
			if (this.cancellationTokenSource == null)
			{
				this.cancellationTokenSource = new CancellationTokenSource();
				this.workerThread.Start();
				return;
			}
			throw new InvalidOperationException("Async Auditing receiver already started");
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000486B File Offset: 0x00002A6B
		public void Stop()
		{
			if (this.cancellationTokenSource != null)
			{
				this.cancellationTokenSource.Cancel();
				this.workerThread.Join(1000);
				this.cancellationTokenSource.Dispose();
				this.cancellationTokenSource = null;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000048A4 File Offset: 0x00002AA4
		private void DoWork()
		{
			while (!this.cancellationTokenSource.Token.IsCancellationRequested)
			{
				this.CheckQueues(DateTime.UtcNow);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000048D3 File Offset: 0x00002AD3
		private void CheckQueues(DateTime currentTime)
		{
			this.CheckMainQueue(currentTime);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004904 File Offset: 0x00002B04
		internal void CheckMainQueue(DateTime currentTime)
		{
			IQueue<AuditData> queue = QueueFactory.GetQueue<AuditData>(Queues.AdminAuditingMainQueue);
			IQueueMessage<AuditData> next = queue.GetNext(1000, this.cancellationTokenSource.Token);
			if (next != null)
			{
				AuditData data = next.Data;
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						data.AuditLogger.WriteAuditRecord(data.AuditRecord);
					});
				}
				catch (GrayException)
				{
					IQueue<AuditData> queue2 = QueueFactory.GetQueue<AuditData>(Queues.AdminAuditingRetryQueue);
					queue2.Send(data);
				}
				finally
				{
					queue.Commit(next);
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000049A0 File Offset: 0x00002BA0
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.Stop();
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000049AB File Offset: 0x00002BAB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AsyncAuditReceiver>(this);
		}

		// Token: 0x04000044 RID: 68
		private const int TimeoutInMilliseconds = 1000;

		// Token: 0x04000045 RID: 69
		private Thread workerThread;

		// Token: 0x04000046 RID: 70
		private CancellationTokenSource cancellationTokenSource;
	}
}
