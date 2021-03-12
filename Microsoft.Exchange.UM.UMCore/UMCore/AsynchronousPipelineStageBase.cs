using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002AA RID: 682
	internal abstract class AsynchronousPipelineStageBase : PipelineStageBase
	{
		// Token: 0x060014AF RID: 5295 RVA: 0x000595B0 File Offset: 0x000577B0
		internal AsynchronousPipelineStageBase()
		{
			this.asyncDelegate = new AsynchronousPipelineStageBase.AsynchronousWorkDelegate(this.DoWork);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000595CA File Offset: 0x000577CA
		internal void DoWork()
		{
			if (base.MarkedForLastChanceHandling)
			{
				this.ReportFailure();
				return;
			}
			this.InternalStartAsynchronousWork();
		}

		// Token: 0x060014B1 RID: 5297
		protected abstract void InternalStartAsynchronousWork();

		// Token: 0x060014B2 RID: 5298 RVA: 0x000595E4 File Offset: 0x000577E4
		protected override void InternalDispatchWorkAsync()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "{0}::InternalDispatchWorkAsync()", new object[]
			{
				base.GetType().ToString()
			});
			this.asyncDelegate.BeginInvoke(new AsyncCallback(this.AsynchronousWorkStarted), null);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0005963A File Offset: 0x0005783A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AsynchronousPipelineStageBase>(this);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00059644 File Offset: 0x00057844
		private void AsynchronousWorkStarted(IAsyncResult r)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "{0}::AsynchronousWorkStarted()", new object[]
			{
				base.GetType().ToString()
			});
			try
			{
				this.asyncDelegate.EndInvoke(r);
			}
			catch (Exception error)
			{
				base.StageCompletionCallback(this, base.WorkItem, error);
				return;
			}
			if (base.MarkedForLastChanceHandling)
			{
				base.StageCompletionCallback(this, base.WorkItem, null);
			}
		}

		// Token: 0x04000CB3 RID: 3251
		private AsynchronousPipelineStageBase.AsynchronousWorkDelegate asyncDelegate;

		// Token: 0x020002AB RID: 683
		// (Invoke) Token: 0x060014B6 RID: 5302
		protected delegate void AsynchronousWorkDelegate();
	}
}
