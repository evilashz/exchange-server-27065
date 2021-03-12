using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002AC RID: 684
	internal abstract class SynchronousPipelineStageBase : PipelineStageBase
	{
		// Token: 0x060014B9 RID: 5305 RVA: 0x000596D0 File Offset: 0x000578D0
		internal SynchronousPipelineStageBase()
		{
			this.syncDelegate = new SynchronousPipelineStageBase.SynchronousWorkDelegate(this.DoWork);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x000596EA File Offset: 0x000578EA
		internal void DoWork()
		{
			if (base.MarkedForLastChanceHandling)
			{
				this.ReportFailure();
				return;
			}
			this.InternalDoSynchronousWork();
		}

		// Token: 0x060014BB RID: 5307
		protected abstract void InternalDoSynchronousWork();

		// Token: 0x060014BC RID: 5308 RVA: 0x00059704 File Offset: 0x00057904
		protected override void InternalDispatchWorkAsync()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "{0}::InternalDispatchWorkAsync()", new object[]
			{
				base.GetType().ToString()
			});
			this.syncDelegate.BeginInvoke(new AsyncCallback(this.EndSynchronousWork), null);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0005975A File Offset: 0x0005795A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SynchronousPipelineStageBase>(this);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00059764 File Offset: 0x00057964
		private void EndSynchronousWork(IAsyncResult r)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "{0}::EndSynchronousWork()", new object[]
			{
				base.GetType().ToString()
			});
			Exception error = null;
			try
			{
				this.syncDelegate.EndInvoke(r);
			}
			catch (Exception ex)
			{
				error = ex;
			}
			base.StageCompletionCallback(this, base.WorkItem, error);
		}

		// Token: 0x04000CB4 RID: 3252
		private SynchronousPipelineStageBase.SynchronousWorkDelegate syncDelegate;

		// Token: 0x020002AD RID: 685
		// (Invoke) Token: 0x060014C0 RID: 5312
		protected delegate void SynchronousWorkDelegate();
	}
}
