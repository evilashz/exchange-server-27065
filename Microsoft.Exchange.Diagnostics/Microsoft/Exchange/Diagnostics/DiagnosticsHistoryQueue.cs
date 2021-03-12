using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000019 RID: 25
	internal class DiagnosticsHistoryQueue<T>
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002E0C File Offset: 0x0000100C
		public DiagnosticsHistoryQueue(int limit)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("limit", limit);
			this.limit = limit;
			this.lastQueuedElement = default(T);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E40 File Offset: 0x00001040
		public void Enqueue(T obj)
		{
			ArgumentValidator.ThrowIfNull("obj", obj);
			lock (this.queue)
			{
				this.lastQueuedElement = obj;
				this.queue.Enqueue(obj);
				while (this.queue.Count > this.limit)
				{
					this.queue.Dequeue();
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EC0 File Offset: 0x000010C0
		public T[] ToArray()
		{
			T[] result;
			lock (this.queue)
			{
				result = this.queue.ToArray();
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F08 File Offset: 0x00001108
		public T GetLastQueuedElement()
		{
			T result;
			lock (this.queue)
			{
				result = this.lastQueuedElement;
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F4C File Offset: 0x0000114C
		public void Clear()
		{
			lock (this.queue)
			{
				this.queue.Clear();
				this.lastQueuedElement = default(T);
			}
		}

		// Token: 0x04000075 RID: 117
		private readonly Queue<T> queue = new Queue<T>();

		// Token: 0x04000076 RID: 118
		private readonly int limit;

		// Token: 0x04000077 RID: 119
		private T lastQueuedElement;
	}
}
