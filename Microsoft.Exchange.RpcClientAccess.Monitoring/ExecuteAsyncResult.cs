using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000038 RID: 56
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExecuteAsyncResult : EasyAsyncResult, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00005977 File Offset: 0x00003B77
		public ExecuteAsyncResult(Func<ExecuteAsyncResult, ExecuteContext> executeContextFactory, AsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
			this.executeContext = executeContextFactory(this);
			this.disposeTracker = DisposeTracker.Get<ExecuteAsyncResult>(this);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000599A File Offset: 0x00003B9A
		public ExecuteContext ExecuteContext
		{
			get
			{
				return this.executeContext;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000059A2 File Offset: 0x00003BA2
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				Util.DisposeIfPresent(this.executeContext);
				Util.DisposeIfPresent(this.disposeTracker);
				GC.SuppressFinalize(this);
				this.isDisposed = true;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000059CF File Offset: 0x00003BCF
		DisposeTracker IDisposeTrackable.GetDisposeTracker()
		{
			return DisposeTracker.Get<ExecuteAsyncResult>(this);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000059D7 File Offset: 0x00003BD7
		void IDisposeTrackable.SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x040000BC RID: 188
		private readonly ExecuteContext executeContext;

		// Token: 0x040000BD RID: 189
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040000BE RID: 190
		private bool isDisposed;
	}
}
