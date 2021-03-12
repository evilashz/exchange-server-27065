using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000010 RID: 16
	public abstract class WorkBroker : MarshalByRefObject
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000707F File Offset: 0x0000527F
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00007087 File Offset: 0x00005287
		internal Action<RestartRequest> RestartRequestEvent { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007090 File Offset: 0x00005290
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00007098 File Offset: 0x00005298
		private RestartRequest RestartRequest { get; set; }

		// Token: 0x060001A9 RID: 425
		public abstract IDataAccessQuery<TEntity> AsDataAccessQuery<TEntity>(IEnumerable<TEntity> query);

		// Token: 0x060001AA RID: 426
		internal abstract void PublishResult(WorkItemResult result, TracingContext traceContext);

		// Token: 0x060001AB RID: 427
		internal abstract BlockingCollection<WorkItem> AsyncGetWork(int maxWorkitemCount, CancellationToken cancellationToken);

		// Token: 0x060001AC RID: 428
		internal abstract void Reject(WorkItem workItem);

		// Token: 0x060001AD RID: 429
		internal abstract void Abandon(WorkItem workItem);

		// Token: 0x060001AE RID: 430
		internal abstract BlockingCollection<string> AsyncGetWorkItemPackages(CancellationToken cancellationToken);

		// Token: 0x060001AF RID: 431 RVA: 0x000070A4 File Offset: 0x000052A4
		internal void RequestRestart(RestartRequest reason)
		{
			this.RestartRequest = reason;
			this.RestartRequestEvent(reason);
			WTFDiagnostics.TraceError<string>(WTFLog.Core, TracingContext.Default, "[WorkBroker:RequestRestart]: Reason {0}", this.RestartRequest.ToString(), null, "RequestRestart", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkBroker.cs", 113);
		}
	}
}
