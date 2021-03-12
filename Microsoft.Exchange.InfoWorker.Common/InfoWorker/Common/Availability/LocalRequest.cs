using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000A8 RID: 168
	internal sealed class LocalRequest : AsyncRequestWithQueryList
	{
		// Token: 0x060003AD RID: 941 RVA: 0x0000F0F8 File Offset: 0x0000D2F8
		public LocalRequest(Application application, ClientContext clientContext, RequestLogger requestLogger, QueryList queryList, DateTime deadline) : base(application, LocalRequest.CloneIfInternalContext(clientContext), RequestType.Local, requestLogger, queryList)
		{
			this.localQuery = application.CreateLocalQuery(base.ClientContext, deadline);
			int num = LimitedThreadPool.MaximumThreads / 2;
			if (num <= 0)
			{
				num = 1;
			}
			LocalRequest.RequestRoutingTracer.TraceDebug<object, int>((long)this.GetHashCode(), "{0}: Initializing LimitedThreadPool with {1} threads to process mailboxes", TraceContext.Get(), num);
			this.threadPool = new LimitedThreadPool(num, new WaitCallback(this.Execute));
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000F16C File Offset: 0x0000D36C
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			this.localRequestStopwatch = Stopwatch.StartNew();
			this.pendingItems = base.QueryList.Count;
			foreach (BaseQuery baseQuery in ((IEnumerable<BaseQuery>)base.QueryList))
			{
				baseQuery.Target = baseQuery.ExchangePrincipal.MailboxInfo.GetDatabaseGuid().ToString();
				this.threadPool.Add(baseQuery);
			}
			LocalRequest.RequestRoutingTracer.TraceDebug((long)this.GetHashCode(), "{0}: Starting the threads in the thread pool", new object[]
			{
				TraceContext.Get()
			});
			this.threadPool.Start();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000F238 File Offset: 0x0000D438
		public override void Abort()
		{
			base.Abort();
			this.threadPool.Cancel();
			this.ComputeStatistics();
			this.DisposeIfInternalContext();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000F258 File Offset: 0x0000D458
		private static ClientContext CloneIfInternalContext(ClientContext clientContext)
		{
			InternalClientContext internalClientContext = clientContext as InternalClientContext;
			if (internalClientContext != null)
			{
				return internalClientContext.Clone();
			}
			return clientContext;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000F298 File Offset: 0x0000D498
		private void Execute(object state)
		{
			ThreadContext.SetWithExceptionHandling("LocalRequest.Execute", base.Application.Worker, base.ClientContext, base.RequestLogger, delegate
			{
				this.ExecuteInternal((BaseQuery)state);
			});
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		private void ExecuteInternal(BaseQuery query)
		{
			IStandardBudget standardBudget = null;
			if (Interlocked.Exchange(ref this.firstThreadEntryTimeCaptured, 1) == 0)
			{
				base.RequestLogger.Add(RequestStatistics.Create(RequestStatisticsType.LocalFirstThreadExecute, this.localRequestStopwatch.ElapsedMilliseconds));
			}
			if (base.Aborted)
			{
				return;
			}
			RequestStatisticsForMapi requestStatisticsForMapi = RequestStatisticsForMapi.Begin();
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				if (base.ClientContext.Budget != null)
				{
					standardBudget = StandardBudget.Acquire(base.ClientContext.Budget.Owner);
					if (standardBudget != null)
					{
						standardBudget.StartLocal("LocalRequest.ExecuteInternal", default(TimeSpan));
					}
				}
				LocalRequest.RequestRoutingTracer.TraceDebug<object, int>((long)this.GetHashCode(), "{0}: Entered LocalRequest.Execute() using thread {1}", TraceContext.Get(), Environment.CurrentManagedThreadId);
				if (query.SetResultOnFirstCall(this.localQuery.GetData(query)))
				{
					LocalRequest.RequestRoutingTracer.TraceDebug<object, EmailAddress, int>((long)this.GetHashCode(), "{0}: Request for mailbox {1} complete and result set in time. Thread {2}", TraceContext.Get(), query.Email, Environment.CurrentManagedThreadId);
				}
			}
			finally
			{
				stopwatch.Stop();
				if (standardBudget != null)
				{
					standardBudget.EndLocal();
					standardBudget.Dispose();
				}
				base.RequestLogger.Add(RequestStatistics.Create(RequestStatisticsType.LocalElapsedTimeLongPole, stopwatch.ElapsedMilliseconds, query.ExchangePrincipal.MailboxInfo.Location.ServerFqdn));
				base.RequestLogger.Add(requestStatisticsForMapi.End(RequestStatisticsType.Local, query.ExchangePrincipal.MailboxInfo.Location.ServerFqdn));
				if (Interlocked.Decrement(ref this.pendingItems) == 0)
				{
					this.ComputeStatistics();
					this.DisposeIfInternalContext();
					base.Complete();
				}
				LocalRequest.RequestRoutingTracer.TraceDebug<object, int>((long)this.GetHashCode(), "{0}: Exited LocalRequest.Execute() using thread {1}", TraceContext.Get(), Environment.CurrentManagedThreadId);
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000F494 File Offset: 0x0000D694
		private void ComputeStatistics()
		{
			if (Interlocked.Exchange(ref this.statisticsComputed, 1) != 0)
			{
				return;
			}
			if (this.localRequestStopwatch != null)
			{
				this.localRequestStopwatch.Stop();
				base.RequestLogger.Add(RequestStatistics.Create(RequestStatisticsType.TotalLocal, this.localRequestStopwatch.ElapsedMilliseconds, base.QueryList.Count));
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		private void DisposeIfInternalContext()
		{
			InternalClientContext internalClientContext = base.ClientContext as InternalClientContext;
			if (internalClientContext != null)
			{
				internalClientContext.Dispose();
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000F512 File Offset: 0x0000D712
		public override string ToString()
		{
			return "Localrequest for " + base.QueryList.Count + " mailboxes";
		}

		// Token: 0x0400022E RID: 558
		private int pendingItems;

		// Token: 0x0400022F RID: 559
		private LimitedThreadPool threadPool;

		// Token: 0x04000230 RID: 560
		private LocalQuery localQuery;

		// Token: 0x04000231 RID: 561
		private Stopwatch localRequestStopwatch;

		// Token: 0x04000232 RID: 562
		private int statisticsComputed;

		// Token: 0x04000233 RID: 563
		private int firstThreadEntryTimeCaptured;

		// Token: 0x04000234 RID: 564
		private static readonly Microsoft.Exchange.Diagnostics.Trace RequestRoutingTracer = ExTraceGlobals.RequestRoutingTracer;
	}
}
