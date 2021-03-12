using System;
using System.Threading.Tasks;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001C RID: 28
	internal static class TaskExtensions
	{
		// Token: 0x0600011C RID: 284 RVA: 0x0000762C File Offset: 0x0000582C
		public static ICancelableAsyncResult AsApm<T>(this Task<T> task, CancelableAsyncCallback callback, object state)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			TaskCompletionSource<T> tcs = new TaskCompletionSource<T>(state);
			ICancelableAsyncResult icar = new CancelableAsyncResultWrapper<T>(tcs.Task);
			task.ContinueWith(delegate(Task<T> t)
			{
				if (t.IsFaulted)
				{
					tcs.TrySetException(t.Exception.InnerExceptions);
				}
				else if (t.IsCanceled)
				{
					tcs.TrySetCanceled();
				}
				else
				{
					tcs.TrySetResult(t.Result);
				}
				if (callback != null)
				{
					callback(icar);
				}
			}, TaskScheduler.Default);
			return icar;
		}
	}
}
