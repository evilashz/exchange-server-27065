using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Rpc.Search;

namespace Microsoft.Exchange.Search.Core.RpcEndpoint
{
	// Token: 0x020000B7 RID: 183
	internal sealed class SearchServiceRpcClient : SearchRpcClient, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x00012040 File Offset: 0x00010240
		internal SearchServiceRpcClient(string server) : base(server)
		{
			this.tracingContext = this.GetHashCode();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00012061 File Offset: 0x00010261
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SearchServiceRpcClient>(this);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00012069 File Offset: 0x00010269
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00012080 File Offset: 0x00010280
		internal new void RecordDocumentProcessing(Guid mdbGuid, Guid flowInstance, Guid correlationId, long docId)
		{
			SearchServiceRpcClient.tracer.TraceDebug<Guid>((long)this.tracingContext, "Executing RPC - RecordDocumentProcessing for CorrelationId: {0}", correlationId);
			try
			{
				base.RecordDocumentProcessing(mdbGuid, flowInstance, correlationId, docId);
			}
			finally
			{
				SearchServiceRpcClient.tracer.TraceDebug<Guid>((long)this.tracingContext, "Completed RPC - RecordDocumentProcessing for CorrelationId: {0}", correlationId);
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000120DC File Offset: 0x000102DC
		internal new void RecordDocumentFailure(Guid mdbGuid, Guid correlationId, long docId, string errorMessage)
		{
			SearchServiceRpcClient.tracer.TraceDebug<Guid>((long)this.tracingContext, "Executing RPC - RecordDocumentFailure for CorrelationId: {0}", correlationId);
			try
			{
				base.RecordDocumentFailure(mdbGuid, correlationId, docId, errorMessage);
			}
			finally
			{
				SearchServiceRpcClient.tracer.TraceDebug<Guid>((long)this.tracingContext, "Completed RPC - RecordDocumentFailure for CorrelationId: {0}", correlationId);
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00012138 File Offset: 0x00010338
		internal new void UpdateIndexSystems()
		{
			SearchServiceRpcClient.tracer.TraceDebug((long)this.tracingContext, "Executing RPC - UpdateIndexSystems");
			try
			{
				base.UpdateIndexSystems();
			}
			finally
			{
				SearchServiceRpcClient.tracer.TraceDebug((long)this.tracingContext, "Completed RPC - UpdateIndexSystems");
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001218C File Offset: 0x0001038C
		internal new void ResumeIndexing(Guid databaseGuid)
		{
			SearchServiceRpcClient.tracer.TraceDebug<Guid>((long)this.tracingContext, "Executing RPC - ResumeIndexing for database: {0}", databaseGuid);
			try
			{
				base.ResumeIndexing(databaseGuid);
			}
			finally
			{
				SearchServiceRpcClient.tracer.TraceDebug<Guid>((long)this.tracingContext, "Completed RPC - ResumeIndexing for database: {0}", databaseGuid);
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000121E4 File Offset: 0x000103E4
		internal new void RebuildIndexSystem(Guid databaseGuid)
		{
			SearchServiceRpcClient.tracer.TraceDebug<Guid>((long)this.tracingContext, "Executing RPC - RebuildIndexSystem for database: {0}", databaseGuid);
			try
			{
				base.RebuildIndexSystem(databaseGuid);
			}
			finally
			{
				SearchServiceRpcClient.tracer.TraceDebug<Guid>((long)this.tracingContext, "Completed RPC - RebuildIndexSystem for database: {0}", databaseGuid);
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001223C File Offset: 0x0001043C
		protected override void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			base.Dispose(calledFromDispose);
		}

		// Token: 0x0400028B RID: 651
		private static readonly Trace tracer = ExTraceGlobals.SearchRpcClientTracer;

		// Token: 0x0400028C RID: 652
		private readonly int tracingContext;

		// Token: 0x0400028D RID: 653
		private DisposeTracker disposeTracker;
	}
}
