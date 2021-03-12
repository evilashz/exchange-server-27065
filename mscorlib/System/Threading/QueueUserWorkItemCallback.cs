using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004F6 RID: 1270
	internal sealed class QueueUserWorkItemCallback : IThreadPoolWorkItem
	{
		// Token: 0x06003CC7 RID: 15559 RVA: 0x000E2AE7 File Offset: 0x000E0CE7
		[SecurityCritical]
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, bool compressStack, ref StackCrawlMark stackMark)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			if (compressStack && !ExecutionContext.IsFlowSuppressed())
			{
				this.context = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x000E2B15 File Offset: 0x000E0D15
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, ExecutionContext ec)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			this.context = ec;
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x000E2B34 File Offset: 0x000E0D34
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			if (this.context == null)
			{
				WaitCallback waitCallback = this.callback;
				this.callback = null;
				waitCallback(this.state);
				return;
			}
			ExecutionContext.Run(this.context, QueueUserWorkItemCallback.ccb, this, true);
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x000E2B76 File Offset: 0x000E0D76
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x000E2B78 File Offset: 0x000E0D78
		[SecurityCritical]
		private static void WaitCallback_Context(object state)
		{
			QueueUserWorkItemCallback queueUserWorkItemCallback = (QueueUserWorkItemCallback)state;
			WaitCallback waitCallback = queueUserWorkItemCallback.callback;
			waitCallback(queueUserWorkItemCallback.state);
		}

		// Token: 0x04001959 RID: 6489
		private WaitCallback callback;

		// Token: 0x0400195A RID: 6490
		private ExecutionContext context;

		// Token: 0x0400195B RID: 6491
		private object state;

		// Token: 0x0400195C RID: 6492
		[SecurityCritical]
		internal static ContextCallback ccb = new ContextCallback(QueueUserWorkItemCallback.WaitCallback_Context);
	}
}
