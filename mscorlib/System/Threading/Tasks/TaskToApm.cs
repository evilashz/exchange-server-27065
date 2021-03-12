using System;
using System.IO;

namespace System.Threading.Tasks
{
	// Token: 0x0200055C RID: 1372
	internal static class TaskToApm
	{
		// Token: 0x06004183 RID: 16771 RVA: 0x000F3AB4 File Offset: 0x000F1CB4
		public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			if (task.IsCompleted)
			{
				asyncResult = new TaskToApm.TaskWrapperAsyncResult(task, state, true);
				if (callback != null)
				{
					callback(asyncResult);
				}
			}
			else
			{
				IAsyncResult asyncResult3;
				if (task.AsyncState != state)
				{
					IAsyncResult asyncResult2 = new TaskToApm.TaskWrapperAsyncResult(task, state, false);
					asyncResult3 = asyncResult2;
				}
				else
				{
					asyncResult3 = task;
				}
				asyncResult = asyncResult3;
				if (callback != null)
				{
					TaskToApm.InvokeCallbackWhenTaskCompletes(task, callback, asyncResult);
				}
			}
			return asyncResult;
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x000F3B04 File Offset: 0x000F1D04
		public static void End(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task;
			}
			else
			{
				task = (asyncResult as Task);
			}
			if (task == null)
			{
				__Error.WrongAsyncResult();
			}
			task.GetAwaiter().GetResult();
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x000F3B44 File Offset: 0x000F1D44
		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task<TResult> task;
			if (taskWrapperAsyncResult != null)
			{
				task = (taskWrapperAsyncResult.Task as Task<TResult>);
			}
			else
			{
				task = (asyncResult as Task<TResult>);
			}
			if (task == null)
			{
				__Error.WrongAsyncResult();
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x000F3B88 File Offset: 0x000F1D88
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x02000BFF RID: 3071
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x06006F08 RID: 28424 RVA: 0x0017E08A File Offset: 0x0017C28A
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this.m_state = state;
				this.m_completedSynchronously = completedSynchronously;
			}

			// Token: 0x1700131F RID: 4895
			// (get) Token: 0x06006F09 RID: 28425 RVA: 0x0017E0A7 File Offset: 0x0017C2A7
			object IAsyncResult.AsyncState
			{
				get
				{
					return this.m_state;
				}
			}

			// Token: 0x17001320 RID: 4896
			// (get) Token: 0x06006F0A RID: 28426 RVA: 0x0017E0AF File Offset: 0x0017C2AF
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this.m_completedSynchronously;
				}
			}

			// Token: 0x17001321 RID: 4897
			// (get) Token: 0x06006F0B RID: 28427 RVA: 0x0017E0B7 File Offset: 0x0017C2B7
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x17001322 RID: 4898
			// (get) Token: 0x06006F0C RID: 28428 RVA: 0x0017E0C4 File Offset: 0x0017C2C4
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x0400363B RID: 13883
			internal readonly Task Task;

			// Token: 0x0400363C RID: 13884
			private readonly object m_state;

			// Token: 0x0400363D RID: 13885
			private readonly bool m_completedSynchronously;
		}
	}
}
