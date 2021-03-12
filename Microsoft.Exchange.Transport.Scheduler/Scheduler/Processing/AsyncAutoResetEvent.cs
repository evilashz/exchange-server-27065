using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000005 RID: 5
	internal class AsyncAutoResetEvent
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002590 File Offset: 0x00000790
		public Task<bool> WaitAsync()
		{
			Task<bool> result;
			lock (this.waits)
			{
				if (this.signaled)
				{
					this.signaled = false;
					result = AsyncAutoResetEvent.Completed;
				}
				else
				{
					TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
					this.waits.Enqueue(taskCompletionSource);
					result = taskCompletionSource.Task;
				}
			}
			return result;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000025FC File Offset: 0x000007FC
		public void Set()
		{
			TaskCompletionSource<bool> taskCompletionSource = null;
			lock (this.waits)
			{
				if (this.waits.Count > 0)
				{
					taskCompletionSource = this.waits.Dequeue();
				}
				else if (!this.signaled)
				{
					this.signaled = true;
				}
			}
			if (taskCompletionSource != null)
			{
				taskCompletionSource.SetResult(true);
			}
		}

		// Token: 0x0400000F RID: 15
		private static readonly Task<bool> Completed = Task.FromResult<bool>(true);

		// Token: 0x04000010 RID: 16
		private readonly Queue<TaskCompletionSource<bool>> waits = new Queue<TaskCompletionSource<bool>>();

		// Token: 0x04000011 RID: 17
		private bool signaled;
	}
}
