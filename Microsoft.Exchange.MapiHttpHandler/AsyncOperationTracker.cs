using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000039 RID: 57
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AsyncOperationTracker
	{
		// Token: 0x0600020E RID: 526 RVA: 0x0000C344 File Offset: 0x0000A544
		public void Register(AsyncOperationInfo asyncOperationInfo)
		{
			lock (this.asyncOperationsLock)
			{
				this.activeAsyncOperations.Add(asyncOperationInfo);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000C38C File Offset: 0x0000A58C
		public void Complete(AsyncOperationInfo asyncOperationInfo)
		{
			lock (this.asyncOperationsLock)
			{
				this.activeAsyncOperations.Remove(asyncOperationInfo);
				if (this.completedAsyncOperations.Count >= 16)
				{
					this.completedAsyncOperations.Dequeue();
				}
				this.completedAsyncOperations.Enqueue(asyncOperationInfo);
				if (asyncOperationInfo.FailureException != null)
				{
					if (this.failedAsyncOperations.Count >= 16)
					{
						this.failedAsyncOperations.Dequeue();
					}
					this.failedAsyncOperations.Enqueue(asyncOperationInfo);
				}
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000C42C File Offset: 0x0000A62C
		public void GetAsyncOperationInfo(out AsyncOperationInfo[] activeOperations, out AsyncOperationInfo[] completedOperations, out AsyncOperationInfo[] failedOperations)
		{
			lock (this.asyncOperationsLock)
			{
				activeOperations = this.activeAsyncOperations.ToArray();
				completedOperations = this.completedAsyncOperations.ToArray();
				failedOperations = this.failedAsyncOperations.ToArray();
			}
		}

		// Token: 0x040000EA RID: 234
		private readonly List<AsyncOperationInfo> activeAsyncOperations = new List<AsyncOperationInfo>();

		// Token: 0x040000EB RID: 235
		private readonly Queue<AsyncOperationInfo> completedAsyncOperations = new Queue<AsyncOperationInfo>(16);

		// Token: 0x040000EC RID: 236
		private readonly Queue<AsyncOperationInfo> failedAsyncOperations = new Queue<AsyncOperationInfo>(16);

		// Token: 0x040000ED RID: 237
		private readonly object asyncOperationsLock = new object();
	}
}
