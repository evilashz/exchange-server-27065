using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x0200004F RID: 79
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SyncQueue<T>
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003C0 RID: 960
		public abstract int Count { get; }

		// Token: 0x060003C1 RID: 961
		public abstract void Clear();

		// Token: 0x060003C2 RID: 962
		public abstract void Enqueue(T item, WorkType workType, ExDateTime nextPollingTime);

		// Token: 0x060003C3 RID: 963
		public abstract void EnqueueAtFront(T item, WorkType workType);

		// Token: 0x060003C4 RID: 964
		public abstract T Dequeue(WorkType workType);

		// Token: 0x060003C5 RID: 965
		public abstract T Peek(ExDateTime currentTime);

		// Token: 0x060003C6 RID: 966
		public abstract bool IsEmpty();

		// Token: 0x060003C7 RID: 967
		public abstract ExDateTime NextPollingTime(ExDateTime currentTime);

		// Token: 0x060003C8 RID: 968 RVA: 0x00018027 File Offset: 0x00016227
		public ExDateTime NextPollingTime()
		{
			return this.NextPollingTime(ExDateTime.UtcNow);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00018034 File Offset: 0x00016234
		public T Peek()
		{
			return this.Peek(ExDateTime.UtcNow);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00018041 File Offset: 0x00016241
		public bool IsWorkDue(ExDateTime currentTime)
		{
			return !this.IsEmpty() && this.NextPollingTime(currentTime) <= currentTime;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001805A File Offset: 0x0001625A
		protected void ThrowIfQueueEmpty()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException("Sync Queue is empty.");
			}
		}
	}
}
