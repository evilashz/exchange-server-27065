using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Logging;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000E4 RID: 228
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RequestQueue : IRequestQueue
	{
		// Token: 0x060006FA RID: 1786 RVA: 0x00013C9F File Offset: 0x00011E9F
		public RequestQueue(Guid queueId, IQueueCounters counters)
		{
			this.activeThreads = 0L;
			this.queueId = queueId;
			this.queue = new ConcurrentQueue<IRequest>();
			this.counters = counters;
			this.queuedItemSignal = new AutoResetEvent(false);
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00013CD4 File Offset: 0x00011ED4
		public Guid Id
		{
			get
			{
				return this.queueId;
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00013CDC File Offset: 0x00011EDC
		public void EnqueueRequest(IRequest request)
		{
			this.counters.QueueLengthCounter.Increment();
			this.counters.IncomingRequestRateCounter.IncrementBy(1L);
			request.AssignQueue(this);
			this.queue.Enqueue(request);
			this.queuedItemSignal.Set();
			this.EnsureThreadIsActiveIfNeeded();
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00013D31 File Offset: 0x00011F31
		public void EnsureThreadIsActiveIfNeeded()
		{
			if (!this.queue.IsEmpty && Interlocked.CompareExchange(ref this.activeThreads, 1L, 0L) == 0L)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ProcessQueuedItems));
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00013D7C File Offset: 0x00011F7C
		public QueueDiagnosticData GetDiagnosticData(bool includeRequestDetails, bool includeRequestVerboseDiagnostics)
		{
			QueueDiagnosticData queueDiagnosticData = new QueueDiagnosticData
			{
				QueueGuid = this.queueId,
				QueueLength = this.queue.Count,
				IsActive = (this.activeThreads > 0L)
			};
			queueDiagnosticData.CurrentRequest = ((this.currentRequest != null) ? this.currentRequest.GetDiagnosticData(includeRequestVerboseDiagnostics) : null);
			if (includeRequestDetails)
			{
				queueDiagnosticData.Requests = (from request in this.queue
				select request.GetDiagnosticData(includeRequestVerboseDiagnostics)).ToList<RequestDiagnosticData>();
			}
			return queueDiagnosticData;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00013E1C File Offset: 0x0001201C
		public void Clean()
		{
			IRequest request;
			while (this.queue.TryDequeue(out request))
			{
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00013E38 File Offset: 0x00012038
		public override string ToString()
		{
			return string.Format("RequestQueue(Id={0},Active={1},QueuedItems={2})", this.queueId, this.activeThreads > 0L, this.queue.Count);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00013E70 File Offset: 0x00012070
		private void ProcessQueuedItems(object state)
		{
			try
			{
				for (;;)
				{
					IRequest request;
					if (this.queue.IsEmpty)
					{
						if (!this.queuedItemSignal.WaitOne(TimeSpan.FromSeconds(10.0)))
						{
							break;
						}
					}
					else if (this.queue.TryDequeue(out request))
					{
						this.counters.QueueLengthCounter.Decrement();
						this.currentRequest = request;
						request.Process();
						this.counters.ExecutionRateCounter.IncrementBy(1L);
						DatabaseRequestLog.Write(request);
						this.currentRequest = null;
					}
				}
			}
			finally
			{
				Interlocked.Exchange(ref this.activeThreads, 0L);
			}
		}

		// Token: 0x040002AD RID: 685
		private readonly IQueueCounters counters;

		// Token: 0x040002AE RID: 686
		private readonly ConcurrentQueue<IRequest> queue;

		// Token: 0x040002AF RID: 687
		private readonly Guid queueId;

		// Token: 0x040002B0 RID: 688
		private readonly AutoResetEvent queuedItemSignal;

		// Token: 0x040002B1 RID: 689
		private long activeThreads;

		// Token: 0x040002B2 RID: 690
		private IRequest currentRequest;
	}
}
