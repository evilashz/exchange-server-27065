using System;
using System.Collections.Generic;

namespace System.Collections.Concurrent
{
	// Token: 0x0200047E RID: 1150
	[__DynamicallyInvokable]
	public interface IProducerConsumerCollection<T> : IEnumerable<!0>, IEnumerable, ICollection
	{
		// Token: 0x06003803 RID: 14339
		[__DynamicallyInvokable]
		void CopyTo(T[] array, int index);

		// Token: 0x06003804 RID: 14340
		[__DynamicallyInvokable]
		bool TryAdd(T item);

		// Token: 0x06003805 RID: 14341
		[__DynamicallyInvokable]
		bool TryTake(out T item);

		// Token: 0x06003806 RID: 14342
		[__DynamicallyInvokable]
		T[] ToArray();
	}
}
