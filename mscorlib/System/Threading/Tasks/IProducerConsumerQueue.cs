using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
	// Token: 0x02000555 RID: 1365
	internal interface IProducerConsumerQueue<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06004142 RID: 16706
		void Enqueue(T item);

		// Token: 0x06004143 RID: 16707
		bool TryDequeue(out T result);

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06004144 RID: 16708
		bool IsEmpty { get; }

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06004145 RID: 16709
		int Count { get; }

		// Token: 0x06004146 RID: 16710
		int GetCountSafe(object syncObj);
	}
}
