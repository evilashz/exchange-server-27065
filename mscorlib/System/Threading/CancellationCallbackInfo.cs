using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200051A RID: 1306
	internal class CancellationCallbackInfo
	{
		// Token: 0x06003E4A RID: 15946 RVA: 0x000E76B4 File Offset: 0x000E58B4
		internal CancellationCallbackInfo(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource)
		{
			this.Callback = callback;
			this.StateForCallback = stateForCallback;
			this.TargetSyncContext = targetSyncContext;
			this.TargetExecutionContext = targetExecutionContext;
			this.CancellationTokenSource = cancellationTokenSource;
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x000E76E4 File Offset: 0x000E58E4
		[SecuritySafeCritical]
		internal void ExecuteCallback()
		{
			if (this.TargetExecutionContext != null)
			{
				ContextCallback contextCallback = CancellationCallbackInfo.s_executionContextCallback;
				if (contextCallback == null)
				{
					contextCallback = (CancellationCallbackInfo.s_executionContextCallback = new ContextCallback(CancellationCallbackInfo.ExecutionContextCallback));
				}
				ExecutionContext.Run(this.TargetExecutionContext, contextCallback, this);
				return;
			}
			CancellationCallbackInfo.ExecutionContextCallback(this);
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x000E772C File Offset: 0x000E592C
		[SecurityCritical]
		private static void ExecutionContextCallback(object obj)
		{
			CancellationCallbackInfo cancellationCallbackInfo = obj as CancellationCallbackInfo;
			cancellationCallbackInfo.Callback(cancellationCallbackInfo.StateForCallback);
		}

		// Token: 0x040019F3 RID: 6643
		internal readonly Action<object> Callback;

		// Token: 0x040019F4 RID: 6644
		internal readonly object StateForCallback;

		// Token: 0x040019F5 RID: 6645
		internal readonly SynchronizationContext TargetSyncContext;

		// Token: 0x040019F6 RID: 6646
		internal readonly ExecutionContext TargetExecutionContext;

		// Token: 0x040019F7 RID: 6647
		internal readonly CancellationTokenSource CancellationTokenSource;

		// Token: 0x040019F8 RID: 6648
		[SecurityCritical]
		private static ContextCallback s_executionContextCallback;
	}
}
