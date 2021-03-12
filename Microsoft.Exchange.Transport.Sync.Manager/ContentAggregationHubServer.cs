using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Rpc.SubscriptionSubmission;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000027 RID: 39
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ContentAggregationHubServer : DisposeTrackableBase
	{
		// Token: 0x06000235 RID: 565 RVA: 0x0000F404 File Offset: 0x0000D604
		internal ContentAggregationHubServer()
		{
			this.localServerMachineName = Environment.MachineName;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000F422 File Offset: 0x0000D622
		internal string MachineName
		{
			get
			{
				base.CheckDisposed();
				return this.localServerMachineName;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000F430 File Offset: 0x0000D630
		internal SubscriptionSubmissionRpcClient RpcClient
		{
			get
			{
				base.CheckDisposed();
				if (this.rpcClient == null)
				{
					lock (this.syncRoot)
					{
						if (this.rpcClient == null)
						{
							this.rpcClient = new SubscriptionSubmissionRpcClient(this.localServerMachineName, ContentAggregationHubServer.localSystemCredential);
						}
					}
				}
				return this.rpcClient;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000F49C File Offset: 0x0000D69C
		internal void IncrementRef()
		{
			base.CheckDisposed();
			lock (this.syncRoot)
			{
				this.refCount++;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		internal void DecrementRef()
		{
			base.CheckDisposed();
			lock (this.syncRoot)
			{
				this.refCount--;
				if (this.disposeRequested && this.refCount == 0)
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000F550 File Offset: 0x0000D750
		internal void RequestDispose()
		{
			base.CheckDisposed();
			this.disposeRequested = true;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F560 File Offset: 0x0000D760
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				lock (this.syncRoot)
				{
					if (this.rpcClient != null)
					{
						this.rpcClient.Dispose();
						this.rpcClient = null;
					}
				}
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ContentAggregationHubServer>(this);
		}

		// Token: 0x04000140 RID: 320
		private static readonly Trace Diag = ExTraceGlobals.SubscriptionSubmitTracer;

		// Token: 0x04000141 RID: 321
		private static readonly NetworkCredential localSystemCredential = new NetworkCredential(Environment.MachineName + "$", string.Empty, string.Empty);

		// Token: 0x04000142 RID: 322
		private readonly object syncRoot = new object();

		// Token: 0x04000143 RID: 323
		private readonly string localServerMachineName;

		// Token: 0x04000144 RID: 324
		private SubscriptionSubmissionRpcClient rpcClient;

		// Token: 0x04000145 RID: 325
		private int refCount;

		// Token: 0x04000146 RID: 326
		private bool disposeRequested;
	}
}
