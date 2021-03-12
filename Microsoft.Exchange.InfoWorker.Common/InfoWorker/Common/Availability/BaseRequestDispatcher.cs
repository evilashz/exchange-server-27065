using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000070 RID: 112
	internal abstract class BaseRequestDispatcher : AsyncTask, IDisposable
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000D1D5 File Offset: 0x0000B3D5
		public int CrossForestQueryCount
		{
			get
			{
				return this.crossForestQueryCount;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000D1DD File Offset: 0x0000B3DD
		public int FederatedCrossForestQueryCount
		{
			get
			{
				return this.federatedCrossForestQueryCount;
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000D1E5 File Offset: 0x0000B3E5
		public BaseRequestDispatcher()
		{
			this.queryListDictionary = new Dictionary<string, QueryList>();
			this.requests = new List<AsyncRequest>();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000D204 File Offset: 0x0000B404
		public void Add(string key, BaseQuery query, RequestType requestType, BaseRequestDispatcher.CreateRequestDelegate createRequestDelegate)
		{
			BaseRequestDispatcher.RequestRoutingTracer.TraceDebug((long)this.GetHashCode(), "{0}: Adding a proxy web request of type {1} for mailbox {2} to request key {3}", new object[]
			{
				TraceContext.Get(),
				requestType,
				query.Email,
				key
			});
			query.Type = new RequestType?(requestType);
			QueryList queryList;
			if (!this.queryListDictionary.TryGetValue(key, out queryList))
			{
				BaseRequestDispatcher.RequestRoutingTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: key {1} was not found. Creating new request for it", TraceContext.Get(), key);
				queryList = new QueryList();
				queryList.Add(query);
				this.requests.Add(createRequestDelegate(queryList));
				this.queryListDictionary.Add(key, queryList);
			}
			else
			{
				BaseRequestDispatcher.RequestRoutingTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: key {1} was found.", TraceContext.Get(), key);
				queryList.Add(query);
			}
			switch (requestType)
			{
			case RequestType.Local:
				PerformanceCounters.IntraSiteCalendarQueriesPerSecond.Increment();
				this.intraSiteQueryCount++;
				return;
			case RequestType.IntraSite:
				PerformanceCounters.IntraSiteProxyFreeBusyQueriesPerSecond.Increment();
				this.intraSiteProxyQueryCount++;
				return;
			case RequestType.CrossSite:
				PerformanceCounters.CrossSiteCalendarQueriesPerSecond.Increment();
				this.crossSiteQueryCount++;
				return;
			case RequestType.CrossForest:
				PerformanceCounters.CrossForestCalendarQueriesPerSecond.Increment();
				this.crossForestQueryCount++;
				return;
			case RequestType.FederatedCrossForest:
				PerformanceCounters.FederatedFreeBusyQueriesPerSecond.Increment();
				this.federatedCrossForestQueryCount++;
				return;
			case RequestType.PublicFolder:
				PerformanceCounters.PublicFolderQueriesPerSecond.Increment();
				this.publicFolderQueryCount++;
				return;
			default:
				return;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000D393 File Offset: 0x0000B593
		protected ICollection<AsyncRequest> Requests
		{
			get
			{
				return this.requests;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D39B File Offset: 0x0000B59B
		public override void Abort()
		{
			base.Abort();
			if (this.parallel != null)
			{
				this.parallel.Abort();
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			this.queryListDictionary = null;
			if (this.requests.Count == 0)
			{
				BaseRequestDispatcher.RequestRoutingTracer.TraceDebug((long)this.GetHashCode(), "{0}: No requests to dispatch.", new object[]
				{
					TraceContext.Get()
				});
				base.Complete();
				return;
			}
			BaseRequestDispatcher.RequestRoutingTracer.TraceDebug<object, int>((long)this.GetHashCode(), "{0}: dispatching {1} requests.", TraceContext.Get(), this.requests.Count);
			this.parallel = new AsyncTaskParallel(this.requests.ToArray());
			this.parallel.BeginInvoke(new TaskCompleteCallback(this.Complete));
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D460 File Offset: 0x0000B660
		private void Complete(AsyncTask task)
		{
			base.Complete();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D468 File Offset: 0x0000B668
		public void Dispose()
		{
			foreach (AsyncRequest asyncRequest in this.requests)
			{
				IDisposable disposable = asyncRequest as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		public void LogStatistics(RequestLogger requestLogger)
		{
			if (this.intraSiteQueryCount > 0)
			{
				requestLogger.AppendToLog<int>("local", this.intraSiteQueryCount);
			}
			if (this.intraSiteProxyQueryCount > 0)
			{
				requestLogger.AppendToLog<int>("intrasiteproxy", this.intraSiteProxyQueryCount);
			}
			if (this.crossSiteQueryCount > 0)
			{
				requestLogger.AppendToLog<int>("x-site", this.crossSiteQueryCount);
			}
			if (this.crossForestQueryCount > 0)
			{
				requestLogger.AppendToLog<int>("x-forest", this.crossForestQueryCount);
			}
			if (this.federatedCrossForestQueryCount > 0)
			{
				requestLogger.AppendToLog<int>("federatedxforest", this.federatedCrossForestQueryCount);
			}
			if (this.publicFolderQueryCount > 0)
			{
				requestLogger.AppendToLog<int>("PF", this.publicFolderQueryCount);
			}
		}

		// Token: 0x040001BD RID: 445
		private Dictionary<string, QueryList> queryListDictionary;

		// Token: 0x040001BE RID: 446
		private List<AsyncRequest> requests;

		// Token: 0x040001BF RID: 447
		private AsyncTaskParallel parallel;

		// Token: 0x040001C0 RID: 448
		private int intraSiteQueryCount;

		// Token: 0x040001C1 RID: 449
		private int intraSiteProxyQueryCount;

		// Token: 0x040001C2 RID: 450
		private int crossSiteQueryCount;

		// Token: 0x040001C3 RID: 451
		private int crossForestQueryCount;

		// Token: 0x040001C4 RID: 452
		private int publicFolderQueryCount;

		// Token: 0x040001C5 RID: 453
		private int federatedCrossForestQueryCount;

		// Token: 0x040001C6 RID: 454
		private static readonly Trace RequestRoutingTracer = ExTraceGlobals.RequestRoutingTracer;

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x060002CA RID: 714
		public delegate AsyncRequest CreateRequestDelegate(QueryList queryList);
	}
}
