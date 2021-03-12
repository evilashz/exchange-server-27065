using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200003B RID: 59
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class TaskEngine
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00005D5C File Offset: 0x00003F5C
		public static IAsyncResult BeginExecute(ITask task, AsyncCallback asyncCallback, object asyncState)
		{
			IAsyncResult result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				ExecuteAsyncResult executeAsyncResult = new ExecuteAsyncResult((ExecuteAsyncResult asyncResult) => new ExecuteContext(task, asyncResult), asyncCallback, asyncState);
				disposeGuard.Add<ExecuteAsyncResult>(executeAsyncResult);
				executeAsyncResult.ExecuteContext.Begin();
				disposeGuard.Success();
				result = executeAsyncResult;
			}
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005DDC File Offset: 0x00003FDC
		public static TaskResult EndExecute(IAsyncResult asyncResult)
		{
			ExecuteAsyncResult executeAsyncResult = (ExecuteAsyncResult)asyncResult;
			TaskResult result;
			try
			{
				result = executeAsyncResult.ExecuteContext.End();
			}
			finally
			{
				executeAsyncResult.Dispose();
			}
			return result;
		}
	}
}
